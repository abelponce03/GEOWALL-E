using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEOWALL_E.Relacionado_con_hulk.Geometria.Secuencias
{
    internal class Randoms : Expresion
    {
        public override Tipo_De_Token Tipo => Tipo_De_Token.randoms_Expresion;

        public Secuencia_Infinita<double> Infinita = new Secuencia_Infinita<double>();

        public Randoms() 
        {
            Random random = new Random();
            Infinita.Add(random.NextDouble());
        }
    }
}
