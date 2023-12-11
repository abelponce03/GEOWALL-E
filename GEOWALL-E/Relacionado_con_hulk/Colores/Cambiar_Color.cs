using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEOWALL_E.Relacionado_con_hulk.Colores
{
    internal class Cambiar_Color : Expresion
    {
        public override Tipo_De_Token Tipo => Tipo_De_Token.color_Expresion;

        public Color _Color { get; }
        public Cambiar_Color(Color _color)
        {
            _Color = _color;
        }
    }
}
