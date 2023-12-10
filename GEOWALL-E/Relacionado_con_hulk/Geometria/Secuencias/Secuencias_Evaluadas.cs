using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEOWALL_E.Relacionado_con_hulk.Geometria
{
    class Secuencias_Evaluada : Expresion
    {

        private Node<object> first;
        private Node<object> last;
        private int count;

        public object this[int index]
        {
            get
            {
                if (index < 0 || index >= count) throw new IndexOutOfRangeException();
                return FindNode(index).Value;
            }
            set
            {
                if (index < 0 || index >= count) throw new IndexOutOfRangeException();
                FindNode(index).Value = value;
            }
        }

        public override Tipo_De_Token Tipo => Tipo_De_Token.secuencia_Expresion;

        public int Count => count;

        public bool IsReadOnly { get; }

        private Node<object> FindNode(int index)
        {
            Node<object> current = first;
            for (int i = 0; i < index; i++)
            {
                current = current.Next;
            }
            return current;
        }

        public void Add(object item)
        {
            if (count == 0)
            {
                first = last = new Node<object>(item, null);
            }
            else
            {
                last = last.Next = new Node<object>(item, null);
            }
            count++;
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= count) throw new IndexOutOfRangeException();

            if (Count == 1)
            {
                first = last = null;
            }
            else if (index == 0)
            {
                first = first.Next;
            }
            else
            {
                var node = FindNode(index - 1);
                node.Next = node.Next.Next;
                if (index == Count - 1)
                {
                    last = node;
                }
            }
            count--;
        }

        //probar despues
        public void Concatenar_con(Secuencias_Evaluada secuencia)
        {
            if (Count == 0)
            {
                //Resultado es undefined
            }
            else if (secuencia.Count == 0)
            {
                //Resultado es la primera secuencia
            }
            else
            {
                for (int i = 0; i < secuencia.Count; i++)
                {
                    last = last.Next = new Node<object>(secuencia.first.Value, null);
                    secuencia.first = secuencia.first.Next;
                }
            }
        }
    }
}
