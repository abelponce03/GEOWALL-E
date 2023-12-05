using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEOWALL_E
{
    class Arc : Expresion, ILugarGeometrico  //representa una arco de una circunferencia
    {
        public Arc(Circle circle, Punto p1, Punto p2)
        {
            Circle = circle;
            InicialRay = new Ray(circle.Centro, p1);
            FinalRay = new Ray(circle.Centro, p2);
            P1 = p1;
            P2 = p2;
            Identificador = "Arc_" + p1.Identificador + p2.Identificador; // identificador por defecto
        }
        public override Tipo_De_Token Tipo => Tipo_De_Token.arc_Expresion;
        public string Identificador { get; set; }
        public Circle Circle { get; }
        public Ray InicialRay { get; }
        public Ray FinalRay { get; }
        public Punto P1 { get; }
        public Punto P2 { get; }

        public void Draw() { }
    }
}
