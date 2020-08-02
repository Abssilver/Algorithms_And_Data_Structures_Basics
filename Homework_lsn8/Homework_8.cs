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
            MergeSortDemo(20, 3, 21);
            PigeonholeSortDemo(20, 3, 21);
            Console.ReadKey();
        }

        //Реализовать сортировку подсчетом.
        static int[] GenereateArray(int numberOfElements, int minValue, int maxValue)
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

        //*Реализовать сортировку слиянием
        static void MergeSortDemo(int numberOfElements, int minValue, int maxValue)
        {
            if (minValue > maxValue)
                Swap(ref minValue, ref maxValue);
            int[] arrayToSort = GenereateArray(numberOfElements, minValue, maxValue);
            Console.WriteLine("Input array:");
            PrintArray(arrayToSort);
            MergeSort(arrayToSort, 0, arrayToSort.Length - 1);
            Console.WriteLine("Sorted array:");
            PrintArray(arrayToSort);
        }
        static void MergeSort(int[] array, int leftIndex, int rightIndex)
        {
            if (array.Length > 0)
            {
                int nextStep = rightIndex - leftIndex;
                switch (nextStep)
                {
                    case 0:
                        break;
                    case 1:
                        if (array[leftIndex] > array[rightIndex])
                            Swap(ref array[leftIndex], ref array[rightIndex]);
                        break;
                    default:
                        MergeSort(array, leftIndex, leftIndex + nextStep / 2);
                        MergeSort(array, leftIndex + 1 + nextStep / 2, rightIndex);
                        Merge(array, leftIndex, rightIndex, nextStep / 2);
                        break;
                }
            }
        }
        static void Merge(int[] array, int leftIndex, int rightIndex, int middle)
        {
            int[] firstArray = new int[middle + 1];
            int firstArrayIndex = 0;
            int[] secondArray = new int[rightIndex - leftIndex - middle];
            int secondArrayIndex = 0;
            for (int i = leftIndex; i < leftIndex + firstArray.Length; i++)
                firstArray[firstArrayIndex++] = array[i];
            for (int j = leftIndex + middle + 1; j < leftIndex + middle + 1 + secondArray.Length; j++)
                secondArray[secondArrayIndex++] = array[j];
            firstArrayIndex = secondArrayIndex = 0;
            while (leftIndex <= rightIndex)
            {
                if (firstArrayIndex > firstArray.Length - 1)
                    array[leftIndex++] = secondArray[secondArrayIndex++];
                else if (secondArrayIndex > secondArray.Length - 1)
                    array[leftIndex++] = firstArray[firstArrayIndex++];
                else if (firstArray[firstArrayIndex] <= secondArray[secondArrayIndex])
                    array[leftIndex++] = firstArray[firstArrayIndex++];
                else
                    array[leftIndex++] = secondArray[secondArrayIndex++];
            }
        }

        //**Реализовать алгоритм сортировки со списком
        static void PigeonholeSortDemo(int numberOfElements, int minValue, int maxValue)
        {
            if (minValue > maxValue)
                Swap(ref minValue, ref maxValue);
            Pigeon[] arrayToSort = GeneratePigeons(numberOfElements, minValue, maxValue);
            Console.WriteLine("Input array:");
            PrintPigeons(arrayToSort);
            PigeonholeSort(arrayToSort);
            Console.WriteLine("Sorted array:");
            PrintPigeons(arrayToSort);
        }
        static void PrintPigeons(Pigeon []pigeons)
        {
            foreach (var bird in pigeons)
                Console.Write($"{bird.Key} ");
            Console.WriteLine();
        }
        static void PigeonholeSort(Pigeon[] array)
        {
            int min = int.MaxValue;
            int max = int.MinValue;
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i].Key < min) min = array[i].Key;
                if (array[i].Key > max) max = array[i].Key;
            }
            List<Pigeon>[] holes = new List<Pigeon>[max - min + 1];
            for (int i = 0; i < array.Length; i++)
            {
                if (holes[array[i].Key - min] == null)
                    holes[array[i].Key - min] = new List<Pigeon>();
                holes[array[i].Key - min].Add(array[i]);
            }
            int pigeons = 0;
            for (int i = 0; i < holes.Length; i++)
            {
                if (holes[i]!=null)
                {
                    holes[i].CopyTo(array, pigeons);
                    pigeons += holes[i].Count;
                }
            }
        }
        public class Pigeon
        {
            public int Key { get; set; }
        }
        static Pigeon[] GeneratePigeons(int numberOfBirds, int minValue, int maxValue)
        {
            Random rnd = new Random();
            Pigeon[] toReturn = new Pigeon[numberOfBirds];
            for (int i = 0; i < toReturn.Length; i++)
                toReturn[i] = new Pigeon() { Key = rnd.Next(minValue, maxValue + 1) };
            return toReturn;
        }
    }
}