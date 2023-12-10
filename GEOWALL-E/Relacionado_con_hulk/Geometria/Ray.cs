using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEOWALL_E
{
    class Ray : Expresion, ILugarGeometrico//representa una rayo
    {
        
        public Ray(Punto p1, Punto p2)
        {
            P1 = p1;
            P2 = p2;
        }
        public Ray(string identificador)    
        { 
            Identificador = identificador;
            P1 = new Punto();
            P2 = new Punto();
        }
        public Ray(Expresion p1, Expresion p2)
        {
            P1 = p1;
            P2 = p2;
        }
        public override Tipo_De_Token Tipo => Tipo_De_Token.ray_Expresion;
        public string Identificador { get; }
        public Expresion P1 { get; }
        public Expresion P2 { get; }

    }
}