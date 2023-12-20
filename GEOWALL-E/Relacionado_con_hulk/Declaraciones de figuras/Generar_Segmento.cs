using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEOWALL_E.Relacionado_con_hulk.Declaraciones_de_figuras
{
    internal class Generar_Segmento : Expresion
    {
        public override Tipo_De_Token Tipo => Tipo_De_Token.generacion_segmentos_Expresion;

        public string Identificador { get; }
        public Expresion P1 { get; }
        public Expresion P2 { get; }

        public Generar_Segmento(string identificador)
        {
            Identificador = identificador;
        }
        public Generar_Segmento( Expresion p1, Expresion p2)
        {
            P1 = p1;
            P2 = p2;
        }
    }
}
