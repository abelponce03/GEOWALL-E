using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEOWALL_E
{
    class Segment : Expresion  //representa un segmento
    {
        public Segment(Punto p1, Punto p2)
        {
            P1 = p1;
            P2 = p2;
        }
        public Segment(Expresion p1, Expresion p2)
        {
            P1 = p1;
            P2 = p2;
        }
        public Segment(string identificador)
        {
            Identificador = identificador;
           
            P1 = new Punto();
            P2 = new Punto();
        }
        public override Tipo_De_Token Tipo => Tipo_De_Token.segment_Expresion;
        public string Identificador { get; set; }
        public Expresion P1 { get; }
        public Expresion P2 { get; }

    }
}
