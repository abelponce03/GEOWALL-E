using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEOWALL_E
{
     class Line: Expresion, ILugarGeometrico  //representa una recta
    {
        public Line(Punto p1, Punto p2) 
        {
            P1 = p1;
            P2 = p2;
        }
        public Line(Expresion p1,Expresion p2)
        {
            P1 = p1;
            P2 = p2;
        }
        public Line (string identificador)
        {
            P1 = new Punto();
            P2 = new Punto();
            Identificador = identificador;
        }
        public Line( string identificador, Punto p1, Punto p2)
        {
            Identificador = identificador;
            P1 = p1;
            P2 = p2;
        }

        public override Tipo_De_Token Tipo => Tipo_De_Token.line_Expresion;
        public Expresion P1 { get; }
        public Expresion P2 { get; }
        public string Identificador { get; }

        public void Draw() 
        {
            Pen lapiz = new Pen(Color.Black, 4);
        }
    }
}
