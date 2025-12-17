using BE;
using BL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Reflection;

namespace Proyecto_IS_Sistema_De_Tickets
{
    public partial class FormPrueba : Form, IIdiomaObserver
    {
        private readonly IdiomaService _idiomaSrv = new IdiomaService();
        private readonly AuditoriaService _auditoriaSrv = AuditoriaService.Instancia;
        private readonly ControlCambiosService _controlCambiosSrv = ControlCambiosService.Instancia;
        private readonly DashboardService _dashboardService = new DashboardService();

        private bool _registroVisible = false;   // estado del bloque de registro
        private bool _regRolesCargados = false;  // ya lo tenés: lo dejamos
        private bool _actualizandoEstadoIdioma = false;
        private bool _verificandoIntegridad = false;
        private Timer _integridadTimer;

        private TabPage _tabRegistrar;
        private TabPage _tabBitacora;
        private TabPage _tabCambios;
        private TabPage _tabPermisos;
        private TabPage _tabIdiomas;
        private BindingList<PublicActivityItem> _actividadPublica = new BindingList<PublicActivityItem>();
        public FormPrueba()
        {
            InitializeComponent();
            IdiomaManager.Instancia.Suscribir(this);
        }

        private void FormPrueba_Load(object sender, EventArgs e)
        {
            ConfigurarDgvCambios();
            ConfigurarTabIdiomas();
            IdiomaManager.Instancia.Suscribir(this);
            if (SessionManager.Instancia.UsuarioActual == null)
            {
                MessageBox.Show("La sesión no está activa. Volviendo al login.");
                Application.Restart();
                return;
            }

            var usuario = SessionManager.Instancia.UsuarioActual;
            bool puedeAltaUsuarios = SessionManager.Instancia.TienePermiso("Usuario.Alta");
            bool puedeBajaUsuarios = SessionManager.Instancia.TienePermiso("Usuario.Baja");
            bool puedeModificarUsuarios = SessionManager.Instancia.TienePermiso("Usuario.Modificar");
            bool puedeVerUsuarios = puedeAltaUsuarios || puedeBajaUsuarios || puedeModificarUsuarios;
            bool puedeVerBitacora = SessionManager.Instancia.TienePermiso("Bitacora.Ver");
            bool puedeVerCambios = SessionManager.Instancia.TienePermiso("ControlCambios.Ver");
            bool puedeCrearTicket = usuario.TienePermiso("Ticket.Crear");
            bool puedeGestionarPermisos = SessionManager.Instancia.TienePermiso("Permiso.Gestionar");
            bool puedeGestionarIdiomas = SessionManager.Instancia.TienePermiso("Idioma.Gestionar");


            _tabIdiomas = tabGeneral.TabPages.Count > 1 ? tabGeneral.TabPages[4] : null;

            if (!puedeGestionarIdiomas)
            {
                tabGeneral.TabPages.Remove(_tabIdiomas);
            }

            // guardo referencia al tab 
            _tabPermisos = tabGeneral.TabPages.Cast<TabPage>().FirstOrDefault(t => t.Name == "tabPermisos")
                ?? (tabGeneral.TabPages.Count > 4 ? tabGeneral.TabPages[4] : null);

            // si no puede gestionarlos, lo quitamos
            if (!puedeGestionarPermisos && _tabPermisos != null)
            {
                tabGeneral.TabPages.Remove(_tabPermisos);
            }

            if (puedeGestionarPermisos)
            {
                CargarUsuariosConPermisos();
                CargarRolesYPermisosDisponibles();
            }


            this.Text = $"FormPrueba - {usuario.Email} (Ticket.Crear={(puedeCrearTicket ? "Sí" : "No")})";

            // guardo tabs
            _tabRegistrar = tabGeneral.TabPages.Count > 1 ? tabGeneral.TabPages[1] : null;
            _tabBitacora = tabGeneral.TabPages.Count > 2 ? tabGeneral.TabPages[2] : null;
            _tabCambios = tabGeneral.TabPages.Count > 3 ? tabGeneral.TabPages[3] : null;

            if (puedeVerUsuarios)
            {
                SetRegistrarVisible(true);
                CargarGrillaGestionUsuarios();
                ActualizarBotonesGestionUsuarios(puedeAltaUsuarios, puedeModificarUsuarios, puedeBajaUsuarios);

                if (puedeVerBitacora)
                {
                    CargarEventosBitacoraHardcoded();
                    CargarBitacoraInicial();
                }

                if (puedeVerCambios)
                {
                    CargarCambiosInicial();
                }
            }
            else
            {
                if (_tabRegistrar != null) tabGeneral.TabPages.Remove(_tabRegistrar);
                if (!puedeVerBitacora && _tabBitacora != null) tabGeneral.TabPages.Remove(_tabBitacora);
                if (!puedeVerCambios && _tabCambios != null) tabGeneral.TabPages.Remove(_tabCambios);
                SetRegistrarVisible(false);
            }

            CargarIdiomasTab();
            RecargarComboIdiomas();

            treeUsuarios.AfterSelect += treeUsuarios_AfterSelect_1;
            dgvCambios.CellDoubleClick += dgvCambios_CellDoubleClick;

            dgvGestionUsuario.AllowUserToAddRows = false;
            dgvGestionUsuario.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvGestionUsuario.MultiSelect = false;
            dgvGestionUsuario.ReadOnly = true;  // si no editás inline

            // Si el usuario ya había seleccionado un idioma en el login,
            // cuando este formulario se suscribe aún no están seteadas las
            // referencias a las pestañas, por lo que los textos no se
            // actualizan. Reaplicamos la última traducción ahora que ya está
            // inicializado todo.
            var ultimaTraduccion = IdiomaManager.Instancia.ObtenerUltimaTraduccion();
            if (ultimaTraduccion != null)
            {
                ActualizarIdioma(ultimaTraduccion);
            }
            bool esAdmin = SessionManager.Instancia.TieneRol("Administrador");
            if (esAdmin)
            {
                btnCrearBackup.Visible = esAdmin;
            }
            else
            {
                btnCrearBackup.Visible = false;
            }

            InicializarDashboardPublico();
            InicializarMonitorIntegridad();

        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            IdiomaManager.Instancia.Desuscribir(this);
            if (_integridadTimer != null)
            {
                _integridadTimer.Stop();
                _integridadTimer.Tick -= IntegridadTimer_Tick;
                _integridadTimer.Dispose();
            }
            base.OnFormClosed(e);
        }

        // este es el MÉTODO que llama el observer
        public void ActualizarIdioma(Dictionary<string, string> t)
        {
            // pestaña de menú (siempre debería existir)
            if (tabGeneral.TabPages.Count > 0)
                tabGeneral.TabPages[0].Text = t["TAB_MENU"];

            // pestaña de usuarios (solo si existe y no fue removida por permisos)
            if (_tabRegistrar != null && tabGeneral.TabPages.Contains(_tabRegistrar))
                _tabRegistrar.Text = t["TAB_USUARIOS"];

            // pestaña de bitácora
            if (_tabBitacora != null && tabGeneral.TabPages.Contains(_tabBitacora))
                _tabBitacora.Text = t["TAB_BITACORA"];

            // pestaña de control de cambios
            if (_tabCambios != null && tabGeneral.TabPages.Contains(_tabCambios))
                _tabCambios.Text = t["TAB_CONTROL_CAMBIOS"];

            // botones
            btnCerrarSesion.Text = t["BTN_CERRAR_SESION"];
            btnActualizar.Text = t["BTN_ACTUALIZAR"];
            btnNuevoRegistro.Text = t["BTN_NUEVO_REGISTRO"];

            // bitácora
            lblUsuarioId.Text = t["LBL_USUARIO_ID"];
            lblAuditoriaId.Text = t["LBL_AUDITORIA_ID"];
            lblEvento.Text = t["LBL_EVENTO"];
            lblDetalle.Text = t["LBL_DETALLE"];
            lblFecha.Text = t["LBL_FECHA"];
            btnFiltrarBitacora.Text = t["BTN_FILTRAR"];
            btnLimpiar.Text = t["BTN_LIMPIAR"];

            // control cambios
            lblCambioId.Text = t["LBL_CAMBIO_ID"];
            lblCambioUsuarioId.Text = t["LBL_CAMBIO_USUARIO_ID"];
            lblCambioEntidad.Text = t["LBL_CAMBIO_ENTIDAD"];
            lblCambioEntidadId.Text = t["LBL_CAMBIO_ENTIDAD_ID"];
            lblCambioCampo.Text = t["LBL_CAMBIO_CAMPO"];
            //lblCambioFecha.Text = t["LBL_CAMBIO_FECHA"];
            btnFiltrarCambios.Text = t["BTN_FILTRAR"];
            btnLimpiarCambios.Text = t["BTN_LIMPIAR"];
        }

        private void InicializarDashboardPublico()
        {
            dgvActividadPublica.AutoGenerateColumns = false;
            if (cmbFiltroActividad.SelectedIndex < 0 && cmbFiltroActividad.Items.Count > 0)
                cmbFiltroActividad.SelectedIndex = 0;

            RefrescarEstadoPublico();
        }

        private void RefrescarEstadoPublico()
        {
            string idiomaActual = cmbIdiomas.SelectedItem is Idioma idiomaSel ? idiomaSel.Nombre : cmbIdiomas.Text;
            string version = Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "-";
            string filtro = cmbFiltroActividad.SelectedItem as string ?? "Todos";

            var estado = _dashboardService.ConstruirEstadoPublico(idiomaActual, version, filtro);

            lblEstadoDb.Text = estado.BaseDatosConectada ? "Base de datos: Conectada" : "Base de datos: Sin conexión";
            lblEstadoDb.ForeColor = estado.BaseDatosConectada ? Color.ForestGreen : Color.Firebrick;
            lblHoraServidor.Text = estado.HoraServidorUtc.HasValue
                ? $"Hora servidor (UTC): {estado.HoraServidorUtc:HH:mm:ss}"
                : "Hora servidor (UTC): Sin conexión";
            lblHoraLocal.Text = $"Hora local: {estado.HoraLocal:HH:mm:ss}";
            lblIdiomaActual.Text = $"Idioma actual: {estado.IdiomaActual}";
            lblVersionApp.Text = $"Versión: {estado.VersionAplicacion}";

            _actividadPublica = new BindingList<PublicActivityItem>(estado.ActividadPublica ?? new List<PublicActivityItem>());
            dgvActividadPublica.DataSource = _actividadPublica;

            lblActividadVacia.Text = estado.BaseDatosConectada
                ? "Aún no hay eventos para mostrar."
                : "Sin conexión. Los eventos no están disponibles.";
            lblActividadVacia.Visible = _actividadPublica.Count == 0;
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            // Cierra la sesión en DB (marca FinUtc y escribe auditoría)
            AuthService.Instancia.Logout();

            // Volvemos limpio al login (lo más simple para WinForms)
            Application.Restart();
        }

        private void btnActualizarEstado_Click(object sender, EventArgs e)
        {
            RefrescarEstadoPublico();
        }

        private void cmbFiltroActividad_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefrescarEstadoPublico();
        }

        private void btnSobreProyecto_Click(object sender, EventArgs e)
        {
            using (var frm = new FormSobreProyecto())
            {
                frm.ShowDialog(this);
            }
        }

        private void TabGeneral_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool puedeAltaUsuarios = SessionManager.Instancia.TienePermiso("Usuario.Alta");
            bool puedeBajaUsuarios = SessionManager.Instancia.TienePermiso("Usuario.Baja");
            bool puedeModificarUsuarios = SessionManager.Instancia.TienePermiso("Usuario.Modificar");
            bool puedeVerUsuarios = puedeAltaUsuarios || puedeBajaUsuarios || puedeModificarUsuarios;
            bool puedeVerBitacora = SessionManager.Instancia.TienePermiso("Bitacora.Ver");
            bool puedeVerCambios = SessionManager.Instancia.TienePermiso("ControlCambios.Ver");

            var selectedTab = tabGeneral.SelectedTab;

            if (selectedTab == _tabRegistrar)
            {
                if (!puedeVerUsuarios)
                {
                    MessageBox.Show("No contás con permiso para gestionar usuarios.");
                    tabGeneral.SelectedIndex = 0; // vuelve al menú
                    return;
                }

                // Si es admin, mostramos y refrescamos la grilla
                SetRegistrarVisible(true);
                ActualizarBotonesGestionUsuarios(puedeAltaUsuarios, puedeModificarUsuarios, puedeBajaUsuarios);
                CargarGrillaGestionUsuarios();
            }
            else if (selectedTab == _tabBitacora)
            {
                if (!puedeVerBitacora)
                {
                    MessageBox.Show("No contás con permiso para ver la Bitácora.");
                    tabGeneral.SelectedIndex = 0;
                    return;
                }

                // Carga inicial (si no se cargó antes)
                CargarEventosBitacoraHardcoded();
                CargarBitacoraInicial();
            }
            else if (selectedTab == _tabCambios)
            {
                if (!puedeVerCambios)
                {
                    MessageBox.Show("No contás con permiso para ver el Control de Cambios.");
                    tabGeneral.SelectedIndex = 0;
                    return;
                }
                CargarCambiosInicial();
            }
            else if (selectedTab == _tabPermisos)
            {
                bool puedeGestionarPermisos = SessionManager.Instancia.TienePermiso("Permiso.Gestionar");
                if (!puedeGestionarPermisos)
                {
                    MessageBox.Show("No contás con permiso para gestionar permisos.");
                    tabGeneral.SelectedIndex = 0;
                    return;
                }

                // si tiene permiso, cargamos el contenido
                CargarUsuariosConPermisos();
                CargarRolesYPermisosDisponibles();
            }
        }
        private void CargarCambiosInicial()
        {
            dtpCambiosDesde.Value = new DateTime(2000, 1, 1);
            dtpCambiosHasta.Value = DateTime.Today.AddDays(1);

            var datos = _controlCambiosSrv.FiltrarCambios(
                id: null,
                usuarioId: null,
                entidad: null,
                entidadId: null,
                campo: null,
                desdeUtc: DateTime.SpecifyKind(dtpCambiosDesde.Value.Date, DateTimeKind.Local).ToUniversalTime(),
                hastaUtcExcl: DateTime.SpecifyKind(dtpCambiosHasta.Value.Date.AddDays(1), DateTimeKind.Local).ToUniversalTime()
            );

            // 👇 esto es clave: ya definimos las columnas nosotros
            dgvCambios.AutoGenerateColumns = false;
            dgvCambios.DataSource = datos;
        }

        /// <summary>
        /// Ejemplo de cómo poblar un TreeView con el árbol completo de permisos.
        /// El Tag de cada nodo se mapea al Permiso_Id correspondiente.
        /// </summary>
        private void CargarPermisosEnTreeView(TreeView tree)
        {
            if (tree == null) return;
            var servicio = PermisoService.Instancia;
            var arbol = servicio.ObtenerArbolPermisos();
            PermisoTreeHelper.PoblarTree(tree, arbol, id => id);
        }

        private void CargarBitacoraInicial()
        {
            // por defecto: desde 2000 hasta mañana
            dtpDesde.Value = new DateTime(2000, 1, 1);
            dtpHasta.Value = DateTime.Today.AddDays(1);

            dgvBitacora.AutoGenerateColumns = true;
            dgvBitacora.DataSource = _auditoriaSrv.FiltrarAuditoria(
                id: null,
                usuarioId: null,
                evento: null,
                texto: null,
                desdeUtc: DateTime.SpecifyKind(dtpDesde.Value.Date, DateTimeKind.Local).ToUniversalTime(),
                hastaUtcExcl: DateTime.SpecifyKind(dtpHasta.Value.Date.AddDays(1), DateTimeKind.Local).ToUniversalTime()
            );

            // formateo de columnas
            if (dgvBitacora.Columns["FechaUtc"] != null)
                dgvBitacora.Columns["FechaUtc"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";
            if (dgvBitacora.Columns["Detalle"] != null)
                dgvBitacora.Columns["Detalle"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void SetRegistrarVisible(bool v)
        {
            dgvGestionUsuario.Visible = v;
            btnActualizar.Visible = v;
            btnNuevoRegistro.Visible = v;
            btnEliminar.Visible = v;
        }

        private void ActualizarBotonesGestionUsuarios(bool puedeAlta, bool puedeModificar, bool puedeBaja)
        {
            btnNuevoRegistro.Enabled = puedeAlta;
            btnActualizar.Enabled = puedeModificar;
            btnEliminar.Enabled = puedeBaja;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnFiltrarBitacora_Click(object sender, EventArgs e)
        {
            int? id = int.TryParse(txtAuditoriaId.Text, out var vId) ? vId : (int?)null;
            int? usuarioId = int.TryParse(txtId.Text, out var vUid) ? vUid : (int?)null;

            string evento = string.IsNullOrWhiteSpace(cmbEvento.Text)
                ? null
                : cmbEvento.Text.Trim().ToUpperInvariant();

            string texto = string.IsNullOrWhiteSpace(txtTexto.Text)
                ? null
                : txtTexto.Text.Trim();

            var desdeUtc = DateTime.SpecifyKind(dtpDesde.Value.Date, DateTimeKind.Local).ToUniversalTime();
            var hastaUtcExcl = DateTime.SpecifyKind(dtpHasta.Value.Date.AddDays(1), DateTimeKind.Local).ToUniversalTime();

            var datos = _auditoriaSrv.FiltrarAuditoria(id, usuarioId, evento, texto, desdeUtc, hastaUtcExcl);

            dgvBitacora.AutoGenerateColumns = true;
            dgvBitacora.DataSource = datos;

            if (dgvBitacora.Columns["FechaUtc"] != null)
                dgvBitacora.Columns["FechaUtc"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";
            if (dgvBitacora.Columns["Detalle"] != null)
                dgvBitacora.Columns["Detalle"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtAuditoriaId.Clear();
            txtId.Clear();
            cmbEvento.SelectedIndex = -1; // vacío
            txtTexto.Clear();
            dtpDesde.Value = new DateTime(2000, 1, 1);
            dtpHasta.Value = DateTime.Today.AddDays(1);

            // defaults de fechas (sin “limitar”)
            dtpDesde.Value = new DateTime(2000, 1, 1);
            dtpHasta.Value = DateTime.Today.AddDays(1);

            // carga inicial (sin filtros de texto)
            dgvBitacora.AutoGenerateColumns = true;
            dgvBitacora.DataSource = _auditoriaSrv.FiltrarAuditoria(
                id: null,
                usuarioId: null,
                evento: null,
                texto: null,
                desdeUtc: DateTime.SpecifyKind(dtpDesde.Value.Date, DateTimeKind.Local).ToUniversalTime(),
                hastaUtcExcl: DateTime.SpecifyKind(dtpHasta.Value.Date.AddDays(1), DateTimeKind.Local).ToUniversalTime()
            );

        }
        private bool _eventosCargados = false;

        private void CargarEventosBitacoraHardcoded()
        {
            if (_eventosCargados) return;

            cmbEvento.Items.Clear();
            cmbEvento.Items.Add("");
            cmbEvento.Items.AddRange(new object[] {
        "APP_START","APP_EXIT","LOGIN_OK","LOGIN_FAIL","LOGIN_BLOQUEADO",
        "LOGOUT","PERMISO_DENEGADO","CAMBIO_PASSWORD",
        "ALTA_USUARIO","BAJA_USUARIO","MODIFICACION_USUARIO"
    });
            cmbEvento.SelectedIndex = 0;
            _eventosCargados = true;
        }

        private void tabRegistrar_Click(object sender, EventArgs e)
        {

        }

        public void CargarGrillaGestionUsuarios()
        {
            var usuarios = BL.UserAdminService.Instancia.ListarUsuarios();

            // agrego una columna calculada "Rol"
            var dt = new DataTable();
            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("Email", typeof(string));
            dt.Columns.Add("Nombre", typeof(string));
            dt.Columns.Add("Rol", typeof(string));
            dt.Columns.Add("Activo", typeof(bool));
            dt.Columns.Add("IntentosFallidos", typeof(int));

            foreach (var u in usuarios)
            {
                string rolNombre = u.Roles != null && u.Roles.Count > 0
                    ? string.Join(", ", u.Roles.Select(r => r.Nombre))
                    : "(sin rol)";

                dt.Rows.Add(u.Id, u.Email, u.Nombre, rolNombre, u.Activo, u.IntentosFallidos);
            }

            dgvGestionUsuario.AutoGenerateColumns = true;
            dgvGestionUsuario.DataSource = dt;

            // renombro encabezados
            dgvGestionUsuario.Columns["Id"].HeaderText = "Id";
            dgvGestionUsuario.Columns["Email"].HeaderText = "Email";
            dgvGestionUsuario.Columns["Nombre"].HeaderText = "Nombre";
            dgvGestionUsuario.Columns["Rol"].HeaderText = "Rol";
            dgvGestionUsuario.Columns["Activo"].HeaderText = "Activo";
            dgvGestionUsuario.Columns["IntentosFallidos"].HeaderText = "Intentos Fallidos";

            // ajuste visual
            dgvGestionUsuario.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (!SessionManager.Instancia.TienePermiso("Usuario.Modificar"))
            {
                MessageBox.Show("No contás con permiso para modificar usuarios.");
                return;
            }

            if (dgvGestionUsuario.CurrentRow == null || dgvGestionUsuario.CurrentRow.Index < 0)
            {
                MessageBox.Show("Seleccioná un usuario primero.");
                return;
            }

            // Asegurate de tener una columna "Id" visible u oculta
            var cell = dgvGestionUsuario.CurrentRow.Cells["Id"];
            if (cell == null || cell.Value == null || !int.TryParse(cell.Value.ToString(), out int usuarioId))
            {
                MessageBox.Show("No se pudo obtener el usuario seleccionado.");
                return;
            }

            // Leé el usuario desde la BL (si querés validar existencia)
            var u = BL.UserAdminService.Instancia.ObtenerUsuarioCompleto(usuarioId);
            if (u == null)
            {
                MessageBox.Show("Usuario no encontrado.");
                return;
            }

            var f = new FormGestionDeUsuario(
                FormGestionDeUsuario.ModoFormulario.Edicion,
                usuarioId: usuarioId);

            f.FormClosed += (_, __) => CargarGrillaGestionUsuarios();
            f.ShowDialog(this);
        }

        private void btnNuevoRegistro_Click(object sender, EventArgs e)
        {
            if (!SessionManager.Instancia.TienePermiso("Usuario.Alta"))
            {
                MessageBox.Show("No contás con permiso para crear usuarios.");
                return;
            }

            var f = new FormGestionDeUsuario(
               FormGestionDeUsuario.ModoFormulario.Alta,
               usuarioId: null);

            f.FormClosed += (_, __) => CargarGrillaGestionUsuarios();
            f.ShowDialog(this);
        }

        private void btnFiltrarCambios_Click(object sender, EventArgs e)
        {
            int? id = int.TryParse(txtCambioId.Text, out var vId) ? vId : (int?)null;
            int? usuarioId = int.TryParse(txtCambioUsuarioId.Text, out var vUid) ? vUid : (int?)null;
            string entidad = string.IsNullOrWhiteSpace(txtCambioEntidad.Text) ? null : txtCambioEntidad.Text.Trim();
            int? entidadId = int.TryParse(txtCambioEntidadId.Text, out var vEid) ? vEid : (int?)null;
            string campo = string.IsNullOrWhiteSpace(txtCambioCampo.Text) ? null : txtCambioCampo.Text.Trim();

            var desdeUtc = DateTime.SpecifyKind(dtpCambiosDesde.Value.Date, DateTimeKind.Local).ToUniversalTime();
            var hastaUtc = DateTime.SpecifyKind(dtpCambiosHasta.Value.Date.AddDays(1), DateTimeKind.Local).ToUniversalTime();

            var datos = _controlCambiosSrv.FiltrarCambios(
                id: id,
                usuarioId: usuarioId,
                entidad: entidad,
                entidadId: entidadId,
                campo: campo,
                desdeUtc: desdeUtc,
                hastaUtcExcl: hastaUtc
            );

            dgvCambios.AutoGenerateColumns = true;
            dgvCambios.DataSource = datos;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btnLimpiarCambios_Click(object sender, EventArgs e)
        {
            txtCambioUsuarioId.Clear();
            txtCambioId.Clear();
            txtCambioEntidad.Clear();
            txtCambioEntidadId.Clear();
            txtCambioCampo.Clear();

            dtpCambiosDesde.Value = new DateTime(2000, 1, 1);
            dtpCambiosHasta.Value = DateTime.Today.AddDays(1);

            CargarCambiosInicial();
        }

        private void dgvCambios_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dgvCambios.Rows.Count == 0)
                return;

            var row = dgvCambios.Rows[e.RowIndex];
            if (row?.DataBoundItem is ControlCambioEntry cambio)
            {
                if (!string.Equals(cambio.Entidad, "Usuario", StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("Solo se pueden restaurar cambios de usuarios desde este apartado.");
                    return;
                }

                if (string.IsNullOrEmpty(cambio.Campo))
                {
                    MessageBox.Show("El registro seleccionado no indica un campo para restaurar.");
                    return;
                }

                if (cambio.ValorAnterior == null && cambio.ValorNuevo == null)
                {
                    MessageBox.Show("No hay un valor anterior para restaurar en este registro.");
                    return;
                }

                var confirmar = MessageBox.Show(
                    $"¿Restaurar el campo '{cambio.Campo}' del usuario {cambio.EntidadId} al valor anterior?",
                    "Restaurar cambio",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirmar != DialogResult.Yes)
                    return;

                try
                {
                    BL.UserAdminService.Instancia.RestaurarCambio(cambio);
                    MessageBox.Show("El valor fue restaurado correctamente.");
                    btnFiltrarCambios_Click(btnFiltrarCambios, EventArgs.Empty);

                    if (tabGeneral.TabPages.Contains(tabUsuarios))
                        CargarGrillaGestionUsuarios();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No se pudo restaurar el cambio: " + ex.Message);
                }
            }
        }
        private void ConfigurarDgvCambios()
        {
            // NO dejamos que se autogenere nada
            dgvCambios.AutoGenerateColumns = false;
            dgvCambios.Columns.Clear();

            // queremos scroll horizontal
            dgvCambios.ScrollBars = ScrollBars.Both;
            dgvCambios.RowHeadersVisible = false;
            dgvCambios.AllowUserToAddRows = false;

            // ahora definimos TODAS las columnas que queremos ver

            dgvCambios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                HeaderText = "Id",
                DataPropertyName = "Id",
                Width = 50
            });

            dgvCambios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FechaUtc",
                HeaderText = "Fecha (UTC)",
                DataPropertyName = "FechaUtc",
                Width = 140,
                DefaultCellStyle = { Format = "yyyy-MM-dd HH:mm:ss" }
            });

            dgvCambios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "UsuarioId",
                HeaderText = "Usuario",
                DataPropertyName = "UsuarioId",
                Width = 70
            });

            dgvCambios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Entidad",
                HeaderText = "Entidad",
                DataPropertyName = "Entidad",
                Width = 90
            });

            dgvCambios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "EntidadId",
                HeaderText = "EntidadId",
                DataPropertyName = "EntidadId",
                Width = 80
            });

            dgvCambios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Accion",
                HeaderText = "Acción",
                DataPropertyName = "Accion",
                Width = 100
            });

            dgvCambios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Campo",
                HeaderText = "Campo",
                DataPropertyName = "Campo",
                Width = 110
            });

            dgvCambios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ValorAnterior",
                HeaderText = "Valor anterior",
                DataPropertyName = "ValorAnterior",
                Width = 180,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            });

            // 👇 ESTA es la que te falta ver
            dgvCambios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ValorNuevo",
                HeaderText = "Valor nuevo",
                DataPropertyName = "ValorNuevo",
                Width = 220,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            });

            // por seguridad, que el grid NO vuelva a autoajustar
            dgvCambios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
        }

        private void cmbIdiomas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbIdiomas.SelectedValue is string cod && !string.IsNullOrWhiteSpace(cod))
            {
                _idiomaSrv.SeleccionarIdioma(cod);
            }
        }

        private void FormPrueba_FormClosed(object sender, FormClosedEventArgs e)
        {
            IdiomaManager.Instancia.Desuscribir(this);
        }

        private void CargarUsuariosConPermisos()
        {
            treeUsuarios.Nodes.Clear();
            var usuarios = BL.UserAdminService.Instancia.ListarUsuarios();

            foreach (var u in usuarios)
            {
                var nodoUsuario = new TreeNode($"{u.Nombre} ({u.Email})") { Tag = u.Id };

                // ===== ROLES =====
                var nodoRoles = new TreeNode("Roles");
                var roles = u.Roles;    // los que vienen de UsuarioRepository / UserAdminService

                foreach (var rol in roles)
                {
                    var nodoRol = new TreeNode(rol.Nombre) { Tag = "ROL_" + rol.Id };

                    // 👇 traigo los permisos que tiene ese rol
                    var permisosDelRol = BL.PermisoService.Instancia.ObtenerPermisosDeRol(rol.Id);
                    // permisosDelRol es List<PermisoComponent>

                    foreach (var comp in permisosDelRol)    // puede haber 1 o varios "árboles" raíz
                    {
                        nodoRol.Nodes.Add(PermisoTreeHelper.CrearNodo(comp));
                    }

                    nodoRoles.Nodes.Add(nodoRol);
                }

                nodoUsuario.Nodes.Add(nodoRoles);

                // ===== PERMISOS SUELTOS =====
                var nodoPermisos = new TreeNode("Permisos");

                // esto tiene que estar en PermisoService, no en el form
                var permisosSueltos = BL.PermisoService.Instancia.ObtenerPermisosDirectosDeUsuario(u.Id);

                if (permisosSueltos != null)
                {
                    foreach (var p in permisosSueltos)
                    {
                        nodoPermisos.Nodes.Add(PermisoTreeHelper.CrearNodo(p));
                    }
                }

                nodoUsuario.Nodes.Add(nodoPermisos);

                treeUsuarios.Nodes.Add(nodoUsuario);
            }

            treeUsuarios.ExpandAll();
        }


        private void CargarRolesYPermisosDisponibles()
        {
            treeDisponibles.Nodes.Clear();

            // roles
            var roles = BL.UserAdminService.Instancia.ListarRoles();
            var nodoRoles = new TreeNode("Roles disponibles");
            foreach (var rol in roles)
                nodoRoles.Nodes.Add(new TreeNode(rol.Nombre) { Tag = "ROL_" + rol.Id });

            treeDisponibles.Nodes.Add(nodoRoles);

            // permisos
            var permisos = BL.PermisoService.Instancia.ObtenerArbolPermisos();
            var nodoPermisos = new TreeNode("Permisos disponibles");
            foreach (var p in permisos)
                nodoPermisos.Nodes.Add(PermisoTreeHelper.CrearNodo(p));

            treeDisponibles.Nodes.Add(nodoPermisos);
            treeDisponibles.ExpandAll();
        }

        private void btnCrearRol_Click(object sender, EventArgs e)
        {
            var nombreRol = txtNuevoRol.Text.Trim();
            var permisosSeleccionados = new HashSet<int>();

            foreach (TreeNode nodo in treeDisponibles.Nodes)
            {
                AgregarPermisosDesdeNodoTree(nodo, permisosSeleccionados);
            }

            if (string.IsNullOrWhiteSpace(nombreRol))
            {
                MessageBox.Show("Ingresá un nombre para el nuevo rol.");
                return;
            }

            if (permisosSeleccionados.Count == 0)
            {
                MessageBox.Show("Seleccioná al menos un permiso o rol existente para componer el nuevo rol.");
                return;
            }

            try
            {
                int nuevoRolId = UserAdminService.Instancia.CrearRol(nombreRol, permisosSeleccionados);
                MessageBox.Show($"Rol creado correctamente (Id={nuevoRolId}).");
                txtNuevoRol.Clear();
                LimpiarChecksDisponibles(treeDisponibles.Nodes);
                CargarRolesYPermisosDisponibles();
                CargarUsuariosConPermisos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo crear el rol: " + ex.Message);
            }
        }

        private void AgregarPermisosDesdeNodoTree(TreeNode nodo, HashSet<int> permisosDestino)
        {
            if (nodo == null) return;

            if (nodo.Checked && nodo.Tag is string tag)
            {
                if (tag.StartsWith("ROL_"))
                {
                    int rolId = int.Parse(tag.Substring(4));
                    var permisosRol = BL.PermisoService.Instancia.ObtenerPermisosDeRol(rolId);
                    foreach (var comp in permisosRol)
                        AplanarPermisos(comp, permisosDestino);
                }
                else if (tag.StartsWith("PERM_"))
                {
                    AgregarPermisosDesdePermisoNodo(nodo, permisosDestino);
                }
            }

            foreach (TreeNode hijo in nodo.Nodes)
                AgregarPermisosDesdeNodoTree(hijo, permisosDestino);
        }

        private void AgregarPermisosDesdePermisoNodo(TreeNode nodo, HashSet<int> permisosDestino)
        {
            if (nodo == null) return;

            if (nodo.Tag is string tag && tag.StartsWith("PERM_"))
            {
                int permisoId = int.Parse(tag.Substring(5));
                permisosDestino.Add(permisoId);
            }

            foreach (TreeNode hijo in nodo.Nodes)
            {
                AgregarPermisosDesdePermisoNodo(hijo, permisosDestino);
            }
        }

        private void LimpiarChecksDisponibles(TreeNodeCollection nodos)
        {
            foreach (TreeNode nodo in nodos)
            {
                nodo.Checked = false;
                LimpiarChecksDisponibles(nodo.Nodes);
            }
        }

        private void btnAsignar_Click(object sender, EventArgs e)
        {
            if (treeUsuarios.SelectedNode == null || treeDisponibles.SelectedNode == null)
            {
                MessageBox.Show("Seleccioná un usuario y un rol o permiso para asignar.");
                return;
            }

            // usuario seleccionado
            var nodoUsuario = treeUsuarios.SelectedNode;
            while (nodoUsuario.Parent != null)
                nodoUsuario = nodoUsuario.Parent;

            int usuarioId = (int)nodoUsuario.Tag;

            // item seleccionado
            string tag = treeDisponibles.SelectedNode.Tag?.ToString();
            if (tag == null) return;

            if (tag.StartsWith("ROL_"))
            {
                int rolId = int.Parse(tag.Substring(4));
                var rolesActuales = UserAdminService.Instancia
                    .ObtenerUsuarioCompleto(usuarioId)?.Roles?
                    .Select(r => r.Id)
                    .ToList() ?? new List<int>();

                if (rolesActuales.Count == 1 && rolesActuales[0] == rolId)
                {
                    MessageBox.Show("El usuario ya tiene asignado ese rol.");
                    return;
                }

                BL.UserAdminService.Instancia.ActualizarRolesUsuario(usuarioId, new List<int> { rolId });
            }
            else if (tag.StartsWith("PERM_"))
            {
                int permisoId = int.Parse(tag.Substring(5));

                // verificar si ya está incluido en un rol del usuario
                var permisosUsuario = BL.PermisoService.Instancia.ObtenerPermisosDeUsuario(usuarioId);
                var permisosActuales = new HashSet<int>();
                AplanarPermisos(permisosUsuario, permisosActuales);
                if (permisosActuales.Contains(permisoId))
                {
                    MessageBox.Show("Ese permiso ya está incluido en un rol asignado.");
                    return;
                }

                BL.PermisoService.Instancia.AsignarPermisoDirectoUsuario(usuarioId, permisoId);

            }

            MessageBox.Show("Asignación realizada con éxito.");
            CargarUsuariosConPermisos();
        }

        private void btnQuitar_Click(object sender, EventArgs e)
        {
            if (treeUsuarios.SelectedNode == null)
            {
                MessageBox.Show("Seleccioná un rol o permiso del usuario para quitar.");
                return;
            }

            TreeNode nodo = treeUsuarios.SelectedNode;
            TreeNode nodoUsuario = nodo;
            while (nodoUsuario.Parent != null)
                nodoUsuario = nodoUsuario.Parent;

            int usuarioId = (int)nodoUsuario.Tag;
            string tag = nodo.Tag?.ToString();
            if (tag == null) return;

            if (tag.StartsWith("ROL_"))
            {
                int rolId = int.Parse(tag.Substring(4));

                // obtenemos los roles actuales y removemos este
                var rolesActuales = UserAdminService.Instancia
                    .ObtenerUsuarioCompleto(usuarioId)?.Roles?
                    .ToList() ?? new List<Rol>();
                var nuevosRoles = rolesActuales
                    .Where(r => r.Id != rolId)
                    .Select(r => r.Id)
                    .ToList();

                if (nuevosRoles.Count == 0)
                {
                    MessageBox.Show("Cada usuario debe conservar al menos un rol.");
                    return;
                }

                // actualizamos la relación
                BL.UserAdminService.Instancia.ActualizarRolesUsuario(usuarioId, nuevosRoles);

            }
            else if (tag.StartsWith("PERM_"))
            {
                int permisoId = int.Parse(tag.Substring(5));
                BL.PermisoService.Instancia.QuitarPermisoDirectoUsuario(usuarioId, permisoId);

            }

            MessageBox.Show("Eliminación realizada.");
            CargarUsuariosConPermisos();

        }



        private void treeUsuarios_AfterSelect_1(object sender, TreeViewEventArgs e)
        {
            // Busco el nodo raíz (el usuario)
            TreeNode nodo = e.Node;
            while (nodo.Parent != null)
                nodo = nodo.Parent;

            if (nodo.Tag is int usuarioId)
            {
                lblUsuarioSel.Text = $"Usuario seleccionado: {nodo.Text}";
                lblUsuarioSel.Tag = usuarioId; // guardo el id para reutilizar
            }
        }

        private void AplanarPermisos(PermisoComponent componente, HashSet<int> destino)
        {
            if (componente == null || destino == null)
                return;

            if (componente.Id != 0)
                destino.Add(componente.Id);

            if (componente is PermisoCompuesto compuesto && compuesto.Hijos != null)
            {
                foreach (var hijo in compuesto.Hijos)
                    AplanarPermisos(hijo, destino);
            }
        }

        private void ConfigurarTabIdiomas()
        {
            dgvListarIdiomas.AutoGenerateColumns = true;
            dgvListarTraduccion.AutoGenerateColumns = true;
            dgvListarIdiomas.MultiSelect = false;
            dgvListarTraduccion.MultiSelect = false;
        }

        private void InicializarMonitorIntegridad()
        {
            _integridadTimer = new Timer
            {
                Interval = 15000
            };
            _integridadTimer.Tick += IntegridadTimer_Tick;
            _integridadTimer.Start();
        }

        private void IntegridadTimer_Tick(object sender, EventArgs e)
        {
            if (_verificandoIntegridad)
                return;

            VerificarIntegridadEnEjecucion();
        }

        private void VerificarIntegridadEnEjecucion()
        {
            _verificandoIntegridad = true;
            try
            {
                var (ok, detalle) = VerificadorIntegridadService.Instancia.ValidarTodo();
                if (ok) return;

                bool esAdmin = SessionManager.Instancia.TieneRol("Administrador")
                    || SessionManager.Instancia.TienePermiso("Permiso.Gestionar");

                _integridadTimer?.Stop();
                AuthService.Instancia.Logout();

                if (!esAdmin)
                {
                    MessageBox.Show("No se puede usar la aplicación en este momento debido a una inconsistencia en los datos.");
                    Application.Restart();
                    return;
                }

                bool reparado = MostrarDialogoIntegridadEnEjecucion(detalle);
                if (reparado)
                {
                    Application.Restart();
                }
                else
                {
                    Application.Exit();
                }
            }
            finally
            {
                _verificandoIntegridad = false;
            }
        }

        private bool MostrarDialogoIntegridadEnEjecucion(string detalle)
        {
            while (true)
            {
                var rutaBackup = DatabaseMaintenanceService.Instancia.RutaBackupPorDefecto;
                var respuesta = MessageBox.Show(
                    "Se detectó una inconsistencia de integridad (DVH/DVV).\n\n" +
                    detalle + "\n\n" +
                    $"Presioná 'Sí' para restaurar la base desde {rutaBackup}.\n" +
                    "Presioná 'No' si ya corregiste la base manualmente y querés revalidar.\n" +
                    "Presioná 'Cancelar' para cerrar la aplicación.",
                    "Integridad de datos",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Stop,
                    MessageBoxDefaultButton.Button1);

                if (respuesta == DialogResult.Cancel)
                    return false;

                if (respuesta == DialogResult.Yes)
                {
                    try
                    {
                        DatabaseMaintenanceService.Instancia.RestaurarDesdeBackup();
                        MessageBox.Show("Se restauró la base de datos desde el backup seleccionado.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("No se pudo restaurar la base de datos: " + ex.Message);
                        continue;
                    }
                }

                var (ok, nuevoDetalle) = VerificadorIntegridadService.Instancia.ValidarTodo();
                if (ok)
                {
                    MessageBox.Show("La base de datos recuperó la consistencia. La aplicación se reiniciará.");
                    return true;
                }

                detalle = nuevoDetalle;
                MessageBox.Show("La inconsistencia persiste. Restaurá el backup o corregí el problema en SQL Server para continuar.");
            }
        }

        private void RecargarComboIdiomas()
        {
            var idiomas = _idiomaSrv.ListarIdiomasActivos();
            var codActual = IdiomaManager.Instancia.CodigoActual;

            cmbIdiomas.DataSource = idiomas;
            cmbIdiomas.DisplayMember = "Nombre";
            cmbIdiomas.ValueMember = "Codigo";

            if (!string.IsNullOrWhiteSpace(codActual) && idiomas.Any(i => i.Codigo == codActual))
                cmbIdiomas.SelectedValue = codActual;
            else if (idiomas.Any())
                cmbIdiomas.SelectedValue = idiomas.FirstOrDefault(i => i.EsPorDefecto)?.Codigo ?? idiomas.First().Codigo;
        }

        private void CargarIdiomasTab(int? idSeleccionar = null)
        {
            var idiomas = _idiomaSrv.ListarIdiomas();
            dgvListarIdiomas.DataSource = idiomas;

            if (idiomas.Count == 0)
            {
                dgvListarTraduccion.DataSource = null;
                txtAgregarTraduccion.Clear();
                _actualizandoEstadoIdioma = true;
                rbtnIdiomaActivo.Checked = false;
                rbtnIdiomaInactivo.Checked = false;
                _actualizandoEstadoIdioma = false;
                return;
            }

            int idABuscar = idSeleccionar ?? idiomas.First().Id;
            SeleccionarIdiomaEnGrilla(idABuscar);
        }

        private void SeleccionarIdiomaEnGrilla(int idIdioma)
        {
            foreach (DataGridViewRow row in dgvListarIdiomas.Rows)
            {
                if (row.DataBoundItem is Idioma idioma && idioma.Id == idIdioma)
                {
                    row.Selected = true;
                    dgvListarIdiomas.CurrentCell = row.Cells[0];
                    return;
                }
            }

            if (dgvListarIdiomas.Rows.Count > 0)
            {
                dgvListarIdiomas.Rows[0].Selected = true;
                dgvListarIdiomas.CurrentCell = dgvListarIdiomas.Rows[0].Cells[0];
            }
        }

        private void CargarTraduccionesParaIdioma(int idIdioma, int? idEtiqueta = null)
        {
            if (idIdioma <= 0)
            {
                dgvListarTraduccion.DataSource = null;
                txtAgregarTraduccion.Clear();
                return;
            }

            var traducciones = _idiomaSrv.ListarEtiquetasConTraduccion(idIdioma);
            dgvListarTraduccion.DataSource = traducciones;

            if (idEtiqueta.HasValue)
                SeleccionarEtiquetaEnGrilla(idEtiqueta.Value);
            else if (traducciones.Count > 0)
                SeleccionarEtiquetaEnGrilla(traducciones.First().IdEtiqueta);
        }

        private void SeleccionarEtiquetaEnGrilla(int idEtiqueta)
        {
            foreach (DataGridViewRow row in dgvListarTraduccion.Rows)
            {
                if (row.DataBoundItem is EtiquetaTraduccion et && et.IdEtiqueta == idEtiqueta)
                {
                    row.Selected = true;
                    dgvListarTraduccion.CurrentCell = row.Cells[0];
                    txtAgregarTraduccion.Text = et.Texto ?? string.Empty;
                    return;
                }
            }

            if (dgvListarTraduccion.Rows.Count > 0)
            {
                dgvListarTraduccion.Rows[0].Selected = true;
                dgvListarTraduccion.CurrentCell = dgvListarTraduccion.Rows[0].Cells[0];
                if (dgvListarTraduccion.Rows[0].DataBoundItem is EtiquetaTraduccion et)
                    txtAgregarTraduccion.Text = et.Texto ?? string.Empty;
            }
        }

        private Idioma ObtenerIdiomaSeleccionado()
        {
            return dgvListarIdiomas.CurrentRow?.DataBoundItem as Idioma;
        }

        private EtiquetaTraduccion ObtenerEtiquetaSeleccionada()
        {
            return dgvListarTraduccion.CurrentRow?.DataBoundItem as EtiquetaTraduccion;
        }

        private void btnAgregarIdioma_Click(object sender, EventArgs e)
        {
            try
            {
                var nuevo = _idiomaSrv.CrearIdioma(txtAgregarIdioma.Text);
                txtAgregarIdioma.Clear();
                CargarIdiomasTab(nuevo.Id);
                RecargarComboIdiomas();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnQuitarIdioma_Click(object sender, EventArgs e)
        {
            var seleccionado = ObtenerIdiomaSeleccionado();
            if (seleccionado == null)
            {
                MessageBox.Show("Seleccioná un idioma primero.");
                return;
            }

            var confirmar = MessageBox.Show(
                $"¿Seguro que querés eliminar el idioma '{seleccionado.Nombre}'?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirmar != DialogResult.Yes)
                return;

            _idiomaSrv.EliminarIdioma(seleccionado.Id);
            CargarIdiomasTab();
            RecargarComboIdiomas();
        }

        private void dgvListarIdiomas_SelectionChanged(object sender, EventArgs e)
        {
            var idioma = ObtenerIdiomaSeleccionado();
            if (idioma == null)
            {
                _actualizandoEstadoIdioma = true;
                rbtnIdiomaActivo.Checked = false;
                rbtnIdiomaInactivo.Checked = false;
                _actualizandoEstadoIdioma = false;
                dgvListarTraduccion.DataSource = null;
                txtAgregarTraduccion.Clear();
                return;
            }

            _actualizandoEstadoIdioma = true;
            rbtnIdiomaActivo.Checked = idioma.Activo;
            rbtnIdiomaInactivo.Checked = !idioma.Activo;
            _actualizandoEstadoIdioma = false;

            CargarTraduccionesParaIdioma(idioma.Id);
        }

        private void rbtnIdiomaActivo_CheckedChanged(object sender, EventArgs e)
        {
            if (_actualizandoEstadoIdioma || !rbtnIdiomaActivo.Checked)
                return;

            var idioma = ObtenerIdiomaSeleccionado();
            if (idioma == null)
                return;

            _idiomaSrv.ActualizarEstadoIdioma(idioma.Id, true);
            CargarIdiomasTab(idioma.Id);
            RecargarComboIdiomas();
        }

        private void rbtnIdiomaInactivo_CheckedChanged(object sender, EventArgs e)
        {
            if (_actualizandoEstadoIdioma || !rbtnIdiomaInactivo.Checked)
                return;

            var idioma = ObtenerIdiomaSeleccionado();
            if (idioma == null)
                return;

            _idiomaSrv.ActualizarEstadoIdioma(idioma.Id, false);
            CargarIdiomasTab(idioma.Id);
            RecargarComboIdiomas();
        }

        private void dgvListarTraduccion_SelectionChanged(object sender, EventArgs e)
        {
            var et = ObtenerEtiquetaSeleccionada();
            txtAgregarTraduccion.Text = et?.Texto ?? string.Empty;
        }

        private void btnAgregarTraduccion_Click(object sender, EventArgs e)
        {
            var idioma = ObtenerIdiomaSeleccionado();
            var etiqueta = ObtenerEtiquetaSeleccionada();
            if (idioma == null || etiqueta == null)
            {
                MessageBox.Show("Seleccioná un idioma y una etiqueta.");
                return;
            }

            try
            {
                _idiomaSrv.GuardarTraduccion(idioma.Id, etiqueta.IdEtiqueta, txtAgregarTraduccion.Text);
                CargarTraduccionesParaIdioma(idioma.Id, etiqueta.IdEtiqueta);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnQuitarTraduccion_Click(object sender, EventArgs e)
        {
            var idioma = ObtenerIdiomaSeleccionado();
            var etiqueta = ObtenerEtiquetaSeleccionada();
            if (idioma == null || etiqueta == null)
            {
                MessageBox.Show("Seleccioná un idioma y una etiqueta.");
                return;
            }

            _idiomaSrv.EliminarTraduccion(idioma.Id, etiqueta.IdEtiqueta);
            CargarTraduccionesParaIdioma(idioma.Id);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (!SessionManager.Instancia.TienePermiso("Usuario.Baja"))
            {
                MessageBox.Show("No contás con permiso para eliminar usuarios.");
                return;
            }

            if (dgvGestionUsuario.CurrentRow == null)
            {
                MessageBox.Show("Seleccioná un usuario primero.");
                return;
            }

            var fila = dgvGestionUsuario.CurrentRow;
            int usuarioId = Convert.ToInt32(fila.Cells["Id"].Value);
            string email = fila.Cells["Email"].Value.ToString();

            var confirmar = MessageBox.Show(
                $"¿Seguro que querés eliminar al usuario '{email}'?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirmar != DialogResult.Yes)
                return;

            try
            {
                BL.UserAdminService.Instancia.BajaLogicaUsuario(usuarioId);
                MessageBox.Show("Usuario eliminado correctamente.");
                CargarGrillaGestionUsuarios();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar el usuario: " + ex.Message);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            BL.VerificadorIntegridadService.Instancia.RecalcularTodo();

            MessageBox.Show("Recalibrado completo");

        }

        private void btnCrearBackup_Click(object sender, EventArgs e)
        {
            try
            {
                string ruta = DatabaseMaintenanceService.Instancia.CrearBackup();
                MessageBox.Show($"Backup creado correctamente en: {ruta}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al crear el backup: " + ex.Message);
            }
        }



        private void treeDisponibles_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void tabMenuPrincipal_Click(object sender, EventArgs e)
        {

        }
    }
}


