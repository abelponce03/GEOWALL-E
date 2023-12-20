using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEOWALL_E.Relacionado_con_hulk.Geometria.Secuencias
{
    internal class Secuencia_Infinita<T> : Sequence
    {
        public override Tipo_De_Token Tipo => Tipo_De_Token.secuencia_infinita_Expresion;

        private Node<T> first;
        private Node<T> last;
        private int count;

        public T this[int index]
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

        public int Count => count;

        public bool IsReadOnly { get; }

        private Node<T> FindNode(int index)
        {
            Node<T> current = first;
            for (int i = 0; i < index; i++)
            {
                current = current.Next;
            }
            return current;
        }

        public void Add(T item)
        {
            if (count == 0)
            {
                first = last = new Node<T>(item, null);
            }
            else
            {
                last = last.Next = new Node<T>(item, null);
            }
            count++;
        }
    }
}
