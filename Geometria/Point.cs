namespace GEOWALL_E
{
    class Points : Expresion
    {
        public Points(string identificador, object componente_x, object componente_y)
        {
            Identificador = identificador;
            Componente_x = componente_x;
            Componente_y = componente_y;
        }
        public override Tipo_De_Token Tipo => Tipo_De_Token.point_Expresion;
        public string Identificador { get; }
        public object Componente_x { get; }
        public object Componente_y { get; }
    }
}

