using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            ShellSortDemo(20, 3, 21);
            HeapSortDemo(20, 2, 21);
            BubbleSortDemo(20, 2, 21);
            CocktailSortDemo(1_000_000, 2, 21);
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
            //Stopwatch watch = new Stopwatch();
            //watch.Start();
            QuickSort(arrayToSort, 0, arrayToSort.Length - 1);
            //watch.Stop();
            //Console.WriteLine(watch.Elapsed);
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
            //Stopwatch watch = new Stopwatch();
            //watch.Start();
            MergeSort(arrayToSort, 0, arrayToSort.Length - 1);
            //watch.Stop();
            //Console.WriteLine(watch.Elapsed);  
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

        //Проанализировать время работы каждого из вида сортировок для 100, 10000, 1000000 элементов.
        //Заполнить таблицу.

        //ФИО - Ремизов П.М.
        //Процессор - Intel(R) Core(TM) i7-8750H CPU @ 2.20GHz
        //ОС - Windows 10 Pro x64
        //Среда программирования Microsoft Visual Studio Community 2019 Версия 16.6.2
        //Название          Время / Кол-во сравнений
        //QuickSort        00:00:00.0004526 / 100 | 00:00:00.0005727 / 250 | 00:00:00.0004681 / 500 | 00:00:00.0004290 / 1000 | 00:00:00.0018355 / 10_000 | 00:00:00.1096428 / 1_000_000
        //ShellSort        00:00:00.0005836 / 100 | 00:00:00.0001656 / 250 | 00:00:00.0001976 / 500 | 00:00:00.0002274 / 1000 | 00:00:00.0012768 / 10_000 | 00:00:00.1617529 / 1_000_000
        //MergeSort        00:00:00.0003854 / 100 | 00:00:00.0004306 / 250 | 00:00:00.0004659 / 500 | 00:00:00.0007755 / 1000 | 00:00:00.0023534 / 10_000 | 00:00:00.3130088 / 1_000_000
        //HeapSort         00:00:00.0001686 / 100 | 00:00:00.0002893 / 250 | 00:00:00.0003513 / 500 | 00:00:00.0010577 / 1000 | 00:00:00.0097855 / 10_000 | 00:00:01.3664901 / 1_000_000
        //BubbleSort       00:00:00.0001449 / 100 | 00:00:00.0002140 / 250 | 00:00:00.0006760 / 500 | 00:00:00.0013021 / 1000 | 00:00:00.1391569 / 10_000 | 00:19:02.1271134 / 1_000_000
        //ShakeSort        00:00:00.0003276 / 100 | 00:00:00.0009077 / 250 | 00:00:00.0016539 / 500 | 00:00:00.0047964 / 1000 | 00:00:00.7689873 / 10_000 | 01:21:00.0000000 / 1_000_000 
        //                                                                                                                                                  (результат не достоверный, окно выполнения скрипта закрылось)
        // -----------------------------------------------------------------------------------------
        // 100              QuickSort | ShellSort | MergeSort | HeapSort | BubbleSort | ShakeSort
        //QuickSort             1     |   Quick   |   Merge   |   Heap   |   Bubble   |   Quick
        //ShellSort           Quick   |     1     |   Merge   |   Heap   |   Bubble   |   Shake
        //MergeSort           Merge   |   Merge   |     1     |   Heap   |   Bubble   |   Shake
        //HeapSort             Heap   |    Heap   |    Heap   |     1    |   Bubble   |    Heap
        //BubbleSort         Bubble   |   Bubble  |   Bubble  |  Bubble  |     1      |   Bubble
        //ShakeSort           Quick   |   Shake   |   Shake   |   Heap   |   Bubble   |     1
        // -----------------------------------------------------------------------------------------
        // 10_000           QuickSort | ShellSort | MergeSort | HeapSort | BubbleSort | ShakeSort
        //QuickSort             1     |   Shell   |   Quick   |   Quick  |    Quick   |   Quick
        //ShellSort           Shell   |     1     |   Shell   |   Shell  |    Shell   |   Shell
        //MergeSort           Quick   |   Shell   |     1     |   Merge  |    Merge   |   Merge
        //HeapSort            Quick   |   Shell   |   Merge   |     1    |     Heap   |    Heap
        //BubbleSort          Quick   |   Shell   |   Merge   |   Heap   |      1     |   Bubble
        //ShakeSort           Quick   |   Shell   |   Merge   |   Heap   |   Bubble   |     1
        // -----------------------------------------------------------------------------------------
        // 1_000_000        QuickSort | ShellSort | MergeSort | HeapSort | BubbleSort | ShakeSort
        //QuickSort             1     |   Quick   |   Quick   |  Quick   |    Quick   |   Quick
        //ShellSort           Quick   |     1     |   Shell   |  Shell   |    Shell   |   Shell
        //MergeSort           Quick   |   Shell   |     1     |  Merge   |    Merge   |   Merge
        //HeapSort            Quick   |   Shell   |   Merge   |     1    |     Heap   |    Heap
        //BubbleSort          Quick   |   Shell   |   Merge   |   Heap   |     1      |   Bubble
        //ShakeSort           Quick   |   Shell   |   Merge   |   Heap   |   Bubble   |     1

        static void ShellSortDemo(int numberOfElements, int minValue, int maxValue)
        {
            if (minValue > maxValue)
                Swap(ref minValue, ref maxValue);
            int[] arrayToSort = GenereateArray(numberOfElements, minValue, maxValue);
            Console.WriteLine("Input array:");
            PrintArray(arrayToSort);
            //Stopwatch watch = new Stopwatch();
            //watch.Start();
            ShellSort(arrayToSort);
            //watch.Stop();
            //Console.WriteLine(watch.Elapsed);
            Console.WriteLine("Sorted array:");
            PrintArray(arrayToSort);
        }
        //Internet
        static void ShellSort(int[] array)
        {
            int step = array.Length / 2;
            while (step > 0)
            {
                int i, j;
                for (i = step; i < array.Length; i++)
                {
                    int value = array[i];
                    for (j = i - step; (j >= 0) && (array[j] > value); j -= step)
                        array[j + step] = array[j];
                    array[j + step] = value;
                }
                step /= 2;
            }
        }
        static void HeapSortDemo(int numberOfElements, int minValue, int maxValue)
        {
            if (minValue > maxValue)
                Swap(ref minValue, ref maxValue);
            int[] arrayToSort = GenereateArray(numberOfElements, minValue, maxValue);
            IntComparer comparer = new IntComparer();
            Heap<int> heap = new Heap<int>(arrayToSort, comparer);
            heap.BuildMaxHeap();
            Console.WriteLine("Input array:");
            PrintArray(arrayToSort);
            //Stopwatch watch = new Stopwatch();
            //watch.Start();
            heap.HeapSort(); // + построение пирамиды
            //watch.Stop();
            //Console.WriteLine(watch.Elapsed);
            Console.WriteLine("Sorted array:");
            PrintArray(arrayToSort);
        }
        //Internet
        public class Heap<T>
        {
            private T[] _array; //массив сортируемых элементов
            private int heapsize;//число необработанных элементов
            private IComparer<T> _comparer;
            public Heap(T[] array, IComparer<T> comparer)
            {
                _array = array;
                heapsize = array.Length;
                _comparer = comparer;
            }
            public void HeapSort()
            {
                //BuildMaxHeap();//Построение пирамиды
                for (int i = _array.Length - 1; i > 0; i--)
                {
                    T temp = _array[0];//Переместим текущий максимальный элемент из нулевой позиции в хвост массива
                    _array[0] = _array[i];
                    _array[i] = temp;

                    heapsize--;//Уменьшим число необработанных элементов
                    MaxHeapify(0);//Восстановление свойства пирамиды
                }
            }

            private int Parent(int i) { return (i - 1) / 2; }//Индекс родительского узла
            private int Left(int i) { return 2 * i + 1; }//Индекс левого потомка
            private int Right(int i) { return 2 * i + 2; }//Индекс правого потомка

            //Метод переупорядочивает элементы пирамиды при условии,
            //что элемент с индексом i меньше хотя бы одного из своих потомков, нарушая тем самым свойство невозрастающей пирамиды
            private void MaxHeapify(int i)
            {
                int l = Left(i);
                int r = Right(i);
                int lagest = i;
                if (l < heapsize && _comparer.Compare(_array[l], _array[i]) > 0)
                    lagest = l;
                if (r < heapsize && _comparer.Compare(_array[r], _array[lagest]) > 0)
                    lagest = r;
                if (lagest != i)
                {
                    T temp = _array[i];
                    _array[i] = _array[lagest];
                    _array[lagest] = temp;

                    MaxHeapify(lagest);
                }
            }

            //метод строит невозрастающую пирамиду
            public void BuildMaxHeap()
            {
                int i = (_array.Length - 1) / 2;

                while (i >= 0)
                {
                    MaxHeapify(i);
                    i--;
                }
            }

        }
        public class IntComparer : IComparer<int>
        {
            public int Compare(int x, int y) { return x - y; }
        }

        static void BubbleSortDemo(int numberOfElements, int minValue, int maxValue)
        {
            if (minValue > maxValue)
                Swap(ref minValue, ref maxValue);
            int[] arrayToSort = GenereateArray(numberOfElements, minValue, maxValue);
            Console.WriteLine("Input array:");
            PrintArray(arrayToSort);
            //Stopwatch watch = new Stopwatch();
            //watch.Start();
            BubbleSort(arrayToSort);
            //watch.Stop();
            //Console.WriteLine(watch.Elapsed);
            Console.WriteLine("Sorted array:");
            PrintArray(arrayToSort);
        }
        //Internet
        static void BubbleSort(int[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = i + 1; j < array.Length; j++)
                {
                    if (array[j] < array[i])
                    {
                        var temp = array[i];
                        array[i] = array[j];
                        array[j] = temp;
                    }
                }
            }
        }

        static void CocktailSortDemo(int numberOfElements, int minValue, int maxValue)
        {
            if (minValue > maxValue)
                Swap(ref minValue, ref maxValue);
            int[] arrayToSort = GenereateArray(numberOfElements, minValue, maxValue);
            Console.WriteLine("Input array:");
            //PrintArray(arrayToSort);
            Stopwatch watch = new Stopwatch();
            watch.Start();
            CocktailSort(arrayToSort);
            watch.Stop();
            Console.WriteLine(watch.Elapsed);
            Console.WriteLine("Sorted array:");
            //PrintArray(arrayToSort);
        }
        //Homework 3
        static void CocktailSort(int[] array)
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
    }
}