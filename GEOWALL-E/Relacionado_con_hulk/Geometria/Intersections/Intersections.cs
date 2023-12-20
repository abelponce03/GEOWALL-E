using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEOWALL_E.Relacionado_con_hulk.Geometria.Intersections
{
    public static class Intersections 
    {
       public static Secuencias<object> IntersectionBetween(ILugarGeometrico fig1, ILugarGeometrico fig2)
        {
            switch (fig1)
            {
                case Punto a:
                    return PointInFigure(a, fig2);
                case Line a:
                    return LineInFigure(a, fig2);
                case Segment a:
                    return SegmentInFigure(a, fig2);
                case Arc a:
                    return ArcInFigure(a, fig2);
                case Circle a:
                    return CircleInFigure(a, fig2);
                case Ray a:
                    return RayInFigure(a, fig2);
                default: return new Secuencias<object>();
            }
        }

        #region PuntoEnFiguras //listo
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
        static Secuencias<object> PuntoIntLine(Punto p1, Line line)
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
            var res = new Secuencias<object>();
            if (_PuntoIntSegment(p1, segment))
            {
                res.Add(p1);
            }
            return res;
        }
        static bool _PuntoIntSegment(Punto p1, Segment segment)
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
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    if (p1.valor_x >= P2.valor_x && p1.valor_x <= P1.valor_x)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

            }
            else
            {
                return false;
            }
        }
        static Secuencias<object> PuntoIntArc(Punto p1, Arc arc)
        {
            if (_PuntoIntArc(p1, arc))
            {
                var x = new Secuencias<object>();
                x.Add(p1);
                return x;
            }
            else return new Secuencias<object>();//secuencia vacia
        }
        static bool _PuntoIntArc(Punto p1, Arc arc)
        {
            bool _PointInCircle = PointIntCircle(p1, new Circle(arc.P1, arc._Measure));
            Punto P1 = (Punto)arc.P1;
            Punto P2 = (Punto)arc.P2;
            Punto P3 = (Punto)arc.P3;
            double M1 = (P2.valor_y - P1.valor_y) / (P2.valor_x - P1.valor_x);
            double angulo_final = Math.Atan(M1) * 180 / Math.PI;

            double M2 = (P3.valor_y - P1.valor_y) / (P3.valor_x - P1.valor_x);
            double angulo_inicial = Math.Atan(M2) * 180 / Math.PI;

            double pendienteP = (p1.valor_y - P1.valor_y) / (p1.valor_x - P1.valor_x);
            double anguloPunto = Math.Atan(pendienteP) * 180 / Math.PI;

            bool PuntoEnAngulos = anguloPunto >= angulo_inicial && anguloPunto <= angulo_final;
            return (_PointInCircle && PuntoEnAngulos);
        }
        static Secuencias<object> PuntoIntCircle(Punto p1, Circle circle)
        {
            if (PointIntCircle(p1, circle))
            {
                var x = new Secuencias<object>();
                x.Add(p1);
                return x;
            }
            else return new Secuencias<object>();//secuencia vacia
        }
        static bool PointIntCircle(Punto p1, Circle circle)
        {
            Punto Centro = (Punto)circle.Centro;
            Measure radio = (Measure)circle.Radio;
            double DistanciaCentroPunto = DistanciaEPuntos(p1, Centro);
            if (DistanciaCentroPunto == radio.Valor) return true; // revisar precision con double.epsilon
            return false;
        }
        static double DistanciaEPuntos(Punto p1, Punto p2)
        {
            double distancia = Math.Sqrt((p1.valor_x - p2.valor_x) * (p1.valor_x - p2.valor_x) + (p1.valor_y - p2.valor_y) * (p1.valor_y - p2.valor_y));
            return distancia;
        }
        static Secuencias<object> PuntoIntRay(Punto p1, Ray ray)
        {
            var result = new Secuencias<object>();
            if (_PuntoIntRay(p1, ray))
            {
                result.Add(p1);
            }
            return result;
        }
        static bool _PuntoIntRay(Punto p1, Ray ray)
        {
            Func<double, double> recta = EcuacionRecta(new Line(ray.P1, ray.P2));
            if (PointInLine(p1, recta))
            {
                Punto P1 = (Punto)ray.P1;//REVISA ESTOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO
                Punto P2 = (Punto)ray.P2;
                if (P1.valor_x <= P2.valor_x)
                {
                    if (p1.valor_x >= P1.valor_x)
                    {
                        return true;
                    }
                    return false;
                }
                else
                {
                    if (p1.valor_x <= P1.valor_x)
                    {
                        return true;
                    }
                    return false;
                }

            }
            return false;
        }
        #endregion

        #region LineInFigure //listo
        static Secuencias<object> LineInFigure(Line line, ILugarGeometrico fig)
        {
            switch (fig)
            {
                case Punto a:
                    return PuntoIntLine(a, line);
                case Line a:
                    return LineInLine(line, a);
                case Segment a:
                    return LineIntSegment(line, a);
                case Arc a:
                    return LineIntArc(line, a);
                case Circle a:
                    return LineIntCircle(line, a);
                case Ray a:
                    return LineIntRay(line, a);
                default: return new Secuencias<object>();
            }
        }
        static Secuencias<object> LineInLine(Line line1, Line line2)
        {
            switch (LinesInterceptions(line1, line2))
            {
                case 1:
                    {
                        var x = new Secuencias<object>();
                        x.Add(IntersectionOfLines(line1, line2));
                        return x;
                    }
                case 0:
                    return new Secuencias<object>();
                case -1:
                    {
                        var x = new Secuencias<object>();
                        x.Add(new undefined());
                        return x;
                    }
                default: return new Secuencias<object>();
            }
        }
        static Punto IntersectionOfLines(Line line1, Line line2)
        {
            var recta1 = EcuacionRecta(line1);// y = mx+n
            var recta2 = EcuacionRecta(line2);// y = tx+k
            var n = recta1(0);
            var k = recta2(0);
            var m = (recta1(1) - n);
            var t = (recta2(1) - n);
            var x = (k - n) / (m - t);
            var y = recta1(x);
            return new Punto(x, y);
        }
        static int LinesInterceptions(Line line1, Line line2)
        {
            bool RectasCoincidentes(Line line1, Line line2)
            {
                if (MismaPendiente(line1, line2))
                {
                    Func<double, double> recta1 = EcuacionRecta(line1);
                    Func<double, double> recta2 = EcuacionRecta(line2);
                    return recta1(1) == recta2(1);
                }
                else return false;
            }
            bool MismaPendiente(Line line1, Line line2)
            {
                Punto P1 = (Punto)line1.P1;
                Punto P2 = (Punto)line1.P2;
                Punto P3 = (Punto)line2.P1;
                Punto P4 = (Punto)line2.P2;
                double M1 = (P2.valor_y - P1.valor_y) / (P2.valor_x - P1.valor_x);

                double M2 = (P3.valor_y - P4.valor_y) / (P3.valor_x - P4.valor_x);
                return M1 == M2;// revisar con math.epsilon
            }
            if (RectasCoincidentes(line1, line2)) return -1; // retorna -1 si la rectas son coincidentes,hay q devolver undefined
            if (MismaPendiente(line1, line2)) return 0; //retorna 0 si la rectas son paralelas, hay que devolver una secuencia vacia
            return 1; // retorna 1 , devuelve una secuencia con un punto
        }
        static Secuencias<object> LineIntSegment(Line line, Segment segment)
        {
            switch (LinesInterceptions(line, new Line(segment.P1, segment.P2)))
            {
                case 0:
                    return new Secuencias<object>();
                case -1:
                    {
                        var x = new Secuencias<object>();
                        x.Add(new undefined());
                        return x;
                    }
                case 1:
                    {
                        var PuntoInt = IntersectionOfLines(line, new Line(segment.P1, segment.P2));
                        return PuntoIntSegment(PuntoInt, segment);//si el punto de intercepcion esta en el segmento lo devuelve,si no devuelve vacio
                    }
                default: return new Secuencias<object>();
            }
        }
        static Secuencias<object> LineIntRay(Line line, Ray ray)
        {
            switch (LinesInterceptions(line, new Line(ray.P1, ray.P2)))
            {
                case 0:
                    return new Secuencias<object>();
                case -1:
                    {
                        var x = new Secuencias<object>();
                        x.Add(new undefined());
                        return x;
                    }
                case 1:
                    {
                        var PuntoInt = IntersectionOfLines(line, new Line(ray.P1, ray.P2));
                        return PuntoIntRay(PuntoInt, ray);//si el punto de intercepcion esta en el rayo lo devuelve,si no devuelve vacio
                    }
                default: return new Secuencias<object>();
            }
        }
        static Secuencias<object> LineIntCircle(Line line, Circle circle)
        {
            var recta = EcuacionRecta(line);
            // Coordenadas del centro de la circunferencia
            Punto Centro = (Punto)circle.Centro;
            double cx = Centro.valor_x;
            double cy = Centro.valor_y;
            // Radio de la circunferencia
            Measure radio = (Measure)circle.Radio;
            double r;
            if(radio.P1 is null && radio.P2 is null)
            {
                r = radio.Valor;
            }
            else r = DistanciaEPuntos((Punto)radio.P1, (Punto)radio.P2);
            // Coeficientes de la ecuación de la recta y = mx + b
            double b = recta(0);
            double m = (recta(1) - b);//ver
            // Calculamos los puntos de intersección
            double A = 1 + Math.Pow(m, 2);
            double B = 2 * (m * b - m * cy - cx);
            double C = Math.Pow(cx, 2) + Math.Pow(cy, 2) + Math.Pow(b, 2) - Math.Pow(r, 2) - 2 * b * cy;
            // Calculamos el discriminante
            double discriminante = Math.Pow(B, 2) - 4 * A * C;
            var res = new Secuencias<object>();
            if (discriminante < 0)
            {
                return res;
            }
            else if (discriminante == 0)
            {
                double x = -B / (2 * A);//hallar punto
                double y = m * x + b;
                Punto p = new Punto(x, y);
                res.Add(p);
                return res;
            }
            else
            {
                double x1 = (-B + Math.Sqrt(discriminante)) / (2 * A);
                double y1 = m * x1 + b;
                double x2 = (-B - Math.Sqrt(discriminante)) / (2 * A);//hallar puntos
                double y2 = m * x2 + b;
                Punto p = new Punto(x1, y1);
                res.Add(p);
                Punto q = new Punto(x2, y2);
                res.Add(p);
                return res;
            }
        }
        static Secuencias<object> LineIntArc(Line line, Arc arc)
        {
            var res = new Secuencias<object>();
            var x = LineIntCircle(line, new Circle(arc.P1, arc._Measure));
            for (int i = 0; i < x.Count; i++)
            {
                Punto p = (Punto)x[i];
                if (_PuntoIntArc(p, arc))
                {
                    res.Add(p);
                }
            }
            return res;
        }
        #endregion

        #region SegmentInFigure
        static Secuencias<object> SegmentInFigure(Segment segment, ILugarGeometrico fig)
        {
            switch (fig)
            {
                case Punto a:
                    return PuntoIntSegment(a, segment);
                case Line a:
                    return LineIntSegment(a, segment);
                case Segment a:
                    return SegmentIntSegment(a, segment);
                case Arc a:
                    return SegmentIntArc(segment, a);
                case Circle a:
                    return SegmentIntCircle(segment, a);
                case Ray a:
                    return SegmentIntRay(segment, a);
                default: return new Secuencias<object>();
            }
        }
        static Secuencias<object> SegmentIntSegment(Segment s1, Segment s2)//falta terminarlo
        {
            var res = new Secuencias<object>();
            var x = LineInLine(new Line(s1.P1, s1.P2), new Line(s2.P1, s2.P2));
            if (x[0] is undefined)//revisar en caso de cambiar la implemntacion de undefined
            {
                //bool PuntoCoincidentes = _PuntoIntSegment((Punto)s1.P1,s2)||_PuntoIntSegment((Punto)s1.P2 ,s2);
                //if (!PuntoCoincidentes)
                //{
                ////no hago nada
                //}
                return res;
            }
            else
            {
                for (int i = 0; i < x.Count; i++)
                {
                    Punto p = (Punto)x[i];
                    if (_PuntoIntSegment(p, s1) && _PuntoIntSegment(p, s2))
                    {
                        res.Add(p);
                    }
                }
            }
            return res;
        }
        static Secuencias<object> SegmentIntArc(Segment segment, Arc arc)
        {
            var res = new Secuencias<object>();
            var x = LineIntCircle(new Line(segment.P1, segment.P2), new Circle(arc.P1, arc._Measure));
            for (int i = 0; i < x.Count; i++)
            {
                Punto p = (Punto)x[i];
                if (_PuntoIntArc(p, arc) && _PuntoIntSegment(p, segment))
                {
                    res.Add(p);
                }
            }
            return res;

        }
        static Secuencias<object> SegmentIntCircle(Segment segment, Circle circle)
        {
            var res = new Secuencias<object>();
            var x = LineIntCircle(new Line(segment.P1, segment.P2), circle);
            for (int i = 0; i < x.Count; i++)
            {
                Punto p = (Punto)x[i];
                if (_PuntoIntSegment(p, segment))
                {
                    res.Add(p);
                }
            }
            return res;
        }
        static Secuencias<object> SegmentIntRay(Segment segment, Ray ray)//revisar cuando da undefined
        {
            var res = new Secuencias<object>();
            var x = LineInLine(new Line(segment.P1, segment.P2), new Line(ray.P1, ray.P2));
            if (x[0] is undefined)//revisar en caso de cambiar la implemntacion de undefined
            {
                //bool PuntoCoincidentes = _PuntoIntSegment((Punto)s1.P1,s2)||_PuntoIntSegment((Punto)s1.P2 ,s2);
                //if (!PuntoCoincidentes)
                //{
                ////no hago nada
                //}
                return res;
            }
            else
            {
                for (int i = 0; i < x.Count; i++)
                {
                    Punto p = (Punto)x[i];
                    if (_PuntoIntSegment(p, segment) && _PuntoIntRay(p, ray))
                    {
                        res.Add(p);
                    }
                }
            }
            return res;
        }
        #endregion

        #region ArcInFigure
        static Secuencias<object> ArcInFigure(Arc arc, ILugarGeometrico fig)
        {
            switch (fig)
            {
                case Punto a:
                    return PuntoIntArc(a, arc);
                case Line a:
                    return LineIntArc(a, arc);
                case Segment a:
                    return SegmentIntArc(a, arc);
                case Arc a:
                    return ArcIntArc(a, arc);
                case Circle a:
                    return ArcIntCircle(arc, a);
                case Ray a:
                    return ArcIntRay(arc, a);
                default: return new Secuencias<object>();
            }
        }
        static Secuencias<object> ArcIntArc(Arc a1, Arc a2)//revisar cuando da undefined
        {
            var res = new Secuencias<object>();
            var x = CircleIntCircle(new Circle(a1.P1, a1._Measure), new Circle(a2.P1, a2._Measure));
            for (int i = 0; i < x.Count; i++)
            {
                Punto p = (Punto)x[i];
                if (_PuntoIntArc(p, a1)&& _PuntoIntArc(p, a2))
                {
                    res.Add(p);
                }
            }
            return res;
        }
        static Secuencias<object> ArcIntCircle(Arc arc, Circle circle)//revisar cuando da undefined
        {
            var res = new Secuencias<object>();
            var x = CircleIntCircle(new Circle(arc.P1, arc._Measure), circle);
            for (int i = 0; i < x.Count; i++)
            {
                Punto p = (Punto)x[i];
                if (_PuntoIntArc(p, arc))
                {
                    res.Add(p);
                }
            }
            return res;
        }
        static Secuencias<object> ArcIntRay(Arc arc, Ray ray)
        {
            var res = new Secuencias<object>();
            var x = LineIntArc(new Line(ray.P1, ray.P2), arc);
            for (int i = 0; i < x.Count; i++)
            {
                Punto p = (Punto)x[i];
                if (_PuntoIntRay(p, ray))
                {
                    res.Add(p);
                }
            }
            return res;
        }
        #endregion

        #region CircleInFigure
        static Secuencias<object> CircleInFigure(Circle circle, ILugarGeometrico fig)
        {
            switch (fig)
            {
                case Punto a:
                    return PuntoIntCircle(a, circle);
                case Line a:
                    return LineIntCircle(a, circle);
                case Segment a:
                    return SegmentIntCircle(a, circle);
                case Arc a:
                    return ArcIntCircle(a, circle);
                case Circle a:
                    return CircleIntCircle(a, circle);
                case Ray a:
                    return CircleIntRay(circle, a);
                default: return new Secuencias<object>();
            }
        }
        static Secuencias<object> CircleIntCircle(Circle c1, Circle c2)// revisar
        {
            var result = new Secuencias<object>();
            Punto Centro1 = (Punto)c1.Centro;
            Punto Centro2 = (Punto)c2.Centro;
            double d = DistanciaEPuntos(Centro1, Centro2);
            Measure radioc1 = (Measure)c1.Radio;
            Measure radioc2 = (Measure)c2.Radio;
            double _radioc1 = radioc1.Valor;
            double _radioc2 = radioc2.Valor;
            double dx = Centro2.valor_x - Centro1.valor_x;
            double dy = Centro2.valor_y - Centro1.valor_y;
            // Si los círculos no se intersectan o son el mismo círculo
            if (d > _radioc1 + _radioc2 || d < Math.Abs(_radioc1 - _radioc2))//corregir
            {
                return result;
            }

            double a = (_radioc1 * _radioc1 - _radioc2 * _radioc2 + d * d) / (2 * d);
            double h = Math.Sqrt(_radioc1 * _radioc1 - a * a);

            double cx2 = Centro1.valor_x + a * (dx) / d;
            double cy2 = Centro1.valor_y + a * (dy) / d;

            var P1X = cx2 + h * (dy) / d;
            var P1Y = cy2 - h * (dx) / d;

            var P2X = cx2 - h * (dy) / d;
            var P2Y = cy2 + h * (dx) / d;

            result.Add(new Punto(P1X, P1Y));
            if(P1X != P2X || P1Y != P2Y)
                result.Add(new Punto(P2X, P2Y));
            return result;
        }
        static Secuencias<object> CircleIntRay(Circle circle, Ray ray)
        {
            var res = new Secuencias<object>();
            var x = LineIntCircle(new Line(ray.P1, ray.P2), circle);
            for (int i = 0; i < x.Count; i++)
            {
                Punto p = (Punto)x[i];
                if (_PuntoIntRay(p, ray))
                {
                    res.Add(p);
                }
            }
            return res;
        }
        #endregion

        #region RayInFigure
        static Secuencias<object> RayInFigure(Ray ray, ILugarGeometrico fig)
        {
            switch (fig)
            {
                case Punto a:
                    return PuntoIntRay(a, ray);
                case Line a:
                    return LineIntRay(a, ray);
                case Segment a:
                    return SegmentIntRay(a, ray);
                case Arc a:
                    return ArcIntRay(a, ray);
                case Circle a:
                    return CircleIntRay(a, ray);
                case Ray a:
                    return RayIntRay(a, ray);
                default: return new Secuencias<object>();
            }

        }
        static Secuencias<object> RayIntRay(Ray r1, Ray r2)//revisar
        {
            var res = new Secuencias<object>();
            var x = LineInLine(new Line(r1.P1, r1.P2), new Line(r2.P1, r2.P2));
            if (x[0] is undefined)//revisar en caso de cambiar la implemntacion de undefined
            {
                //bool PuntoCoincidentes = _PuntoIntSegment((Punto)s1.P1,s2)||_PuntoIntSegment((Punto)s1.P2 ,s2);
                //if (!PuntoCoincidentes)
                //{
                ////no hago nada
                //}
                return res;
            }
            else
            {
                for (int i = 0; i < x.Count; i++)
                {
                    Punto p = (Punto)x[i];
                    if (_PuntoIntRay(p, r1) && _PuntoIntRay(p, r2))
                    {
                        res.Add(p);
                    }
                }
            }
            return res;
        }
        #endregion
    }

}
