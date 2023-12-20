using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEOWALL_E.Relacionado_con_hulk.Geometria.Draw_Functions
{
    internal class Dibujar : Expresion 
    {
        public Expresion _Expresion { get; }

        public string Etiqueta { get; }

        public Dibujar( Expresion expresion)
        {
            _Expresion = expresion;
        }

        public Dibujar(Expresion expresion, string etiqueta)
        {
            _Expresion = expresion;
            Etiqueta = etiqueta;
        }
        public override Tipo_De_Token Tipo => Tipo_De_Token.draw_Expresion;
    }
}
