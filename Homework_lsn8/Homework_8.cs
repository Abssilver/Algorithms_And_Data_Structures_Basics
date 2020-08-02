using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

//Задание выполнил Ремизов Павел
namespace Homework_lsn8
{
    class Homework_8
    {
        static void Main(string[] args)
        {
            CountingSort(20, 3, 21);
            QuickSortDemo(20, 3, 21);
            Console.ReadKey();
        }

        //Реализовать сортировку подсчетом.
        static int [] GenereateArray(int numberOfElements, int minValue, int maxValue)
        {
            Random rnd = new Random();
            int[] toReturn = new int[numberOfElements];
            for (int i = 0; i < numberOfElements; i++)
                toReturn[i] = rnd.Next(minValue, maxValue + 1);
            return toReturn;
        }
        static void CountingSort(int numberOfElements, int minValue, int maxValue)
        {
            if (minValue > maxValue)
                Swap(ref minValue, ref maxValue);
            int[] arrayToSort = GenereateArray(numberOfElements, minValue, maxValue);
            int[] entries = new int[maxValue - minValue + 1];
            Console.WriteLine("Input array:");
            PrintArray(arrayToSort);
            foreach (var element in arrayToSort)
                entries[element - minValue]++;
            int index = 0;
            for (int i = 0; i < entries.Length; i++)
            {
                while (entries[i] > 0)
                {
                    arrayToSort[index++] = minValue + i;
                    entries[i]--;
                }
            }
            Console.WriteLine("Sorted array:");
            PrintArray(arrayToSort);
        }
        static void PrintArray(int[] array)
        {
            foreach (var element in array)
                Console.Write($"{element} ");
            Console.WriteLine();
        }

        //Реализовать быструю сортировку
        static void QuickSortDemo(int numberOfElements, int minValue, int maxValue)
        {
            if (minValue > maxValue)
                Swap(ref minValue, ref maxValue);
            int[] arrayToSort = GenereateArray(numberOfElements, minValue, maxValue);
            Console.WriteLine("Input array:");
            PrintArray(arrayToSort);
            QuickSort(arrayToSort, 0, arrayToSort.Length - 1);
            Console.WriteLine("Sorted array:");
            PrintArray(arrayToSort);
        }
        static void Swap<T>(ref T first, ref T second)
        {
            T temp = second;
            second = first;
            first = temp;
        }
        static void QuickSort(int[] array, int leftIndex, int rightIndex)
        {
            int i = leftIndex;
            int j = rightIndex;

            int middle = array[(i + j) / 2];
        
            do
            {
                while (array[i] < middle) i++;
                while (array[j] > middle) j--;

                if (i <= j)
                {
                    if (array[i] > array[j])
                        Swap(ref array[i], ref array[j]);
                    i++;
                    j--;
                }
            } while (i <= j);

            if (i < rightIndex) QuickSort(array, i, rightIndex);
            if (leftIndex < j) QuickSort(array, leftIndex, j);
        }
    }
}