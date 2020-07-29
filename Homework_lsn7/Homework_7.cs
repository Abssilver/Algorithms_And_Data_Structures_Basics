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
    }
}
