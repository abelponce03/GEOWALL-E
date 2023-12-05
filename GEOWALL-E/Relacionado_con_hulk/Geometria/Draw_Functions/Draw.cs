using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEOWALL_E.Relacionado_con_hulk.Geometria.Draw_Functions
{
    internal class Dibujar : Expresion, ILugarGeometrico
    {
        public Expresion _Expresion { get; }
        public Dibujar( Expresion expresion)
        {
            _Expresion = expresion;
        }
        public override Tipo_De_Token Tipo => Tipo_De_Token.draw_Expresion;
    }
}
