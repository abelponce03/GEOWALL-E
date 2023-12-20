using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEOWALL_E.Relacionado_con_hulk.Declaraciones_de_figuras
{
    internal class Generar_Arco : Expresion

    {
        public override Tipo_De_Token Tipo => Tipo_De_Token.generacion_arcos_Expresion;

        public string Identificador { get;}

        public Expresion P1 { get;}
        public Expresion P2 { get;}
        public Expresion P3 { get;}
        public Expresion _Measure { get;}

        public Generar_Arco(string identificador) 
        {
            Identificador = identificador;
        }
        public Generar_Arco(Expresion p1, Expresion p2, Expresion p3, Expresion measure) 
        {
            P1 = p1;
            P2 = p2;
            P3 = p3;    
            _Measure = measure;
        }
    }
}
