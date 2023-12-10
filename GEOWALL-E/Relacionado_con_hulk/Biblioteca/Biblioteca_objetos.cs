namespace GEOWALL_E
{
    class Biblioteca
    {
        public static Dictionary<string, Declaracion_Funcion> Functions = new Dictionary<string, Declaracion_Funcion>();

        public static Dictionary<string, object> Variables = new Dictionary<string, object>();

        public static Stack<Dictionary<string, object>> Pila = new Stack<Dictionary<string, object>>();

        public static void Limpiar()
        {
            Variables.Clear();
            Functions.Clear();
        }
    }
}
