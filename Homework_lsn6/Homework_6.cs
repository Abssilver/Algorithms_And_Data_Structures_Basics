using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Задание выполнил Ремизов Павел
namespace Homework_lsn6
{
    class Homework_6
    {
        static void Main(string[] args)
        {
            Console.WriteLine(SimpleHash("How can a clam cram in a clean cream can?"));
            Console.WriteLine(SimpleHash("Six sick hicks nick six slick bricks with picks and sticks."));

            Console.ReadLine();
        }
        //Реализовать простейшую хэш-функцию. 
        //На вход функции подается строка, на выходе сумма кодов символов
        static string SimpleHash(string input)
        {
            int result = 0;
            for (int i = 0; i < input.Length; i++)
            {
                result += Convert.ToInt32(input[i]);
                if (result >= 1_000_000_000)
                    result -= 1_000_000_000;
            }
            return string.Format("{0:d9}", result);
        }
    }
}
