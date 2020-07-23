using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            int _count = 0;

            public void Push(T element)
            {
                TNode<T> next = new TNode<T> { Value = element};
                next.Next = _head;
                _head = next;
                _count++;
            }
            public T Pop()
            {
                if (_count == 0)
                    throw new ArgumentOutOfRangeException("Stack is empty!");
                TNode<T> temp = _head;
                _head = _head.Next;
                _count--;
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
            Console.WriteLine("{0}", validate? "Test passed" : "Test failed");
        }
    }
}
