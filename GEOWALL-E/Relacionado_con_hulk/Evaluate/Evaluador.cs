using GEOWALL_E.Relacionado_con_hulk.AST;
using GEOWALL_E.Relacionado_con_hulk.Colores;
using GEOWALL_E.Relacionado_con_hulk.Declaraciones_de_figuras;
using GEOWALL_E.Relacionado_con_hulk.Geometria;
using GEOWALL_E.Relacionado_con_hulk.Geometria.Draw_Functions;
using GEOWALL_E.Relacionado_con_hulk.Geometria.Intersections;
using GEOWALL_E.Relacionado_con_hulk.Geometria.Secuencias;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace GEOWALL_E
{
    class Evaluador
    {
        private int Contador;

        private readonly List<Expresion> _rama;

        private int _posicion;

        private Stack<Color> Colores = new Stack<Color>();

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
            Colores.Push(Color.Black);

            while (_posicion < _rama.Count)
            {
                Evaluar_Expresion(Verificandose);

                Proxima_Expresion();
            }
        }
        object Evaluar_Expresion(Expresion nodo)
        {
            Contador++;
            if (Contador > 10000000000)
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
                case Print:
                    {
                        Print _print = (Print)nodo;
                        var _expresion = Evaluar_Expresion(_print._Expresion);
                        MessageBox.Show(_expresion.ToString());
                        return _expresion;
                    }
                case In:
                    {
                        In _in = (In)nodo;
                        return Evaluar_Expresion(_in._expresion);
                    }
                case Parentesis:
                    {
                        Parentesis parentesis = (Parentesis)nodo;
                        return Evaluar_Expresion(parentesis.Expresion);
                    }

                //------------------------------------------------//SECUENCIAS//------------------------------------------------------//

                case Asignacion_Secuencia: return Evaluar_Expresion_Asignacion_Secuencia((Asignacion_Secuencia)nodo);
                case Count: return Evaluar_Expresion_Count((Count)nodo);
                case Point_Sequence: return Evaluar_Expresion_Point_Sequence((Point_Sequence)nodo);
                case Line_Sequence: return Evaluar_Expresion_Line_Sequence((Line_Sequence)nodo);
                case Circle_Sequence: return Evaluar_Expresion_Circle_Sequence((Circle_Sequence)nodo);
                case Ray_Sequence: return Evaluar_Expresion_Ray_Sequence((Ray_Sequence)nodo);
                case Segment_Sequence: return Evaluar_Expresion_Segment_Sequence((Segment_Sequence)nodo);
                case Secuencias<Expresion>: return Evaluar_Expresion_Secuencia((Secuencias<Expresion>)nodo);
                case Secuencia_Infinita<Expresion>: return Evaluar_Expresion_Secuencia_Infinita((Secuencia_Infinita<Expresion>)nodo);
                case Secuencia_Infinita<Punto>: return nodo;
                case Randoms: return nodo;
                case undefined: return nodo;

                //------------------------------------------------//GEOMETRIA//------------------------------------------------------//

                case Generacion_Punto: return Evaluar_Expresion_Punto((Generacion_Punto)nodo);
                case Measure: return Evaluar_Expresion_Measure((Measure)nodo);
                case Generar_Segmento: return Evaluar_Expresion_Segment((Generar_Segmento)nodo);
                case Generar_Circulo: return Evaluar_Expresion_Circle((Generar_Circulo)nodo);
                case Generar_Linea: return Evaluar_Expresion_Line((Generar_Linea)nodo);
                case Generar_Rayo: return Evaluar_Expresion_Ray((Generar_Rayo)nodo);
                case Generar_Arco: return Evaluar_Expresion_Arc((Generar_Arco)nodo);

                //----------------------------------------------// INTERSECCION //-----------------------------------------------------//

                case Generar_Inter: return Evaluar_Expresion_Intersect((Generar_Inter) nodo);

                //------------------------------------------------// Dibujo //-----------------------------------------------------//

                case Dibujar: return Evaluar_Expresion_Dibujar((Dibujar)nodo);
                case Cambiar_Color: return Cambiar_Color_Lapiz((Cambiar_Color)nodo);
                case Restore: return Color_Anterior();

                default: throw new Exception($"! SYNTAX ERROR : Unexpected node <{nodo}>");

                //----------------------------------------------------------------------------------------------------------------//
            }
        }
        private object Color_Anterior()
        {
            if (Colores.Count == 1) return null;
            else
            {
                Colores.Pop();
                return null;
            }
        }
        private object Cambiar_Color_Lapiz(Cambiar_Color cambiar_Color)
        {
            Colores.Push(cambiar_Color._Color);
            return null;
        }
        private static void Verificar_Asignacion_Identificadores(string a)
        {
           if(a != "_" && Biblioteca.Variables.ContainsKey(a) && Biblioteca.Pila.Count == 0)
               throw new Exception($"! SEMANTIC ERROR : Redefination of the constant <{a}>");
        }
        private static void Guardar_En_Biblioteca_Variables(string a, object b)
        {
            if (Biblioteca.Pila.Count == 0 && a != "underscore") Biblioteca.Variables[a] = b;
            else
            {
                if(a != "underscore") Biblioteca.Pila.Peek()[a] = b;
            } 
        }
        private static bool Existencia(Literal a)
        {
            if(!Biblioteca.Variables.ContainsKey(a._Literal.Texto)) return false;
            return true;
        }
        private static object Tomar_valor(Literal a)
        {
            if (Biblioteca.Pila.Count == 0) return Biblioteca.Variables[a._Literal.Texto];
            else return Biblioteca.Pila.Peek()[a._Literal.Texto];
        }

            private object Evaluar_Literal(Literal a)
        {
            if (a._Literal.Tipo == Tipo_De_Token.Identificador)
            {
                if (!Existencia(a) && Biblioteca.Pila.Count == 0) throw new Exception($"! SEMANTIC ERROR : Variable <{a._Literal.Texto}> is not defined");
                return Tomar_valor(a);
            }
            return a.Valor;
        }

        //////////////////////////////////////////INTERSECT//////////////////////////////////////////////////
        public object Evaluar_Expresion_Intersect(Generar_Inter inter)
        {
            var a = Evaluar_Expresion(inter.Expresion_1);
            var b = Evaluar_Expresion(inter.Expresion_2);
            var x = Intersections.IntersectionBetween((ILugarGeometrico)a, (ILugarGeometrico)b);
            return x;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
      
        private object Evaluar_Expresion_Let_in(Let_in g) 
        {

            var temp = new Dictionary<string, object>();

            if (Biblioteca.Pila.Count != 0) Copiar_Valores_De_Pila_En_Diccionario_Temporal(temp);


            Biblioteca.Pila.Push(temp);

            foreach (var check in g.Bloque)
            {
                Evaluar_Expresion(check);
            }

            var _in = Evaluar_Expresion(g._IN);

            Biblioteca.Pila.Pop();

            return _in;
        }
        private void Copiar_Valores_De_Pila_En_Diccionario_Temporal(Dictionary<string, object> pila)
        {
            foreach(var x in Biblioteca.Pila.Peek())
            {
                pila.Add(x.Key, x.Value);
            }
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

            if(condicion is Secuencias<object>)
            {
                Secuencias<object> secuencia = (Secuencias<object>) condicion;
                if(secuencia.Count == 0)
                {
                    return Evaluar_Expresion(j._Else);
                }
                else return Evaluar_Expresion(j._expresion);
            }
            else if(condicion is Secuencia_Infinita<double>)
            {
                Secuencia_Infinita<double> secuencia = (Secuencia_Infinita<double>)condicion;
                if( secuencia.Count == 0)
                {
                    return Evaluar_Expresion(j._Else);

                }
                else return Evaluar_Expresion(j._expresion);
            }
            else if (condicion is undefined ||  condicion is (double)0) return Evaluar_Expresion(j._Else);

            else return Evaluar_Expresion(j._expresion);
        }
        private object Evaluar_Expresion_Asignacion(Asignacion w)
        {
            var _expresion = Evaluar_Expresion(w._Expresion);
            Verificar_Asignacion_Identificadores(w.Identificador);
            Guardar_En_Biblioteca_Variables(w.Identificador, _expresion);
            return _expresion;
        }

        #region EVALUAR ASIGNACION DE SECUENCIAS
        private object Evaluar_Expresion_Asignacion_Secuencia(Asignacion_Secuencia y)
        {
            //En caso de que sea un literal se busca en el diccionario y hay q verificar segun el tipo 
            var _expresion = Evaluar_Expresion(y._Secuencia);
            if(_expresion is Generar_Inter)
            {
                Secuencias<object> _secuencia = (Secuencias<object>)Evaluar_Expresion(y._Secuencia);
                Secuencias<object> _resto = new Secuencias<object>();
                int marcador = 0;
                for (int i = 0; i < y.Identificadores.Count; i++)
                {
                    Verificar_Asignacion_Identificadores(y.Identificadores[i]);
                    if (i >= _secuencia.Count)
                    {
                        Guardar_En_Biblioteca_Variables(y.Identificadores[i], new undefined());
                    }
                    else
                    {
                        Guardar_En_Biblioteca_Variables(y.Identificadores[i], _secuencia[i]);
                        marcador++;
                    }
                }
                for (int i = marcador; i < _secuencia.Count; i++)
                {
                    _resto.Add(_secuencia[i]);
                }
                //resto
                Verificar_Asignacion_Identificadores(y.Identificador_resto_de_secuencia);
                Guardar_En_Biblioteca_Variables(y.Identificador_resto_de_secuencia, _resto);
            }
            else if (_expresion is undefined)
            {
                for (int i = 0; i < y.Identificadores.Count; i++)
                {
                    Verificar_Asignacion_Identificadores(y.Identificadores[i]);
                    Guardar_En_Biblioteca_Variables(y.Identificadores[i], new undefined());
                }
                Verificar_Asignacion_Identificadores(y.Identificador_resto_de_secuencia);
                Guardar_En_Biblioteca_Variables(y.Identificador_resto_de_secuencia, new Secuencias<object>());
            }
            //Secuencias numericas
            else if (_expresion is Secuencia_Infinita<Expresion>)
            {
                Secuencia_Infinita<double> secuencia_infinita = (Secuencia_Infinita<double>)Evaluar_Expresion(y._Secuencia);

                Secuencia_Infinita<double> _resto = new Secuencia_Infinita<double>();

                double marcador = 0;
                for (int i = 0; i < y.Identificadores.Count; i++)
                {
                    Verificar_Asignacion_Identificadores(y.Identificadores[i]);
                    // caso {1 ...}
                    if (secuencia_infinita.Count == 1)
                    {
                        Guardar_En_Biblioteca_Variables(y.Identificadores[i], secuencia_infinita[0] + i);
                        marcador = secuencia_infinita[0] + i + 1;
                    }
                    // caso {a ... b}
                    else
                    {
                        if ((secuencia_infinita[0] + i) > secuencia_infinita[secuencia_infinita.Count - 1])
                        {
                            Guardar_En_Biblioteca_Variables(y.Identificadores[i], new undefined());
                            marcador = 0;
                        }
                        else
                        {
                            Guardar_En_Biblioteca_Variables(y.Identificadores[i], secuencia_infinita[0] + i);
                            marcador = secuencia_infinita[0] + i + 1;
                        }
                    }
                }
                //case {1...} si habia cuatro constantes el resto seria {5...}
                if (secuencia_infinita.Count == 1)
                {
                    _resto.Add(marcador);
                    Verificar_Asignacion_Identificadores(y.Identificador_resto_de_secuencia);
                    Guardar_En_Biblioteca_Variables(y.Identificador_resto_de_secuencia, _resto);
                }
                else
                {
                    if (marcador == 0 || marcador >= secuencia_infinita[secuencia_infinita.Count - 1])
                    {
                        Verificar_Asignacion_Identificadores(y.Identificador_resto_de_secuencia);
                        Guardar_En_Biblioteca_Variables(y.Identificador_resto_de_secuencia, _resto);

                    }
                    else
                    {
                        _resto.Add(marcador);
                        _resto.Add(secuencia_infinita[secuencia_infinita.Count - 1]);
                        Verificar_Asignacion_Identificadores(y.Identificador_resto_de_secuencia);
                        Guardar_En_Biblioteca_Variables(y.Identificador_resto_de_secuencia, _resto);
                    }
                }
            }
            //caso para samples() y bueno despues points(f)
            else if (_expresion is Secuencia_Infinita<Punto>)
            {
                Secuencia_Infinita<Punto> _resto = new Secuencia_Infinita<Punto>();

                for (int i = 0; i < y.Identificadores.Count; i++)
                {
                    Verificar_Asignacion_Identificadores(y.Identificadores[i]);
                    Guardar_En_Biblioteca_Variables(y.Identificadores[i], new Punto());
                }
                _resto.Add(new Punto());
                Verificar_Asignacion_Identificadores(y.Identificador_resto_de_secuencia);
                Guardar_En_Biblioteca_Variables(y.Identificador_resto_de_secuencia, _resto);
            }
            else if (_expresion is Randoms)
            {
                Secuencia_Infinita<double> _resto = new Secuencia_Infinita<double>();

                Random random = new Random();

                for (int i = 0; i < y.Identificadores.Count; i++)
                {
                    Verificar_Asignacion_Identificadores(y.Identificadores[i]);
                    Guardar_En_Biblioteca_Variables(y.Identificadores[i], random.NextDouble());
                }
                _resto.Add(random.NextDouble());
                Verificar_Asignacion_Identificadores(y.Identificador_resto_de_secuencia);
                Guardar_En_Biblioteca_Variables(y.Identificador_resto_de_secuencia, _resto);
            }
            else if (_expresion is Secuencias<object>)
            {
                Secuencias<object> _secuencia = (Secuencias<object>)Evaluar_Expresion(y._Secuencia);

                Secuencias<object> _resto = new Secuencias<object>();

                int marcador = 0;
                for (int i = 0; i < y.Identificadores.Count; i++)
                {
                    Verificar_Asignacion_Identificadores(y.Identificadores[i]);
                    if (i >= _secuencia.Count)
                    {
                        Guardar_En_Biblioteca_Variables(y.Identificadores[i], new undefined());
                    }
                    else
                    {
                        Guardar_En_Biblioteca_Variables(y.Identificadores[i], _secuencia[i]);
                        marcador++;
                    }
                }
                for (int i = marcador; i < _secuencia.Count; i++)
                {
                    _resto.Add(_secuencia[i]);
                }
                //resto
                Verificar_Asignacion_Identificadores(y.Identificador_resto_de_secuencia);
                Guardar_En_Biblioteca_Variables(y.Identificador_resto_de_secuencia, _resto);
            }
            else if(_expresion is Expresion_Binaria)
            {
                var expresion = Evaluar_Expresion(y._Secuencia);

                if (expresion is undefined)
                {
                    for (int i = 0; i < y.Identificadores.Count; i++)
                    {
                        Verificar_Asignacion_Identificadores(y.Identificadores[i]);
                        Guardar_En_Biblioteca_Variables(y.Identificadores[i], new undefined());
                    }
                    Verificar_Asignacion_Identificadores(y.Identificador_resto_de_secuencia);
                    Guardar_En_Biblioteca_Variables(y.Identificador_resto_de_secuencia, new Secuencias<object>());
                }
                else if (expresion is Secuencias<object>)
                {
                    Secuencias<object> _secuencia = (Secuencias<object>)expresion;

                    Secuencias<object> _resto = new Secuencias<object>();

                    int marcador = 0;
                    for (int i = 0; i < y.Identificadores.Count; i++)
                    {
                        Verificar_Asignacion_Identificadores(y.Identificadores[i]);
                        if (i >= _secuencia.Count)
                        {
                            Guardar_En_Biblioteca_Variables(y.Identificadores[i], new undefined());
                        }
                        else
                        {
                            Guardar_En_Biblioteca_Variables(y.Identificadores[i], _secuencia[i]);
                            marcador++;
                        }
                    }
                    for (int i = marcador; i < _secuencia.Count; i++)
                    {
                        _resto.Add(_secuencia[i]);
                    }
                    //resto
                    Verificar_Asignacion_Identificadores(y.Identificador_resto_de_secuencia);
                    Guardar_En_Biblioteca_Variables(y.Identificador_resto_de_secuencia, _resto);
                }
                else throw new Exception($"! SYNTAX ERROR : Cannot match this <{expresion.GetType().Name}>");
            }
            else throw new Exception($"! SYNTAX ERROR : Cannot match this <{y._Secuencia.GetType().Name}>");

            return null;
        }
        #endregion

        private object Evaluar_Expresion_Secuencia(Secuencias<Expresion> z)
        {
            var _secuencia = new Secuencias<object>();
            for (int i = 0; i < z.Count; i++)
            {
                _secuencia.Add(Evaluar_Expresion(z[i]));
            }
            return _secuencia;
            
        }
        private object Evaluar_Expresion_Secuencia_Infinita(Secuencia_Infinita<Expresion> z)
        {
            var _secuecia_infinita = new Secuencia_Infinita<double>();
            for(int i = 0; i < z.Count; i++)
            {
                var item = Evaluar_Expresion(z[i]);
                if (item is double)
                {
                    _secuecia_infinita.Add((double) item);
                }
                else throw new Exception($"! SEMANTIC ERROR : Infinite sequences cannot have <{item.GetType().Name}>");
            }
            return _secuecia_infinita;
        }
        private object Evaluar_Expresion_Count(Count count)
        {
            if(count._Secuencia is Literal)
            {
                var expresion = Tomar_valor((Literal)count._Secuencia);

                if(expresion is Secuencias<object>)
                {
                    Secuencias<object> _secuencia = (Secuencias<object>)expresion;
                    return _secuencia.Count;
                }
                else if(expresion is Secuencia_Infinita<double> || expresion is Secuencia_Infinita<Punto>)
                {
                    return new undefined();
                }

                else throw new Exception($"! SEMANTIC ERROR : This {expresion.GetType().Name} does not have the count property");
            }
            else if(count._Secuencia is undefined)
            {
                return new undefined();
            }
            else throw new Exception($"! SEMANTIC ERROR : Count function cannot take this argument");
        }

        #region  EVALUAR SECUENCIAS DE PUNTOS, LINEAS, CIRCULOS, RAYOS Y SEGMENTOS
        private object Evaluar_Expresion_Point_Sequence(Point_Sequence point_Sequence)
        {
            Guardar_En_Biblioteca_Variables(point_Sequence.Identificador, point_Sequence._Secuencias_Evaluada);
            return null;
        }
        private object Evaluar_Expresion_Line_Sequence(Line_Sequence line_Sequence)
        {
            Guardar_En_Biblioteca_Variables(line_Sequence.Identificador, line_Sequence._Secuencias_Evaluada);
            return null;
        }
        private object Evaluar_Expresion_Circle_Sequence(Circle_Sequence circle_Sequence)
        {
            Guardar_En_Biblioteca_Variables(circle_Sequence.Identificador, circle_Sequence._Secuencias_Evaluada);
            return null;
        }
        private object Evaluar_Expresion_Ray_Sequence(Ray_Sequence ray_Sequence)
        {
            Guardar_En_Biblioteca_Variables(ray_Sequence.Identificador, ray_Sequence._Secuencias_Evaluada);
            return null;
        }
        private object Evaluar_Expresion_Segment_Sequence(Segment_Sequence segment_Sequence)
        {
            Guardar_En_Biblioteca_Variables(segment_Sequence.Identificador, segment_Sequence._Secuencias_Evaluada);
            return null;
        }

        #endregion

        #region EVALUAR EXPRESIONES BINARIAS

        private object Evaluar_Expresion_Binaria(Expresion_Binaria b)
        {
            var left = Evaluar_Expresion(b.Left);
            var right = Evaluar_Expresion(b.Right);

            switch (b.Operador.Tipo)
            {
                case Tipo_De_Token.resto:
                    {
                        if(left is double && right is double)
                        {
                            return (double)left % (double)right;
                        }
                        else throw new Exception($"! SEMANTIC ERROR : Invalid expression: Can't operate <{left.GetType().Name}> with <{right.GetType().Name}> using <{b.Operador.Texto}>");
                    }
                case Tipo_De_Token.concatenacion:
                    {
                        if(left is string && right is string)
                        {
                            return (string)left + (string)right;
                        }
                        else throw new Exception($"! SEMANTIC ERROR : Invalid expression: Can't operate <{left.GetType().Name}> with <{right.GetType().Name}> using <{b.Operador.Texto}>");
                    }
                case Tipo_De_Token.Suma:
                    {
                        if (left is Sequence && right is Sequence)
                        {
                            return Concatenacion_Secuencias.Concatenar((Sequence)left, (Sequence)right);
                        }
                        else if (left is undefined && right is Secuencias<object>)
                        {
                            return left;
                        }
                        else if (left is Secuencias<object> && right is undefined)
                        {
                            return left;
                        }
                        else if (left is double && right is double)
                        {
                            return (double)left + (double)right;
                        }
                        else if (left is Measure && right is Measure)
                        {
                            Measure m1 = (Measure)left;
                            Measure m2 = (Measure)right;
                            var valor = m1.Valor + m2.Valor;
                            return new Measure(valor);
                        }
                        else throw new Exception($"! SEMANTIC ERROR : Invalid expression: Can't operate <{left.GetType().Name}> with <{right.GetType().Name}> using <{b.Operador.Texto}>");
                    }

                case Tipo_De_Token.Resta:
                    {
                        if(left is double && right is double)
                        {
                            return (double)left - (double)right;
                        }
                        else if (left is Measure && right is Measure)
                        {
                            Measure m1 = (Measure)left;
                            Measure m2 = (Measure)right;
                            var valor = Math.Abs(m1.Valor - m2.Valor);
                            return new Measure(valor);
                        }
                        else throw new Exception($"! SEMANTIC ERROR : Invalid expression: Can't operate <{left.GetType().Name}> with <{right.GetType().Name}> using <{b.Operador.Texto}>");
                    }

                case Tipo_De_Token.Producto:
                    {
                        if(left is double && right is double) 
                        {
                            return (double)left * (double)right;
                        }
                        else if (left is double && right is Measure)
                        {
                            double natural = (double)left;
                            Measure m = (Measure)right;
                            
                            var valor = natural * m.Valor;
                            return new Measure(valor);
                        }
                        else if (left is Measure && right is double)
                        {
                            Measure m = (Measure)left;
                            double natural = (double)right;

                            var valor = natural * m.Valor;
                            return new Measure(valor);
                        }
                        else throw new Exception($"! SEMANTIC ERROR : Invalid expression: Can't operate <{left.GetType().Name}> with <{right.GetType().Name}> using <{b.Operador.Texto}>");
                    }

                case Tipo_De_Token.Division:
                    {
                        if( left is double && right is double)
                        {
                            if ((double)right == 0) throw new Exception($"! SEMANTIC ERROR : Cannot divide <{left}> by <{right}>");
                            else return (double)left / (double)right;
                        }
                        else if (left is Measure && right is Measure)
                        {
                            Measure m1 = (Measure)left;
                            Measure m2 = (Measure)right;
                            var valor = m1.Valor / m2.Valor;
                            return valor;
                        }
                        else throw new Exception($"! SEMANTIC ERROR : Invalid expression: Can't operate <{left.GetType().Name}> with <{right.GetType().Name}> using <{b.Operador.Texto}>");
                    }

                case Tipo_De_Token.Potenciacion:
                    {
                        if(left is double && right is double)
                        {
                            if ((double)left == 0 && (double)right == 0) throw new Exception($"! SEMANTIC ERROR : <{left}> pow to <{right}> is not defined");
                            else return Math.Pow((double)left, (double)right);
                        }
                        else throw new Exception($"! SEMANTIC ERROR : Invalid expression: Can't operate <{left.GetType().Name}> with <{right.GetType().Name}> using <{b.Operador.Texto}>");
                    }

                case Tipo_De_Token.AmpersandAmpersand:
                    {
                        if (left is 0 || right is 0) return 0;

                        else if (left is undefined || right is undefined) return 0;

                        else if (left is Secuencias<object>)
                        {
                            Secuencias<object> secuencia = (Secuencias<object>)left;
                            if (secuencia.Count == 0) return 0;
                            else return 1;
                        }
                        else if (right is Secuencias<object>)
                        {
                            Secuencias<object> secuencia = (Secuencias<object>)right;
                            if (secuencia.Count == 0) return 0;
                            else return 1;
                        }
                        else return 1;
                    }

                case Tipo_De_Token.PipePipe:
                    {
                        if (left is 0 && right is 0) return 0;

                        else if (left is undefined && right is undefined) return 0;

                        else if (left is Secuencias<object> && right is Secuencias<object>)
                        {
                            Secuencias<object> secuencia_1 = (Secuencias<object>)left;
                            Secuencias<object> secuencia_2 = (Secuencias<object>)right;
                            if (secuencia_1.Count == 0 && secuencia_2.Count == 0) return 0;
                            else return 1;
                        }
                        else return 1;
                    }

                case Tipo_De_Token.Menor_que:
                    {
                        if(left is double && right is double) 
                        {
                            if((double)left < (double)right) return (double) 1;
                            return (double) 0;
                        }
                        else if (left is Measure && right is Measure)
                        {
                            Measure m1 = (Measure)left;
                            Measure m2 = (Measure)right;
                            if (m1.Valor < m2.Valor)
                            {
                                return (double)1;
                            }
                            else return (double)0;
                        }
                        else throw new Exception($"! SEMANTIC ERROR : Invalid expression: Can't operate <{left.GetType().Name}> with <{right.GetType().Name}> using <{b.Operador.Texto}>");
                    }
                case Tipo_De_Token.Menor_igual_que:
                    {
                        if (left is double && right is double)
                        {
                            if ((double)left <= (double)right) return (double)1;
                            return (double)0;
                        }
                        else if (left is Measure && right is Measure)
                        {
                            Measure m1 = (Measure)left;
                            Measure m2 = (Measure)right;
                            if (m1.Valor <= m2.Valor)
                            {
                                return (double)1;
                            }
                            else return (double)0;
                        }
                        else throw new Exception($"! SEMANTIC ERROR : Invalid expression: Can't operate <{left.GetType().Name}> with <{right.GetType().Name}> using <{b.Operador.Texto}>");
                    }
                case Tipo_De_Token.Mayor_que:
                    {
                        if (left is double && right is double)
                        {
                            if ((double)left > (double)right) return (double)1;
                            return (double)0;
                        }
                        else if (left is Measure && right is Measure)
                        {
                            Measure m1 = (Measure)left;
                            Measure m2 = (Measure)right;
                            if (m1.Valor > m2.Valor)
                            {
                                return (double)1;
                            }
                            else return (double)0;
                        }
                        else throw new Exception($"! SEMANTIC ERROR : Invalid expression: Can't operate <{left.GetType().Name}> with <{right.GetType().Name}> using <{b.Operador.Texto}>");
                    }
                case Tipo_De_Token.Mayor_igual_que:
                    {
                        if (left is double && right is double)
                        {
                            if ((double)left >= (double)right) return (double)1;
                            return (double)0;
                        }
                        else if(left is Measure && right is Measure)
                        {
                            Measure m1 = (Measure)left;
                            Measure m2 = (Measure)right;
                            if (m1.Valor >= m2.Valor)
                            {
                                return (double)1;
                            }
                            else return (double)0;
                        }
                        else throw new Exception($"! SEMANTIC ERROR : Invalid expression: Can't operate <{left.GetType().Name}> with <{right.GetType().Name}> using <{b.Operador.Texto}>");
                    }
                case Tipo_De_Token.IgualIgual:
                    {
                        if (left is Measure && right is Measure)
                        {
                            Measure m1 = (Measure)left;
                            Measure m2 = (Measure)right;
                            if (m1.Valor == m2.Valor)
                            {
                                return (double)1;
                            }
                            else return (double)0;
                        }
                        else if(left is double && right is double)
                        {
                            if (Equals(left, right)) return (double)1;
                            return (double)0;
                        }
                        else throw new Exception($"! SEMANTIC ERROR : Invalid expression: Can't operate <{left.GetType().Name}> with <{right.GetType().Name}> using <{b.Operador.Texto}>");
                    }
                case Tipo_De_Token.Bang_Igual:
                    {
                        if (left is Measure && right is Measure)
                        {
                            Measure m1 = (Measure)left;
                            Measure m2 = (Measure)right;
                            if (m1.Valor != m2.Valor)
                            {
                                return (double)1;
                            }
                            else return (double)0;
                        }
                        else if( left is double && right is double)
                        {
                            if (!Equals(left, right)) return (double)1;
                            return (double)0;
                        }
                        else throw new Exception($"! SEMANTIC ERROR : Invalid expression: Can't operate <{left.GetType().Name}> with <{right.GetType().Name}> using <{b.Operador.Texto}>");
                    }
                default: throw new Exception($"! SEMANTIC ERROR : Unexpected binary operator <{b.Operador.Tipo}>");
            }
        }
        #endregion 




        ///////////////////////////////////////////////METODOS GEOMETRIA/////////////////////////////////////////////////////

        private object Evaluar_Expresion_Punto(Generacion_Punto l)
        {
            if (l.Componente_x is null && l.Componente_y is null)
            {
                Verificar_Asignacion_Identificadores(l.Identificador);
                Punto punto = new Punto(); 
                Guardar_En_Biblioteca_Variables(l.Identificador, punto);
                return punto;
            }

            var valor_x = Evaluar_Expresion(l.Componente_x);
            var valor_y = Evaluar_Expresion(l.Componente_y);

            int x1 = Convert.ToInt32(valor_x);
            int y1 = Convert.ToInt32(valor_y);

            if (l.Identificador is null) return new Punto(x1, y1);

            Verificar_Asignacion_Identificadores(l.Identificador);
            Guardar_En_Biblioteca_Variables(l.Identificador, new Punto(x1, y1));

            return new Punto(x1, y1);
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
                throw new Exception($"! FUNCTION ERROR : Measure function takes two arguments points");
            }
        }
        private object Evaluar_Expresion_Segment(Generar_Segmento q)
        {   if(q.P1 is null && q.P2 is null)
            {
                Verificar_Asignacion_Identificadores(q.Identificador);
                Segment _segment = new Segment( new Punto(), new Punto());
                Guardar_En_Biblioteca_Variables(q.Identificador, _segment);
                return _segment;
            }
            var P1 = Evaluar_Expresion(q.P1);
            var P2 = Evaluar_Expresion(q.P2);
            if (P1 is Punto && P2 is Punto)
            {
                if (q.Identificador is null)
                {
                    return new Segment((Punto)P1,(Punto) P2);
                }
                Segment _segment = new Segment((Punto)P1, (Punto)P2);
                Verificar_Asignacion_Identificadores(q.Identificador);
                Guardar_En_Biblioteca_Variables(q.Identificador, _segment);
                return _segment;
            }
            throw new Exception($"! FUNCTION ERROR : Segment function takes two arguments points");
        }
        private object Evaluar_Expresion_Circle(Generar_Circulo r)
        {
            if(r.Centro is null && r.Radio is null)
            {
                Verificar_Asignacion_Identificadores(r.Identificador);
                Circle circulo = new Circle(r.Identificador);
                Guardar_En_Biblioteca_Variables(r.Identificador, circulo);
                return circulo;
            }
             var centro = Evaluar_Expresion(r.Centro);
             var radio = Evaluar_Expresion(r.Radio);
             if(centro is Punto && radio is Measure)
             {
                if (r.Identificador is null)
                {
                    return new Circle((Punto)centro, (Measure)radio);
                }
                Circle circulo = new Circle((Punto)centro, (Measure)radio);
                Verificar_Asignacion_Identificadores(r.Identificador);
                Guardar_En_Biblioteca_Variables(r.Identificador, circulo);
                return circulo;
             } 
             else
             {
                throw new Exception($"! FUNCTION ERROR : Circle function takes a point and an argument measure");
             }
        }
        private object Evaluar_Expresion_Line(Generar_Linea s)
        {
            if(s.P1 is null && s.P2 is null)
            {
                Verificar_Asignacion_Identificadores(s.Identificador);
                Line line = new Line(s.Identificador);
                Guardar_En_Biblioteca_Variables(s.Identificador, line);
                return line;
            }
            var P1 = Evaluar_Expresion(s.P1);
            var P2 = Evaluar_Expresion(s.P2);
            if (P1 is Punto && P2 is Punto)
            {
                if (s.Identificador is null)
                {
                    return new Line((Punto)P1,(Punto) P2);
                }
                Line _line = new Line((Punto)P1,(Punto)P2);
                Verificar_Asignacion_Identificadores(s.Identificador);
                Guardar_En_Biblioteca_Variables(s.Identificador, _line);
                return _line;
            }
            else
            {
                throw new Exception($"! FUNCTION ERROR : Line function takes two arguments points");
            }
        }
        private object Evaluar_Expresion_Ray(Generar_Rayo t)
        {
            if(t.P1 is null &&  t.P2 is null)
            {
                Verificar_Asignacion_Identificadores(t.Identificador);
                Ray _ray = new Ray(new Punto(), new Punto());
                Guardar_En_Biblioteca_Variables(t.Identificador, _ray);
                return _ray;
            }
            var P1 = Evaluar_Expresion(t.P1);
            var P2 = Evaluar_Expresion(t.P2);
            if (P1 is Punto && P2 is Punto)
            {
                if (t.Identificador is null)
                {
                    return new Ray((Punto)P1,(Punto)P2);
                }
                Ray _ray = new Ray((Punto)P1, (Punto)P2);
                Verificar_Asignacion_Identificadores(t.Identificador);
                Guardar_En_Biblioteca_Variables(t.Identificador, _ray);
                return _ray;
            }
            else
            {
                throw new Exception($"! FUNCTION ERROR : Ray function takes two arguments points");
            }
        }
        private object Evaluar_Expresion_Arc(Generar_Arco v)
        {
            if(v.P1 is null && v.P2 is null && v.P3 is null && v._Measure is null)
            {
                Random random = new Random(); 
                Verificar_Asignacion_Identificadores(v.Identificador);
                Arc _arc = new Arc(new Punto(), new Punto(), new Punto(), new Measure(random.Next(0, 100)));
                Guardar_En_Biblioteca_Variables(v.Identificador, _arc);
                return _arc;
            }
            var P1 = Evaluar_Expresion(v.P1);
            var P2 = Evaluar_Expresion(v.P2);
            var P3 = Evaluar_Expresion(v.P3);
            var measure = Evaluar_Expresion(v._Measure);
            if(P1 is Punto &&  P2 is Punto && P3 is Punto && measure is Measure)
            {
                if(v.Identificador is null) 
                {
                    return new Arc((Punto)P1, (Punto)P2, (Punto)P3, (Measure)measure);
                }
                Arc _arc = new Arc((Punto)P1, (Punto)P2, (Punto)P3, (Measure)measure);
                Verificar_Asignacion_Identificadores(v.Identificador);
                Guardar_En_Biblioteca_Variables(v.Identificador, _arc);
                return _arc;
            }
            else
            {
                throw new Exception($"! FUNCTION ERROR : Arc function takes three points and an argument measure");
            }
        }
        #region DIBUJAR FIGURAS
        private object Evaluar_Expresion_Dibujar(Dibujar p)
        {
            Pen lapiz = new Pen(Colores.Peek(), 2);

            var a = Evaluar_Expresion(p._Expresion);

            GEOWALL_E.Papel.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            switch (a)
            {
                case Punto:
                    {
                        Punto _punto = (Punto)Evaluar_Expresion(p._Expresion);
                        Dibujar_Punto(_punto, lapiz);
                        if (p.Etiqueta is not null) Crear_Etiqueta((int)_punto.valor_x,(int) _punto.valor_y, p.Etiqueta);

                        return null;
                    }
                case Arc:
                    {
                        Arc _arc = (Arc)Evaluar_Expresion(p._Expresion);
                        Dibujar_Arco(_arc, lapiz);
                        return null;
                    }
                case Circle:
                    {
                        Circle _circle = (Circle)Evaluar_Expresion(p._Expresion);
                        Dibujar_Circunsferencia(_circle, lapiz);
                        return null;
                    }
                case Line:
                    {
                        Line _line = (Line)Evaluar_Expresion(p._Expresion);
                        Dibujar_Linea(_line, lapiz);
                        return null;
                    }
                case Ray:
                    {
                        Ray _ray = (Ray)Evaluar_Expresion(p._Expresion);
                        Dibujar_Rayo(_ray, lapiz);
                        return null;
                    }
                case Segment:
                    {
                        Segment _segment = (Segment)Evaluar_Expresion(p._Expresion);
                        Dibujar_Segmento(_segment, lapiz);
                        return null;
                    }
                case Secuencias<object>:
                    {
                        Secuencias<object> _secuencia = (Secuencias<object>)Evaluar_Expresion(p._Expresion);
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
                            else if (_secuencia[i] is Arc)
                            {
                                Dibujar_Arco((Arc)_secuencia[i], lapiz);
                            }
                            else if (_secuencia[i] is Ray)
                            {
                                Dibujar_Rayo((Ray)_secuencia[i], lapiz);
                            }
                            else throw new Exception($"! SEMANTIC ERROR : <{_secuencia[i].GetType().Name}> cannot be draw");
                        }
                        return null;
                    }
                case undefined:
                    {
                        return null;
                    }
                default: throw new Exception($"! SEMANTIC ERROR : <{p._Expresion.GetType().Name}> cannot be draw");
            }

        }
        #endregion 

        private void Crear_Etiqueta(int valor_x, int valor_y, string identificador)
        {
            Font drawFont = new Font("Arial", 10);
            SolidBrush drawBrush = new SolidBrush(Color.Black);

            Point location = new Point(valor_x, valor_y);
            //visualizar etiqueta
            GEOWALL_E.Papel.DrawString(identificador, drawFont, drawBrush, location);
        }

        private void Dibujar_Punto(Punto _punto, Pen lapiz)
        {
           PointF punto= new PointF((float)_punto.valor_x,(float)_punto.valor_y);

            GEOWALL_E.Papel.DrawEllipse(lapiz, punto.X - 1, punto.Y, 2, 2);
        }
        private void Dibujar_Circunsferencia(Circle _circle, Pen lapiz)
        {
            Punto centro = (Punto)_circle.Centro;
            Measure radio = (Measure)_circle.Radio;

            PointF punto = new PointF((float)centro.valor_x, (float)centro.valor_y);
            float measure = (float) radio.Valor;

            GEOWALL_E.Papel.DrawEllipse(lapiz, punto.X - measure, punto.Y - measure, measure * 2, measure * 2);
        }
        private void Dibujar_Segmento(Segment _segment, Pen lapiz)
        {
            Punto P1 = (Punto)_segment.P1;
            Punto P2 = (Punto)_segment.P2;

            PointF punto_1 = new PointF((float)P1.valor_x, (float)P1.valor_y);
            PointF punto_2 = new PointF((float)P2.valor_x, (float)P2.valor_y);

            GEOWALL_E.Papel.DrawLine(lapiz, punto_1.X, punto_1.Y, punto_2.X, punto_2.Y);
        }
        private void Dibujar_Linea(Line _line, Pen lapiz)
        {
            Punto p1 = (Punto)_line.P1;
            Punto p2 = (Punto)_line.P2;

            PointF punto_1 = new PointF((float)p1.valor_x, (float)p1.valor_y);
            PointF punto_2 = new PointF((float)p2.valor_x, (float)p2.valor_y);

            float pendiente = (punto_2.Y - punto_1.Y) / (punto_2.X - punto_1.X);
            float _n = punto_1.Y - pendiente * punto_1.X;

            float Yfinal = pendiente * 10000 + _n;

            GEOWALL_E.Papel.DrawLine(lapiz, 0, _n, 10000, Yfinal);
        }
        private void Dibujar_Rayo(Ray _ray, Pen lapiz) 
        {
            Punto p1 = (Punto)_ray.P1;
            Punto p2 = (Punto)_ray.P2;

            PointF punto_1 = new PointF((float)p1.valor_x, (float)p1.valor_y);
            PointF punto_2 = new PointF((float)p2.valor_x, (float)p2.valor_y);


            float pendiente = (punto_2.Y - punto_1.Y) / (punto_2.X - punto_1.X);
            float _n = punto_1.Y - pendiente * punto_1.X;


            if (punto_2.X == punto_1.X && punto_2.Y == punto_1.Y)
            {

            }
            else if(punto_2.X == punto_1.X && punto_2.Y > punto_1.Y) 
            {
                GEOWALL_E.Papel.DrawLine(lapiz, punto_1.X, punto_1.Y, punto_1.X, 10000);
            }
            else if(punto_2.X == punto_1.X && punto_2.Y < punto_1.Y)
            {
                GEOWALL_E.Papel.DrawLine(lapiz, punto_1.X, punto_1.Y, punto_1.X, -10000);
            }
            else if(punto_2.X > punto_1.X)
            {
                float Yfinal1 = pendiente * 10000 + _n;

                GEOWALL_E.Papel.DrawLine(lapiz, punto_1.X, punto_1.Y, 10000, Yfinal1);
            }
            else 
            {
                float Yfinal = pendiente * -10000 + _n;
                GEOWALL_E.Papel.DrawLine(lapiz, punto_1.X, punto_1.Y, -10000, Yfinal);
            }
        }

        private void Dibujar_Arco(Arc _arc, Pen lapiz)
        {
            Punto P1 = (Punto)_arc.P1;
            Punto P2 = (Punto)_arc.P2;
            Punto P3 = (Punto)_arc.P3;

            Measure measure = (Measure)_arc._Measure;

            //Tangente

            double diferencial_y_M1 = P2.valor_y - P1.valor_y;
            double diferencial_x_M1 = P2.valor_x - P1.valor_x;
            

            double M1 = diferencial_y_M1 / diferencial_x_M1;

            double angulo_final;

            if(diferencial_y_M1 < 0 && diferencial_x_M1 > 0) 
            {
                angulo_final =(Math.Atan(M1) + 2*Math.PI) * 180 / Math.PI;
            }
            else if(diferencial_y_M1 > 0 && diferencial_x_M1 < 0)
            {
                angulo_final = (Math.Atan(M1) + Math.PI) * 180 / Math.PI;
            }
            else angulo_final = Math.Atan(M1) * 180 / Math.PI;


            double diferencial_y_M2 = P3.valor_y - P1.valor_y;
            double diferencial_x_M2 = P3.valor_x - P1.valor_x;

            double M2 = diferencial_y_M2 / diferencial_x_M2;

            double angulo_inicial;

            if (diferencial_y_M2 < 0 && diferencial_x_M2 > 0)
            {
                angulo_inicial = (Math.Atan(M2) + 2*Math.PI) * 180 / Math.PI;
            }
            else if (diferencial_y_M2 > 0 && diferencial_x_M2 < 0)
            {
                angulo_inicial = (Math.Atan(M2) + Math.PI) * 180 / Math.PI;
            }
            else angulo_inicial = Math.Atan(M2) * 180 / Math.PI;

            // double angulo_entre_tangentes = Math.Atan((M2 - M1) / 1 + M2 * M1) * 180 / Math.PI;

            double diferencia = angulo_final - angulo_inicial;

            if (angulo_final - angulo_inicial < 0)
            {
                GEOWALL_E.Papel.DrawArc(lapiz, (int)P1.valor_x - (int)measure.Valor, (int)P1.valor_y - (int)measure.Valor, 2 * (int)measure.Valor, 2 * (int)measure.Valor, (int)(angulo_inicial), (int)diferencia + 360);
            }
            else GEOWALL_E.Papel.DrawArc(lapiz, (int)P1.valor_x - (int)measure.Valor, (int)P1.valor_y - (int)measure.Valor, 2 * (int)measure.Valor, 2 * (int)measure.Valor, (int)angulo_inicial, (int)diferencia);


            //Prueba arcos

            //point p;
            //point a;
            //point q;
            //color blue;
            //draw ray(p, a);
            //draw ray(p, q);
            //restore;
            //m = measure(p, a);
            //draw circle(p, m);
            //color red;
            //draw arc(p, q, a, m);
        }

    }
}
