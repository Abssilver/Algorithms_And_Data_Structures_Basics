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
            Console.WriteLine("Converter");
            Console.WriteLine(Converter(166));
            Console.WriteLine("Power of a the number");
            Console.WriteLine(PowerA(3, 12));
            Console.WriteLine(PowerB(3, 12));
            Console.WriteLine(PowerC(3, 12));
            Console.WriteLine("Calculator (3 to 20)");
            CalculatorA(3, 20);
            Console.WriteLine("Calculator (17 to 55)");
            CalculatorA(17, 55);
            Console.WriteLine("Calculator B (3 to 20)");
            Console.WriteLine(CalculatorB(3, 20));
            Console.WriteLine("Calculator B (17 to 55)");
            Console.WriteLine(CalculatorB(17, 55));
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

        // Исполнитель Калькулятор преобразует целое число, записанное на экране. 
        // У исполнителя две команды, каждой команде присвоен номер:
        // Прибавь 1
        // Умножь на 2
        // Первая команда увеличивает число на экране на 1, 
        // Вторая увеличивает это число в 2 раза. Сколько существует программ, которые число 3 преобразуют в число 20?
        // а) с использованием массива;
        // б) с использованием рекурсии.
        // Реализовать меню с выбором способа заполнения массива: из файла, случайными числами, с клавиатуры.

        static void CalculatorA(int from, int to)
        {
            int[] moves = new int[to - from];
            int counter = 0;
            int current = to;
            if (to >= from)
            {
                moves[0] = current;
                if (current != from)
                {
                    for (counter = 1; counter < moves.Length; counter++)
                    {
                        if (current % 2 == 0 && current / 2 >= from)
                        {
                            current /= 2;
                            moves[counter] = current;
                        }
                        else
                        {
                            current -= 1;
                            moves[counter] = current;
                        }
                        if (current == from)
                            break;
                    }
                }
                Console.WriteLine($"There is(are) {counter} step(s):");
                for (int i = counter; i >= 0; i--)
                    Console.WriteLine(moves[i]);
            }
            else Console.WriteLine("Invalid Input!");
        }
        static string CalculatorB(int from, int to)
        {
            if (to % 2 == 0 && to / 2 >= from)
            {
                return CalculatorB(from, to / 2) + Environment.NewLine + to.ToString();
            }
            else if (to > from)
            {
                return CalculatorB(from, to - 1) + Environment.NewLine + to.ToString();
            }
            else
                return to.ToString();
        }
    }
}
