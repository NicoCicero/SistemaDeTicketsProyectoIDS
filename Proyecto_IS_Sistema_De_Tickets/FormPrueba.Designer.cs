using System;
using System.Windows.Forms;

namespace Proyecto_IS_Sistema_De_Tickets
{
    partial class FormPrueba
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnCerrarSesion = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.tabGeneral = new System.Windows.Forms.TabControl();
            this.tabMenuPrincipal = new System.Windows.Forms.TabPage();
            this.groupBoxActividad = new System.Windows.Forms.GroupBox();
            this.lblActividadVacia = new System.Windows.Forms.Label();
            this.dgvActividadPublica = new System.Windows.Forms.DataGridView();
            this.colFechaActividad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCategoriaActividad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDescripcionActividad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmbFiltroActividad = new System.Windows.Forms.ComboBox();
            this.lblFiltroActividad = new System.Windows.Forms.Label();
            this.btnSobreProyecto = new System.Windows.Forms.Button();
            this.groupBoxEstado = new System.Windows.Forms.GroupBox();
            this.lblVersionApp = new System.Windows.Forms.Label();
            this.lblIdiomaActual = new System.Windows.Forms.Label();
            this.lblHoraLocal = new System.Windows.Forms.Label();
            this.lblHoraServidor = new System.Windows.Forms.Label();
            this.lblEstadoDb = new System.Windows.Forms.Label();
            this.btnActualizarEstado = new System.Windows.Forms.Button();
            this.btnCrearBackup = new System.Windows.Forms.Button();
            this.cmbIdiomas = new System.Windows.Forms.ComboBox();
            this.tabUsuarios = new System.Windows.Forms.TabPage();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnNuevoRegistro = new System.Windows.Forms.Button();
            this.btnActualizar = new System.Windows.Forms.Button();
            this.dgvGestionUsuario = new System.Windows.Forms.DataGridView();
            this.tabBitacora = new System.Windows.Forms.TabPage();
            this.lblFecha = new System.Windows.Forms.Label();
            this.lblDetalle = new System.Windows.Forms.Label();
            this.lblEvento = new System.Windows.Forms.Label();
            this.lblAuditoriaId = new System.Windows.Forms.Label();
            this.lblUsuarioId = new System.Windows.Forms.Label();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.txtAuditoriaId = new System.Windows.Forms.TextBox();
            this.btnFiltrarBitacora = new System.Windows.Forms.Button();
            this.dtpHasta = new System.Windows.Forms.DateTimePicker();
            this.dtpDesde = new System.Windows.Forms.DateTimePicker();
            this.cmbEvento = new System.Windows.Forms.ComboBox();
            this.txtTexto = new System.Windows.Forms.TextBox();
            this.txtId = new System.Windows.Forms.TextBox();
            this.dgvBitacora = new System.Windows.Forms.DataGridView();
            this.tabControlCambios = new System.Windows.Forms.TabPage();
            this.lblCambioCampo = new System.Windows.Forms.Label();
            this.txtCambioCampo = new System.Windows.Forms.TextBox();
            this.txtCambioEntidad = new System.Windows.Forms.TextBox();
            this.btnLimpiarCambios = new System.Windows.Forms.Button();
            this.lblCambioFecha = new System.Windows.Forms.Label();
            this.lblCambioEntidadId = new System.Windows.Forms.Label();
            this.lblCambioEntidad = new System.Windows.Forms.Label();
            this.lblCambioId = new System.Windows.Forms.Label();
            this.lblCambioUsuarioId = new System.Windows.Forms.Label();
            this.txtCambioId = new System.Windows.Forms.TextBox();
            this.btnFiltrarCambios = new System.Windows.Forms.Button();
            this.dtpCambiosHasta = new System.Windows.Forms.DateTimePicker();
            this.dtpCambiosDesde = new System.Windows.Forms.DateTimePicker();
            this.txtCambioEntidadId = new System.Windows.Forms.TextBox();
            this.txtCambioUsuarioId = new System.Windows.Forms.TextBox();
            this.dgvCambios = new System.Windows.Forms.DataGridView();
            this.tabPermisos = new System.Windows.Forms.TabPage();
            this.btnCrearRol = new System.Windows.Forms.Button();
            this.txtNuevoRol = new System.Windows.Forms.TextBox();
            this.lblNuevoRol = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.lblUsuarioSel = new System.Windows.Forms.Label();
            this.btnAsignar = new System.Windows.Forms.Button();
            this.btnQuitar = new System.Windows.Forms.Button();
            this.treeDisponibles = new System.Windows.Forms.TreeView();
            this.treeUsuarios = new System.Windows.Forms.TreeView();
            this.tabIdiomas = new System.Windows.Forms.TabPage();
            this.rbtnIdiomaInactivo = new System.Windows.Forms.RadioButton();
            this.rbtnIdiomaActivo = new System.Windows.Forms.RadioButton();
            this.btnQuitarTraduccion = new System.Windows.Forms.Button();
            this.btnAgregarTraduccion = new System.Windows.Forms.Button();
            this.txtAgregarTraduccion = new System.Windows.Forms.TextBox();
            this.btnQuitarIdioma = new System.Windows.Forms.Button();
            this.btnAgregarIdioma = new System.Windows.Forms.Button();
            this.txtAgregarIdioma = new System.Windows.Forms.TextBox();
            this.dgvListarTraduccion = new System.Windows.Forms.DataGridView();
            this.dgvListarIdiomas = new System.Windows.Forms.DataGridView();
            this.lblTraduccion = new System.Windows.Forms.Label();
            this.lblNuevoIdioma = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.tabGeneral.SuspendLayout();
            this.tabMenuPrincipal.SuspendLayout();
            this.groupBoxActividad.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvActividadPublica)).BeginInit();
            this.groupBoxEstado.SuspendLayout();
            this.tabUsuarios.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGestionUsuario)).BeginInit();
            this.tabBitacora.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBitacora)).BeginInit();
            this.tabControlCambios.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCambios)).BeginInit();
            this.tabPermisos.SuspendLayout();
            this.tabIdiomas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListarTraduccion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListarIdiomas)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCerrarSesion
            // 
            this.btnCerrarSesion.Location = new System.Drawing.Point(16, 513);
            this.btnCerrarSesion.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCerrarSesion.Name = "btnCerrarSesion";
            this.btnCerrarSesion.Size = new System.Drawing.Size(144, 28);
            this.btnCerrarSesion.TabIndex = 0;
            this.btnCerrarSesion.Text = "Cerrar Sesion";
            this.btnCerrarSesion.UseVisualStyleBackColor = true;
            this.btnCerrarSesion.Click += new System.EventHandler(this.btnCerrarSesion_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // tabGeneral
            // 
            this.tabGeneral.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabGeneral.Controls.Add(this.tabMenuPrincipal);
            this.tabGeneral.Controls.Add(this.tabUsuarios);
            this.tabGeneral.Controls.Add(this.tabBitacora);
            this.tabGeneral.Controls.Add(this.tabControlCambios);
            this.tabGeneral.Controls.Add(this.tabPermisos);
            this.tabGeneral.Controls.Add(this.tabIdiomas);
            this.tabGeneral.Location = new System.Drawing.Point(12, 2);
            this.tabGeneral.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabGeneral.Name = "tabGeneral";
            this.tabGeneral.SelectedIndex = 0;
            this.tabGeneral.Size = new System.Drawing.Size(1024, 503);
            this.tabGeneral.TabIndex = 17;
            this.tabGeneral.SelectedIndexChanged += new System.EventHandler(this.TabGeneral_SelectedIndexChanged);
            // 
            // tabMenuPrincipal
            // 
            this.tabMenuPrincipal.Controls.Add(this.groupBoxActividad);
            this.tabMenuPrincipal.Controls.Add(this.btnSobreProyecto);
            this.tabMenuPrincipal.Controls.Add(this.groupBoxEstado);
            this.tabMenuPrincipal.Controls.Add(this.btnCrearBackup);
            this.tabMenuPrincipal.Controls.Add(this.cmbIdiomas);
            this.tabMenuPrincipal.Location = new System.Drawing.Point(4, 25);
            this.tabMenuPrincipal.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabMenuPrincipal.Name = "tabMenuPrincipal";
            this.tabMenuPrincipal.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabMenuPrincipal.Size = new System.Drawing.Size(1016, 474);
            this.tabMenuPrincipal.TabIndex = 2;
            this.tabMenuPrincipal.Text = "Menu Principal";
            this.tabMenuPrincipal.UseVisualStyleBackColor = true;
            this.tabMenuPrincipal.Click += new System.EventHandler(this.tabMenuPrincipal_Click);
            // 
            // groupBoxActividad
            // 
            this.groupBoxActividad.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxActividad.Controls.Add(this.lblActividadVacia);
            this.groupBoxActividad.Controls.Add(this.dgvActividadPublica);
            this.groupBoxActividad.Controls.Add(this.cmbFiltroActividad);
            this.groupBoxActividad.Controls.Add(this.lblFiltroActividad);
            this.groupBoxActividad.Location = new System.Drawing.Point(25, 247);
            this.groupBoxActividad.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBoxActividad.Name = "groupBoxActividad";
            this.groupBoxActividad.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBoxActividad.Size = new System.Drawing.Size(960, 210);
            this.groupBoxActividad.TabIndex = 2;
            this.groupBoxActividad.TabStop = false;
            this.groupBoxActividad.Text = "Actividad reciente pública";
            // 
            // lblActividadVacia
            // 
            this.lblActividadVacia.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblActividadVacia.AutoSize = true;
            this.lblActividadVacia.Location = new System.Drawing.Point(21, 183);
            this.lblActividadVacia.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblActividadVacia.Name = "lblActividadVacia";
            this.lblActividadVacia.Size = new System.Drawing.Size(206, 16);
            this.lblActividadVacia.TabIndex = 3;
            this.lblActividadVacia.Text = "Aún no hay eventos para mostrar.";
            // 
            // dgvActividadPublica
            // 
            this.dgvActividadPublica.AllowUserToAddRows = false;
            this.dgvActividadPublica.AllowUserToDeleteRows = false;
            this.dgvActividadPublica.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvActividadPublica.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvActividadPublica.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvActividadPublica.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colFechaActividad,
            this.colCategoriaActividad,
            this.colDescripcionActividad});
            this.dgvActividadPublica.Location = new System.Drawing.Point(25, 55);
            this.dgvActividadPublica.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvActividadPublica.MultiSelect = false;
            this.dgvActividadPublica.Name = "dgvActividadPublica";
            this.dgvActividadPublica.ReadOnly = true;
            this.dgvActividadPublica.RowHeadersVisible = false;
            this.dgvActividadPublica.RowHeadersWidth = 51;
            this.dgvActividadPublica.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvActividadPublica.Size = new System.Drawing.Size(909, 119);
            this.dgvActividadPublica.TabIndex = 2;
            // 
            // colFechaActividad
            // 
            this.colFechaActividad.DataPropertyName = "FechaLocal";
            this.colFechaActividad.HeaderText = "Fecha";
            this.colFechaActividad.MinimumWidth = 6;
            this.colFechaActividad.Name = "colFechaActividad";
            this.colFechaActividad.ReadOnly = true;
            // 
            // colCategoriaActividad
            // 
            this.colCategoriaActividad.DataPropertyName = "Categoria";
            this.colCategoriaActividad.HeaderText = "Categoría";
            this.colCategoriaActividad.MinimumWidth = 6;
            this.colCategoriaActividad.Name = "colCategoriaActividad";
            this.colCategoriaActividad.ReadOnly = true;
            // 
            // colDescripcionActividad
            // 
            this.colDescripcionActividad.DataPropertyName = "Descripcion";
            this.colDescripcionActividad.HeaderText = "Descripción";
            this.colDescripcionActividad.MinimumWidth = 6;
            this.colDescripcionActividad.Name = "colDescripcionActividad";
            this.colDescripcionActividad.ReadOnly = true;
            // 
            // cmbFiltroActividad
            // 
            this.cmbFiltroActividad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFiltroActividad.FormattingEnabled = true;
            this.cmbFiltroActividad.Items.AddRange(new object[] {
            "Todos",
            "Sistema",
            "Tickets",
            "Idioma"});
            this.cmbFiltroActividad.Location = new System.Drawing.Point(133, 23);
            this.cmbFiltroActividad.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbFiltroActividad.Name = "cmbFiltroActividad";
            this.cmbFiltroActividad.Size = new System.Drawing.Size(160, 24);
            this.cmbFiltroActividad.TabIndex = 1;
            this.cmbFiltroActividad.SelectedIndexChanged += new System.EventHandler(this.cmbFiltroActividad_SelectedIndexChanged);
            // 
            // lblFiltroActividad
            // 
            this.lblFiltroActividad.AutoSize = true;
            this.lblFiltroActividad.Location = new System.Drawing.Point(21, 28);
            this.lblFiltroActividad.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFiltroActividad.Name = "lblFiltroActividad";
            this.lblFiltroActividad.Size = new System.Drawing.Size(36, 16);
            this.lblFiltroActividad.TabIndex = 0;
            this.lblFiltroActividad.Text = "Filtro";
            // 
            // btnSobreProyecto
            // 
            this.btnSobreProyecto.Location = new System.Drawing.Point(543, 54);
            this.btnSobreProyecto.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSobreProyecto.Name = "btnSobreProyecto";
            this.btnSobreProyecto.Size = new System.Drawing.Size(280, 74);
            this.btnSobreProyecto.TabIndex = 3;
            this.btnSobreProyecto.Text = "¿De qué se trata este proyecto?";
            this.btnSobreProyecto.UseVisualStyleBackColor = true;
            this.btnSobreProyecto.Click += new System.EventHandler(this.btnSobreProyecto_Click);
            // 
            // groupBoxEstado
            // 
            this.groupBoxEstado.Controls.Add(this.lblVersionApp);
            this.groupBoxEstado.Controls.Add(this.lblIdiomaActual);
            this.groupBoxEstado.Controls.Add(this.lblHoraLocal);
            this.groupBoxEstado.Controls.Add(this.lblHoraServidor);
            this.groupBoxEstado.Controls.Add(this.lblEstadoDb);
            this.groupBoxEstado.Controls.Add(this.btnActualizarEstado);
            this.groupBoxEstado.Location = new System.Drawing.Point(24, 37);
            this.groupBoxEstado.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBoxEstado.Name = "groupBoxEstado";
            this.groupBoxEstado.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBoxEstado.Size = new System.Drawing.Size(473, 203);
            this.groupBoxEstado.TabIndex = 4;
            this.groupBoxEstado.TabStop = false;
            this.groupBoxEstado.Text = "Estado del sistema";
            // 
            // lblVersionApp
            // 
            this.lblVersionApp.AutoSize = true;
            this.lblVersionApp.Location = new System.Drawing.Point(21, 140);
            this.lblVersionApp.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblVersionApp.Name = "lblVersionApp";
            this.lblVersionApp.Size = new System.Drawing.Size(63, 16);
            this.lblVersionApp.TabIndex = 5;
            this.lblVersionApp.Text = "Versión: -";
            // 
            // lblIdiomaActual
            // 
            this.lblIdiomaActual.AutoSize = true;
            this.lblIdiomaActual.Location = new System.Drawing.Point(21, 113);
            this.lblIdiomaActual.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblIdiomaActual.Name = "lblIdiomaActual";
            this.lblIdiomaActual.Size = new System.Drawing.Size(97, 16);
            this.lblIdiomaActual.TabIndex = 4;
            this.lblIdiomaActual.Text = "Idioma actual: -";
            // 
            // lblHoraLocal
            // 
            this.lblHoraLocal.AutoSize = true;
            this.lblHoraLocal.Location = new System.Drawing.Point(21, 86);
            this.lblHoraLocal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblHoraLocal.Name = "lblHoraLocal";
            this.lblHoraLocal.Size = new System.Drawing.Size(79, 16);
            this.lblHoraLocal.TabIndex = 3;
            this.lblHoraLocal.Text = "Hora local: -";
            // 
            // lblHoraServidor
            // 
            this.lblHoraServidor.AutoSize = true;
            this.lblHoraServidor.Location = new System.Drawing.Point(21, 59);
            this.lblHoraServidor.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblHoraServidor.Name = "lblHoraServidor";
            this.lblHoraServidor.Size = new System.Drawing.Size(138, 16);
            this.lblHoraServidor.TabIndex = 2;
            this.lblHoraServidor.Text = "Hora servidor (UTC): -";
            // 
            // lblEstadoDb
            // 
            this.lblEstadoDb.AutoSize = true;
            this.lblEstadoDb.Location = new System.Drawing.Point(21, 32);
            this.lblEstadoDb.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEstadoDb.Name = "lblEstadoDb";
            this.lblEstadoDb.Size = new System.Drawing.Size(105, 16);
            this.lblEstadoDb.TabIndex = 1;
            this.lblEstadoDb.Text = "Base de datos: -";
            // 
            // btnActualizarEstado
            // 
            this.btnActualizarEstado.Location = new System.Drawing.Point(25, 162);
            this.btnActualizarEstado.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnActualizarEstado.Name = "btnActualizarEstado";
            this.btnActualizarEstado.Size = new System.Drawing.Size(175, 30);
            this.btnActualizarEstado.TabIndex = 0;
            this.btnActualizarEstado.Text = "Actualizar estado";
            this.btnActualizarEstado.UseVisualStyleBackColor = true;
            this.btnActualizarEstado.Click += new System.EventHandler(this.btnActualizarEstado_Click);
            // 
            // btnCrearBackup
            // 
            this.btnCrearBackup.Location = new System.Drawing.Point(543, 135);
            this.btnCrearBackup.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCrearBackup.Name = "btnCrearBackup";
            this.btnCrearBackup.Size = new System.Drawing.Size(213, 49);
            this.btnCrearBackup.TabIndex = 1;
            this.btnCrearBackup.Text = "Crear Backup";
            this.btnCrearBackup.UseVisualStyleBackColor = true;
            this.btnCrearBackup.Click += new System.EventHandler(this.btnCrearBackup_Click);
            // 
            // cmbIdiomas
            // 
            this.cmbIdiomas.FormattingEnabled = true;
            this.cmbIdiomas.Location = new System.Drawing.Point(821, 6);
            this.cmbIdiomas.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbIdiomas.Name = "cmbIdiomas";
            this.cmbIdiomas.Size = new System.Drawing.Size(184, 24);
            this.cmbIdiomas.TabIndex = 0;
            this.cmbIdiomas.SelectedIndexChanged += new System.EventHandler(this.cmbIdiomas_SelectedIndexChanged);
            // 
            // tabUsuarios
            // 
            this.tabUsuarios.Controls.Add(this.btnEliminar);
            this.tabUsuarios.Controls.Add(this.btnNuevoRegistro);
            this.tabUsuarios.Controls.Add(this.btnActualizar);
            this.tabUsuarios.Controls.Add(this.dgvGestionUsuario);
            this.tabUsuarios.Location = new System.Drawing.Point(4, 25);
            this.tabUsuarios.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabUsuarios.Name = "tabUsuarios";
            this.tabUsuarios.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabUsuarios.Size = new System.Drawing.Size(1016, 474);
            this.tabUsuarios.TabIndex = 0;
            this.tabUsuarios.Text = "Usuarios";
            this.tabUsuarios.UseVisualStyleBackColor = true;
            this.tabUsuarios.Click += new System.EventHandler(this.tabRegistrar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Location = new System.Drawing.Point(771, 382);
            this.btnEliminar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(128, 28);
            this.btnEliminar.TabIndex = 3;
            this.btnEliminar.Text = "Eliminar Usuario";
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnNuevoRegistro
            // 
            this.btnNuevoRegistro.Location = new System.Drawing.Point(513, 382);
            this.btnNuevoRegistro.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnNuevoRegistro.Name = "btnNuevoRegistro";
            this.btnNuevoRegistro.Size = new System.Drawing.Size(128, 28);
            this.btnNuevoRegistro.TabIndex = 2;
            this.btnNuevoRegistro.Text = "Nuevo Registro";
            this.btnNuevoRegistro.UseVisualStyleBackColor = true;
            this.btnNuevoRegistro.Click += new System.EventHandler(this.btnNuevoRegistro_Click);
            // 
            // btnActualizar
            // 
            this.btnActualizar.Location = new System.Drawing.Point(339, 382);
            this.btnActualizar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(128, 28);
            this.btnActualizar.TabIndex = 1;
            this.btnActualizar.Text = "Actualizar";
            this.btnActualizar.UseVisualStyleBackColor = true;
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // dgvGestionUsuario
            // 
            this.dgvGestionUsuario.AllowUserToAddRows = false;
            this.dgvGestionUsuario.AllowUserToDeleteRows = false;
            this.dgvGestionUsuario.AllowUserToResizeColumns = false;
            this.dgvGestionUsuario.AllowUserToResizeRows = false;
            this.dgvGestionUsuario.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGestionUsuario.Location = new System.Drawing.Point(91, 37);
            this.dgvGestionUsuario.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvGestionUsuario.Name = "dgvGestionUsuario";
            this.dgvGestionUsuario.RowHeadersWidth = 51;
            this.dgvGestionUsuario.Size = new System.Drawing.Size(808, 337);
            this.dgvGestionUsuario.TabIndex = 0;
            // 
            // tabBitacora
            // 
            this.tabBitacora.Controls.Add(this.lblFecha);
            this.tabBitacora.Controls.Add(this.lblDetalle);
            this.tabBitacora.Controls.Add(this.lblEvento);
            this.tabBitacora.Controls.Add(this.lblAuditoriaId);
            this.tabBitacora.Controls.Add(this.lblUsuarioId);
            this.tabBitacora.Controls.Add(this.btnLimpiar);
            this.tabBitacora.Controls.Add(this.txtAuditoriaId);
            this.tabBitacora.Controls.Add(this.btnFiltrarBitacora);
            this.tabBitacora.Controls.Add(this.dtpHasta);
            this.tabBitacora.Controls.Add(this.dtpDesde);
            this.tabBitacora.Controls.Add(this.cmbEvento);
            this.tabBitacora.Controls.Add(this.txtTexto);
            this.tabBitacora.Controls.Add(this.txtId);
            this.tabBitacora.Controls.Add(this.dgvBitacora);
            this.tabBitacora.Location = new System.Drawing.Point(4, 25);
            this.tabBitacora.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabBitacora.Name = "tabBitacora";
            this.tabBitacora.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabBitacora.Size = new System.Drawing.Size(1016, 474);
            this.tabBitacora.TabIndex = 1;
            this.tabBitacora.Text = "Bitacora";
            this.tabBitacora.UseVisualStyleBackColor = true;
            // 
            // lblFecha
            // 
            this.lblFecha.AutoSize = true;
            this.lblFecha.Location = new System.Drawing.Point(805, 318);
            this.lblFecha.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(45, 16);
            this.lblFecha.TabIndex = 18;
            this.lblFecha.Text = "Fecha";
            // 
            // lblDetalle
            // 
            this.lblDetalle.AutoSize = true;
            this.lblDetalle.Location = new System.Drawing.Point(805, 228);
            this.lblDetalle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDetalle.Name = "lblDetalle";
            this.lblDetalle.Size = new System.Drawing.Size(50, 16);
            this.lblDetalle.TabIndex = 17;
            this.lblDetalle.Text = "Detalle";
            // 
            // lblEvento
            // 
            this.lblEvento.AutoSize = true;
            this.lblEvento.Location = new System.Drawing.Point(805, 164);
            this.lblEvento.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEvento.Name = "lblEvento";
            this.lblEvento.Size = new System.Drawing.Size(49, 16);
            this.lblEvento.TabIndex = 16;
            this.lblEvento.Text = "Evento";
            // 
            // lblAuditoriaId
            // 
            this.lblAuditoriaId.AutoSize = true;
            this.lblAuditoriaId.Location = new System.Drawing.Point(805, 95);
            this.lblAuditoriaId.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAuditoriaId.Name = "lblAuditoriaId";
            this.lblAuditoriaId.Size = new System.Drawing.Size(74, 16);
            this.lblAuditoriaId.TabIndex = 15;
            this.lblAuditoriaId.Text = "Auditoria Id";
            // 
            // lblUsuarioId
            // 
            this.lblUsuarioId.AutoSize = true;
            this.lblUsuarioId.Location = new System.Drawing.Point(805, 17);
            this.lblUsuarioId.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblUsuarioId.Name = "lblUsuarioId";
            this.lblUsuarioId.Size = new System.Drawing.Size(68, 16);
            this.lblUsuarioId.TabIndex = 14;
            this.lblUsuarioId.Text = "Usuario Id";
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Location = new System.Drawing.Point(912, 427);
            this.btnLimpiar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(75, 23);
            this.btnLimpiar.TabIndex = 13;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = true;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // txtAuditoriaId
            // 
            this.txtAuditoriaId.Location = new System.Drawing.Point(804, 126);
            this.txtAuditoriaId.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtAuditoriaId.Name = "txtAuditoriaId";
            this.txtAuditoriaId.Size = new System.Drawing.Size(124, 22);
            this.txtAuditoriaId.TabIndex = 12;
            // 
            // btnFiltrarBitacora
            // 
            this.btnFiltrarBitacora.Location = new System.Drawing.Point(813, 427);
            this.btnFiltrarBitacora.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnFiltrarBitacora.Name = "btnFiltrarBitacora";
            this.btnFiltrarBitacora.Size = new System.Drawing.Size(75, 23);
            this.btnFiltrarBitacora.TabIndex = 10;
            this.btnFiltrarBitacora.Text = "Filtrar";
            this.btnFiltrarBitacora.UseVisualStyleBackColor = true;
            this.btnFiltrarBitacora.Click += new System.EventHandler(this.btnFiltrarBitacora_Click);
            // 
            // dtpHasta
            // 
            this.dtpHasta.Location = new System.Drawing.Point(807, 383);
            this.dtpHasta.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dtpHasta.Name = "dtpHasta";
            this.dtpHasta.Size = new System.Drawing.Size(200, 22);
            this.dtpHasta.TabIndex = 9;
            // 
            // dtpDesde
            // 
            this.dtpDesde.Location = new System.Drawing.Point(807, 353);
            this.dtpDesde.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dtpDesde.Name = "dtpDesde";
            this.dtpDesde.Size = new System.Drawing.Size(200, 22);
            this.dtpDesde.TabIndex = 8;
            // 
            // cmbEvento
            // 
            this.cmbEvento.FormattingEnabled = true;
            this.cmbEvento.Location = new System.Drawing.Point(807, 182);
            this.cmbEvento.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmbEvento.Name = "cmbEvento";
            this.cmbEvento.Size = new System.Drawing.Size(121, 24);
            this.cmbEvento.TabIndex = 6;
            // 
            // txtTexto
            // 
            this.txtTexto.Location = new System.Drawing.Point(807, 257);
            this.txtTexto.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtTexto.Name = "txtTexto";
            this.txtTexto.Size = new System.Drawing.Size(121, 22);
            this.txtTexto.TabIndex = 5;
            // 
            // txtId
            // 
            this.txtId.Location = new System.Drawing.Point(807, 55);
            this.txtId.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtId.Name = "txtId";
            this.txtId.Size = new System.Drawing.Size(121, 22);
            this.txtId.TabIndex = 1;
            // 
            // dgvBitacora
            // 
            this.dgvBitacora.AllowUserToAddRows = false;
            this.dgvBitacora.AllowUserToDeleteRows = false;
            this.dgvBitacora.AllowUserToOrderColumns = true;
            this.dgvBitacora.AllowUserToResizeColumns = false;
            this.dgvBitacora.AllowUserToResizeRows = false;
            this.dgvBitacora.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBitacora.Location = new System.Drawing.Point(27, 17);
            this.dgvBitacora.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvBitacora.Name = "dgvBitacora";
            this.dgvBitacora.RowHeadersWidth = 51;
            this.dgvBitacora.RowTemplate.Height = 24;
            this.dgvBitacora.Size = new System.Drawing.Size(759, 433);
            this.dgvBitacora.TabIndex = 0;
            this.dgvBitacora.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // tabControlCambios
            // 
            this.tabControlCambios.Controls.Add(this.lblCambioCampo);
            this.tabControlCambios.Controls.Add(this.txtCambioCampo);
            this.tabControlCambios.Controls.Add(this.txtCambioEntidad);
            this.tabControlCambios.Controls.Add(this.btnLimpiarCambios);
            this.tabControlCambios.Controls.Add(this.lblCambioFecha);
            this.tabControlCambios.Controls.Add(this.lblCambioEntidadId);
            this.tabControlCambios.Controls.Add(this.lblCambioEntidad);
            this.tabControlCambios.Controls.Add(this.lblCambioId);
            this.tabControlCambios.Controls.Add(this.lblCambioUsuarioId);
            this.tabControlCambios.Controls.Add(this.txtCambioId);
            this.tabControlCambios.Controls.Add(this.btnFiltrarCambios);
            this.tabControlCambios.Controls.Add(this.dtpCambiosHasta);
            this.tabControlCambios.Controls.Add(this.dtpCambiosDesde);
            this.tabControlCambios.Controls.Add(this.txtCambioEntidadId);
            this.tabControlCambios.Controls.Add(this.txtCambioUsuarioId);
            this.tabControlCambios.Controls.Add(this.dgvCambios);
            this.tabControlCambios.Location = new System.Drawing.Point(4, 25);
            this.tabControlCambios.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabControlCambios.Name = "tabControlCambios";
            this.tabControlCambios.Size = new System.Drawing.Size(1016, 474);
            this.tabControlCambios.TabIndex = 3;
            this.tabControlCambios.Text = "Control Cambios";
            this.tabControlCambios.UseVisualStyleBackColor = true;
            // 
            // lblCambioCampo
            // 
            this.lblCambioCampo.AutoSize = true;
            this.lblCambioCampo.Location = new System.Drawing.Point(800, 270);
            this.lblCambioCampo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCambioCampo.Name = "lblCambioCampo";
            this.lblCambioCampo.Size = new System.Drawing.Size(101, 16);
            this.lblCambioCampo.TabIndex = 35;
            this.lblCambioCampo.Text = "Cambio Campo";
            // 
            // txtCambioCampo
            // 
            this.txtCambioCampo.Location = new System.Drawing.Point(804, 288);
            this.txtCambioCampo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtCambioCampo.Name = "txtCambioCampo";
            this.txtCambioCampo.Size = new System.Drawing.Size(121, 22);
            this.txtCambioCampo.TabIndex = 34;
            // 
            // txtCambioEntidad
            // 
            this.txtCambioEntidad.Location = new System.Drawing.Point(801, 161);
            this.txtCambioEntidad.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtCambioEntidad.Name = "txtCambioEntidad";
            this.txtCambioEntidad.Size = new System.Drawing.Size(132, 22);
            this.txtCambioEntidad.TabIndex = 33;
            // 
            // btnLimpiarCambios
            // 
            this.btnLimpiarCambios.Location = new System.Drawing.Point(915, 418);
            this.btnLimpiarCambios.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnLimpiarCambios.Name = "btnLimpiarCambios";
            this.btnLimpiarCambios.Size = new System.Drawing.Size(75, 23);
            this.btnLimpiarCambios.TabIndex = 32;
            this.btnLimpiarCambios.Text = "Limpiar";
            this.btnLimpiarCambios.UseVisualStyleBackColor = true;
            this.btnLimpiarCambios.Click += new System.EventHandler(this.btnLimpiarCambios_Click);
            // 
            // lblCambioFecha
            // 
            this.lblCambioFecha.AutoSize = true;
            this.lblCambioFecha.Location = new System.Drawing.Point(800, 326);
            this.lblCambioFecha.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCambioFecha.Name = "lblCambioFecha";
            this.lblCambioFecha.Size = new System.Drawing.Size(45, 16);
            this.lblCambioFecha.TabIndex = 31;
            this.lblCambioFecha.Text = "Fecha";
            // 
            // lblCambioEntidadId
            // 
            this.lblCambioEntidadId.AutoSize = true;
            this.lblCambioEntidadId.Location = new System.Drawing.Point(803, 204);
            this.lblCambioEntidadId.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCambioEntidadId.Name = "lblCambioEntidadId";
            this.lblCambioEntidadId.Size = new System.Drawing.Size(109, 16);
            this.lblCambioEntidadId.TabIndex = 30;
            this.lblCambioEntidadId.Text = "Cambio Entida Id";
            // 
            // lblCambioEntidad
            // 
            this.lblCambioEntidad.AutoSize = true;
            this.lblCambioEntidad.Location = new System.Drawing.Point(797, 142);
            this.lblCambioEntidad.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCambioEntidad.Name = "lblCambioEntidad";
            this.lblCambioEntidad.Size = new System.Drawing.Size(103, 16);
            this.lblCambioEntidad.TabIndex = 29;
            this.lblCambioEntidad.Text = "Cambio Entidad";
            // 
            // lblCambioId
            // 
            this.lblCambioId.AutoSize = true;
            this.lblCambioId.Location = new System.Drawing.Point(800, 18);
            this.lblCambioId.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCambioId.Name = "lblCambioId";
            this.lblCambioId.Size = new System.Drawing.Size(68, 16);
            this.lblCambioId.TabIndex = 28;
            this.lblCambioId.Text = "Cambio Id";
            // 
            // lblCambioUsuarioId
            // 
            this.lblCambioUsuarioId.AutoSize = true;
            this.lblCambioUsuarioId.Location = new System.Drawing.Point(797, 80);
            this.lblCambioUsuarioId.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCambioUsuarioId.Name = "lblCambioUsuarioId";
            this.lblCambioUsuarioId.Size = new System.Drawing.Size(68, 16);
            this.lblCambioUsuarioId.TabIndex = 27;
            this.lblCambioUsuarioId.Text = "Usuario Id";
            // 
            // txtCambioId
            // 
            this.txtCambioId.Location = new System.Drawing.Point(804, 37);
            this.txtCambioId.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtCambioId.Name = "txtCambioId";
            this.txtCambioId.Size = new System.Drawing.Size(124, 22);
            this.txtCambioId.TabIndex = 25;
            // 
            // btnFiltrarCambios
            // 
            this.btnFiltrarCambios.Location = new System.Drawing.Point(808, 418);
            this.btnFiltrarCambios.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnFiltrarCambios.Name = "btnFiltrarCambios";
            this.btnFiltrarCambios.Size = new System.Drawing.Size(75, 23);
            this.btnFiltrarCambios.TabIndex = 24;
            this.btnFiltrarCambios.Text = "Filtrar";
            this.btnFiltrarCambios.UseVisualStyleBackColor = true;
            this.btnFiltrarCambios.Click += new System.EventHandler(this.btnFiltrarCambios_Click);
            // 
            // dtpCambiosHasta
            // 
            this.dtpCambiosHasta.Location = new System.Drawing.Point(801, 374);
            this.dtpCambiosHasta.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dtpCambiosHasta.Name = "dtpCambiosHasta";
            this.dtpCambiosHasta.Size = new System.Drawing.Size(200, 22);
            this.dtpCambiosHasta.TabIndex = 23;
            // 
            // dtpCambiosDesde
            // 
            this.dtpCambiosDesde.Location = new System.Drawing.Point(801, 345);
            this.dtpCambiosDesde.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dtpCambiosDesde.Name = "dtpCambiosDesde";
            this.dtpCambiosDesde.Size = new System.Drawing.Size(200, 22);
            this.dtpCambiosDesde.TabIndex = 22;
            // 
            // txtCambioEntidadId
            // 
            this.txtCambioEntidadId.Location = new System.Drawing.Point(804, 223);
            this.txtCambioEntidadId.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtCambioEntidadId.Name = "txtCambioEntidadId";
            this.txtCambioEntidadId.Size = new System.Drawing.Size(121, 22);
            this.txtCambioEntidadId.TabIndex = 20;
            // 
            // txtCambioUsuarioId
            // 
            this.txtCambioUsuarioId.Location = new System.Drawing.Point(801, 98);
            this.txtCambioUsuarioId.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtCambioUsuarioId.Name = "txtCambioUsuarioId";
            this.txtCambioUsuarioId.Size = new System.Drawing.Size(121, 22);
            this.txtCambioUsuarioId.TabIndex = 19;
            // 
            // dgvCambios
            // 
            this.dgvCambios.AllowUserToAddRows = false;
            this.dgvCambios.AllowUserToDeleteRows = false;
            this.dgvCambios.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvCambios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCambios.Location = new System.Drawing.Point(19, 16);
            this.dgvCambios.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvCambios.Name = "dgvCambios";
            this.dgvCambios.ReadOnly = true;
            this.dgvCambios.RowHeadersWidth = 51;
            this.dgvCambios.Size = new System.Drawing.Size(759, 438);
            this.dgvCambios.TabIndex = 1;
            // 
            // tabPermisos
            // 
            this.tabPermisos.Controls.Add(this.btnCrearRol);
            this.tabPermisos.Controls.Add(this.txtNuevoRol);
            this.tabPermisos.Controls.Add(this.lblNuevoRol);
            this.tabPermisos.Controls.Add(this.button1);
            this.tabPermisos.Controls.Add(this.lblUsuarioSel);
            this.tabPermisos.Controls.Add(this.btnAsignar);
            this.tabPermisos.Controls.Add(this.btnQuitar);
            this.tabPermisos.Controls.Add(this.treeDisponibles);
            this.tabPermisos.Controls.Add(this.treeUsuarios);
            this.tabPermisos.Location = new System.Drawing.Point(4, 25);
            this.tabPermisos.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPermisos.Name = "tabPermisos";
            this.tabPermisos.Size = new System.Drawing.Size(1016, 474);
            this.tabPermisos.TabIndex = 4;
            this.tabPermisos.Text = "Permisos";
            this.tabPermisos.UseVisualStyleBackColor = true;
            // 
            // btnCrearRol
            // 
            this.btnCrearRol.Location = new System.Drawing.Point(437, 126);
            this.btnCrearRol.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCrearRol.Name = "btnCrearRol";
            this.btnCrearRol.Size = new System.Drawing.Size(144, 28);
            this.btnCrearRol.TabIndex = 8;
            this.btnCrearRol.Text = "Crear rol";
            this.btnCrearRol.UseVisualStyleBackColor = true;
            this.btnCrearRol.Click += new System.EventHandler(this.btnCrearRol_Click);
            // 
            // txtNuevoRol
            // 
            this.txtNuevoRol.Location = new System.Drawing.Point(300, 128);
            this.txtNuevoRol.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtNuevoRol.Name = "txtNuevoRol";
            this.txtNuevoRol.Size = new System.Drawing.Size(125, 22);
            this.txtNuevoRol.TabIndex = 7;
            // 
            // lblNuevoRol
            // 
            this.lblNuevoRol.AutoSize = true;
            this.lblNuevoRol.Location = new System.Drawing.Point(296, 108);
            this.lblNuevoRol.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNuevoRol.Name = "lblNuevoRol";
            this.lblNuevoRol.Size = new System.Drawing.Size(65, 16);
            this.lblNuevoRol.TabIndex = 6;
            this.lblNuevoRol.Text = "Nuevo rol";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(437, 340);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(144, 28);
            this.button1.TabIndex = 5;
            this.button1.Text = "Quitar Permiso";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // lblUsuarioSel
            // 
            this.lblUsuarioSel.AutoSize = true;
            this.lblUsuarioSel.Location = new System.Drawing.Point(296, 27);
            this.lblUsuarioSel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblUsuarioSel.Name = "lblUsuarioSel";
            this.lblUsuarioSel.Size = new System.Drawing.Size(44, 16);
            this.lblUsuarioSel.TabIndex = 4;
            this.lblUsuarioSel.Text = "label1";
            // 
            // btnAsignar
            // 
            this.btnAsignar.Location = new System.Drawing.Point(437, 182);
            this.btnAsignar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAsignar.Name = "btnAsignar";
            this.btnAsignar.Size = new System.Drawing.Size(144, 28);
            this.btnAsignar.TabIndex = 3;
            this.btnAsignar.Text = "Asignar Permiso";
            this.btnAsignar.UseVisualStyleBackColor = true;
            this.btnAsignar.Click += new System.EventHandler(this.btnAsignar_Click);
            // 
            // btnQuitar
            // 
            this.btnQuitar.Location = new System.Drawing.Point(437, 249);
            this.btnQuitar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnQuitar.Name = "btnQuitar";
            this.btnQuitar.Size = new System.Drawing.Size(144, 28);
            this.btnQuitar.TabIndex = 2;
            this.btnQuitar.Text = "Quitar Permiso";
            this.btnQuitar.UseVisualStyleBackColor = true;
            this.btnQuitar.Click += new System.EventHandler(this.btnQuitar_Click);
            // 
            // treeDisponibles
            // 
            this.treeDisponibles.CheckBoxes = true;
            this.treeDisponibles.Location = new System.Drawing.Point(712, 4);
            this.treeDisponibles.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.treeDisponibles.Name = "treeDisponibles";
            this.treeDisponibles.Size = new System.Drawing.Size(283, 463);
            this.treeDisponibles.TabIndex = 1;
            this.treeDisponibles.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeDisponibles_AfterSelect);
            // 
            // treeUsuarios
            // 
            this.treeUsuarios.Location = new System.Drawing.Point(4, 4);
            this.treeUsuarios.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.treeUsuarios.Name = "treeUsuarios";
            this.treeUsuarios.Size = new System.Drawing.Size(283, 463);
            this.treeUsuarios.TabIndex = 0;
            this.treeUsuarios.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeUsuarios_AfterSelect_1);
            // 
            // tabIdiomas
            // 
            this.tabIdiomas.Controls.Add(this.rbtnIdiomaInactivo);
            this.tabIdiomas.Controls.Add(this.rbtnIdiomaActivo);
            this.tabIdiomas.Controls.Add(this.btnQuitarTraduccion);
            this.tabIdiomas.Controls.Add(this.btnAgregarTraduccion);
            this.tabIdiomas.Controls.Add(this.txtAgregarTraduccion);
            this.tabIdiomas.Controls.Add(this.btnQuitarIdioma);
            this.tabIdiomas.Controls.Add(this.btnAgregarIdioma);
            this.tabIdiomas.Controls.Add(this.txtAgregarIdioma);
            this.tabIdiomas.Controls.Add(this.dgvListarTraduccion);
            this.tabIdiomas.Controls.Add(this.dgvListarIdiomas);
            this.tabIdiomas.Controls.Add(this.lblTraduccion);
            this.tabIdiomas.Controls.Add(this.lblNuevoIdioma);
            this.tabIdiomas.Location = new System.Drawing.Point(4, 25);
            this.tabIdiomas.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabIdiomas.Name = "tabIdiomas";
            this.tabIdiomas.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabIdiomas.Size = new System.Drawing.Size(1016, 474);
            this.tabIdiomas.TabIndex = 5;
            this.tabIdiomas.Text = "Idiomas";
            this.tabIdiomas.UseVisualStyleBackColor = true;
            // 
            // rbtnIdiomaInactivo
            // 
            this.rbtnIdiomaInactivo.AutoSize = true;
            this.rbtnIdiomaInactivo.Location = new System.Drawing.Point(160, 75);
            this.rbtnIdiomaInactivo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rbtnIdiomaInactivo.Name = "rbtnIdiomaInactivo";
            this.rbtnIdiomaInactivo.Size = new System.Drawing.Size(74, 20);
            this.rbtnIdiomaInactivo.TabIndex = 12;
            this.rbtnIdiomaInactivo.TabStop = true;
            this.rbtnIdiomaInactivo.Text = "Inactivo";
            this.rbtnIdiomaInactivo.UseVisualStyleBackColor = true;
            this.rbtnIdiomaInactivo.CheckedChanged += new System.EventHandler(this.rbtnIdiomaInactivo_CheckedChanged);
            // 
            // rbtnIdiomaActivo
            // 
            this.rbtnIdiomaActivo.AutoSize = true;
            this.rbtnIdiomaActivo.Location = new System.Drawing.Point(28, 75);
            this.rbtnIdiomaActivo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rbtnIdiomaActivo.Name = "rbtnIdiomaActivo";
            this.rbtnIdiomaActivo.Size = new System.Drawing.Size(65, 20);
            this.rbtnIdiomaActivo.TabIndex = 11;
            this.rbtnIdiomaActivo.TabStop = true;
            this.rbtnIdiomaActivo.Text = "Activo";
            this.rbtnIdiomaActivo.UseVisualStyleBackColor = true;
            this.rbtnIdiomaActivo.CheckedChanged += new System.EventHandler(this.rbtnIdiomaActivo_CheckedChanged);
            // 
            // btnQuitarTraduccion
            // 
            this.btnQuitarTraduccion.Location = new System.Drawing.Point(819, 65);
            this.btnQuitarTraduccion.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnQuitarTraduccion.Name = "btnQuitarTraduccion";
            this.btnQuitarTraduccion.Size = new System.Drawing.Size(172, 28);
            this.btnQuitarTraduccion.TabIndex = 10;
            this.btnQuitarTraduccion.Text = "Quitar Traducción";
            this.btnQuitarTraduccion.UseVisualStyleBackColor = true;
            this.btnQuitarTraduccion.Click += new System.EventHandler(this.btnQuitarTraduccion_Click);
            // 
            // btnAgregarTraduccion
            // 
            this.btnAgregarTraduccion.Location = new System.Drawing.Point(819, 30);
            this.btnAgregarTraduccion.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAgregarTraduccion.Name = "btnAgregarTraduccion";
            this.btnAgregarTraduccion.Size = new System.Drawing.Size(172, 28);
            this.btnAgregarTraduccion.TabIndex = 9;
            this.btnAgregarTraduccion.Text = "Agregar Traducción";
            this.btnAgregarTraduccion.UseVisualStyleBackColor = true;
            this.btnAgregarTraduccion.Click += new System.EventHandler(this.btnAgregarTraduccion_Click);
            // 
            // txtAgregarTraduccion
            // 
            this.txtAgregarTraduccion.Location = new System.Drawing.Point(539, 37);
            this.txtAgregarTraduccion.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtAgregarTraduccion.Name = "txtAgregarTraduccion";
            this.txtAgregarTraduccion.Size = new System.Drawing.Size(252, 22);
            this.txtAgregarTraduccion.TabIndex = 8;
            // 
            // btnQuitarIdioma
            // 
            this.btnQuitarIdioma.Location = new System.Drawing.Point(391, 30);
            this.btnQuitarIdioma.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnQuitarIdioma.Name = "btnQuitarIdioma";
            this.btnQuitarIdioma.Size = new System.Drawing.Size(143, 28);
            this.btnQuitarIdioma.TabIndex = 7;
            this.btnQuitarIdioma.Text = "Quitar Idioma";
            this.btnQuitarIdioma.UseVisualStyleBackColor = true;
            this.btnQuitarIdioma.Click += new System.EventHandler(this.btnQuitarIdioma_Click);
            // 
            // btnAgregarIdioma
            // 
            this.btnAgregarIdioma.Location = new System.Drawing.Point(243, 30);
            this.btnAgregarIdioma.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAgregarIdioma.Name = "btnAgregarIdioma";
            this.btnAgregarIdioma.Size = new System.Drawing.Size(140, 28);
            this.btnAgregarIdioma.TabIndex = 6;
            this.btnAgregarIdioma.Text = "Agregar Idioma";
            this.btnAgregarIdioma.UseVisualStyleBackColor = true;
            this.btnAgregarIdioma.Click += new System.EventHandler(this.btnAgregarIdioma_Click);
            // 
            // txtAgregarIdioma
            // 
            this.txtAgregarIdioma.Location = new System.Drawing.Point(28, 32);
            this.txtAgregarIdioma.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtAgregarIdioma.Name = "txtAgregarIdioma";
            this.txtAgregarIdioma.Size = new System.Drawing.Size(205, 22);
            this.txtAgregarIdioma.TabIndex = 5;
            // 
            // dgvListarTraduccion
            // 
            this.dgvListarTraduccion.AllowUserToAddRows = false;
            this.dgvListarTraduccion.AllowUserToDeleteRows = false;
            this.dgvListarTraduccion.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvListarTraduccion.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvListarTraduccion.Location = new System.Drawing.Point(539, 101);
            this.dgvListarTraduccion.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvListarTraduccion.MultiSelect = false;
            this.dgvListarTraduccion.Name = "dgvListarTraduccion";
            this.dgvListarTraduccion.ReadOnly = true;
            this.dgvListarTraduccion.RowHeadersWidth = 51;
            this.dgvListarTraduccion.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvListarTraduccion.Size = new System.Drawing.Size(452, 352);
            this.dgvListarTraduccion.TabIndex = 4;
            this.dgvListarTraduccion.SelectionChanged += new System.EventHandler(this.dgvListarTraduccion_SelectionChanged);
            // 
            // dgvListarIdiomas
            // 
            this.dgvListarIdiomas.AllowUserToAddRows = false;
            this.dgvListarIdiomas.AllowUserToDeleteRows = false;
            this.dgvListarIdiomas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvListarIdiomas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvListarIdiomas.Location = new System.Drawing.Point(28, 101);
            this.dgvListarIdiomas.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvListarIdiomas.MultiSelect = false;
            this.dgvListarIdiomas.Name = "dgvListarIdiomas";
            this.dgvListarIdiomas.ReadOnly = true;
            this.dgvListarIdiomas.RowHeadersWidth = 51;
            this.dgvListarIdiomas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvListarIdiomas.Size = new System.Drawing.Size(443, 352);
            this.dgvListarIdiomas.TabIndex = 3;
            this.dgvListarIdiomas.SelectionChanged += new System.EventHandler(this.dgvListarIdiomas_SelectionChanged);
            // 
            // lblTraduccion
            // 
            this.lblTraduccion.AutoSize = true;
            this.lblTraduccion.Location = new System.Drawing.Point(535, 14);
            this.lblTraduccion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTraduccion.Name = "lblTraduccion";
            this.lblTraduccion.Size = new System.Drawing.Size(75, 16);
            this.lblTraduccion.TabIndex = 1;
            this.lblTraduccion.Text = "Traducción";
            // 
            // lblNuevoIdioma
            // 
            this.lblNuevoIdioma.AutoSize = true;
            this.lblNuevoIdioma.Location = new System.Drawing.Point(24, 14);
            this.lblNuevoIdioma.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNuevoIdioma.Name = "lblNuevoIdioma";
            this.lblNuevoIdioma.Size = new System.Drawing.Size(91, 16);
            this.lblNuevoIdioma.TabIndex = 0;
            this.lblNuevoIdioma.Text = "Nuevo idioma";
            // 
            // FormPrueba
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 554);
            this.Controls.Add(this.tabGeneral);
            this.Controls.Add(this.btnCerrarSesion);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FormPrueba";
            this.Text = "FormPrueba";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormPrueba_FormClosed);
            this.Load += new System.EventHandler(this.FormPrueba_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.tabGeneral.ResumeLayout(false);
            this.tabMenuPrincipal.ResumeLayout(false);
            this.groupBoxActividad.ResumeLayout(false);
            this.groupBoxActividad.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvActividadPublica)).EndInit();
            this.groupBoxEstado.ResumeLayout(false);
            this.groupBoxEstado.PerformLayout();
            this.tabUsuarios.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGestionUsuario)).EndInit();
            this.tabBitacora.ResumeLayout(false);
            this.tabBitacora.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBitacora)).EndInit();
            this.tabControlCambios.ResumeLayout(false);
            this.tabControlCambios.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCambios)).EndInit();
            this.tabPermisos.ResumeLayout(false);
            this.tabPermisos.PerformLayout();
            this.tabIdiomas.ResumeLayout(false);
            this.tabIdiomas.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListarTraduccion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListarIdiomas)).EndInit();
            this.ResumeLayout(false);

        }

        private void treeUsuarios_AfterSelect(object sender, TreeViewEventArgs e)
        {
            throw new NotImplementedException();
        }



        #endregion

        private System.Windows.Forms.Button btnCerrarSesion;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.TabControl tabGeneral;
        private System.Windows.Forms.TabPage tabUsuarios;
        private System.Windows.Forms.TabPage tabBitacora;
        private System.Windows.Forms.TabPage tabMenuPrincipal;
        private System.Windows.Forms.DataGridView dgvBitacora;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.TextBox txtTexto;
        private System.Windows.Forms.ComboBox cmbEvento;
        private System.Windows.Forms.DateTimePicker dtpDesde;
        private System.Windows.Forms.DateTimePicker dtpHasta;
        private System.Windows.Forms.Button btnFiltrarBitacora;
        private System.Windows.Forms.TextBox txtAuditoriaId;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Label lblUsuarioId;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.Label lblDetalle;
        private System.Windows.Forms.Label lblEvento;
        private System.Windows.Forms.Label lblAuditoriaId;
        private System.Windows.Forms.Button btnNuevoRegistro;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.DataGridView dgvGestionUsuario;
        private System.Windows.Forms.TabPage tabControlCambios;
        private System.Windows.Forms.DataGridView dgvCambios;
        private System.Windows.Forms.Label lblCambioFecha;
        private System.Windows.Forms.Label lblCambioEntidadId;
        private System.Windows.Forms.Label lblCambioEntidad;
        private System.Windows.Forms.Label lblCambioId;
        private System.Windows.Forms.Label lblCambioUsuarioId;
        private System.Windows.Forms.TextBox txtCambioId;
        private System.Windows.Forms.Button btnFiltrarCambios;
        private System.Windows.Forms.DateTimePicker dtpCambiosHasta;
        private System.Windows.Forms.DateTimePicker dtpCambiosDesde;
        private System.Windows.Forms.TextBox txtCambioEntidadId;
        private System.Windows.Forms.TextBox txtCambioUsuarioId;
        private System.Windows.Forms.Button btnLimpiarCambios;
        private System.Windows.Forms.TextBox txtCambioEntidad;
        private System.Windows.Forms.Label lblCambioCampo;
        private System.Windows.Forms.TextBox txtCambioCampo;
        private System.Windows.Forms.ComboBox cmbIdiomas;
        private System.Windows.Forms.TabPage tabPermisos;
        private System.Windows.Forms.TreeView treeUsuarios;
        private System.Windows.Forms.Label lblUsuarioSel;
        private System.Windows.Forms.Button btnAsignar;
        private System.Windows.Forms.Button btnQuitar;
        private System.Windows.Forms.TreeView treeDisponibles;
        private System.Windows.Forms.Button btnCrearBackup;
        private Button btnEliminar;
        private Button button1;
        private System.Windows.Forms.Label lblNuevoRol;
        private System.Windows.Forms.TextBox txtNuevoRol;
        private System.Windows.Forms.Button btnCrearRol;
        private System.Windows.Forms.TabPage tabIdiomas;
        private System.Windows.Forms.RadioButton rbtnIdiomaInactivo;
        private System.Windows.Forms.RadioButton rbtnIdiomaActivo;
        private System.Windows.Forms.Button btnQuitarTraduccion;
        private System.Windows.Forms.Button btnAgregarTraduccion;
        private System.Windows.Forms.TextBox txtAgregarTraduccion;
        private System.Windows.Forms.Button btnQuitarIdioma;
        private System.Windows.Forms.Button btnAgregarIdioma;
        private System.Windows.Forms.TextBox txtAgregarIdioma;
        private System.Windows.Forms.DataGridView dgvListarTraduccion;
        private System.Windows.Forms.DataGridView dgvListarIdiomas;
        private System.Windows.Forms.Label lblTraduccion;
        private System.Windows.Forms.Label lblNuevoIdioma;
        private System.Windows.Forms.GroupBox groupBoxEstado;
        private System.Windows.Forms.Button btnActualizarEstado;
        private System.Windows.Forms.Label lblEstadoDb;
        private System.Windows.Forms.Label lblHoraServidor;
        private System.Windows.Forms.Label lblHoraLocal;
        private System.Windows.Forms.Label lblIdiomaActual;
        private System.Windows.Forms.Label lblVersionApp;
        private System.Windows.Forms.Button btnSobreProyecto;
        private System.Windows.Forms.GroupBox groupBoxActividad;
        private System.Windows.Forms.Label lblActividadVacia;
        private System.Windows.Forms.DataGridView dgvActividadPublica;
        private System.Windows.Forms.ComboBox cmbFiltroActividad;
        private System.Windows.Forms.Label lblFiltroActividad;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFechaActividad;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCategoriaActividad;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDescripcionActividad;
    }
}