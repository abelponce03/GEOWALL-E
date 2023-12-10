using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEOWALL_E.Relacionado_con_hulk.Geometria
{
    class Secuencias : Expresion
    {
        private Node<Expresion> first;
        private Node<Expresion> last;
        private int count;

        public Expresion this[int index]
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

        private Node<Expresion> FindNode(int index)
        {
            Node<Expresion> current = first;
            for (int i = 0; i < index; i++)
            {
                current = current.Next;
            }
            return current;
        }

        public void Add(Expresion item)
        {
            if (count == 0)
            {
                first = last = new Node<Expresion>(item, null);
            }
            else
            {
                last = last.Next = new Node<Expresion>(item, null);
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
    }
}
