using GEOWALL_E.Relacionado_con_hulk.Geometria.Secuencias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEOWALL_E.Relacionado_con_hulk.Geometria
{
    internal class Point_Sequence : Sequence
    {
        public override Tipo_De_Token Tipo => Tipo_De_Token.point_sequence_Expresion;
        
        public string Identificador { get; }

        public Secuencias<object> _Secuencias_Evaluada { get; set; }

        public Point_Sequence(string identificador)
        {
            Identificador = identificador;

            Random _random = new Random();
            int tope = _random.Next(0, 4000);

            Secuencias<object> _secuencia = new Secuencias<object>();
            for(int i = 0; i < tope; i++)
            {
                _secuencia.Add(new Punto());
            }

            _Secuencias_Evaluada = _secuencia;
        }
    }
}
