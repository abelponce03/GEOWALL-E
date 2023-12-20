using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEOWALL_E
{
    class Arc : Expresion, ILugarGeometrico //representa una arco de una circunferencia
    {
        public Arc(string identificador) 
        {
            Random random = new Random();
            Identificador = identificador;
            P1 = new Punto();
            P2 = new Punto();
            P3 = new Punto();
            _Measure = new Measure(random.Next(0, 100));
        }
        public Arc(Punto p1, Punto p2, Punto p3, Measure measure)
        {
            P1 = p1;
            P2 = p2;
            P3 = p3;
            _Measure = measure;
        }
        public Arc(Expresion p1, Expresion p2, Expresion p3, Expresion measure)
        {
            P1 = p1;
            P2 = p2;
            P3 = p3;
            _Measure = measure;
        }

        public override Tipo_De_Token Tipo => Tipo_De_Token.arc_Expresion;
        public Expresion P1 { get; }
        public Expresion P2 { get; }
        public Expresion P3 { get; }
        public Expresion _Measure { get; }

        public string Identificador { get; }
    }
}
