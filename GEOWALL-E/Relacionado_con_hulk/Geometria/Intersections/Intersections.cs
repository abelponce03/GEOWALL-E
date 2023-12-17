using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEOWALL_E.Relacionado_con_hulk.Geometria.Intersections
{
    class Intersections
    {
        #region PuntoEnFiguras
        static Secuencias<object> PointInFigure(Punto punto, ILugarGeometrico figura)
        {
            switch (figura)
            {
                case Punto a:
                    return PuntoIntPunto(punto, a);
                case Line a:
                    return PuntoIntLine(punto, a);
                case Segment a:
                    return PuntoIntSegment(punto, a);
                case Arc a:
                    return PuntoIntArc(punto, a);
                case Circle a:
                    return PuntoIntCircle(punto, a);
                case Ray a:
                    return PuntoIntRay(punto, a);
                default: return new Secuencias<object>();
            }

        }
        static Secuencias<object> PuntoIntPunto(Punto p1, Punto p2)
        {
            if (p1.valor_x == p2.valor_x && p1.valor_y == p2.valor_y)
            {
                var x = new Secuencias<object>();
                x.Add(p1);
                return x;//secuencia con un punto
            }
            else
            {
                return new Secuencias<object>();//secuencia vacia
            }
        }
        public static Secuencias<object> PuntoIntLine(Punto p1, Line line)
        {
            Func<double, double> recta = EcuacionRecta(line);
            if (PointInLine(p1, recta))
            {
                var x = new Secuencias<object>();
                x.Add(p1);
                return x;
            }
            else return new Secuencias<object>();
        }
        static bool PointInLine(Punto p1, Func<double, double> recta)
        {
            var Y = recta(p1.valor_x); // evalua en la recta
            if (Y == p1.valor_y) return true;//aqui puede que halla que agragarle un casteo a int o utilzar double.epsilon//OJO
            return false;
        }
        static Func<double, double> EcuacionRecta(Line line)
        {
            Punto P1 = (Punto)line.P1;//REVISA ESTOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO
            Punto P2 = (Punto)line.P2;
            // Calcular la pendiente y el término independiente
            double pendiente = (P2.valor_y - P1.valor_y) / (P2.valor_x - P1.valor_x);
            double terminoIndependiente = P1.valor_y - pendiente * P1.valor_x;

            // Devolver la función de la recta
            return x => pendiente * x + terminoIndependiente;
        } // devuelve la ecuacion de una recta a partir de dos puntos
        static Secuencias<object> PuntoIntSegment(Punto p1, Segment segment)
        {
            Func<double, double> recta = EcuacionRecta(new Line(segment.P1, segment.P2));
            if (PointInLine(p1, recta))
            {
                Punto P1 = (Punto)segment.P1;//REVISA ESTOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO
                Punto P2 = (Punto)segment.P2;
                if (P1.valor_x <= P2.valor_x)
                {
                    if (p1.valor_x >= P1.valor_x && p1.valor_x <= P2.valor_x)
                    {
                        var x = new Secuencias<object>();
                        x.Add(p1);
                        return x;
                    }
                    else
                    {
                        return new Secuencias<object>();//secuencia vacia
                    }
                }
                else
                {
                    if (p1.valor_x >= P2.valor_x && p1.valor_x <= P1.valor_x)
                    {
                        var x = new Secuencias<object>();
                        x.Add(p1);
                        return x;
                    }
                    else
                    {
                        return new Secuencias<object>();//secuencia vacia
                    }
                }

            }
            else
            {
                return new Secuencias<object>();//secuencia vacia
            }
        }
        static Secuencias<object> PuntoIntArc(Punto p1, Arc arc)//falta
        {
            return new Secuencias<object>();
        }
        static Secuencias<object> PuntoIntCircle(Punto p1, Circle circle)//falta
        {
            return new Secuencias<object>();
        }
        static Secuencias<object> PuntoIntRay(Punto p1, Ray ray)//falta
        {
            Func<double, double> recta = EcuacionRecta(new Line(ray.P1, ray.P2));
            if (PointInLine(p1, recta))
            {
                Punto P1 = (Punto)ray.P1;//REVISA ESTOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO
                Punto P2 = (Punto)ray.P2;
                //  if(P1.valor_x>)
                return new Secuencias<object>();
            }
            else return new Secuencias<object>();

        }
        #endregion
    }

}
