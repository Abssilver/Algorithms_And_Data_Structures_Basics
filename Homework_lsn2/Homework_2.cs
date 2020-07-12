using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

//Задание выполнил Ремизов Павел
namespace Homework_lsn2
{
    class Homework_2
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Converter(166));
            Console.ReadLine();
        }

        //Реализовать функцию перевода чисел из десятичной системы в двоичную, используя
        //рекурсию.
        static string Converter(int number) =>
            number >= 2 ? Converter(number / 2) + (number % 2).ToString() : number.ToString();
    }
}
