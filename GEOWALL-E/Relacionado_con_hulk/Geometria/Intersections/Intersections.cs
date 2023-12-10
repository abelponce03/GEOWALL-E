using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEOWALL_E.Relacionado_con_hulk.Geometria.Intersections
{
    public class Intersections
    {
        #region PuntoEnFiguras
        Secuencia PointInFigure(Punto punto, ILugarGeometrico figura)
        {
            switch (figura)
            {
                case Punto a:
                        return PuntoIntPunto(punto,a);
                case Line a:
                        return PuntoIntLine(punto,a);
                case Segment a:
                        return PuntoIntSegment(punto,a);
                case Arc a:
                        return PuntoIntArc(punto,a);
                case Circle a:
                        return PuntoIntCircle(punto,a);
                case Ray a:
                        return PuntoIntRay(punto,a);
                default: return new Secuencia();
            }
            Secuencia PuntoIntPunto(Punto p1,Punto p2)
            {
                if (p1.valor_x == p2.valor_x && p1.valor_y == p2.valor_y)
                {
                    return new Secuencia();//secuencia con un punto
                }
                else
                {
                    return new Secuencia();//secuencia vacia
                }
            }
            Secuencia PuntoIntLine(Punto p1,Line line)
            {
                Func<double, double> recta = EcuacionRecta(line);
                var Y = recta(p1.valor_x); // evalua en la recta
                if (Y == p1.valor_y)//aqui puede que halla que agragarle un casteo a int o utilzar double.epsilon//OJO
                {
                    return new Secuencia();//secuencia con un punto p1
                }
                else
                {
                    return new Secuencia();//secuencia vacia
                }
            }
            Func<double,double> EcuacionRecta(Line line)
            {
                // Calcular la pendiente y el término independiente
                double pendiente = (line.P2.valor_y - line.P1.valor_y) / (line.P2.valor_x - line.P1.valor_x);
                double terminoIndependiente = line.P1.valor_y - pendiente * line.P1.valor_x;

                // Devolver la función de la recta
                return x => pendiente * x + terminoIndependiente;
            } // devuelve la ecuacion de una recta a partir de dos puntos
            Secuencia PuntoIntSegment(Punto p1,Segment segment)
            {
                Func<double, double> recta = EcuacionRecta(new Line(segment.P1,segment.P2));
                var Y = recta(p1.valor_x); // evalua en la recta
                if (Y == p1.valor_y)//aqui puede que halla que agragarle un casteo a int o utilzar double.epsilon//OJO
                {
                    if (segment.P1.valor_x <= segment.P2.valor_x)
                    {
                        if(p1.valor_x >= segment.P1.valor_x && p1.valor_x <= segment.P2.valor_x)
                        {
                            return new Secuencia();//secuencia con un punto p1
                        }
                        else
                        {
                            return new Secuencia();//secuencia vacia
                        }
                    }
                    else
                    {
                        if (p1.valor_x >= segment.P2.valor_x && p1.valor_x <= segment.P1.valor_x)
                        {
                            return new Secuencia();//secuencia con un punto p1
                        }
                        else
                        {
                            return new Secuencia();//secuencia vacia
                        }
                    }
                    return new Secuencia();//secuencia con un punto p1
                }
                else
                {
                    return new Secuencia();//secuencia vacia
                }
            }
            Secuencia PuntoIntArc(Punto p1, Arc arc)//falta
            {
                return new Secuencia();
            }
            Secuencia PuntoIntCircle(Punto p1, Circle circle)//falta
            {
                return new Secuencia();
            }
            Secuencia PuntoIntRay(Punto p1, Ray ray)//falta
            {
                return new Secuencia();
            }

        }
        #endregion
    }
}
