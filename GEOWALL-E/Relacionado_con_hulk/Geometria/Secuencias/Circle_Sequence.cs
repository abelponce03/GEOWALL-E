using GEOWALL_E.Relacionado_con_hulk.Geometria.Secuencias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEOWALL_E.Relacionado_con_hulk.Geometria
{
    internal class Circle_Sequence : Sequence
    {
        public override Tipo_De_Token Tipo => Tipo_De_Token.circle_sequence_Expresion;

        public string Identificador { get; }

        public Secuencias<object> _Secuencias_Evaluada { get; set; }

        public Circle_Sequence(string identificador)
        {
            Identificador = identificador;

            Random _random = new Random();
            int tope = _random.Next(0, 500);

            Secuencias<object> _secuencia = new Secuencias<object>();
            for (int i = 0; i < tope; i++)
            {
                Random rd = new Random();
                float radio = rd.Next(0, 100);

                Punto p1 = new Punto();
                Measure m = new Measure(radio);
                Circle _circle = new Circle(p1, m);
                _secuencia.Add(_circle);
            }
            _Secuencias_Evaluada = _secuencia;
        }
    }
}
