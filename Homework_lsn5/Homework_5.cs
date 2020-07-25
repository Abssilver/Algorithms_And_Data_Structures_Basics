using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

//Задание выполнил Ремизов Павел
namespace Homework_lsn5
{
    class Homework_5
    {
        static void Main(string[] args)
        {
            ConvertToBinary(166);
            BracketsValidator("[2/{5*(4+7)}]");
            BracketsValidator("33*{5*[4+7}]");
            ListCopy();
            ReversePolishNotationAlgorithm("3 + 4 * 2 / ( 1 - 5 ) ^ 2 ^ 3");
            ReversePolishNotationAlgorithm("5 * 6 + ( 2 - 9 )");
            QueueDemonstration();
            Console.ReadLine();
        }

        //Реализовать перевод из десятичной в двоичную систему счисления с использованием стека.
        static void ConvertToBinary(int number)
        {
            TStack<int> binary = new TStack<int>();
            while (number >= 2)
            {
                binary.Push(number % 2);
                number /= 2;
            }
            binary.Push(number);
            binary.PrintStackData();
        }
        public class TNode<T>
        {
            public T Value { get; set; }
            public TNode<T> Next { get; set; }
        }
        public class TStack<T>
        {
            TNode<T> _head;
            public int Count { get; private set; } = 0;
            public void Push(T element)
            {
                TNode<T> next = new TNode<T> { Value = element };
                next.Next = _head;
                _head = next;
                Count++;
            }
            public T Pop()
            {
                if (Count == 0)
                    throw new ArgumentOutOfRangeException("Stack is empty!");
                TNode<T> temp = _head;
                _head = _head.Next;
                Count--;
                return temp.Value;
            }
            public void PrintStackData()
            {
                TNode<T> current = _head;
                while (current != null)
                {
                    Console.Write(current.Value);
                    current = current.Next;
                }
                Console.WriteLine();
            }
        }

        //Написать программу, которая определяет, является ли введенная скобочная последовательность правильной.
        //Примеры правильных скобочных выражений: (), ([])(), { }(), ([{}]), 
        //неправильных — )(, ()) ({), (, ])}), ([(]) для скобок [, (, {.
        //Например: (2+(2*2)) или [2/{5*(4+7)}]
        static void BracketsValidator(string input)
        {
            TStack<char> sequence = new TStack<char>();
            bool validate = true;
            char[] openBracketsPull = { '[', '{', '(' };
            char[] closeBracketsPull = { ']', '}', ')' };

            for (int i = 0; i < input.Length; i++)
            {
                if (openBracketsPull.Contains(input[i]))
                    sequence.Push(input[i]);
                else if (closeBracketsPull.Contains(input[i]))
                {
                    try
                    {
                        char toCompare = sequence.Pop();
                        if (Array.IndexOf(openBracketsPull, toCompare) != (Array.IndexOf(closeBracketsPull, input[i])))
                        {
                            validate = false;
                            break;
                        }
                    }
                    catch (Exception)
                    {
                        validate = false;
                        break;
                    }
                }
            }
            Console.WriteLine($"Input: {input}");
            Console.WriteLine("{0}", validate ? "Test passed" : "Test failed");
        }

        //*Создать функцию, копирующую односвязный список (то есть создающую в памяти копию
        //односвязного списка без удаления первого списка).
        static void ListCopy()
        {
            LinkedList<int> intList = new LinkedList<int>();
            intList.Add(1);
            intList.Add(2);
            intList.Add(3);
            intList.Add(4);
            intList.Add(5);
            intList.Add(6);
            intList.Add(7);
            intList.Add(8);
            intList.Add(9);
            intList.Add(10);
            LinkedList<int> copyList = intList.GetCopy();
            intList.Remove(8);
            intList.PrintData();
            intList.Clear();
            copyList.PrintData();
        }
        public class LinkedList<T>
        {
            TNode<T> _head;
            TNode<T> _tail;
            int _count;

            public void Add(T element)
            {
                TNode<T> next = new TNode<T> { Value = element };
                if (_head == null)
                    _head = next;
                else
                    _tail.Next = next;
                _tail = next;
                _count++;
            }
            public bool Remove(T element)
            {
                TNode<T> current = _head;
                TNode<T> previous = null;

                while (current != null)
                {
                    if (current.Value.Equals(element))
                    {
                        if (previous != null)
                        {
                            previous.Next = current.Next;
                            if (current.Next == null)
                                _tail = previous;
                        }
                        else
                        {
                            _head = _head.Next;
                            if (_head == null)
                                _tail = null;
                        }
                        _count--;
                        return true;
                    }
                    previous = current;
                    current = current.Next;
                }
                return false;
            }
            public void Clear()
            {
                _head = null;
                _tail = null;
                _count = 0;
            }
            public LinkedList<T> GetCopy()
            {
                LinkedList<T> copyList = new LinkedList<T>();
                TNode<T> current = _head;
                while (current != null)
                {
                    copyList.Add(current.Value);
                    current = current.Next;
                }
                return copyList;
            }
            public void PrintData()
            {
                TNode<T> current = _head;
                while (current != null)
                {
                    Console.Write(current.Value + " ");
                    current = current.Next;
                }
                Console.WriteLine();
            }
        }

        //*Реализовать алгоритм перевода из инфиксной записи арифметического выражения в постфиксную
        static void ReversePolishNotationAlgorithm(string input)
        {
            Console.WriteLine($"Input: {input}");
            Console.Write("Result: ");
            TStack<char> operatorStack = new TStack<char>();
            bool operatorCheck;
            char toCheckWith;
            for (int i = 0; i < input.Length; i++)
            {
                operatorCheck = false;
                if (input[i] == ' ') continue;
                if (IsOperator(input[i]))
                {
                    while (operatorStack.Count > 0 && !operatorCheck)
                    {
                        toCheckWith = operatorStack.Pop();
                        if (input[i] == ')')
                        {
                            if (toCheckWith != '(')
                            {
                                Console.Write($"{toCheckWith} ");
                                continue;
                            }
                        }
                        else if (input[i] == '(')
                        {
                            operatorStack.Push(toCheckWith);
                            operatorStack.Push(input[i]);
                            operatorCheck = true;
                            continue;
                        }
                        if (toCheckWith == '(')
                        {
                            operatorCheck = true;
                            if (input[i] != ')')
                            {
                                operatorStack.Push(toCheckWith);
                                operatorStack.Push(input[i]);
                            }
                            continue;
                        }
                        operatorCheck = PriorityCheck(input[i], toCheckWith);
                        if (!operatorCheck)
                            Console.Write($"{toCheckWith} ");
                        else
                        {
                            operatorStack.Push(toCheckWith);
                            operatorStack.Push(input[i]);
                        }
                    }
                    if (operatorStack.Count == 0 && input[i] != ')')
                        operatorStack.Push(input[i]);
                }
                else
                    Console.Write($"{input[i]} ");
            }
            while (operatorStack.Count != 0)
                Console.Write(operatorStack.Pop() + " ");
            Console.WriteLine();
        }
        static bool IsOperator(char toCheck)
        {
            char[] operators = { '+', '-', '*', '/', '^' };
            char[] brackets = { '(', ')' };
            return operators.Contains(toCheck) || brackets.Contains(toCheck);
        }
        static bool PriorityCheck(char toCheck, char checkWith)
        {
            Dictionary<char, int> precedence = new Dictionary<char, int>
            {
                ['+'] = 2,
                ['-'] = 2,
                ['*'] = 3,
                ['/'] = 3,
                ['^'] = 4
            };
            if (precedence[checkWith] == 4 & precedence[toCheck] == 4)
                return true;
            else
                return precedence[checkWith] < precedence[toCheck];
        }

        //Реализовать очередь:
        //1. С использованием массива.
        //2. *С использованием односвязного списка.
        static void QueueDemonstration()
        {
            TQueueArray<int> arrayQueue = new TQueueArray<int>(5);
            arrayQueue.Enqueue(5);//5
            arrayQueue.Enqueue(4);//54
            arrayQueue.Dequeue();//4
            arrayQueue.Enqueue(3);//43
            arrayQueue.Enqueue(2);//432
            arrayQueue.Dequeue();//32
            arrayQueue.Dequeue();//2
            arrayQueue.Enqueue(1);//21
            arrayQueue.Enqueue(0);//210
            arrayQueue.PrintAllElements();
            TQueueList<int> listQueue = new TQueueList<int>();
            listQueue.Enqueue(10);//10
            listQueue.Enqueue(5);//10 5
            listQueue.Dequeue();//5
            listQueue.Enqueue(25);//5 25
            listQueue.Enqueue(8);//5 25 8
            listQueue.Dequeue();//25 8
            listQueue.Enqueue(12);//25 8 12
            listQueue.Enqueue(8);//25 8 12 8
            listQueue.Enqueue(25);//25 8 12 8 25
            listQueue.PrintAllElements();
        }
        public class TQueueNodeArray<T>
        {
            public T Value { get; set; }
        }
        public class TQueueArray<T>
        {
            int _head = 0;
            int _tail = 0;
            int _count = 0;
            TQueueNodeArray<T>[] _elementArray;

            public TQueueArray(int size)
            {
                _elementArray = new TQueueNodeArray<T>[size];
            }
            public void Enqueue(T element)
            {
                TQueueNodeArray<T> newElement = new TQueueNodeArray<T>() { Value = element };
                if (_count == 0)
                {
                    _elementArray[_head] = newElement;
                    _head = 0;
                    _tail = _head;
                    _count++;
                }
                else
                {
                    if (_count + 1 >= _elementArray.Length)
                        throw new ArgumentOutOfRangeException("The queue is full");
                    if (++_tail > _elementArray.Length - 1)
                        _tail = 0;
                    _elementArray[_tail] = newElement;
                    _count++;
                }
            }
            public T Dequeue()
            {
                if (_count == 0)
                    throw new ArgumentOutOfRangeException("The queue is empty");
                T toReturn = _elementArray[_head].Value;
                _elementArray[_head] = null;
                if (++_head > _elementArray.Length - 1)
                    _head = 0;
                _count--;
                return toReturn;
            }
            public void PrintAllElements()
            {
                int head = _head;
                int tail = _tail;
                while (head!=tail)
                {
                    Console.Write($"{_elementArray[head].Value} ");
                    if (++head > _elementArray.Length - 1)
                        head = 0;
                }
                if (_elementArray[head] != null)
                    Console.Write($"{_elementArray[head].Value} ");
                Console.WriteLine();
            }
        }

        public class TQueueList<T>
        {
            TNode<T> _head=null;
            TNode<T> _tail=null;
            int _count = 0;

            public void Enqueue(T element)
            {
                TNode<T> next = new TNode<T> { Value = element };
                if (_head == null)
                {
                    _head = next;
                    _head.Next = _tail;
                    _tail = _head;
                }
                else
                {
                    _tail.Next = next;
                    _tail = next;
                }
                _count++;
            }
            public T Dequeue()
            {
                if (_count == 0) 
                    throw new InvalidOperationException();
                T toReturn = _head.Value;
                TNode<T> temp = _head.Next;
                _head = null;
                _head = temp;
                _count--;
                return toReturn;
            }
            public void PrintAllElements()
            {
                TNode<T> toPrint = new TNode<T> { Value = _head.Value };
                toPrint.Next = _head.Next;
                Console.Write($"{toPrint.Value} ");
                while (toPrint.Next!=null)
                {
                    toPrint.Value = toPrint.Next.Value;
                    Console.Write($"{toPrint.Value} ");
                    toPrint.Next = toPrint.Next.Next;
                }
                Console.WriteLine();
            }
        }
    }
}

