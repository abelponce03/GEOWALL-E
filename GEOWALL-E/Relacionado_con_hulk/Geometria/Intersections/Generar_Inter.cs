using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEOWALL_E.Relacionado_con_hulk.Geometria.Intersections
{
    internal class Generar_Inter : Expresion
    {
        public override Tipo_De_Token Tipo => Tipo_De_Token.intersect_Expresion;

        public Expresion Expresion_1 { get; }

        public Expresion Expresion_2 { get; }

        public Generar_Inter(Expresion expresion_1, Expresion expresion_2) 
        {
            Expresion_1 = expresion_1;
            Expresion_2 = expresion_2;
        }  
    }
}
