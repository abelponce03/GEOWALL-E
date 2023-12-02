
namespace GEOWALL_E
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void BTN_ACCION_Click(object sender, EventArgs h)
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("> ");
                string Entrada = PANEL_COMANDOS.Text;
                if (string.IsNullOrWhiteSpace(Entrada)) return;

                var Parser = new Parser(Entrada);
                var Arbol = Parser.Parse();

                if (!Arbol.Errores.Any())
                {
                    var e = new Evaluador(Arbol.Rama);
                    var resultado = e.Evaluar();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(resultado);
                }
                else
                {
                    foreach (var error in Arbol.Errores)
                    {
                        string[] mensaje = error.Split();
                        for (int i = 0; i < mensaje.Length; i++)
                        {
                            if (mensaje[i] == "SYNTAX")
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                break;
                            }
                            if (mensaje[i] == "SEMANTIC")
                            {
                                Console.ForegroundColor = ConsoleColor.Magenta;
                                break;
                            }
                            if (mensaje[i] == "LEXICAL")
                            {
                                Console.ForegroundColor = ConsoleColor.Blue;
                                break;
                            }
                            if (mensaje[i] == "FUNCTION")
                            {
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                break;
                            }
                        }
                        MessageBox.Show(error);
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                string[] mensaje = e.ToString().Split();
                for (int i = 0; i < mensaje.Length; i++)
                {
                    if (mensaje[i] == "OVERFLOW")
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        break;
                    }
                    if (mensaje[i] == "SYNTAX")
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        break;
                    }
                    if (mensaje[i] == "SEMANTIC")
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        break;
                    }
                    if (mensaje[i] == "LEXICAL")
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        break;
                    }
                    if (mensaje[i] == "FUNCTION")
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        break;
                    }
                }
                MessageBox.Show(e.Message);
            }
        }
        private void PANEL_COMANDOS_TextChanged(object sender, EventArgs e)
        {

        }
    }
}