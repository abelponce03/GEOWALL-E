using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEOWALL_E.Relacionado_con_hulk.Declaraciones_de_figuras
{
    internal class Generacion_Punto : Expresion
    {

        public string Identificador { get; }
        public Expresion Componente_x { get; }
        public Expresion Componente_y { get; }

        public override Tipo_De_Token Tipo => Tipo_De_Token.generacion_puntos_Expresion;

        public Generacion_Punto(Expresion componente_x, Expresion componente_y)
        {
            Componente_x = componente_x;
            Componente_y = componente_y;
        }
        public Generacion_Punto(string identificador)
        {
            Identificador = identificador;
        }
        public Generacion_Punto(string identificador, Expresion componente_x, Expresion componente_y)
        {
            Componente_x = componente_x;
            Componente_y = componente_y;
        }
    }
}
