namespace GEOWALL_E
{
    class Biblioteca
    {
        public static Dictionary<string, Declaracion_Funcion> Functions = new Dictionary<string, Declaracion_Funcion>();

        public static Dictionary<string, object> Variables = new Dictionary<string, object>();

        public static Stack<Dictionary<string, object>> Pila = new Stack<Dictionary<string, object>>();

        //Geometria-----------------------------------------------------------------------------------

        public static Dictionary<string, Punto> Puntos = new Dictionary<string, Punto>();

        public static Dictionary<string, Arc> Arcos = new Dictionary<string, Arc>();

        public static Dictionary<string, Line> Lineas = new Dictionary<string, Line>();

        public static Dictionary<string, Circle> Circulos = new Dictionary<string, Circle>();

        public static Dictionary<string, Ray> Rayos = new Dictionary<string, Ray>();

        public static Dictionary<string, Segment> Segmentos = new Dictionary<string, Segment>();

        public static void Limpiar()
        {
            Variables.Clear();
            Puntos.Clear();
            Arcos.Clear();
            Lineas.Clear();
            Circulos.Clear();
            Rayos.Clear();
            Segmentos.Clear();
        }
    }
}
