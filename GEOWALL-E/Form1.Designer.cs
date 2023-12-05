namespace GEOWALL_E
{
    partial class GEOWALL_E
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GEOWALL_E));
            PANEL_COMANDOS = new TextBox();
            PANEL_DIBUJO = new PictureBox();
            IMAGEN_WALL_E_NOMBRE = new Label();
            WALLE_E_NOMBRE = new Label();
            IMAGEN_WALLE = new Label();
            DRAW = new Boton_personalizado();
            IMPORT = new Boton_personalizado();
            LIMPIAR = new Boton_personalizado();
            CLOSE_BTN = new Boton_personalizado();
            MIN_BTN = new Boton_personalizado();
            BARRA_SUPERIOR = new Panel();
            MAX_BTN = new Boton_personalizado();
            ((System.ComponentModel.ISupportInitialize)PANEL_DIBUJO).BeginInit();
            BARRA_SUPERIOR.SuspendLayout();
            SuspendLayout();
            // 
            // PANEL_COMANDOS
            // 
            PANEL_COMANDOS.BackColor = SystemColors.HighlightText;
            PANEL_COMANDOS.BorderStyle = BorderStyle.FixedSingle;
            PANEL_COMANDOS.Cursor = Cursors.IBeam;
            PANEL_COMANDOS.Location = new Point(99, 159);
            PANEL_COMANDOS.Multiline = true;
            PANEL_COMANDOS.Name = "PANEL_COMANDOS";
            PANEL_COMANDOS.Size = new Size(426, 521);
            PANEL_COMANDOS.TabIndex = 2;
            PANEL_COMANDOS.TextChanged += PANEL_COMANDOS_TextChanged;
            // 
            // PANEL_DIBUJO
            // 
            PANEL_DIBUJO.BackColor = SystemColors.ButtonHighlight;
            PANEL_DIBUJO.BorderStyle = BorderStyle.FixedSingle;
            PANEL_DIBUJO.Location = new Point(580, 101);
            PANEL_DIBUJO.Name = "PANEL_DIBUJO";
            PANEL_DIBUJO.Size = new Size(1290, 883);
            PANEL_DIBUJO.TabIndex = 3;
            PANEL_DIBUJO.TabStop = false;
            PANEL_DIBUJO.Click += PANEL_DIBUJO_Click;
            // 
            // IMAGEN_WALL_E_NOMBRE
            // 
            IMAGEN_WALL_E_NOMBRE.AutoSize = true;
            IMAGEN_WALL_E_NOMBRE.Image = (Image)resources.GetObject("IMAGEN_WALL_E_NOMBRE.Image");
            IMAGEN_WALL_E_NOMBRE.ImageAlign = ContentAlignment.TopCenter;
            IMAGEN_WALL_E_NOMBRE.Location = new Point(1017, 28);
            IMAGEN_WALL_E_NOMBRE.Name = "IMAGEN_WALL_E_NOMBRE";
            IMAGEN_WALL_E_NOMBRE.Size = new Size(0, 20);
            IMAGEN_WALL_E_NOMBRE.TabIndex = 6;
            // 
            // WALLE_E_NOMBRE
            // 
            WALLE_E_NOMBRE.BackColor = SystemColors.ButtonHighlight;
            WALLE_E_NOMBRE.ForeColor = SystemColors.ControlText;
            WALLE_E_NOMBRE.Image = Properties.Resources.walle_nombre;
            WALLE_E_NOMBRE.Location = new Point(83, 50);
            WALLE_E_NOMBRE.Name = "WALLE_E_NOMBRE";
            WALLE_E_NOMBRE.Size = new Size(466, 106);
            WALLE_E_NOMBRE.TabIndex = 7;
            // 
            // IMAGEN_WALLE
            // 
            IMAGEN_WALLE.Image = Properties.Resources.images;
            IMAGEN_WALLE.Location = new Point(122, 724);
            IMAGEN_WALLE.Name = "IMAGEN_WALLE";
            IMAGEN_WALLE.Size = new Size(192, 270);
            IMAGEN_WALLE.TabIndex = 8;
            IMAGEN_WALLE.Click += IMAGEN_WALLE_Click;
            // 
            // DRAW
            // 
            DRAW.BackColor = Color.Red;
            DRAW.BackgroundColor = Color.Red;
            DRAW.BorderColor = Color.PaleVioletRed;
            DRAW.BorderRadius = 80;
            DRAW.BorderSize = 0;
            DRAW.FlatAppearance.BorderSize = 0;
            DRAW.FlatStyle = FlatStyle.Flat;
            DRAW.ForeColor = Color.White;
            DRAW.Location = new Point(349, 734);
            DRAW.Name = "DRAW";
            DRAW.Size = new Size(163, 79);
            DRAW.TabIndex = 9;
            DRAW.Text = "DRAW";
            DRAW.TextColor = Color.White;
            DRAW.UseVisualStyleBackColor = false;
            DRAW.Click += DRAW_Click;
            // 
            // IMPORT
            // 
            IMPORT.BackColor = Color.Red;
            IMPORT.BackgroundColor = Color.Red;
            IMPORT.BorderColor = Color.PaleVioletRed;
            IMPORT.BorderRadius = 80;
            IMPORT.BorderSize = 0;
            IMPORT.FlatAppearance.BorderSize = 0;
            IMPORT.FlatStyle = FlatStyle.Flat;
            IMPORT.ForeColor = Color.White;
            IMPORT.Location = new Point(349, 819);
            IMPORT.Name = "IMPORT";
            IMPORT.Size = new Size(163, 79);
            IMPORT.TabIndex = 10;
            IMPORT.Text = "IMPORT";
            IMPORT.TextColor = Color.White;
            IMPORT.UseVisualStyleBackColor = false;
            IMPORT.Click += IMPORT_Click;
            // 
            // LIMPIAR
            // 
            LIMPIAR.BackColor = Color.Red;
            LIMPIAR.BackgroundColor = Color.Red;
            LIMPIAR.BorderColor = Color.PaleVioletRed;
            LIMPIAR.BorderRadius = 80;
            LIMPIAR.BorderSize = 0;
            LIMPIAR.FlatAppearance.BorderSize = 0;
            LIMPIAR.FlatStyle = FlatStyle.Flat;
            LIMPIAR.ForeColor = Color.White;
            LIMPIAR.Location = new Point(349, 905);
            LIMPIAR.Name = "LIMPIAR";
            LIMPIAR.Size = new Size(163, 79);
            LIMPIAR.TabIndex = 11;
            LIMPIAR.Text = "CLEAN";
            LIMPIAR.TextColor = Color.White;
            LIMPIAR.UseVisualStyleBackColor = false;
            LIMPIAR.Click += LIMPIAR_Click;
            // 
            // CLOSE_BTN
            // 
            CLOSE_BTN.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            CLOSE_BTN.BackColor = Color.Red;
            CLOSE_BTN.BackgroundColor = Color.Red;
            CLOSE_BTN.BorderColor = Color.White;
            CLOSE_BTN.BorderRadius = 45;
            CLOSE_BTN.BorderSize = 0;
            CLOSE_BTN.FlatAppearance.BorderSize = 0;
            CLOSE_BTN.FlatStyle = FlatStyle.Flat;
            CLOSE_BTN.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            CLOSE_BTN.ForeColor = Color.White;
            CLOSE_BTN.Location = new Point(1796, 2);
            CLOSE_BTN.Name = "CLOSE_BTN";
            CLOSE_BTN.Padding = new Padding(2, 0, 0, 0);
            CLOSE_BTN.Size = new Size(47, 42);
            CLOSE_BTN.TabIndex = 12;
            CLOSE_BTN.Text = "X";
            CLOSE_BTN.TextColor = Color.White;
            CLOSE_BTN.UseVisualStyleBackColor = false;
            CLOSE_BTN.Click += CLOSE_BTN_Click;
            // 
            // MIN_BTN
            // 
            MIN_BTN.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            MIN_BTN.BackColor = Color.Red;
            MIN_BTN.BackgroundColor = Color.Red;
            MIN_BTN.BorderColor = Color.White;
            MIN_BTN.BorderRadius = 45;
            MIN_BTN.BorderSize = 0;
            MIN_BTN.FlatAppearance.BorderSize = 0;
            MIN_BTN.FlatStyle = FlatStyle.Flat;
            MIN_BTN.Font = new Font("Arial Rounded MT Bold", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            MIN_BTN.ForeColor = Color.White;
            MIN_BTN.Location = new Point(1690, 2);
            MIN_BTN.Name = "MIN_BTN";
            MIN_BTN.Padding = new Padding(2, 0, 0, 14);
            MIN_BTN.Size = new Size(47, 42);
            MIN_BTN.TabIndex = 13;
            MIN_BTN.Text = "__";
            MIN_BTN.TextColor = Color.White;
            MIN_BTN.UseVisualStyleBackColor = false;
            MIN_BTN.Click += MIN_BTN_Click;
            // 
            // BARRA_SUPERIOR
            // 
            BARRA_SUPERIOR.BackColor = Color.FromArgb(64, 64, 64);
            BARRA_SUPERIOR.Controls.Add(MAX_BTN);
            BARRA_SUPERIOR.Controls.Add(MIN_BTN);
            BARRA_SUPERIOR.Controls.Add(CLOSE_BTN);
            BARRA_SUPERIOR.Dock = DockStyle.Top;
            BARRA_SUPERIOR.Location = new Point(0, 0);
            BARRA_SUPERIOR.Name = "BARRA_SUPERIOR";
            BARRA_SUPERIOR.Size = new Size(1882, 47);
            BARRA_SUPERIOR.TabIndex = 14;
            // 
            // MAX_BTN
            // 
            MAX_BTN.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            MAX_BTN.BackColor = Color.Red;
            MAX_BTN.BackgroundColor = Color.Red;
            MAX_BTN.BorderColor = Color.White;
            MAX_BTN.BorderRadius = 45;
            MAX_BTN.BorderSize = 0;
            MAX_BTN.FlatAppearance.BorderSize = 0;
            MAX_BTN.FlatStyle = FlatStyle.Flat;
            MAX_BTN.Font = new Font("Segoe UI Symbol", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            MAX_BTN.ForeColor = Color.White;
            MAX_BTN.Location = new Point(1743, 2);
            MAX_BTN.Name = "MAX_BTN";
            MAX_BTN.Padding = new Padding(2, 0, 0, 0);
            MAX_BTN.Size = new Size(47, 42);
            MAX_BTN.TabIndex = 15;
            MAX_BTN.Text = "O";
            MAX_BTN.TextColor = Color.White;
            MAX_BTN.UseVisualStyleBackColor = false;
            MAX_BTN.Click += MAX_BTN_Click_1;
            // 
            // GEOWALL_E
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonHighlight;
            ClientSize = new Size(1882, 953);
            Controls.Add(BARRA_SUPERIOR);
            Controls.Add(LIMPIAR);
            Controls.Add(IMPORT);
            Controls.Add(DRAW);
            Controls.Add(IMAGEN_WALLE);
            Controls.Add(WALLE_E_NOMBRE);
            Controls.Add(IMAGEN_WALL_E_NOMBRE);
            Controls.Add(PANEL_DIBUJO);
            Controls.Add(PANEL_COMANDOS);
            ForeColor = SystemColors.ControlLightLight;
            Name = "GEOWALL_E";
            Text = "GEOWALL-E";
            WindowState = FormWindowState.Maximized;
            Load += GEOWALL_E_Load;
            MouseDown += GEOWALL_E_MouseDown;
            ((System.ComponentModel.ISupportInitialize)PANEL_DIBUJO).EndInit();
            BARRA_SUPERIOR.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TextBox PANEL_COMANDOS;
        private PictureBox PANEL_DIBUJO;
        private Label IMAGEN_WALL_E_NOMBRE;
        private Label WALLE_E_NOMBRE;
        private Label IMAGEN_WALLE;
        private Boton_personalizado DRAW;
        private Boton_personalizado IMPORT;
        private Boton_personalizado LIMPIAR;
        private Boton_personalizado CLOSE_BTN;
        private Boton_personalizado MIN_BTN;
        private Panel BARRA_SUPERIOR;
        private Boton_personalizado MAX_BTN;
    }
}