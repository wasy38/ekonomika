using System;
using System.IO;

namespace ekonomika
{
    class Program
    {
        static void SetParametrs(int r, double S, int n)
        {
            bool AllPassed = false;
            while (!AllPassed)
            {
                try
                {
                    Console.WriteLine("Введите сумму кредитования");
                    S = Convert.ToInt32(Console.ReadLine());
                    Console.Clear();
                    Console.WriteLine("Ввести годовую процентную ставку(без знака %)");
                    r = Convert.ToInt32(Console.ReadLine());
                    Console.Clear();
                    Console.WriteLine("Введите срок кредитования(в месяцах)");
                    n = Convert.ToInt32(Console.ReadLine());
                    Console.Clear();
                    if ((S < 0) || (n < 0) || (r < 0)) throw new ArgumentException("Данные не могут быть <0");
                    AllPassed = true;
                }
                catch {
                    Console.Clear();
                    Console.WriteLine("Данные введены некоректно!! попробуйте снова");
                    Console.ReadKey();
                    Console.Clear();
                }                
            }
            menu(r, S, n);
        }

        static void SaveMenu(string output)
        {
            Console.WriteLine("\nВыберите действие:");
            Console.WriteLine("Сохранить график в текстовом файле > > save");
            Console.WriteLine("выход > > exit\n");
            string command = Console.ReadLine().ToLower();
            switch (command)
            {
                case "save":
                    Console.Clear();
                    Console.WriteLine("Введите путь и имя файла");
                    string path = Console.ReadLine();
                    try
                    {
                        using (StreamWriter sw = new StreamWriter(@path+".txt", false))
                        {
                            sw.WriteLine(output);
                            Console.WriteLine("Успешно сохранено!");
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    break;
                case "exit":
                    Console.Clear();
                break;
                default:
                    Console.Clear();
                    Console.WriteLine(output);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n\n\nКоманда не распознана!!");
                    Console.ForegroundColor = ConsoleColor.White;
                    SaveMenu(output);
                    break;
            }
            Console.ReadKey();
            Console.Clear();
        }
        static void Сalculate(int r, double S, int n)
        {
            string output=null;
            double R= (double)r/(100*12);
            double h = Math.Pow(1 + R, n);
            double K = h * R / (h - 1);
            double x = Math.Round(K*S, 2);
            var s = S;
            var pere =Math.Round((S * K * 36)-S, 2);
            output = $"При условии взятия кредита на сумму {S} рублей под {r}% годовых на срок {n} месяцев\nежемесячный аннуитетный платеж составит {x} рублей/месяц, переплата составит {pere} рублей\n" +
                $"Месяцев до погашения\t| Сумма\t\t| Осталось\n" +
                 $"--------------------------------------------------------\n" +
                 $"{n}\t\t\t| 0\t\t| {S}\n" +
                 $"--------------------------------------------------------\n";
            for (int i = n-1; i > 0; i--)
            {
                s = s * (1 + R);
                s = Math.Round(s - S*K, 2);
                output += $"{i}\t\t\t| {x}\t| {s}\n" +
                    $"--------------------------------------------------------\n";
                
            }
            output += $"0\t\t\t| {s}\t| 0";
            Console.WriteLine(output);
            SaveMenu(output);
        }

        static void menu(int r, double S, int n)
        {
            String command = null;
            while(command!="exit")
            {
                Console.WriteLine("\n\nВыберите действие:");
                Console.WriteLine("ввести данные > > input");
                Console.WriteLine("рассчитать выплаты > > result");
                Console.WriteLine("выход > > exit\n");
                command=Console.ReadLine().ToLower();
                switch(command)
                {
                    case "input":
                        Console.Clear();
                        SetParametrs(r, S, n);
                        break;
                    case "result":
                        Console.Clear();
                        Сalculate(r, S, n);
                        break;
                    case "exit":
                        Console.Clear();
                        break;
                    default:
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nКоманда не распознана!!");
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                }    
            }
        }
        static void Main(string[] args)
        {
            menu(12, 20000, 36);
        }
    }
}
