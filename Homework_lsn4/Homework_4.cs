using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

//Задание выполнил Ремизов Павел
namespace Homework_lsn4
{
    class Homework_4
    {
        static void Main(string[] args)
        {
            Routes((8, 8));
            LongestCommonSubsequence();
            Console.ReadLine();
        }

        //*Количество маршрутов с препятствиями.
        //Реализовать чтение массива с препятствием и нахождение количество маршрутов.
        //Например, карта: 3x3
        //1 1 1
        //0 1 0
        //0 1 0
        //Примечание: алгоритм учитывает возможность передвижения только вправо/вниз
        static int[,] GenerateField((int x, int y) size)
        {
            Random rnd = new Random();
            int[] probability = { 0, 1, 1, 1, 1 };
            int[,] matrixToReturn = new int[size.x, size.y];
            for (int i = 0; i < matrixToReturn.GetLength(0); i++)
                for (int j = 0; j < matrixToReturn.GetLength(1); j++)
                    matrixToReturn[i,j] = probability[rnd.Next(0, probability.Length)];
            matrixToReturn[0, 0] = 1;
            matrixToReturn[size.x - 1, size.y - 1] = 1;
            return matrixToReturn;
        }
        static void Routes((int x, int y) size)
        {
            int[,] matrixField = GenerateField(size);
            Console.WriteLine("Generated matrix field:");
            Display(matrixField);
            Console.WriteLine(new string('=', size.x * 6));
            int i, j;
            for (j = 1; j < matrixField.GetLength(1); j++)
                if (matrixField[0, j - 1] == 0)
                    matrixField[0, j] = matrixField[0, j - 1];
            for (i = 1; i < matrixField.GetLength(0); i++)
            {
                if (matrixField[i - 1, 0] == 0)
                    matrixField[i, 0] = matrixField[i - 1, 0];
                for (j = 1; j < matrixField.GetLength(1); j++)
                {
                    if (matrixField[i, j] != 0)
                        matrixField[i, j] = matrixField[i, j - 1] + matrixField[i - 1, j];
                }
            }
            Console.WriteLine("Route matrix field");
            Display(matrixField);
        }
        static void Display(int[,] array)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    Console.Write($"{array[i, j], 5} ");
                }
                Console.WriteLine();
            }
        }

        //Решить задачу о нахождении длины максимальной последовательности с помощью матрицы.
        static void LongestCommonSubsequence()
        {
            string firstSequence = "Выдра у выдры".ToUpper();
            string secondSequence = "норовила вырвать рыбку".ToUpper();
            char[,] charIntersection = new char[firstSequence.Length + 1, secondSequence.Length + 1];
            int[,] count = new int[firstSequence.Length + 1, secondSequence.Length + 1];

            for (int i = 0; i <= firstSequence.Length; i++)
            {
                count[i, 0] = 0;
                if (i<firstSequence.Length)
                    charIntersection[i + 1, 0] = firstSequence[i];
            }
            for (int i = 0; i <= secondSequence.Length; i++)
            {
                count[0, i] = 0;
                if (i < secondSequence.Length)
                    charIntersection[0, i + 1] = secondSequence[i];
            }
            for (int i = 1; i <= firstSequence.Length; i++)
            {
                for (int j = 1; j <= secondSequence.Length; j++)
                {
                    if (firstSequence[i - 1] == secondSequence[j - 1])
                    {
                        count[i, j] = count[i - 1, j - 1] + 1;
                        charIntersection[i, j] = 'X';
                    }
                    else if (count[i - 1, j] >= count[i, j - 1])
                    {
                        count[i, j] = count[i - 1, j];
                        charIntersection[i, j] = '^';
                    }
                    else
                    {
                        count[i, j] = count[i, j - 1];
                        charIntersection[i, j] = '<';
                    }
                }
            }
            DisplaySubsequence(firstSequence.Length, secondSequence.Length, charIntersection, firstSequence);
            Console.WriteLine();
            DisplayArray(count, "Number of Intersections: ");
            DisplayArray(charIntersection, "Direction array: ");
        }
        static void DisplayArray<T>(T[,] array, string msg)
        {
            Console.WriteLine(msg);
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    Console.Write(array[i, j] + " ");
                }
                Console.WriteLine();
            }
        }
        static void DisplaySubsequence(int firstSequenceLength, int secondSequenceLength, 
            char [,] charIntersection, string firstSequence)
        {
            if (firstSequenceLength == 0 || secondSequenceLength == 0)
            {
                Console.WriteLine();
                return;
            }
            if (charIntersection[firstSequenceLength, secondSequenceLength] == 'X')
            {
                charIntersection[firstSequenceLength, secondSequenceLength] = '0';
                DisplaySubsequence(firstSequenceLength - 1, secondSequenceLength - 1, charIntersection, firstSequence);
                Console.Write(firstSequence[firstSequenceLength - 1]);
            }
            else if (charIntersection[firstSequenceLength, secondSequenceLength] == '^')
            {
                charIntersection[firstSequenceLength, secondSequenceLength] = '0';
                DisplaySubsequence(firstSequenceLength - 1, secondSequenceLength, charIntersection, firstSequence);
            }
            else
            {
                charIntersection[firstSequenceLength, secondSequenceLength] = '0';
                DisplaySubsequence(firstSequenceLength, secondSequenceLength - 1, charIntersection, firstSequence);
            }
        }
    }
}
