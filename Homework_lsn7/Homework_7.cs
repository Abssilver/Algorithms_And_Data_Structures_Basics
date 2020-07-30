using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Задание выполнил Ремизов Павел
namespace Homework_lsn7
{
    class Homework_7
    {
        static void Main(string[] args)
        {
            ReadFileData("matrix.txt");
            Console.WriteLine();
            DepthFirstSearch();
            Console.WriteLine();
            BreadthFirstSearch();
            Console.ReadLine();
        }

        //Написать функции, которые считывают матрицу смежности из файла и выводят ее на экран.
        static void ReadFileData(string filename)
        {
            List<string> filedata = new List<string>();
            if (File.Exists(filename))
            {
                Console.WriteLine("Loading data...");
                foreach (var line in File.ReadLines(filename))
                    filedata.Add(line);
                int[,] adjacencyMatrix = new int[filedata.Count, filedata.Count];
                int index = 0;
                bool convertCompleted = false;
                char[] separator = { ' ' };
                foreach (var dataLine in filedata)
                {
                    string [] data = dataLine.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                    if (data.Length.Equals(filedata.Count))
                    {
                        for (int i = 0; i < data.Length; i++)
                        {
                            convertCompleted = int.TryParse(data[i], out adjacencyMatrix[index, i]);
                            if (!convertCompleted)
                                break;
                        }
                        index++;
                    }
                    else
                    {
                        Console.WriteLine("The amount of data does not meet the requirements.");
                        break;
                    }
                    if (!convertCompleted)
                    {
                        Console.WriteLine("The attempt to load data failed.");
                        break;
                    }
                }
                if (convertCompleted)
                {
                    Console.Write(new string(' ', 5));
                    for (int i = 0; i < filedata.Count; i++)
                        Console.Write("{0,5}", String.Concat((char)(65 + i), '(', i, ')'));
                    Console.WriteLine();
                    for (int i = 0; i < adjacencyMatrix.GetLength(0); i++)
                    {
                        Console.Write("{0,5}", String.Concat((char)(65 + i), '(', i, ')'));
                        for (int j = 0; j < adjacencyMatrix.GetLength(1); j++) 
                            Console.Write("{0,5}", adjacencyMatrix[i, j] == -1 ? "inf" : adjacencyMatrix[i, j].ToString());
                        Console.WriteLine();
                    }
                }
            }
            else
            {
                Console.WriteLine("File does not exist!");
            }
        }

        //Написать рекурсивную функцию обхода графа в глубину.
        class Node
        {
            public int Vertex { get; set; }
            public bool Visited { get; set; }
            public void AddEdge(Node toAdd) => Edges.Add(toAdd);
            public List<Node> Edges { get; } = new List<Node>();
        }
        static void DepthFirstSearch()
        {
            Node first = new Node { Vertex = 1, Visited = false };
            Node second = new Node { Vertex = 2, Visited = false };
            Node third = new Node { Vertex = 3, Visited = false };
            Node fourth = new Node { Vertex = 4, Visited = false };
            Node fifth = new Node { Vertex = 5, Visited = false };
            Node sixth = new Node { Vertex = 6, Visited = false };
            Node seventh = new Node { Vertex = 7, Visited = false };
            Node eigth = new Node { Vertex = 8, Visited = false };

            first.AddEdge(second);
            first.AddEdge(fourth);
            second.AddEdge(first);
            second.AddEdge(third);
            second.AddEdge(fourth);
            third.AddEdge(second);
            third.AddEdge(fifth);
            fourth.AddEdge(first);
            fourth.AddEdge(sixth);
            fifth.AddEdge(third);
            fifth.AddEdge(seventh);
            sixth.AddEdge(fourth);
            sixth.AddEdge(seventh);
            sixth.AddEdge(eigth);
            seventh.AddEdge(fifth);
            seventh.AddEdge(sixth);
            eigth.AddEdge(sixth);

            //      1  - 2 - 3 - 5
            //      |    |       |
            //      |    |    8  7
            //       \   |     \ |
            //        -  4 - - - 6

            Console.WriteLine("Depth-FirstSearch:");
            DFS(first);
        }
        static void DFS(Node toSearchIn)
        {
            if (toSearchIn.Visited)
                return;
            else
            {
                toSearchIn.Visited = true;
                Console.WriteLine($"Visiting {toSearchIn.Vertex} vertex.");
                foreach (var edge in toSearchIn.Edges)
                    DFS(edge);
            }
        }

        //Написать функцию обхода графа в ширину.
        static void BreadthFirstSearch()
        {
            Node first = new Node { Vertex = 1, Visited = false };
            Node second = new Node { Vertex = 2, Visited = false };
            Node third = new Node { Vertex = 3, Visited = false };
            Node fourth = new Node { Vertex = 4, Visited = false };
            Node fifth = new Node { Vertex = 5, Visited = false };
            Node sixth = new Node { Vertex = 6, Visited = false };
            Node seventh = new Node { Vertex = 7, Visited = false };
            Node eigth = new Node { Vertex = 8, Visited = false };

            Queue<Node> traverse = new Queue<Node>();

            first.AddEdge(second);
            first.AddEdge(third);
            second.AddEdge(first);
            second.AddEdge(fourth);
            third.AddEdge(first);
            third.AddEdge(fourth);
            fourth.AddEdge(second);
            fourth.AddEdge(third);
            fourth.AddEdge(fifth);
            fifth.AddEdge(fourth);
            fifth.AddEdge(sixth);
            fifth.AddEdge(eigth);
            sixth.AddEdge(fifth);
            sixth.AddEdge(seventh);
            seventh.AddEdge(sixth);
            eigth.AddEdge(fifth);

            //      1 - 2 - 4 - 5 - 6 - 7
            //       \     /     \
            //        3 --        8

            Console.WriteLine("Breadth-FirstSearch:");
            traverse.Enqueue(first);
            first.Visited = true;
            while (traverse.Count>0)
            {
                Node step = traverse.Dequeue();
                Console.WriteLine($"Visiting {step.Vertex} vertex.");
                foreach (var edge in step.Edges)
                    if (!edge.Visited)
                    {
                        traverse.Enqueue(edge);
                        edge.Visited = true;
                    }
            }
        }
    }
}
