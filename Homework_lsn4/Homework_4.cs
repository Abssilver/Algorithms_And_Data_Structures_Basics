using System;
using System.Collections.Generic;
using System.Linq;
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
            Console.ReadLine();
        }

        //*Количество маршрутов с препятствиями.
        //Реализовать чтение массива с препятствием и нахождение количество маршрутов.
        //Например, карта: 3x3
        //1 1 1
        //0 1 0
        //0 1 0
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
    }
}
