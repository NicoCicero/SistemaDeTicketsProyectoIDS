namespace Proyecto_IS_Sistema_De_Tickets
{
    partial class FormSobreProyecto
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

        private void InitializeComponent()
        {
            this.lblDescripcion = new System.Windows.Forms.Label();
            this.btnVerModulos = new System.Windows.Forms.Button();
            this.btnCerrarSobre = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblDescripcion
            // 
            this.lblDescripcion.AutoSize = true;
            this.lblDescripcion.Location = new System.Drawing.Point(12, 9);
            this.lblDescripcion.MaximumSize = new System.Drawing.Size(360, 0);
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new System.Drawing.Size(343, 65);
            this.lblDescripcion.TabIndex = 0;
            this.lblDescripcion.Text = "Este sistema implementa un Sistema de Gestión de Tickets con arquitectura en 4 capas (UI, BL, DAL, BE). Incluye login seguro (hash PBKDF2 + bloqueo por intentos), bitácora de auditoría, control de cambios para rastrear modificaciones, verificación de integridad DVH/DVV, y múltiples idiomas con patrón Observer. El objetivo es aplicar principios OO (bajo acoplamiento, alta cohesión) y buenas prácticas de ingeniería de software.";
            // 
            // btnVerModulos
            // 
            this.btnVerModulos.Location = new System.Drawing.Point(15, 90);
            this.btnVerModulos.Name = "btnVerModulos";
            this.btnVerModulos.Size = new System.Drawing.Size(120, 30);
            this.btnVerModulos.TabIndex = 1;
            this.btnVerModulos.Text = "Ver módulos";
            this.btnVerModulos.UseVisualStyleBackColor = true;
            this.btnVerModulos.Click += new System.EventHandler(this.btnVerModulos_Click);
            // 
            // btnCerrarSobre
            // 
            this.btnCerrarSobre.Location = new System.Drawing.Point(245, 90);
            this.btnCerrarSobre.Name = "btnCerrarSobre";
            this.btnCerrarSobre.Size = new System.Drawing.Size(120, 30);
            this.btnCerrarSobre.TabIndex = 2;
            this.btnCerrarSobre.Text = "Cerrar";
            this.btnCerrarSobre.UseVisualStyleBackColor = true;
            this.btnCerrarSobre.Click += new System.EventHandler(this.btnCerrarSobre_Click);
            // 
            // FormSobreProyecto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 141);
            this.Controls.Add(this.btnCerrarSobre);
            this.Controls.Add(this.btnVerModulos);
            this.Controls.Add(this.lblDescripcion);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSobreProyecto";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Sobre el proyecto";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblDescripcion;
        private System.Windows.Forms.Button btnVerModulos;
        private System.Windows.Forms.Button btnCerrarSobre;
    }
}
