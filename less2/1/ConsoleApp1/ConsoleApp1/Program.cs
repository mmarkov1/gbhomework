using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Запросить у пользователя минимальную и максимальную температуру за сутки и вывести среднесуточную температуру.


namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите минимальную температуру:");
            int min = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите максимальную температуру:");
            int max = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Средняя температура: " + (min + max)/2 );
            Console.ReadLine();
        }
    }
}
