using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEOWALL_E.Relacionado_con_hulk.Declaraciones_de_figuras
{
    internal class Generar_Circulo : Expresion
    {
        public override Tipo_De_Token Tipo => Tipo_De_Token.generacion_circulos_Expresion;

        public Expresion Centro { get; }

        public Expresion Radio { get; }

        public string Identificador { get; }

        public Generar_Circulo(string identificador)
        {
            Identificador = identificador;
        }
        public Generar_Circulo(Expresion centro, Expresion radio)
        {
            this.Centro = centro;
            this.Radio = radio;
        }
    }
}
