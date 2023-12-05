using System.Diagnostics.Contracts;
using System.Security.Cryptography.X509Certificates;

namespace GEOWALL_E
{
   sealed class Arbol
   {
        public Arbol( List<string> error, List<Expresion> rama)
        {
            Rama = rama;
             Errores = error;
        }
        public  List<string> Errores {get;}
        public List<Expresion> Rama {get;}
   
   }
}
