namespace GEOWALL_E
{
    class Punto: Expresion, ILugarGeometrico
    {
       
        public Punto() // agregado por Abraham
        {
            Random rd = new Random();
            double componete_x = rd.Next(300, 900);
            double componente_y = rd.Next(200, 600);

            Token x = new Token(Tipo_De_Token.Numero, 0, componete_x.ToString(), componete_x);
            Token y = new Token(Tipo_De_Token.Numero, 0, componente_y.ToString(), componente_y);

            Componente_x = new Literal(x, componete_x);
            Componente_y = new Literal(y, componente_y);
        }
        public Punto(string identificador) // constructor de punto para expression tipo "point p1"
        {
            Identificador = identificador;
            Random rd = new Random();

            double componete_x = rd.Next(300, 900);
            double componente_y = rd.Next(200, 600);

            Token x = new Token( Tipo_De_Token.Numero, 0, componete_x.ToString(), componete_x );
            Token y = new Token(Tipo_De_Token.Numero, 0, componente_y.ToString(), componente_y );

            Componente_x = new Literal(x, componete_x);
            Componente_y = new Literal(y, componente_y);

        }
        public Punto(string identificador, Expresion componentex,Expresion componentey)// ctor para expressiones tipo "intersec" donde el punto no es aleatorio
        {
            Identificador = identificador;
            Componente_x=componentex;
            Componente_y=componentey;
        }
        public Punto(string identificador, double valor_x, double valor_y) 
        {
            Identificador = identificador;
            this.valor_x = valor_x;
            this.valor_y = valor_y;
        }

        public Punto(double valorx, double valory)
        {
            valor_x = valorx;
            valor_y = valory;
        }
        public override Tipo_De_Token Tipo => Tipo_De_Token.point_Expresion;
        public string Identificador { get; } // revisar nombres en secuencias
        public Expresion Componente_x { get; }
        public Expresion Componente_y { get; }

        public double valor_x { get; }
        public double valor_y { get; }

        public void Draw() 
        {
            
        }
    }
    public interface ILugarGeometrico 
    { 
        void Draw() 
        {
            
        }
    }
}
