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
                    case 4:
                        RootsOfTheEquation();
                        break;
                    case 5:
                        Seasons();
                        break;
                    case 6:
                        AgesRus();
                        break;
                    case 7:
                        ChessColor();
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
            Console.WriteLine("4 - Task 4. Roots of the equation");
            Console.WriteLine("5 - Task 5. Seasons");
            Console.WriteLine("6 - Task 6. Ages");
            Console.WriteLine("7 - Task 7. Chess");
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

        //Написать программу нахождения корней заданного квадратного уравнения.
        static void RootsOfTheEquation()
        {
            Console.Title = "Roots Of The Equation";
            Console.WriteLine
                ("The root finding program welcomes you!\nPlease, provide data for calculations.");
            //ax^2 + bx + c = 0;
            double [] equationCoef = new double [3];
            double[] roots = new double[2];
            Console.WriteLine("Your equation template is: ax^2 + bx + c = 0\n{0}\n{1}\n{2}",
                "a - first coefficient",
                "b - second coefficient",
                "c - third coefficient");
            int numOfValues = 0;
            while (numOfValues < equationCoef.Length)
            {
                Console.WriteLine($"Please, enter the {(NumberDisplay)numOfValues} coefficient");
                bool isValidValue = double.TryParse(Console.ReadLine(), out equationCoef[numOfValues]);
                if (isValidValue)
                    numOfValues++;
                else
                    Console.WriteLine("Invalid value, try again");
            }
            double discriminant = Math.Pow(equationCoef[1], 2) - 4 * equationCoef[0] * equationCoef[2];
            if (discriminant < 0)
                Console.WriteLine("No real roots of this equation");
            else if (discriminant > 0)
            {
                roots[0] = (-equationCoef[1] + Math.Sqrt(discriminant)) / (2 * equationCoef[0]);
                roots[1] = (-equationCoef[1] - Math.Sqrt(discriminant)) / (2 * equationCoef[0]);
                Console.WriteLine($"Roots are: {roots[0]:f4} || {roots[1]:f4}");
            }
            else
            {
                roots[0] = -equationCoef[1] / (2 * equationCoef[0]);
                Console.WriteLine($"The root is: {roots[0]:f4}");
            }
        }

        //С клавиатуры вводится номер месяца. Требуется определить, к какому времени года он относится.
        static void Seasons()
        {
            int season = 0;
            do
            {
                Console.WriteLine($"Please, enter the number of month");
                int.TryParse(Console.ReadLine(), out season);
                if (season < 1 || season > 12)
                    Console.WriteLine("Invalid value, try again");
            } while (season < 1 || season > 12);
            switch (season)
            {
                case 1:
                case 2:
                case 12:
                    Console.WriteLine($"This is winter!");
                    break;
                case 3:
                case 4:
                case 5:
                    Console.WriteLine("This is spring!");
                    break;
                case 6:
                case 7:
                case 8:
                    Console.WriteLine("This is summer!");
                    break;
                default:
                    Console.WriteLine("This is autumn!");
                    break;
            }
        }

        //Ввести возраст человека (от 1 до 150 лет) и вывести его вместе с последующим словом «год», «года» или «лет».
        static void AgesRus()
        {
            int age = 0, calculations = 0;
            int[] exceptions = { 11, 12, 13, 14 };
            do
            {
                Console.WriteLine($"Пожалуйста, введите Ваш возраст");
                int.TryParse(Console.ReadLine(), out age);
                if (age < 1 || age > 151)
                    Console.WriteLine("Некорректный ввод данных, попробуйте снова");
            } while (age < 1 || age > 151);
            if (age > 99)
                calculations = age % 100;
            if ((calculations != 0 && exceptions.Contains(calculations)) || 
                (calculations == 0 && exceptions.Contains(age)))
                Console.WriteLine($"Отлично, Вам {age} лет!");
            else
            {
                calculations = calculations == 0 ? age % 10 : calculations % 10;
                switch (calculations)
                {
                    case 1:
                        Console.WriteLine($"{age} год! Отличный возраст!");
                        break;
                    case 2:
                    case 3:
                    case 4:
                        Console.WriteLine($"{age} года! Вы как Карлсон, в самом расцвете сил!");
                        break;
                    default:
                        Console.WriteLine($"{age} лет! У Вас скоро день рождения?");
                        break;
                }
            }
        }

        // С клавиатуры вводятся числовые координаты двух полей шахматной доски (x1,y1,x2,y2). 
        // Требуется определить, относятся поля к одному цвету или нет
        static bool IsBlack(int x, int y)
        {
            if (x == y)
                return true;
            bool isEvenX = x % 2 == 0;
            bool isEvenY = y % 2 == 0;
            return isEvenX == isEvenY;
        }
        struct Position
        {
            public int x, y;
        }
        static bool ValidateInt(int toValidate) => toValidate > 0 && toValidate < 9;
        static void ChessColor()
        {
            char[] separators = { ' ', ',' };
            Position[] positions = new Position[2];
            int positionsIndex = 0;
            do
            {
                Console.WriteLine($"Please, enter field coordinate (x,y), use space to split values");
                string[] userInput = Console.ReadLine().Split(separators, StringSplitOptions.RemoveEmptyEntries);
                if (userInput.Length == 2)
                {
                    bool isValidX = int.TryParse(userInput[0], out int x);
                    bool isValidY = int.TryParse(userInput[1], out int y);
                    if ((isValidX && isValidY) && ValidateInt(x) && ValidateInt(y))
                    {
                        positions[positionsIndex].x = x;
                        positions[positionsIndex].y = y;
                        positionsIndex++;
                    }
                    else
                        Console.WriteLine("Invalid value, try again");
                }
                else
                    Console.WriteLine("Invalid value, try again");
            } while (positionsIndex < 2);
            if (IsBlack(positions[0].x, positions[0].y) == IsBlack(positions[1].x, positions[1].y))
                Console.WriteLine("This cells have the same color");
            else
                Console.WriteLine("This cells have different color");
        }
    }
}
