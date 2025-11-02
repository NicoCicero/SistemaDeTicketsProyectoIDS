using BE;
using BL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_IS_Sistema_De_Tickets
{
    public partial class FormGestionDeUsuario : Form, IIdiomaObserver
    {
        private readonly IdiomaService _idiomaSrv = new IdiomaService();

        public FormGestionDeUsuario(ModoFormulario modo, int? usuarioId = null)
        {
            InitializeComponent();
            
            _modo = modo;
            _usuarioId = usuarioId;

            IdiomaManager.Instancia.Suscribir(this);
        }
        public enum ModoFormulario
        {
            Alta,
            Edicion
        }
        private readonly ModoFormulario _modo;
        private readonly int? _usuarioId;

        public void ActualizarIdioma(Dictionary<string, string> t)
        {
            // títulos según modo
            if ((string)lblGestionUsuario.Tag == "NEW")
                lblGestionUsuario.Text = t["TITLE_NUEVO_REGISTRO"];
            else if ((string)lblGestionUsuario.Tag == "EDIT")
                lblGestionUsuario.Text = t["TITLE_ACTUALIZAR_USUARIO"];
            else
                lblGestionUsuario.Text = t["TITLE_GESTION_USUARIO"];

            // labels
            lblEmail.Text = t["LBL_EMAIL"];
            lblNombre.Text = t["LBL_NOMBRE"];
            lblContraseña.Text = t["LBL_CONTRASENA"];
            lblConfirmarContraseña.Text = t["LBL_CONFIRMAR_CONTRASENA"];

            // checks
            chkActivo.Text = t["CHK_USUARIO_ACTIVO"];
            chkMostrarContraseña.Text = t["CHK_MOSTRAR_CONTRASENA"];

            // botones
            btnGuardar.Text = t["BTN_GUARDAR"];
            btnCancelar.Text = t["BTN_CANCELAR"];
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            IdiomaManager.Instancia.Desuscribir(this);
            base.OnFormClosed(e);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string nombre = txtNombre.Text.Trim();
            string pass1 = txtContraseña.Text;
            string pass2 = txtConfirmarContraseña.Text;
            bool activo = chkActivo.Checked;

            var rolesIds = clbRoles.CheckedItems.Cast<Rol>().Select(r => r.Id).ToList();
            if (rolesIds.Count == 0)
            {
                MessageBox.Show("Seleccioná al menos un rol.");
                return;
            }

            try
            {
                if (_modo == ModoFormulario.Alta)
                {
                    // en alta la pass es obligatoria
                    if (string.IsNullOrEmpty(pass1) || pass1.Length < 8)
                    {
                        MessageBox.Show("La contraseña debe tener al menos 8 caracteres.");
                        return;
                    }
                    if (pass1 != pass2)
                    {
                        MessageBox.Show("Las contraseñas no coinciden.");
                        return;
                    }

                    int nuevoId = UserAdminService.Instancia.CrearUsuario(
                        email, nombre, pass1, activo, rolesIds);

                    MessageBox.Show("Usuario creado. Id=" + nuevoId);
                    this.Close();
                }
                else
                {
                    // EDICIÓN
                    string passOpcional = null;
                    if (!string.IsNullOrEmpty(pass1) || !string.IsNullOrEmpty(pass2))
                    {
                        if (pass1.Length < 8)
                        {
                            MessageBox.Show("La contraseña debe tener al menos 8 caracteres.");
                            return;
                        }
                        if (pass1 != pass2)
                        {
                            MessageBox.Show("Las contraseñas no coinciden.");
                            return;
                        }
                        passOpcional = pass1;
                    }

                    UserAdminService.Instancia.ActualizarUsuario(
                        id: _usuarioId.Value,
                        email: email,
                        nombre: nombre,
                        activo: activo,
                        rolesIds: rolesIds,
                        passwordPlano: passOpcional
                    );

                    MessageBox.Show("Usuario actualizado.");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }


        private void FormGestionDeUsuario_Load(object sender, EventArgs e)
        {
            if (SessionManager.Instancia.UsuarioActual == null)
            {
                MessageBox.Show("La sesión no está activa. Volviendo al login.");
                Application.Restart();
                return;
            }
            CargarRolesDesdeBD();
            if (_modo == ModoFormulario.Alta)
            {
                lblGestionUsuario.Tag = "NEW";
                chkActivo.Checked = true;
    
            }
            else
            {
                lblGestionUsuario.Tag = "EDIT";
                
                CargarUsuarioEnControles(_usuarioId.Value);
            }
            txtContraseña.UseSystemPasswordChar = true;
            txtConfirmarContraseña.UseSystemPasswordChar = true;
            chkMostrarContraseña.CheckedChanged += (s, ev) =>
            {
                bool ver = chkMostrarContraseña.Checked;
                txtContraseña.UseSystemPasswordChar = !ver;
                txtConfirmarContraseña.UseSystemPasswordChar = !ver;
            };
            // Mostrar/ocultar según rol
            bool esAdmin = SessionManager.Instancia.TieneRol("Administrador");
            var roles = string.Join(", ", SessionManager.Instancia.UsuarioActual.Roles.Select(r => r.Nombre));
            this.Text = $"FormPrueba - {SessionManager.Instancia.UsuarioActual.Email} [{roles}]";


        }



        private void CargarUsuarioEnControles(int id)
        {
            var u = UserAdminService.Instancia.ObtenerUsuarioCompleto(id); 
            if (u == null)
            {
                MessageBox.Show("Usuario no encontrado.");
                Close();
                return;
            }

            txtEmail.Text = u.Email;
            txtNombre.Text = u.Nombre;
            chkActivo.Checked = u.Activo;

            // borro todos los checks
            for (int i = 0; i < clbRoles.Items.Count; i++)
                clbRoles.SetItemChecked(i, false);

            // marco los roles que tiene el usuario
            foreach (var rolDelUsuario in u.Roles)
            {
                for (int i = 0; i < clbRoles.Items.Count; i++)
                {
                    var rolItem = (Rol)clbRoles.Items[i];
                    if (rolItem.Id == rolDelUsuario.Id)
                        clbRoles.SetItemChecked(i, true);
                }
            }
        }      

        private void CargarRolesDesdeBD()
        {
            clbRoles.CheckOnClick = true;
            clbRoles.Items.Clear();
            var roles = UserAdminService.Instancia.ListarRoles();
            foreach (var r in roles)
                clbRoles.Items.Add(r, false);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
             this.Close();
        }
    }
}
