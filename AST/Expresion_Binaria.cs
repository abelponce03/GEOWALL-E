namespace GEOWALL_E
{
    sealed class Expresion_Binaria : Expresion
    {
        public override Tipo_De_Token Tipo => Tipo_De_Token.Expresion_Binaria;
        public Expresion Left { get; }
        public Token Operador { get; }
        public Expresion Right { get; }
        public Expresion_Binaria(Expresion left, Token operador, Expresion right)
        {
            Left = left;
            Operador = operador;
            Right = right;
        }
    }
}