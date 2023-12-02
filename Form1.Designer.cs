namespace GEOWALL_E
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            BTN_ACCION = new Button();
            PANEL_COMANDOS = new TextBox();
            SuspendLayout();
            // 
            // BTN_ACCION
            // 
            BTN_ACCION.Location = new Point(671, 384);
            BTN_ACCION.Name = "BTN_ACCION";
            BTN_ACCION.Size = new Size(115, 50);
            BTN_ACCION.TabIndex = 0;
            BTN_ACCION.Text = "Evaluar";
            BTN_ACCION.UseVisualStyleBackColor = true;
            BTN_ACCION.Click += BTN_ACCION_Click;
            // 
            // PANEL_COMANDOS
            // 
            PANEL_COMANDOS.BackColor = SystemColors.HighlightText;
            PANEL_COMANDOS.Cursor = Cursors.IBeam;
            PANEL_COMANDOS.Dock = DockStyle.Left;
            PANEL_COMANDOS.Location = new Point(0, 0);
            PANEL_COMANDOS.Multiline = true;
            PANEL_COMANDOS.Name = "PANEL_COMANDOS";
            PANEL_COMANDOS.Size = new Size(224, 450);
            PANEL_COMANDOS.TabIndex = 2;
            PANEL_COMANDOS.TextChanged += PANEL_COMANDOS_TextChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(PANEL_COMANDOS);
            Controls.Add(BTN_ACCION);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button BTN_ACCION;
        private TextBox PANEL_COMANDOS;
    }
}