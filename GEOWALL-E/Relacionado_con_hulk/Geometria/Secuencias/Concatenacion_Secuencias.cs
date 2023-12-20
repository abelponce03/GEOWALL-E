using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEOWALL_E.Relacionado_con_hulk.Geometria.Secuencias
{
    internal class Concatenacion_Secuencias
    {
        public static Sequence Concatenar(Sequence sequence_1, Sequence sequence_2)
        {
            switch (sequence_1)
            {
                case Point_Sequence a:
                    return Concatenar_Secuencia_Puntos(a, sequence_2);
                case Line_Sequence a:
                    return Concatenar_Secuencia_Lineas(a, sequence_2);
                case Segment_Sequence a:
                    return Concatenar_Secuencia_Segment(a, sequence_2);
                case Circle_Sequence a:
                    return Concatenar_Secuencia_Circle(a, sequence_2);
                case Ray_Sequence a:
                    return Concatenar_Secuencia_Ray(a, sequence_2);
                default:
                    return Concatenar_Secuencia_otros((Secuencias<object>)sequence_1, sequence_2);
                
            }
        }
        static Sequence Concatenar_Secuencia_Puntos(Point_Sequence puntos, Sequence secuencia)
        {
            if(secuencia is Secuencias<object>)
            {
                Secuencias<object> secuencia_2 = (Secuencias <object>) secuencia;
                for (int i = 0; i < secuencia_2.Count; i++)
                {
                    if (secuencia_2[i] is not Punto) throw new Exception($"! SEMANTIC ERROR : <{puntos.GetType().Name}> cannot contains <{secuencia_2[i].GetType()}>");
                    puntos._Secuencias_Evaluada.Add(secuencia_2[i]);
                }
            }
            else if( secuencia is Point_Sequence)
            {
                Point_Sequence secuencia_2 = (Point_Sequence)secuencia;
                for (int i = 0; i < secuencia_2._Secuencias_Evaluada.Count; i++)
                {
                    puntos._Secuencias_Evaluada.Add(secuencia_2._Secuencias_Evaluada[i]);
                }
            }
            else throw new Exception($"! SEMANTIC ERROR : Invalid expression: Can't operate <{puntos.GetType().Name}> with <{secuencia.GetType().Name}>");

            return puntos;
        }
        static Sequence Concatenar_Secuencia_Lineas(Line_Sequence lineas, Sequence secuencia)
        {
            if(secuencia is Secuencias<object>)
            {
                Secuencias<object> secuencia_2 = (Secuencias<object>)secuencia;
                for (int i = 0; i < secuencia_2.Count; i++)
                {
                    if (secuencia_2[i] is not Line) throw new Exception($"! SEMANTIC ERROR : <{lineas.GetType().Name}> cannot contains <{secuencia_2[i].GetType()}>");
                    lineas._Secuencias_Evaluada.Add(secuencia_2[i]);
                }
            }
            else if(secuencia is Line_Sequence)
            {
                Line_Sequence secuencia_2 = (Line_Sequence)secuencia;
                for (int i = 0; i < secuencia_2._Secuencias_Evaluada.Count; i++)
                {
                    lineas._Secuencias_Evaluada.Add(secuencia_2._Secuencias_Evaluada[i]);
                }
            }
            else throw new Exception($"! SEMANTIC ERROR : Invalid expression: Can't operate <{lineas.GetType().Name}> with <{secuencia.GetType().Name}>");
            return lineas;
        }
        static Sequence Concatenar_Secuencia_Segment(Segment_Sequence segmentos, Sequence secuencia)
        {

            if (secuencia is Secuencias<object>)
            {
                Secuencias<object> secuencia_2 = (Secuencias<object>)secuencia;
                for (int i = 0; i < secuencia_2.Count; i++)
                {
                    if (secuencia_2[i] is not Segment) throw new Exception($"! SEMANTIC ERROR : <{segmentos.GetType().Name}> cannot contains <{secuencia_2[i].GetType()}>");
                    segmentos._Secuencias_Evaluada.Add(secuencia_2[i]);
                }
            }
            else if (secuencia is Segment_Sequence)
            {
                Segment_Sequence secuencia_2 = (Segment_Sequence)secuencia;
                for (int i = 0; i < secuencia_2._Secuencias_Evaluada.Count; i++)
                {
                    segmentos._Secuencias_Evaluada.Add(secuencia_2._Secuencias_Evaluada[i]);
                }
            }
            else throw new Exception($"! SEMANTIC ERROR : Invalid expression: Can't operate <{segmentos.GetType().Name}> with <{secuencia.GetType().Name}>");
            return segmentos;
        }
        static Sequence Concatenar_Secuencia_Circle(Circle_Sequence circulos, Sequence secuencia)
        {

            if (secuencia is Secuencias<object>)
            {
                Secuencias<object> secuencia_2 = (Secuencias<object>)secuencia;
                for (int i = 0; i < secuencia_2.Count; i++)
                {
                    if (secuencia_2[i] is not Circle) throw new Exception($"! SEMANTIC ERROR : <{circulos.GetType().Name}> cannot contains <{secuencia_2[i].GetType()}>");
                    circulos._Secuencias_Evaluada.Add(secuencia_2[i]);
                }
            }
            else if (secuencia is Circle_Sequence)
            {
                Circle_Sequence secuencia_2 = (Circle_Sequence)secuencia;
                for (int i = 0; i < secuencia_2._Secuencias_Evaluada.Count; i++)
                {
                    circulos._Secuencias_Evaluada.Add(secuencia_2._Secuencias_Evaluada[i]);
                }
            }
            else throw new Exception($"! SEMANTIC ERROR : Invalid expression: Can't operate <{circulos.GetType().Name}> with <{secuencia.GetType().Name}>");
            return circulos;
        }
        static Sequence Concatenar_Secuencia_Ray(Ray_Sequence rayos, Sequence secuencia)
        {
            if (secuencia is Secuencias<object>)
            {
                Secuencias<object> secuencia_2 = (Secuencias<object>)secuencia;
                for (int i = 0; i < secuencia_2.Count; i++)
                {
                    if (secuencia_2[i] is not Ray) throw new Exception($"! SEMANTIC ERROR : <{rayos.GetType().Name}> cannot contains <{secuencia_2[i].GetType()}>");
                    rayos._Secuencias_Evaluada.Add(secuencia_2[i]);
                }
            }
            else if (secuencia is Ray_Sequence)
            {
                Ray_Sequence secuencia_2 = (Ray_Sequence)secuencia;
                for (int i = 0; i < secuencia_2._Secuencias_Evaluada.Count; i++)
                {
                    rayos._Secuencias_Evaluada.Add(secuencia_2._Secuencias_Evaluada[i]);
                }
            }
            else throw new Exception($"! SEMANTIC ERROR : Invalid expression: Can't operate <{rayos.GetType().Name}> with <{secuencia.GetType().Name}>");
            return rayos;
        }
        static Sequence Concatenar_Secuencia_otros(Secuencias<object> otros, Sequence secuencia)
        {
            if (otros.Count > 0)
            {
                if (secuencia is Secuencias<object>)
                {
                    Secuencias<object> secuencia_2 = (Secuencias<object>)secuencia;
                    for (int i = 0; i < secuencia_2.Count; i++)
                    {

                        if (secuencia_2[i].GetType() != otros[0].GetType()) throw new Exception($"! SEMANTIC ERROR : <{otros.GetType().Name}> cannot contains <{secuencia_2[i].GetType()}>");
                        otros.Add(secuencia_2[i]);
                    }
                }
                else if (secuencia is Point_Sequence)
                {
                    Point_Sequence secuencia_2 = (Point_Sequence)secuencia;
                    for (int i = 0; i < secuencia_2._Secuencias_Evaluada.Count; i++)
                    {
                        if (secuencia_2._Secuencias_Evaluada[i].GetType() != otros[0].GetType()) throw new Exception($"! SEMANTIC ERROR : <{otros.GetType().Name}> cannot contains <{secuencia_2._Secuencias_Evaluada[i].GetType()}>");
                        otros.Add(secuencia_2._Secuencias_Evaluada[i]);
                    }
                }
                else if (secuencia is Line_Sequence)
                {
                    Line_Sequence secuencia_2 = (Line_Sequence)secuencia;
                    for (int i = 0; i < secuencia_2._Secuencias_Evaluada.Count; i++)
                    {
                        if (secuencia_2._Secuencias_Evaluada[i].GetType() != otros[0].GetType()) throw new Exception($"! SEMANTIC ERROR : <{otros.GetType().Name}> cannot contains <{secuencia_2._Secuencias_Evaluada[i].GetType()}>");
                        otros.Add(secuencia_2._Secuencias_Evaluada[i]);
                    }
                }
                else if (secuencia is Ray_Sequence)
                {
                    Ray_Sequence secuencia_2 = (Ray_Sequence)secuencia;
                    for (int i = 0; i < secuencia_2._Secuencias_Evaluada.Count; i++)
                    {
                        if (secuencia_2._Secuencias_Evaluada[i].GetType() != otros[0].GetType()) throw new Exception($"! SEMANTIC ERROR : <{otros.GetType().Name}> cannot contains <{secuencia_2._Secuencias_Evaluada[i].GetType()}>");
                        otros.Add(secuencia_2._Secuencias_Evaluada[i]);
                    }
                }
                else if (secuencia is Segment_Sequence)
                {
                    Segment_Sequence secuencia_2 = (Segment_Sequence)secuencia;
                    for (int i = 0; i < secuencia_2._Secuencias_Evaluada.Count; i++)
                    {
                        if (secuencia_2._Secuencias_Evaluada[i].GetType() != otros[0].GetType()) throw new Exception($"! SEMANTIC ERROR : <{otros.GetType().Name}> cannot contains <{secuencia_2._Secuencias_Evaluada[i].GetType()}>");
                        otros.Add(secuencia_2._Secuencias_Evaluada[i]);
                    }
                }
                else if (secuencia is Circle_Sequence)
                {
                    Circle_Sequence secuencia_2 = (Circle_Sequence)secuencia;
                    for (int i = 0; i < secuencia_2._Secuencias_Evaluada.Count; i++)
                    {
                        if (secuencia_2._Secuencias_Evaluada[i].GetType() != otros[0].GetType()) throw new Exception($"! SEMANTIC ERROR : <{otros.GetType().Name}> cannot contains <{secuencia_2._Secuencias_Evaluada[i].GetType()}>");
                        otros.Add(secuencia_2._Secuencias_Evaluada[i]);
                    }
                }
                else throw new Exception($"! SEMANTIC ERROR : Invalid expression: Can't operate <{otros.GetType().Name}> with <{secuencia.GetType().Name}>");
                return otros;
            }
            else return secuencia;    
        }
    }
}
