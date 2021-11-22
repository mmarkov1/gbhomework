using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Для полного закрепления понимания простых типов найдите любой чек, либо фотографию этого чека в интернете и схематично нарисуйте его в консоли,
//только за место динамических, по вашему мнению, данных (это может быть дата, название магазина, сумма покупок) подставляйте переменные,
//которые были заранее заготовлены до вывода на консоль.


namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int busNumber = 41;
            int cost = 20;
            int ticketNumber = 0015;
            string series = "14000 - 712831037 - 555";
            Console.WriteLine("Автобусный билет");
            Console.WriteLine("");
            Console.WriteLine("билет № " + ticketNumber);
            Console.WriteLine("Серия № " + series);
            Console.WriteLine("Время  " + DateTime.Now);
            Console.WriteLine("Стоимость  " + cost);
            Console.WriteLine("Номер автобуса  " + busNumber);

            Console.ReadLine();

        }
    }
}
