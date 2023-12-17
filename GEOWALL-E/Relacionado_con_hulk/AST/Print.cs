using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEOWALL_E.Relacionado_con_hulk.AST
{
    internal class Print : Expresion
    {
        public override Tipo_De_Token Tipo => Tipo_De_Token.Print_Expresion;

        public Expresion _Expresion { get; }

        public Print(Expresion expresion)
        {
            _Expresion = expresion;
        }
    }
}
