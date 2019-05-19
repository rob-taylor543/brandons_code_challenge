using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FunStuff;

namespace RobsList
{
    interface IPhone
    {
        void CallSomeone();
    }

    public class Phone : IPhone
    {
        public string Name { get; set; }
        public Phone(string name)
        {
            Name = name;
        }


        public void CallSomeone()
        {

        }

        public void MyMethod()
        {

        }


    }

    public class BetterPhone : IPhone
    {
        public void CallSomeone()
        {

        }
    }

    class Program
    {
        static void Main(string[] args)
        {

            try
            {
                var enumList = new List<char>("Hello World");
                Console.WriteLine($"Capacity: {enumList.Capacity}");
                enumList.Clear();
                Console.WriteLine($"Count after clear: {enumList.Count}");
                Console.WriteLine($"Capacity after clear: {enumList.Capacity}");
                enumList.Capacity = 3;
                Console.WriteLine($"Capacity after set: {enumList.Capacity}");
                enumList.Add('a');
                enumList[0] = 'b';
                Console.WriteLine($"Capacity: {enumList.Capacity}, Count: {enumList.Count}, {enumList[0]}");

                var phoneList = new RobsList<Phone>();
                Phone phoneA = new Phone("A");
                Phone phoneB = new Phone("b");
                Phone phoneC = new Phone("C");
                phoneList.Add(phoneA);
                Console.WriteLine($"Capacity: {phoneList.Capacity}, Count: {phoneList.Count}");

                phoneList.Add(phoneB);
                Console.WriteLine($"Capacity: {phoneList.Capacity}, Count: {phoneList.Count}");

                phoneList.Add(phoneC);
                Console.WriteLine($"Capacity: {phoneList.Capacity}, Count: {phoneList.Count}");

                phoneList.Remove(phoneC);
                Console.WriteLine($"Capacity: {phoneList.Capacity}, Count: {phoneList.Count}");



                Console.WriteLine(phoneList[0].Name);
                Console.WriteLine(phoneList[1].Name);

                Console.WriteLine(phoneList.IndexOf(phoneC));
                Console.WriteLine(phoneList.Contains(phoneC));
                Console.WriteLine(phoneList.Contains(phoneA));
                Console.WriteLine(phoneList.Contains(phoneB));


                phoneList.RemoveAt(1);
                Console.WriteLine(phoneList.Contains(phoneB));

                Console.WriteLine($"Capacity: {phoneList.Capacity}, Count: {phoneList.Count}");

  
                Console.ReadLine();

            }
            catch (Exception ex)
            {
            }
        }

        public void InstanceMethod()
        {

        }

        public static void Foo(IPhone phone)
        {
            phone.CallSomeone();

        }

        public static int Add(int one, int two)
        {
            return one + two;
        }

    }

    public class BetterStringList : StringList
    {
        public void Food()
        {
            //_something = "hi";
        }
    }

    public class StringList
    {
        public void Add(string str)
        {

        }

        private string _something;

        public string Something
        {
            get
            {
                return _something;
            }
            set { _something = value; }
        }


        public string GetSomething()
        {
            return _something;
        }

        public void SetSomething(string s)
        {
            _something = s;
        }
    }

    public class RobsList<T> : IList<T>
    {
        private T[] _elements;

        public RobsList()
        {
            _elements = new T[2];
            Count = 0;
        }
        public T this[int index] //properties
        {
            get
            {
                if (index < Count)
                {
                    return _elements[index];
                }
                throw new ArgumentOutOfRangeException();
            }
            set
            {
                if (index < Count)
                {
                    _elements[index] = value;
                }
                throw new ArgumentOutOfRangeException();
            }
        }

        public int Count { get; private set; }

        public int Capacity
        {
            get => _elements.Length;
            set
            {
                if (value < Count)
                {
                    throw new ArgumentOutOfRangeException();
                }
                else
                {
                    T[] newArray = new T[value];
                    int counter = 0;
                    foreach (T element in _elements)
                    {
                        newArray[counter++] = element;
                    }
                    _elements = newArray;
                }
            }
        }

        public bool IsReadOnly => false;

        public void Add(T item)
        {
            if (Count < Capacity)
            {
                _elements[Count++] = item;
            }
            else
            {
                Capacity *= 2;
                _elements[Count++] = item;
            }
        }

        public void Clear()
        {
            _elements = new T[Capacity];
            Count = 0;
        }

        public bool Contains(T item)
        {
            bool contains = false;
            for (int i=0; i < Count; i++)
            {
                if (Object.ReferenceEquals(_elements[i], item))
                {
                    contains = true;
                    break;
                }
            }
            return contains;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            int counter = arrayIndex;
            for (int i=0; i<Count; i++)
            {
                array[counter++] = _elements[i];
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public int IndexOf(T item)
        {
            int elementIndex = -1;
            for (int i = 0; i < Count; i++)
            {
                if (Object.ReferenceEquals(_elements[i], item))
                {
                    elementIndex = i;
                    break;
                }
            }
            return elementIndex;
        }

        public void Insert(int index, T item)
        {
            if ((index < Count) && (index >= 0))
            {
                if (Count + 1 <= Capacity)
                {
                    Count++;
                    for (int i = Count - 1; i > index; i--)
                    {
                        _elements[i] = _elements[i - 1];
                    }
                    _elements[index] = item;
                }
                else
                {
                    Capacity *= 2;
                    Count++;
                    for (int i = Count - 1; i > index; i--)
                    {
                        _elements[i] = _elements[i - 1];
                    }
                    _elements[index] = item;
                }
            }
        }

        public bool Remove(T item)
        {
            if (Contains(item))
            {
                int index = IndexOf(item);
                for (int i = index; i < Count - 1; i++)
                {
                    _elements[i] = _elements[i + 1];
                }
                Count--;
                return true;
            }
            return false;
        }

        public void RemoveAt(int index)
        {
            if (index>=0 && index < Count)
            {
                for (int i = index; i < Count - 1; i++)
                {
                    _elements[i] = _elements[i + 1];
                }
                Count--;
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
