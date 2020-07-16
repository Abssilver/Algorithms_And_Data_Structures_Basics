using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

//Задание выполнил Ремизов Павел
namespace Homework_lsn3
{
    class Homework_3
    {
        static void Swap<T>(ref T first, ref T second)
        {
            T temp = first;
            first = second;
            second = temp;
        }
        static void Main(string[] args)
        {
            int numberOfElements = 25;
            Random rnd = new Random();

            int[] arrayToSort = new int[numberOfElements];
            int[] copyArray = new int[numberOfElements];
            for (int i = 0; i < arrayToSort.Length; i++)
                arrayToSort[i] = rnd.Next(-20, 20);
            
            arrayToSort.CopyTo(copyArray, 0);

            Console.WriteLine("An array before sorting");
            PrintArray(arrayToSort);

            Console.WriteLine("An array after buble sorting");
            BubbleSort(ref arrayToSort);
            PrintArray(arrayToSort);

            Console.WriteLine("An array after upgrade buble sorting");
            BubbleSortUpgated(ref copyArray);
            PrintArray(copyArray);
            Console.ReadLine();
        }

        static void PrintArray(int[] array)
        {
            Console.WriteLine("Printing an array");
            foreach (var element in array)
                Console.Write(element + " ");
            Console.WriteLine();
        }
        
        static void BubbleSort(ref int[] array)
        {
            int iterations = 0, compares = 0, swaps = 0;
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = 0; j < array.Length-1; j++)
                {
                    ++iterations;
                    ++compares;
                    if (array[j]>array[j+1])
                    {
                        ++swaps;
                        Swap(ref array[j], ref array[j + 1]);
                    }
                }
            }
            Console.WriteLine($"Iterations: {iterations}, compares: {compares}, swaps: {swaps}");
        }

        //Попробовать оптимизировать пузырьковую сортировку. 
        //Сравнить количество операций сравнения оптимизированной и не оптимизированной программы.
        //Написать функции сортировки, которые возвращают количество операций.
        static void BubbleSortUpgated(ref int[] array)
        {
            bool changed = true;
            int iterations = 0, compares = 0, swaps = 0;
            for (int i = 0; i < array.Length; i++)
            {
                changed = false;
                for (int j = 0; j < array.Length - i - 1; j++)
                {
                    ++iterations;
                    ++compares;
                    if (array[j] > array[j + 1])
                    {
                        ++swaps;
                        Swap(ref array[j], ref array[j + 1]);
                        changed = true;
                    }
                }
                ++compares;
                if (!changed)
                    break;
            }
            Console.WriteLine($"Iterations: {iterations}, compares: {compares}, swaps: {swaps}");
        }
    }
}
