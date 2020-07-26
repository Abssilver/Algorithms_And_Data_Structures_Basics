using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

//Задание выполнил Ремизов Павел
namespace Homework_lsn6
{
    class Homework_6
    {
        static void Main(string[] args)
        {
            Console.WriteLine(SimpleHash("How can a clam cram in a clean cream can?"));
            Console.WriteLine(SimpleHash("Six sick hicks nick six slick bricks with picks and sticks."));
            BinarySearchTree(15);
            Console.ReadLine();
        }
        //Реализовать простейшую хэш-функцию. 
        //На вход функции подается строка, на выходе сумма кодов символов
        static string SimpleHash(string input)
        {
            int result = 0;
            //int multiplier = 29;
            for (int i = 0; i < input.Length; i++)
            {
                result += Convert.ToInt32(input[i]);
                //result += (int)Math.Pow(multiplier, input.Length - i - 1) * (int)(input[i]);
                if (result >= 1_000_000_000)
                    result -= 1_000_000_000;
            }
            return string.Format("{0:d9}", result);
        }

        //Переписать программу, реализующее двоичное дерево поиска.
        //а) Добавить в него обход дерева различными способами;
        //б) Реализовать поиск в двоичном дереве поиска;
        public class Vertex
        {
            public int Value { get; set; }
            public Vertex Left { get; set; }
            public Vertex Right { get; set; }
            public Vertex Root { get; set; }
        }
        static void DisplayTreeInBracketNotation(Vertex toPrint)
        {
            if (toPrint != null)
            {
                Console.Write($"{toPrint.Value}");
                if (toPrint.Left != null || toPrint.Right != null)
                {
                    Console.Write("(");
                    if (toPrint.Left != null)
                        DisplayTreeInBracketNotation(toPrint.Left);
                    else
                        Console.Write("Null");
                    Console.Write(",");
                    if (toPrint.Right != null)
                        DisplayTreeInBracketNotation(toPrint.Right);
                    else
                        Console.Write("Null");
                    Console.Write(")");
                }
            }
        }
        static Vertex GetEmptyVertex(int value, Vertex root) =>
            new Vertex()
            {
                Value = value,
                Left = null,
                Right = null,
                Root = root,
            };
        static void Insert(ref Vertex toInsert, int value)
        {
            Vertex tmp = null;
            if (toInsert == null)
            {
                toInsert = GetEmptyVertex(value, null);
                return;
            }
            tmp = toInsert;
            while (tmp != null)
            {
                if (value > tmp.Value)
                {
                    if (tmp.Right != null)
                    {
                        tmp = tmp.Right;
                        continue;
                    }
                    else
                    {
                        tmp.Right = GetEmptyVertex(value, tmp);
                        return;
                    }
                }
                else if (value < tmp.Value)
                {
                    if (tmp.Left != null)
                    {
                        tmp = tmp.Left;
                        continue;
                    }
                    else
                    {
                        tmp.Left = GetEmptyVertex(value, tmp);
                        return;
                    }
                }
                else
                {
                    Console.WriteLine("Дерево построено неправильно!");
                    tmp = null;
                }
            }
        }
        static void PreOrderTreeTraversal(Vertex toPrint)
        {
            if (toPrint != null)
            {
                Console.Write($"{toPrint.Value} ");
                PreOrderTreeTraversal(toPrint.Left);
                PreOrderTreeTraversal(toPrint.Right);
            }
        }
        static void InOrderTreeTraversal(Vertex toPrint)
        {
            if (toPrint != null)
            {
                InOrderTreeTraversal(toPrint.Left);
                Console.Write($"{toPrint.Value} ");
                InOrderTreeTraversal(toPrint.Right);
            }
        }
        static void ReverseInOrderTreeTraversal(Vertex toPrint)
        {
            if (toPrint != null)
            {
                ReverseInOrderTreeTraversal(toPrint.Right);
                Console.Write($"{toPrint.Value} ");
                ReverseInOrderTreeTraversal(toPrint.Left);
            }
        }
        static void PostOrderTreeTraversal(Vertex toPrint)
        {
            if (toPrint != null)
            {
                PostOrderTreeTraversal(toPrint.Left);
                PostOrderTreeTraversal(toPrint.Right);
                Console.Write($"{toPrint.Value} ");
            }
        }
        static Vertex SearchVertex(int value, Vertex compareWith)
        {
            Vertex toReturn = null;
            if (compareWith!=null)
            {
                if (compareWith.Value > value)
                    toReturn = SearchVertex(value, compareWith.Left);
                else if (compareWith.Value < value)
                    toReturn = SearchVertex(value, compareWith.Right);
                else 
                    return compareWith;
            }
            return toReturn;
        }
        static void BinarySearchTree(int numberOfElements)
        {
            Vertex tree = null;
            List<int> sequence = GenerateSequence(numberOfElements);
            sequence.ForEach(x => Insert(ref tree, x));
            Console.WriteLine("PreOrderTreeTraversal:");
            PreOrderTreeTraversal(tree);
            Console.WriteLine();
            Console.WriteLine("InOrderTreeTraversal:");
            InOrderTreeTraversal(tree);
            Console.WriteLine();
            Console.WriteLine("ReverseInOrderTreeTraversal:");
            ReverseInOrderTreeTraversal(tree);
            Console.WriteLine();
            Console.WriteLine("PostOrderTreeTraversal:");
            PostOrderTreeTraversal(tree);
            Console.WriteLine();
            DisplayTreeInBracketNotation(tree);
            Console.WriteLine();
            Console.WriteLine("Searching 45...");
            if (SearchVertex(45, tree)!=null)
                Console.WriteLine("Found!");
            else
                Console.WriteLine("Not found!");
        }
        static List<int> GenerateSequence(int length)
        {
            if (length <= 0)
                return null;
            List<int> toReturn = new List<int>();
            int root = 25;
            toReturn.Add(root);
            int valuesAdded = 1;
            Random rnd = new Random();
            while (valuesAdded < length)
            {
                int valueToAdd = rnd.Next(0, root * 2);
                if (!toReturn.Contains(valueToAdd))
                {
                    toReturn.Add(valueToAdd);
                    valuesAdded++;
                }
            }
            return toReturn;
        }
    }
}
