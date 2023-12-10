using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEOWALL_E.Relacionado_con_hulk.Geometria
{
    internal class Segment_Sequence : Expresion
    {
        public override Tipo_De_Token Tipo => Tipo_De_Token.segment_sequence_Expresion;

        public string Identificador { get; }

        public Secuencias_Evaluada _Secuencias_Evaluada { get; set; }

        public Segment_Sequence(string identificador)
        {
            Identificador = identificador;

            Random _random = new Random();
            int tope = _random.Next(0, 500);

            Secuencias_Evaluada _secuencia = new Secuencias_Evaluada();
            for (int i = 0; i < tope; i++)
            {
                Punto p1 = new Punto();
                Punto p2 = new Punto();
                Segment _segment = new Segment(p1, p2);
                _secuencia.Add(_segment);
            }
            _Secuencias_Evaluada = _secuencia;
        }
    }
}
