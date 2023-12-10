using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEOWALL_E.Relacionado_con_hulk.Geometria
{
    internal class Count : Expresion
    {
        public override Tipo_De_Token Tipo => Tipo_De_Token.count_Expresion;

        public Expresion _Secuencia { get; }

        public Count (Expresion _secuencia) 
        {
            _Secuencia = _secuencia;
        }
    }
}
