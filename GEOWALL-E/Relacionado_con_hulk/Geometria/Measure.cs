using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEOWALL_E
{
    class Measure : Expresion
    {
        public Measure(double valor) 
        {
            Valor = valor;
        }
        public Measure(Expresion p1, Expresion p2)
        {
            P1 = p1;
            P2 = p2;
        }
        public override Tipo_De_Token Tipo => Tipo_De_Token.measure_Expresion;
        public Expresion P1 { get; }
        public Expresion P2 { get; }
        public double Valor { get; }
    }
}
