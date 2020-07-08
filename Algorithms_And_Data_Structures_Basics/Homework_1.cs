using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;


//Задание выполнил Ремизов Павел
namespace Algorithms_And_Data_Structures_Basics
{
    class Homework_1
    {
        static void Main(string[] args)
        {
            int selection = 0;
            bool validValue = false;
            do
            {
                Menu();
                validValue = int.TryParse(Console.ReadLine(), out selection);
                switch (selection)
                {
                    case 1:
                        BMI();
                        break;
                    case 2:
                        MaxOfFour();
                        break;
                    case 3:
                        SwappingSystem();
                        break;
                    case 0:
                        Console.WriteLine("Bye-bye!");
                        break;
                    default:
                        Console.WriteLine("Invalid value!");
                        break;
                }
            } while ((validValue && selection!=0) || !validValue);
            Console.ReadKey();
        }
        static void Menu()
        {
            Console.WriteLine("0 - Exit");
            Console.WriteLine("1 - Task 1. BMI");
            Console.WriteLine("2 - Task 2. Max of 4");
            Console.WriteLine("3 - Task 3. Swapping System");

        }

        //Ввести вес и рост человека. Рассчитать и вывести индекс массы тела по формуле I=m/(h*h); где
        //m-масса тела в килограммах, h - рост в метрах.
        static void BMI()
        {
            double height, weight;
            double bmiValue;

            Console.Title = "BMI";
            Console.WriteLine
                 ("The program of calculating your body mass index (BMI) welcomes you!\n" +
                 "Please, enter your height (cm) and weight (kg).\nUse space key or comma to split values.");
            string[] userInput = Regex.Split(Console.ReadLine(), @"\s|[,]\s|[,]");
            while (userInput.Length != 2)
            {
                Console.WriteLine("Sorry, the number of values you entered is exceeded.\nTry again.\n" +
                    "Please, enter your height (cm) and weight (kg).\nUse space key or comma to split values.");
                userInput = Regex.Split(Console.ReadLine(), @"\s|[,]\s|[,]");
            }

            bool heightIsNum = double.TryParse(userInput[0], out height);
            bool weightIsNum = double.TryParse(userInput[1], out weight);

            if (heightIsNum && weightIsNum)
            {
                bmiValue = weight / Math.Pow(height / 100, 2);
                Console.WriteLine
                    ($"Your BMI is {bmiValue:F1} and this value is considered to be {BMIRecommendation(bmiValue)}");
            }
            else
                Console.WriteLine("You entered invalid values");
        }
        static string BMIRecommendation(double BMIvalue) =>
            BMIvalue <= 15 ? "very severely underweight" :
            BMIvalue <= 16 ? "severely underweight" :
            BMIvalue <= 18.5 ? "underweight" :
            BMIvalue <= 25 ? "normal(healthy weight)" :
            BMIvalue <= 30 ? "overweight" :
            BMIvalue <= 35 ? "obese Class I (Moderately obese)" :
            BMIvalue <= 40 ? "obese Class II (Severely obese)" :
            "obese Class III (Very severely obese)";


        //Найти максимальное из четырех чисел. Массивы не использовать
        enum NumberDisplay: byte
        {
            first = 0,
            second = 1,
            third = 2,
            fourth = 3
        }
        static void MaxOfFour()
        {
            Console.Title = "MaxOfFour";
            Console.WriteLine("The program find the max of the four numbers");
            int numOfValues = 0;
            double maxValue = double.MinValue;
            double userInput;
            while (numOfValues < 4)
            {
                Console.WriteLine($"Please, enter the {(NumberDisplay)numOfValues} number");
                bool isValidValue = double.TryParse(Console.ReadLine(), out userInput);
                if (isValidValue)
                {
                    maxValue = userInput > maxValue ? userInput : maxValue;
                    numOfValues++;
                }
                else
                    Console.WriteLine("Invalid value, try again");
            }
            Console.WriteLine($"The maximum is: {maxValue}");
        }

        //Написать программу обмена значениями двух целочисленных переменных:
        //a. с использованием третьей переменной;
        //b. * без использования третьей переменной.

        static void SwapValue<T>(ref T a, ref T b)
        {
            T temporal = a;
            a = b;
            b = temporal;
        }
        static void SwapWithNoIntermediary(ref int a, ref int b)
        {
            a = a ^ b;
            b = b ^ a;
            a = a ^ b;
        }
        //Есть вероятность возникновения переполнения
        static void SwapWithNoIntermediaryWithMathHelps(ref int a, ref int b)
        {
            a = a + b;
            b = a - b;
            a = a - b;
        }

        static void SwappingSystem()
        {
            Console.Title = "Swapping System";
            Console.WriteLine
                ("The Swap program welcomes you!\n" +
                "To swap any numerical values, please enter them below\n" +
                "Use space key or comma to split values.");
            string[] userInput = Regex.Split(Console.ReadLine(), @"\s|[,]\s|[,]");
            while (userInput.Length != 2)
            {
                Console.WriteLine("Sorry, the number of values you entered is exceeded.\nTry again.\n" +
                    "Please, enter two numerical values.\nUse space key or comma to split values.");
                userInput = Regex.Split(Console.ReadLine(), @"\s|[,]\s|[,]");
            }
            int first, second;
            bool firstIsNum = int.TryParse(userInput[0], out first);
            bool secondIsNum = int.TryParse(userInput[1], out second);
            if (firstIsNum && secondIsNum)
            {
                Console.WriteLine($"Your values are {first} and {second}");
                ChooseAnAlgorithm(ref first, ref second);
            }
            else
                Console.WriteLine("You entered invalid values");
            Console.WriteLine($"Now your values are {first} and {second}");
        }
        private static void ChooseAnAlgorithm(ref int first, ref int second)
        {
            Console.WriteLine("Please, choose an algorithm:\n{0},\n{1},\n{2}",
                "a - for regular swap with buffer",
                "b - for swap with no intermediary",
                "c - for swap with no intermediary with helps of math (no recommended)") ;
            string choice = Console.ReadLine().ToLower();
            switch (choice)
            {
                case "a":
                    SwapValue(ref first, ref second);
                    break;
                case "b":
                    SwapWithNoIntermediary(ref first, ref second);
                    break;
                default:
                    SwapWithNoIntermediaryWithMathHelps(ref first, ref second);
                    break;
            }
        }
    }
}
