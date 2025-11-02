using BE;
using BL;
using DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_IS_Sistema_De_Tickets
{
    public partial class FormPrueba : Form, IIdiomaObserver
    {
        private readonly IdiomaService _idiomaSrv = new IdiomaService();

        private bool _registroVisible = false;   // estado del bloque de registro
        private bool _regRolesCargados = false;  // ya lo tenés: lo dejamos 

        private TabPage _tabRegistrar;
        private TabPage _tabBitacora;
        private TabPage _tabCambios;
        public FormPrueba()
        {
            InitializeComponent();
            IdiomaManager.Instancia.Suscribir(this);
        }

        private void FormPrueba_Load(object sender, EventArgs e)
        {
            ConfigurarDgvCambios();
            if (SessionManager.Instancia.UsuarioActual == null)
            {
                MessageBox.Show("La sesión no está activa. Volviendo al login.");
                Application.Restart();
                return;
            }

            bool esAdmin = SessionManager.Instancia.TieneRol("Administrador");
            var rolesUser = string.Join(", ", SessionManager.Instancia.UsuarioActual.Roles.Select(r => r.Nombre));
            this.Text = $"FormPrueba - {SessionManager.Instancia.UsuarioActual.Email} [{rolesUser}]";

            // guardo tabs
            _tabRegistrar = tabGeneral.TabPages.Count > 1 ? tabGeneral.TabPages[1] : null;
            _tabBitacora = tabGeneral.TabPages.Count > 2 ? tabGeneral.TabPages[2] : null;
            _tabCambios = tabGeneral.TabPages.Count > 3 ? tabGeneral.TabPages[3] : null;

            if (esAdmin)
            {
                SetRegistrarVisible(true);
                CargarGrillaGestionUsuarios();

                CargarEventosBitacoraHardcoded();
                CargarBitacoraInicial();

                CargarCambiosInicial();
            }
            else
            {
                if (_tabRegistrar != null) tabGeneral.TabPages.Remove(_tabRegistrar);
                if (_tabBitacora != null) tabGeneral.TabPages.Remove(_tabBitacora);
                if (_tabCambios != null) tabGeneral.TabPages.Remove(_tabCambios);
                SetRegistrarVisible(false);
            }

            var idiomas = _idiomaSrv.ListarIdiomas();
            if (cmbIdiomas != null)
            {
                cmbIdiomas.DataSource = idiomas;
                cmbIdiomas.DisplayMember = "Nombre";   // 👈 propiedad de IdiomaDTO
                cmbIdiomas.ValueMember = "Codigo";     // 👈 propiedad de IdiomaDTO

                // seleccionar por defecto
                var def = idiomas.FirstOrDefault(i => i.EsPorDefecto);
                if (def != null)
                    cmbIdiomas.SelectedValue = def.Codigo;
            }

            // disparo el idioma por defecto para que todos los forms se pinten
            _idiomaSrv.SeleccionarIdiomaPorDefecto();

        }

        // este es el MÉTODO que llama el observer
        public void ActualizarIdioma(Dictionary<string, string> t)
        {
            // pestañas
            tabGeneral.TabPages[0].Text = t["TAB_MENU"];
            tabGeneral.TabPages[1].Text = t["TAB_USUARIOS"];
            tabGeneral.TabPages[2].Text = t["TAB_BITACORA"];
            tabGeneral.TabPages[3].Text = t["TAB_CONTROL_CAMBIOS"];

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

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            // Cierra la sesión en DB (marca FinUtc y escribe auditoría)
            AuthService.Instancia.Logout();

            // Volvemos limpio al login (lo más simple para WinForms)
            Application.Restart();
        }

        private void TabGeneral_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool esAdmin = SessionManager.Instancia.TieneRol("Administrador");

            switch (tabGeneral.SelectedIndex)
            {
                case 0: // 🏠 Menú principal
                        // No hay restricción, todos pueden verlo
                    break;

                case 1: // 👤 Registrar usuarios
                    if (!esAdmin)
                    {
                        MessageBox.Show("Solo un Administrador puede gestionar usuarios.");
                        tabGeneral.SelectedIndex = 0; // vuelve al menú
                        return;
                    }

                    // Si es admin, mostramos y refrescamos la grilla
                    SetRegistrarVisible(true);
                    CargarGrillaGestionUsuarios();
                    break;

                case 2: // 📜 Bitácora
                    if (!esAdmin)
                    {
                        MessageBox.Show("Solo un Administrador puede ver la Bitácora.");
                        tabGeneral.SelectedIndex = 0;
                        return;
                    }

                    // Carga inicial (si no se cargó antes)
                    CargarEventosBitacoraHardcoded();
                    CargarBitacoraInicial();
                    break;
                case 3: // 👈 cambios
                    if (!esAdmin)
                    {
                        MessageBox.Show("Solo un Administrador puede ver el Control de Cambios.");
                        tabGeneral.SelectedIndex = 0;
                        return;
                    }
                    CargarCambiosInicial();
                    break;
                default:
                    // En caso de que se agreguen nuevas pestañas y quieras controlar más
                    tabGeneral.SelectedIndex = 0;
                    break;
            }
        }
        private void CargarCambiosInicial()
        {
            var repo = new ControlCambiosRepository();

            dtpCambiosDesde.Value = new DateTime(2000, 1, 1);
            dtpCambiosHasta.Value = DateTime.Today.AddDays(1);

            var datos = repo.FiltrarCambios(
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
        private void CargarBitacoraInicial()
        {
            var repoInit = new AuditoriaRepository();

            // por defecto: desde 2000 hasta mañana
            dtpDesde.Value = new DateTime(2000, 1, 1);
            dtpHasta.Value = DateTime.Today.AddDays(1);

            dgvBitacora.AutoGenerateColumns = true;
            dgvBitacora.DataSource = repoInit.FiltrarAuditoria(
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

            var repo = new AuditoriaRepository();
            var datos = repo.FiltrarAuditoria(id, usuarioId, evento, texto, desdeUtc, hastaUtcExcl);

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
            var repoInit = new AuditoriaRepository();
            dgvBitacora.AutoGenerateColumns = true;
            dgvBitacora.DataSource = repoInit.FiltrarAuditoria(
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
            // este método ahora se usa SOLO cuando ya sabemos que es admin
            var usuarios = BL.UserAdminService.Instancia.ListarUsuarios();
            dgvGestionUsuario.AutoGenerateColumns = true;
            dgvGestionUsuario.DataSource = usuarios;

            if (dgvGestionUsuario.Columns["Roles"] != null)
                dgvGestionUsuario.Columns["Roles"].Visible = false;
            if (dgvGestionUsuario.Columns["Permisos"] != null)
                dgvGestionUsuario.Columns["Permisos"].Visible = false;
            if (dgvGestionUsuario.Columns["DatosSensiblesEnc"] != null)
                dgvGestionUsuario.Columns["DatosSensiblesEnc"].Visible = false;

        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (dgvGestionUsuario.CurrentRow == null)
            {
                MessageBox.Show("Seleccioná un usuario primero.");
                return;
            }

            var usuario = dgvGestionUsuario.CurrentRow.DataBoundItem as BE.Usuario;
            if (usuario == null)
            {
                MessageBox.Show("No se pudo obtener el usuario seleccionado.");
                return;
            }

            var f = new FormGestionDeUsuario(
                FormGestionDeUsuario.ModoFormulario.Edicion,
                usuarioId: usuario.Id);

            f.FormClosed += (_, __) => CargarGrillaGestionUsuarios();
            f.ShowDialog(this);
        }

        private void btnNuevoRegistro_Click(object sender, EventArgs e)
        {
            var f = new FormGestionDeUsuario(
               FormGestionDeUsuario.ModoFormulario.Alta,
               usuarioId: null);

            f.FormClosed += (_, __) => CargarGrillaGestionUsuarios();
            f.ShowDialog(this);
        }

        private void btnFiltrarCambios_Click(object sender, EventArgs e)
        {
            int? id = int.TryParse(txtCambioUsuarioId.Text, out var vId) ? vId : (int?)null;
            int? usuarioId = int.TryParse(txtCambioId.Text, out var vUid) ? vUid : (int?)null;
            string entidad = string.IsNullOrWhiteSpace(txtCambioEntidad.Text) ? null : txtCambioEntidad.Text.Trim();
            int? entidadId = int.TryParse(txtCambioEntidadId.Text, out var vEid) ? vEid : (int?)null;
            string campo = string.IsNullOrWhiteSpace(txtCambioCampo.Text) ? null : txtCambioCampo.Text.Trim();

            var desdeUtc = DateTime.SpecifyKind(dtpCambiosDesde.Value.Date, DateTimeKind.Local).ToUniversalTime();
            var hastaUtc = DateTime.SpecifyKind(dtpCambiosHasta.Value.Date.AddDays(1), DateTimeKind.Local).ToUniversalTime();

            var repo = new ControlCambiosRepository();
            var datos = repo.FiltrarCambios(
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
    }
}


