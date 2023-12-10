using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEOWALL_E.Relacionado_con_hulk.AST
{
    internal class Asignacion : Expresion
    {
        public Asignacion(string identificador, Expresion _exprexion)
        {
            Identificador = identificador;
            _Expresion = _exprexion;
        }
        public override Tipo_De_Token Tipo => Tipo_De_Token.asignacion_Expresion;
        public Expresion _Expresion { get; }

        public string Identificador { get; }

    }
}
