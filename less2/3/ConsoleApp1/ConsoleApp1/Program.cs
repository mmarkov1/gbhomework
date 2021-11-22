using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите число");
            if (Convert.ToInt32(Console.ReadLine()) % 2 == 0) Console.WriteLine("Четное");
            else Console.WriteLine("Нечетное");
            Console.ReadLine();
        }
    }
}
