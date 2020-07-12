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

            Console.WriteLine(PowerA(3, 12));
            Console.WriteLine(PowerB(3, 12));
            Console.WriteLine(PowerC(3, 12));
            Console.ReadLine();
        }

        //Реализовать функцию перевода чисел из десятичной системы в двоичную, используя
        //рекурсию.
        static string Converter(int number) =>
            number >= 2 ? Converter(number / 2) + (number % 2).ToString() : number.ToString();

        //Реализовать функцию возведения числа a в степень b:
        //a.без рекурсии;
        //b.рекурсивно;
        //c. * рекурсивно, используя свойство четности степени.
        static int PowerA(int number, int power)
        {
            int result = 1;
            for (int i = 0; i < power; i++)
                result *= number;
            return result;
        }
        static int PowerB(int number, int power) =>
            power >= 1 ? number * PowerB(number, --power) : 1;

        static int PowerC(int number, int power) =>
            power >= 1 ?
            power % 2 == 0 ? PowerC(number * number, power / 2) : number * PowerC(number, --power) : 1;
       
    }
}
