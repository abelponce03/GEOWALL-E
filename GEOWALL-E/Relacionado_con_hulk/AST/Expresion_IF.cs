namespace GEOWALL_E
{
    sealed class IF : Expresion
    {
        public IF(Expresion condicion, Expresion expresion, Expresion _else)
        {
            Condicion = condicion;
            _expresion = expresion;
            _Else = _else;
        }
        public override Tipo_De_Token Tipo => Tipo_De_Token.if_Expresion;
        public Token Keyword { get; }
        public Token _parentesis_abierto { get; }
        public Expresion Condicion { get; }
        public Token _parentesis_cerrado { get; }
        public Expresion _expresion { get; }
        public Expresion _Else { get; }
    }
}
