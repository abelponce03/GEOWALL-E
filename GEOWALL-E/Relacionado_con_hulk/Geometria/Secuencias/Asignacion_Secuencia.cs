using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEOWALL_E.Relacionado_con_hulk.Geometria
{
    class Asignacion_Secuencia : Expresion
    {
        public List<string> Identificadores { get; }

        public string Identificador_resto_de_secuencia { get; }

        public Expresion _Secuencia { get; }

        public override Tipo_De_Token Tipo => Tipo_De_Token.asignacion_secuencia_Expresion;

        public Asignacion_Secuencia(List<string> identificadores, string identificador_resto_de_secuencia, Expresion _secuencia)
        {
            Identificadores = identificadores;
            Identificador_resto_de_secuencia = identificador_resto_de_secuencia;
            _Secuencia = _secuencia;
        }



    }
}
