
using System.Drawing.Text;
using System.Runtime.InteropServices;

namespace GEOWALL_E
{


    public partial class GEOWALL_E : Form
    {

        public static Graphics Papel { get; set; }

        public GEOWALL_E()
        {
            InitializeComponent();
            //quitar bordes
            this.Text = string.Empty;
            this.ControlBox = false;
            //this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
            Papel = PANEL_DIBUJO.CreateGraphics();
        }

        //permitir movimiento de la interfaz
        [DllImport("user32.dll", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void PANEL_COMANDOS_TextChanged(object sender, EventArgs e)
        {

        }

        private void PANEL_DIBUJO_Click(object sender, EventArgs e)
        {

        }

        private void DRAW_Click(object sender, EventArgs h)
        {
            Papel.Clear(BackColor); Papel.Clear(ForeColor);
            Biblioteca.Limpiar();
            string Entrada = PANEL_COMANDOS.Text;
            if (Entrada != string.Empty)
            {
                try
                {

                    var Parser = new Parser(Entrada);
                    var Arbol = Parser.Parse();

                    if (!Arbol.Errores.Any())
                    {
                        var e = new Evaluador(Arbol.Rama);
                        e.Evaluar();
                    }
                    else
                    {
                        MessageBox.Show(Arbol.Errores[0]);
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }

        }


        private void IMPORT_Click(object sender, EventArgs e)
        {

        }

        private void LIMPIAR_Click(object sender, EventArgs e)
        {
           
        }





        private void GEOWALL_E_Load(object sender, EventArgs e)
        {

        }

        private void IMAGEN_WALLE_Click(object sender, EventArgs e)
        {

        }


        //botones para maximizar, cerrar, y minimizar aplicacion

        private void CLOSE_BTN_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void MIN_BTN_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        //movimiento de la interfaz
        private void GEOWALL_E_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void MAX_BTN_Click_1(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
            }
        }
    }
}