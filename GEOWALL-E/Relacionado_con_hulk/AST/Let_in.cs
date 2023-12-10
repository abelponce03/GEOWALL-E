namespace GEOWALL_E
{
    sealed class Let_in : Expresion
    {
        public Let_in(List<Expresion> bloque, Expresion IN)
        {
            Bloque = bloque;
            _IN = IN;
        }
        public override Tipo_De_Token Tipo => Tipo_De_Token.let_in_Expresion;
        public List<Expresion> Bloque { get; }
        public Expresion _IN { get; }
    }
}


