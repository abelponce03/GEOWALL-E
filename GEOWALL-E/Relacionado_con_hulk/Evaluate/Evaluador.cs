using GEOWALL_E.Relacionado_con_hulk.AST;
using GEOWALL_E.Relacionado_con_hulk.Geometria;
using GEOWALL_E.Relacionado_con_hulk.Geometria.Draw_Functions;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace GEOWALL_E
{
    class Evaluador
    {
        private int Contador;

        private readonly List<Expresion> _rama;

        private int _posicion;

        public Evaluador(List<Expresion> rama)
        {
            this._rama = rama;
        }
        private Expresion Tomar(int offset)
        {
            int index = _posicion + offset;
            if (index >= _rama.Count) return _rama[_rama.Count - 1];
            return _rama[index];
        }
        private Expresion Verificandose => Tomar(0);

        public Expresion Proxima_Expresion()
        {
            var _expresion = Verificandose;
            _posicion++;
            return _expresion;
        }
        public void Evaluar()
        {
            while (_posicion < _rama.Count)
            {
                Evaluar_Expresion(Verificandose);
             
                Proxima_Expresion();
            }
        }
        object Evaluar_Expresion(Expresion nodo)
        {
            Contador++;
            if (Contador > 1000)
            {
                throw new Exception("! OVERFLOW ERROR : Hulk Stack overflow");
            }
            switch (nodo)
            {
                case Literal:  return Evaluar_Literal((Literal) nodo);
                case Expresion_Binaria: return Evaluar_Expresion_Binaria((Expresion_Binaria)nodo);
                case Expresion_Unaria: return Evaluar_Expresion_Unaria((Expresion_Unaria)nodo);
                case Declaracion_Funcion: return null;
                case LLamada_Funcion: return Evaluar_Expresion_Llamada_Funcion((LLamada_Funcion)nodo);
                case Sen: return Evaluar_Expresion_Sen((Sen)nodo);
                case Cos: return Evaluar_Expresion_Cos((Cos)nodo);
                case Let_in: return Evaluar_Expresion_Let_in((Let_in)nodo);
                case Logaritmo: return Evaluar_Expresion_Logaritmo((Logaritmo)nodo);
                case IF: return Evaluar_Expresion_IF((IF)nodo);
                case Asignacion: return Evaluar_Expresion_Asignacion((Asignacion)nodo);
                case In: return Evaluar_Expresion(nodo);
                case Parentesis: return Evaluar_Expresion(nodo);

                //------------------------------------------------//SECUENCIAS//------------------------------------------------------//

                case Asignacion_Secuencia: return Evaluar_Expresion_Asignacion_Secuencia((Asignacion_Secuencia)nodo);
                case Secuencias: return Evaluar_Expresion_Secuencia((Secuencias)nodo);
                case Randoms: return Evaluar_Expresion_Randoms((Randoms)nodo);
                case Samples: return Evaluar_Expresion_Samples((Samples)nodo);
                case Count: return Evaluar_Expresion_Count((Count)nodo);
                case undefined: return null;

                //------------------------------------------------//GEOMETRIA//------------------------------------------------------//

                case Punto: return Evaluar_Expresion_Punto((Punto)nodo);
                case Measure: return Evaluar_Expresion_Measure((Measure)nodo);
                case Segment: return Evaluar_Expresion_Segment((Segment)nodo);
                case Circle: return Evaluar_Expresion_Circle((Circle)nodo);
                case Line: return Evaluar_Expresion_Line((Line)nodo);
                case Ray: return Evaluar_Expresion_Ray((Ray)nodo);
                case Arc: return Evaluar_Expresion_Arc((Arc)nodo);

                //---------------------------------------------------// Dibujo //-----------------------------------------------------//

                case Dibujar: return Evaluar_Expresion_Dibujar((Dibujar)nodo);

                default: throw new Exception($"! SYNTAX ERROR : Unexpected node <{nodo}>");
            }
        }
        private static void Verificar_tipos(Expresion_Binaria b, object left, object right)
        {
            if (left.GetType() != right.GetType())
            {
                throw new Exception($"! SEMANTIC ERROR : Invalid expression: Can't operate <{left.GetType().Name}> with <{right.GetType().Name}> using <{b.Operador.Texto}>");
            }
        }
        private static void verificar_asignacion_identificadores(string a)
        {
           if(a != "_" && Biblioteca.Variables.ContainsKey(a) ||
              (Biblioteca.Pila.Count > 0) && Biblioteca.Pila.Peek().ContainsKey(a))  throw new Exception($"! SEMANTIC ERROR : Redefination of the constant <{a}>");
        }
        private static bool Existencia(Literal a)
        {
            if(!Biblioteca.Variables.ContainsKey(a._Literal.Texto)) return false;
            return true;
        }
        private static object Tomar_valor(Literal a) => Biblioteca.Variables[a._Literal.Texto];
        
        private object Evaluar_Literal(Literal a)
        {
            if (a._Literal.Tipo == Tipo_De_Token.Identificador)
            {
                if (!Existencia(a) && Biblioteca.Pila.Count == 0) throw new Exception($"! SEMANTIC ERROR : Variable <{a._Literal.Texto}> is not defined");
                else if (Biblioteca.Pila.Count != 0) return Biblioteca.Pila.Peek()[a._Literal.Texto];
                return Tomar_valor(a);
            }
            return a.Valor;
        }
        private object Evaluar_Expresion_Unaria(Expresion_Unaria c)
        {
            var right = Evaluar_Expresion(c.Right);

            switch (c.Operador.Tipo)
            {
                case Tipo_De_Token.Suma: return (double)right;

                case Tipo_De_Token.Resta: return -(double)right;

                case Tipo_De_Token.Bang: return !(bool)right;

                default: throw new Exception($"! SEMANTIC ERROR : Invalid unary operator <{c.Operador.Tipo}>");
            }
        }
        private object Evaluar_Expresion_Llamada_Funcion(LLamada_Funcion d)
        {

            if (!Biblioteca.Functions.ContainsKey(d.Nombre))
            {
                throw new Exception($"! FUNCTION ERROR : Function <{d.Nombre}> is not defined");
            }
            var Declaracion_Funcion = Biblioteca.Functions[d.Nombre];
            if (Declaracion_Funcion.Parametros.Count != d.Parametros.Count)
            {
                throw new Exception($"! FUNCTION ERROR : Function <{d.Nombre}> does not have <{d.Parametros.Count}> parameters but has <{Biblioteca.Functions[d.Nombre].Parametros.Count}> parameters");
            }

            var temp = new Dictionary<string, object>();

            var parametros = d.Parametros;
            var argumentos = Declaracion_Funcion.Parametros;

            for (int i = 0; i < parametros.Count; i++)
            {
                var id = argumentos[i];
                var expresion = Evaluar_Expresion(parametros[i]);
                temp.Add(id, expresion);
            }

            Biblioteca.Pila.Push(temp);

            var valor = Evaluar_Expresion(Declaracion_Funcion.Cuerpo);

            Biblioteca.Pila.Pop();

            return valor;
        }
        private object Evaluar_Expresion_Sen(Sen e)
        {
            var expresion = Evaluar_Expresion(e._expresion);
            var valor = Math.Sin((double)expresion);
            return valor;
        }
        private object Evaluar_Expresion_Cos(Cos f)
        {
            var expresion = Evaluar_Expresion(f._expresion);
            var valor = Math.Cos((double)expresion);
            return valor;
        }
        //evaluacion de let in mallllllllllll arreglar la pilaaaaaaa
        private object Evaluar_Expresion_Let_in(Let_in g) 
        {

            var temp = new Dictionary<string, object>();

            foreach(var check in g.Bloque)
            {
                if (check is Asignacion)
                {
                    Asignacion asignacion = (Asignacion)check;
                    var valor = Evaluar_Expresion(check);
                    temp.Add(asignacion.Identificador, valor);
                }
                else if (check is Punto)
                {
                    Punto _punto = (Punto)check;
                    var valor = Evaluar_Expresion(check);
                    temp.Add(_punto.Identificador, valor);
                }
                else if (check is Line)
                {
                    Line _Line = (Line)check;
                    var valor = Evaluar_Expresion(_Line);
                    temp.Add(_Line.Identificador, valor);
                }
                else if (check is Circle)
                {
                    Circle _circle = (Circle)check;
                    var valor = Evaluar_Expresion(check);
                    temp.Add(_circle.Identificador, valor);
                }
                else if (check is Ray)
                {
                    Ray _ray = (Ray)check;
                    var valor = Evaluar_Expresion(check);
                    temp.Add(_ray.Identificador, valor);
                }
                else if (check is Segment)
                {
                    Segment _segment = (Segment)check;
                    var valor = Evaluar_Expresion(check);
                    temp.Add(_segment.Identificador, valor);
                }
                else Evaluar_Expresion(check);
            }
            Biblioteca.Pila.Push(temp);

            var _in = Evaluar_Expresion(g._IN);

            Biblioteca.Pila.Pop();

            return _in;
        }
        private object Evaluar_Expresion_Logaritmo(Logaritmo x)
        {
            var expresion = Evaluar_Expresion(x._expresion);
            var valor = Math.Log((double)expresion);
            return valor;
        }
        private object Evaluar_Expresion_IF(IF j) 
        {
            var condicion = Evaluar_Expresion(j.Condicion);
            if (condicion.GetType() != typeof(bool)) throw new Exception("! SEMANTIC ERROR : If-ELSE expressions must have a boolean condition");

            if ((bool)condicion) return Evaluar_Expresion(j._expresion);

            else
            {

                return Evaluar_Expresion(j._Else);
            }
        }
        private object Evaluar_Expresion_Asignacion(Asignacion w)
        {
            var _expresion = Evaluar_Expresion(w._Expresion);
            verificar_asignacion_identificadores(w.Identificador);
            if(Biblioteca.Pila.Count == 0)Biblioteca.Variables[w.Identificador] = _expresion;
            return _expresion;
        }
        private object Evaluar_Expresion_Asignacion_Secuencia(Asignacion_Secuencia y)
        {
            if(y._Secuencia is undefined)
            {
                for(int i = 0; i < y.Identificadores.Count; i++)
                {
                    verificar_asignacion_identificadores(y.Identificadores[i]);
                    if (Biblioteca.Pila.Count == 0) Biblioteca.Variables[y.Identificadores[i]] = new undefined();
                }
                verificar_asignacion_identificadores(y.Identificador_resto_de_secuencia);
                if (Biblioteca.Pila.Count == 0) Biblioteca.Variables[y.Identificador_resto_de_secuencia] = new Secuencias_Evaluada();
            }
            else
            {
                Secuencias_Evaluada _secuencia = (Secuencias_Evaluada)Evaluar_Expresion(y._Secuencia);

                Secuencias_Evaluada _resto = new Secuencias_Evaluada();

                int marcador = 0;
                for (int i = 0; i < y.Identificadores.Count; i++)
                {
                    verificar_asignacion_identificadores(y.Identificadores[i]);
                    if (y.Identificadores[i] == "_")
                    {

                    }
                    else if (i >= _secuencia.Count)
                    {
                        if (Biblioteca.Pila.Count == 0) Biblioteca.Variables[y.Identificadores[i]] = new undefined();
                    }
                    else
                    {
                        if (Biblioteca.Pila.Count == 0) Biblioteca.Variables[y.Identificadores[i]] = _secuencia[i];
                        marcador++;
                    }
                }
                for (int i = marcador; i < _secuencia.Count; i++)
                {
                    _resto.Add(_secuencia[i]);
                }
                //resto
                verificar_asignacion_identificadores(y.Identificador_resto_de_secuencia);
                if (Biblioteca.Pila.Count == 0 && y.Identificador_resto_de_secuencia != "_") Biblioteca.Variables[y.Identificador_resto_de_secuencia] = _resto;
            }
            return null;
        }

        private object Evaluar_Expresion_Secuencia(Secuencias z)
        {
            var _secuencia = new Secuencias_Evaluada();
            for(int i= 0; i < z.Count; i++)
            {
                _secuencia.Add(Evaluar_Expresion(z[i]));
            }
            return _secuencia;
        }
        private object Evaluar_Expresion_Randoms(Randoms r)
        {
            var _secuencia = new Secuencias_Evaluada();
            foreach(var x in r)
            {
                _secuencia.Add(x);
            }
            return _secuencia;
        }
        private object Evaluar_Expresion_Samples(Samples samples) 
        {
            var _secuencia = new Secuencias_Evaluada();
            foreach(var x in samples)
            {
                _secuencia.Add(x);
            }
            return _secuencia;
        }
        private object Evaluar_Expresion_Count(Count count)
        {
            if (count._Secuencia is Secuencias)
            {
                Secuencias_Evaluada _secuencia = (Secuencias_Evaluada)Evaluar_Expresion(count._Secuencia);
                return _secuencia.Count;
            }
            else throw new Exception($"");
        }
        private object Evaluar_Expresion_Binaria(Expresion_Binaria b)
        {
            var left = Evaluar_Expresion(b.Left);
            var right = Evaluar_Expresion(b.Right);

            switch (b.Operador.Tipo)
            {
                case Tipo_De_Token.resto:
                    {
                        Verificar_tipos(b, left, right);
                        return (double)left % (double)right;
                    }
                case Tipo_De_Token.concatenacion:
                    {
                        Verificar_tipos(b, left, right);
                        return (string)left + (string)right;
                    }
                case Tipo_De_Token.Suma:
                    {
                        Verificar_tipos(b, left, right);
                        return (double)left + (double)right;
                    }

                case Tipo_De_Token.Resta:
                    {

                        Verificar_tipos(b, left, right);
                        return (double)left - (double)right;
                    }

                case Tipo_De_Token.Producto:
                    {
                        Verificar_tipos(b, left, right);
                        return (double)left * (double)right;
                    }

                case Tipo_De_Token.Division:
                    {
                        Verificar_tipos(b, left, right);
                        if ((double)right == 0) throw new Exception($"! SEMANTIC ERROR : Cannot divide <{left}> by <{right}>");
                        else return (double)left / (double)right;
                    }

                case Tipo_De_Token.Potenciacion:
                    {
                        Verificar_tipos(b, left, right);
                        if ((double)left == 0 && (double)right == 0) throw new Exception($"! SEMANTIC ERROR : <{left}> pow to <{right}> is not defined");
                        else return Math.Pow((double)left, (double)right);
                    }

                case Tipo_De_Token.AmpersandAmpersand:
                    {
                        Verificar_tipos(b, left, right);
                        return (bool)left && (bool)right;
                    }

                case Tipo_De_Token.PipePipe:
                    {
                        Verificar_tipos(b, left, right);
                        return (bool)left || (bool)right;
                    }

                case Tipo_De_Token.Menor_que:
                    {
                        Verificar_tipos(b, left, right);
                        return (double)left < (double)right;
                    }
                case Tipo_De_Token.Menor_igual_que:
                    {
                        Verificar_tipos(b, left, right);
                        return (double)left <= (double)right;
                    }
                case Tipo_De_Token.Mayor_que:
                    {
                        Verificar_tipos(b, left, right);
                        return (double)left > (double)right;
                    }
                case Tipo_De_Token.Mayor_igual_que:
                    {
                        Verificar_tipos(b, left, right);
                        return (double)left >= (double)right;
                    }
                case Tipo_De_Token.IgualIgual:
                    {
                        Verificar_tipos(b, left, right);
                        return Equals(left, right);
                    }
                case Tipo_De_Token.Bang_Igual:
                    {
                        Verificar_tipos(b, left, right);
                        return !Equals(left, right);
                    }
                default: throw new Exception($"! SEMANTIC ERROR : Unexpected binary operator <{b.Operador.Tipo}>");
            }
        }
        ///////////////////////////////////METODOS GEOMETRIA/////////////////////////////////////////////////////
        private object Evaluar_Expresion_Punto(Punto l)
        {
            if (l.Componente_x is null && l.Componente_y is null) return l;

            var valor_x = Evaluar_Expresion(l.Componente_x);
            var valor_y = Evaluar_Expresion(l.Componente_y);

            int x1 = Convert.ToInt32(valor_x);
            int y1 = Convert.ToInt32(valor_y);

            if (l.Identificador is null) return new Punto(x1, y1);

            verificar_asignacion_identificadores(l.Identificador);
            if(Biblioteca.Pila.Count == 0)Biblioteca.Variables[l.Identificador] = new Punto(l.Identificador, x1, y1);
            return new Punto(l.Identificador, x1, y1);
        }
        private object Evaluar_Expresion_Measure(Measure o)
        {
            if (o.P1 is null && o.P2 is null) return o;

            var _P1 = Evaluar_Expresion(o.P1);
            var _P2 = Evaluar_Expresion(o.P2);

            if (_P1 is Punto && _P2 is Punto)
            {
                Punto P1 = (Punto)_P1;
                Punto P2 = (Punto)_P2;

                var x1 = P1.valor_x;
                var y1 = P1.valor_y;
                var x2 = P2.valor_x;
                var y2 = P2.valor_y;
                double valor = Math.Sqrt(Math.Pow(((double)x2 - (double)x1), 2) + Math.Pow(((double)y2 - (double)y1), 2));
                return new Measure(valor);
            }
            else
            {
                throw new Exception($"");
            }
        }
        private object Evaluar_Expresion_Segment(Segment q)
        {   
            var P1 = Evaluar_Expresion(q.P1);
            var P2 = Evaluar_Expresion(q.P2);
            if (P1 is Punto && P2 is Punto)
            {
                if (q.Identificador is null)
                {
                    return new Segment((Punto)P1,(Punto) P2);
                }
                Segment _segment = new Segment((Punto)P1, (Punto)P2);
                verificar_asignacion_identificadores(q.Identificador);
                if (Biblioteca.Pila.Count == 0) Biblioteca.Variables[q.Identificador] = _segment;
                return _segment;
            }
            throw new Exception($"");
        }
        private object Evaluar_Expresion_Circle(Circle r)
        {
             var centro = Evaluar_Expresion(r.Centro);
             var radio = Evaluar_Expresion(r.Radio);
             if(centro is Punto && radio is Measure)
            {
                if (r.Identificador is null)
                {
                    return new Circle((Punto)centro, (Measure)radio);
                }
                Circle circulo = new Circle((Punto)centro, (Measure)radio);
                verificar_asignacion_identificadores(r.Identificador);
                if (Biblioteca.Pila.Count == 0) Biblioteca.Variables[r.Identificador] = circulo;
                return circulo;
            }
            
            else
            {
                throw new Exception($"");
            }
        }
        private object Evaluar_Expresion_Line(Line s)
        {
            var P1 = Evaluar_Expresion(s.P1);
            var P2 = Evaluar_Expresion(s.P2);
            if (P1 is Punto && P2 is Punto)
            {
                if (s.Identificador is null)
                {
                    return new Line((Punto)P1,(Punto) P2);
                }
                Line _line = new Line((Punto)P1,(Punto)P2);
                verificar_asignacion_identificadores(s.Identificador);
                if (Biblioteca.Pila.Count == 0) Biblioteca.Variables[s.Identificador] = _line;
                return _line;
            }
            else
            {
                throw new Exception($"");
            }
        }
        private object Evaluar_Expresion_Ray(Ray t)
        {
            var P1 = Evaluar_Expresion(t.P1);
            var P2 = Evaluar_Expresion(t.P2);
            if (P1 is Punto && P2 is Punto)
            {
                if (t.Identificador is null)
                {
                    return new Ray((Punto)P1,(Punto)P2);
                }
                Ray _ray = new Ray((Punto)P1, (Punto)P2);
                verificar_asignacion_identificadores(t.Identificador);
                if (Biblioteca.Pila.Count == 0) Biblioteca.Variables[t.Identificador] = _ray;
                return _ray;
            }
            else
            {
                throw new Exception($"");
            }
        }
        private object Evaluar_Expresion_Arc(Arc v)
        {
            var P1 = Evaluar_Expresion(v.P1);
            var P2 = Evaluar_Expresion(v.P2);
            var P3 = Evaluar_Expresion(v.P3);
            var measure = Evaluar_Expresion(v._Measure);
            if(P1 is Punto &&  P2 is Punto && P3 is Punto && measure is Measure)
            {
                return new Arc((Punto) P1, (Punto)P2, (Punto)P3, (Measure) measure);
            }
            else
            {
                throw new Exception($"");
            }
        }
        private  object Evaluar_Expresion_Dibujar(Dibujar p)
        {
            Pen lapiz = new Pen(Color.Black, 4);

            switch (p._Expresion.Tipo)
            {
                case Tipo_De_Token.point_Expresion:
                    {
                        Punto _punto = (Punto)Evaluar_Expresion(p._Expresion);
                        Dibujar_Punto(_punto, lapiz);
                        return null;
                    }
                case Tipo_De_Token.arc_Expresion:
                    {
                        Arc _arc = (Arc)Evaluar_Expresion(p._Expresion);
                        Dibujar_Arco(_arc, lapiz);
                        return null;
                    }
                case Tipo_De_Token.circle_Expresion:
                    {
                        Circle _circle = (Circle)Evaluar_Expresion(p._Expresion);
                        Dibujar_Circunsferencia(_circle, lapiz);
                        return null;
                    }
                case Tipo_De_Token.line_Expresion:
                    {
                        Line _line = (Line)Evaluar_Expresion(p._Expresion);
                        Dibujar_Linea(_line, lapiz);
                        return null;
                    }
                case Tipo_De_Token.ray_Expresion:
                    {
                        Ray _ray = (Ray)Evaluar_Expresion(p._Expresion);
                        Dibujar_Rayo(_ray, lapiz);
                        return null;
                    }
                case Tipo_De_Token.segment_Expresion:
                    {
                        Segment _segment = (Segment)Evaluar_Expresion(p._Expresion);
                        Dibujar_Segmento(_segment, lapiz);
                        return null;
                    }
                case Tipo_De_Token.secuencia_Expresion:
                    {
                        Secuencias_Evaluada _secuencia = (Secuencias_Evaluada)Evaluar_Expresion(p._Expresion);
                        for(int i = 0; i < _secuencia.Count; i++)
                        {
                            if (_secuencia[i] is Punto)
                            {
                                Dibujar_Punto((Punto)_secuencia[i], lapiz);
                            }
                            else if (_secuencia[i] is Circle)
                            {
                                Dibujar_Circunsferencia((Circle)_secuencia[i], lapiz);
                            }
                            else if (_secuencia[i] is Line)
                            {
                                Dibujar_Linea((Line)_secuencia[i], lapiz);
                            }
                            else if (_secuencia[i] is Segment)
                            {
                                Dibujar_Segmento((Segment)_secuencia[i], lapiz);
                            }
                            else
                            {
                                Dibujar_Rayo((Ray)_secuencia[i], lapiz);
                            }
                        }
                        return null;
                    }
                case Tipo_De_Token.Literal:
                    {
                        Literal _literal = (Literal)p._Expresion;

                        if (Biblioteca.Variables.ContainsKey(_literal._Literal.Texto))
                        {
                            var _expresion = Biblioteca.Variables[_literal._Literal.Texto];

                            if (_expresion is Punto)
                            {
                                Dibujar_Punto((Punto)_expresion, lapiz);
                            }
                            else if (_expresion is Line)
                            {
                                Dibujar_Linea((Line)_expresion, lapiz);
                            }
                            else if (_expresion is Circle)
                            {
                                Dibujar_Circunsferencia((Circle)_expresion, lapiz);
                            }
                            else if (_expresion is Ray)
                            {
                                Dibujar_Rayo((Ray)_expresion, lapiz);
                            }
                            else if (_expresion is Secuencias_Evaluada)
                            {
                                var _secuencia = (Secuencias_Evaluada)_expresion;

                                for (int i = 0; i < _secuencia.Count; i++)
                                {
                                    if (_secuencia[i] is Punto)
                                    {
                                        Dibujar_Punto((Punto)_secuencia[i], lapiz);
                                    }
                                    else if (_secuencia[i] is Circle)
                                    {
                                        Dibujar_Circunsferencia((Circle)_secuencia[i], lapiz);
                                    }
                                    else if (_secuencia[i] is Line)
                                    {
                                        Dibujar_Linea((Line)_secuencia[i], lapiz);
                                    }
                                    else if (_secuencia[i] is Segment)
                                    {
                                        Dibujar_Segmento((Segment)_secuencia[i], lapiz);
                                    }
                                    else
                                    {
                                        Dibujar_Rayo((Ray)_secuencia[i], lapiz);
                                    }
                                }
                            }
                            else if(_expresion is undefined)
                            {

                            }
                            else Dibujar_Segmento((Segment)_expresion, lapiz);

                            return null;
                        }
                        else if(Biblioteca.Pila.Peek().ContainsKey(_literal._Literal.Texto))
                        {
                            var _expresion = Biblioteca.Pila.Peek()[_literal._Literal.Texto];

                            if (_expresion is Punto)
                            {
                                Dibujar_Punto((Punto)_expresion, lapiz);
                            }
                            else if (_expresion is Line)
                            {
                                Dibujar_Linea((Line)_expresion, lapiz);
                            }
                            else if (_expresion is Circle)
                            {
                                Dibujar_Circunsferencia((Circle)_expresion, lapiz);
                            }
                            else if (_expresion is Ray)
                            {
                                Dibujar_Rayo((Ray)_expresion, lapiz);
                            }
                            else if (_expresion is Secuencias_Evaluada)
                            {
                                var _secuencia = (Secuencias_Evaluada)_expresion;

                                for (int i = 0; i < _secuencia.Count; i++)
                                {
                                    if (_secuencia[i] is Punto)
                                    {
                                        Dibujar_Punto((Punto)_secuencia[i], lapiz);
                                    }
                                    else if (_secuencia[i] is Circle)
                                    {
                                        Dibujar_Circunsferencia((Circle)_secuencia[i], lapiz);
                                    }
                                    else if (_secuencia[i] is Line)
                                    {
                                        Dibujar_Linea((Line)_secuencia[i], lapiz);
                                    }
                                    else if (_secuencia[i] is Segment)
                                    {
                                        Dibujar_Segmento((Segment)_secuencia[i], lapiz);
                                    }
                                    else
                                    {
                                        Dibujar_Rayo((Ray)_secuencia[i], lapiz);
                                    }
                                }
                            }
                            else if (_expresion is undefined)
                            {

                            }
                            else Dibujar_Segmento((Segment)_expresion, lapiz);

                            return null;
                        }
                        else
                        {
                            throw new Exception($"! SEMANTIC ERROR : Variable <{_literal._Literal.Texto}> is not defined");
                        }
                    }
                default:
                    {
                        throw new Exception($"! SEMANTIC ERROR : This expression cannot be drawn");
                    }
            }

        }
        private void Dibujar_Punto(Punto _punto, Pen lapiz)
        {
            int x1 = Convert.ToInt32(_punto.valor_x);
            int y1 = Convert.ToInt32(_punto.valor_y);

            GEOWALL_E.Papel.DrawEllipse(lapiz, x1 - 2, y1 - 2, 4, 4);
        }
        private void Dibujar_Circunsferencia(Circle _circle, Pen lapiz)
        {
            Punto centro = (Punto)_circle.Centro;
            Measure radio = (Measure)_circle.Radio;

            int x1 = Convert.ToInt32(centro.valor_x);
            int y1 = Convert.ToInt32(centro.valor_y);
            int measure = Convert.ToInt32(radio.Valor);

            GEOWALL_E.Papel.DrawEllipse(lapiz, x1 - measure, y1 - measure, measure * 2, measure * 2);
        }
        private void Dibujar_Segmento(Segment _segment, Pen lapiz)
        {
            Punto P1 = (Punto)_segment.P1;
            Punto P2 = (Punto)_segment.P2;

            int x1 = Convert.ToInt32(P1.valor_x);
            int y1 = Convert.ToInt32(P1.valor_y);
            int x2 = Convert.ToInt32(P2.valor_x);
            int y2 = Convert.ToInt32(P2.valor_y);

            GEOWALL_E.Papel.DrawLine(lapiz, x1, y1, x2, y2);
        }
        private void Dibujar_Linea(Line _line, Pen lapiz)
        {
            Punto p1 = (Punto)_line.P1;
            Punto p2 = (Punto)_line.P2;

            double x1 = Convert.ToInt32(p1.valor_x);
            double y1 = Convert.ToInt32(p1.valor_y);
            double x2 = Convert.ToInt32(p2.valor_x);
            double y2 = Convert.ToInt32(p2.valor_y);

            double pendiente = (y2 - y1) / (x2 - x1);
            double _n = y1 - pendiente * x1;

            double Yfinal = pendiente * 10000 + _n;

            GEOWALL_E.Papel.DrawLine(lapiz, 0, (int)_n, 10000, (int)Yfinal);
        }
        private void Dibujar_Rayo(Ray _ray, Pen lapiz) 
        {
            Punto p1 = (Punto)_ray.P1;
            Punto p2 = (Punto)_ray.P2;

            double x1 = Convert.ToInt32(p1.valor_x);
            double y1 = Convert.ToInt32(p1.valor_y);
            double x2 = Convert.ToInt32(p2.valor_x);
            double y2 = Convert.ToInt32(p2.valor_y);

            double pendiente = (y2 - y1) / (x2 - x1);

            double _n = y2 - pendiente * x2;

            if(x2 == x1 && y2 == y1)
            {

            }
            else if(x2 == x1 && y2 > y1) 
            {
                GEOWALL_E.Papel.DrawLine(lapiz, (int)x1, (int)y1, (int)x1, 10000);
            }
            else if(x2 == x1 && y2 < y1)
            {
                GEOWALL_E.Papel.DrawLine(lapiz, (int)x1, (int)y1, (int)x1, -10000);
            }
            else if(x2 > x1)
            {
                double Yfinal1 = pendiente * 10000 + _n;

                GEOWALL_E.Papel.DrawLine(lapiz, (int)x1, (int)y1, 10000, (int)Yfinal1);
            }
            else 
            {
                double Yfinal = pendiente * -10000 + _n;
                GEOWALL_E.Papel.DrawLine(lapiz, (int)x1, (int)y1, -10000, (int)Yfinal);
            }
        }

        //Arreglar-------------------------------------------------------------------
        private void Dibujar_Arco(Arc _arc, Pen lapiz)
        {
            Punto P1 = (Punto)_arc.P1;
            Punto P2 = (Punto)_arc.P2;
            Punto P3 = (Punto)_arc.P3;

            Measure measure = (Measure)_arc._Measure;

            //Tangente
            double M1 = (P3.valor_y - P1.valor_y) / (P3.valor_x - P1.valor_x);
            double angulo_inicial = Math.Atan(M1) * 180/Math.PI;

            double M2 = (P2.valor_y - P1.valor_y) / (P2.valor_x - P1.valor_x);
            double angulo_final = Math.Atan(M2) * 180/Math.PI;

            double angulo_entre_tangentes = Math.Atan((M2 - M1)/ 1 + M2 * M1) * 180/ Math.PI;

            if(angulo_final - angulo_inicial < 180)
            {
                GEOWALL_E.Papel.DrawArc(lapiz, (int)P1.valor_x - (int)measure.Valor, (int)P1.valor_y - (int)measure.Valor, 2 * (int)measure.Valor, 2 * (int)measure.Valor, (int)angulo_inicial, 360 - (int)angulo_entre_tangentes);
            }
            else GEOWALL_E.Papel.DrawArc(lapiz, (int)P1.valor_x - (int)measure.Valor, (int)P1.valor_y - (int)measure.Valor, 2 * (int)measure.Valor, 2 * (int)measure.Valor, (int)angulo_inicial, (int)angulo_entre_tangentes);


            //point p(600, 400);
            //point a(700, 300);
            //point q(600, 700);
            //draw ray(p, a);
            //draw ray(p, q);
            //draw arc(p, a, q, measure(p, a));
        }

    }
}
