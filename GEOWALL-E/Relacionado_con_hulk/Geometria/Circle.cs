using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEOWALL_E
{
    class Circle: Expresion, ILugarGeometrico
    {
        public Circle(Punto centro, Measure radio)
        {
            Centro = centro;
            Radio = radio;
        }
        public Circle(Expresion centro, Expresion radio)
        {
            Centro = centro;
            Radio = radio;
        }
        public Circle (string identificador)
        {
            Identificador = identificador;
            Random rd = new Random();
            double radio = rd.Next(0, 100);

            Punto _punto = new Punto();

            Measure _measure = new Measure(radio);

            Centro = _punto;
            Radio  = _measure;
        }
        public override Tipo_De_Token Tipo => Tipo_De_Token.circle_Expresion;
        public string Identificador { get; set; }
        public Expresion Centro { get; }
        public Expresion Radio { get; }

        public void Draw()
        {
           

        }
    }
}
