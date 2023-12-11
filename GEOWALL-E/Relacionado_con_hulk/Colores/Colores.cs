using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEOWALL_E.Relacionado_con_hulk.Tipos
{
    internal class Colores
    {
        public static Color VerColor(string texto)
        {
            switch (texto)
            {
                case "blue":
                    return Color.Blue;
                case "red":
                    return Color.Red; ;
                case "yellow":
                    return Color.Yellow;
                case "green":
                    return Color.Green;
                case "cyan":
                    return Color.Cyan;
                case "magenta":
                    return Color.Magenta;
                case "white":
                    return Color.White;
                case "gray":
                    return Color.Gray;
                case "black":
                    return Color.Black;
                default:
                    {
                        throw new Exception($"! SYNTAX ERROR : Unexpected color <{texto}> ");
                    }
            }
        }
    }
}
