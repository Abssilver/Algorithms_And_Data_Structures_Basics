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
            int[] cocktailArray = new int[numberOfElements];
            for (int i = 0; i < arrayToSort.Length; i++)
                arrayToSort[i] = rnd.Next(-20, 20);
            
            arrayToSort.CopyTo(copyArray, 0);
            arrayToSort.CopyTo(cocktailArray, 0);
            Console.WriteLine("An array before sorting");
            PrintArray(arrayToSort);

            Console.WriteLine("\nAn array after silly bubble sorting");
            SillyBubbleSort(ref arrayToSort);
            PrintArray(arrayToSort);

            Console.WriteLine("\nAn array after upgrade bubble sorting");
            BubbleSortUpgated(ref copyArray);
            PrintArray(copyArray);

            Console.WriteLine("\nAn array after cocktail sorting");
            CocktailSort(ref cocktailArray);
            PrintArray(cocktailArray);

            Console.WriteLine("\nA binary search of element 10:");
            Console.WriteLine($"Index: {BinarySearch(arrayToSort, 10)}");
            Console.ReadLine();
        }

        static void PrintArray(int[] array)
        {
            Console.WriteLine("Printing an array");
            foreach (var element in array)
                Console.Write(element + " ");
            Console.WriteLine();
        }
        
        static void SillyBubbleSort(ref int[] array)
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

        //*Реализовать шейкерную сортировку.
        static void CocktailSort(ref int[] array)
        {
            bool changed = true;
            int rightLimit = array.Length - 1;
            int leftLimit = 0;
            while (changed)
            {
                changed = false;
                for (int i = leftLimit; i < rightLimit; i++)
                    if (array[i] > array[i + 1])
                    {
                        Swap(ref array[i], ref array[i + 1]);
                        changed = true;
                    }
                rightLimit--;
                for (int j = rightLimit + 1; j > leftLimit; j--)
                {
                    if (array[j] < array[j - 1])
                    {
                        Swap(ref array[j], ref array[j - 1]);
                        changed = true;
                    }
                }
                leftLimit++;
            }       
        }

        //Реализовать бинарный алгоритм поиска в виде функции, которой передается отсортированный массив.
        //Функция возвращает индекс найденного элемента или -1, если элемент не найден.
        static int BinarySearch(int[] array, int element)
        {
            int rightLimit = array.Length - 1;
            int leftLimit = 0;
            int middle = 0;
            while (rightLimit>leftLimit)
            {
                middle = leftLimit + (rightLimit - leftLimit) / 2;
                if (array[middle] == element)
                    break;
                if (array[middle] < element)
                    leftLimit = middle + 1;
                else
                    rightLimit = middle - 1;
            }
            return array[middle] == element ? middle : -1;
        }
    }
}
