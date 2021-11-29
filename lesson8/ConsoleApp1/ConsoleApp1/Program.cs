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
            /*Создать консольное приложение, которое при старте выводит приветствие, записанное в настройках приложения (application-scope). 
             * Запросить у пользователя имя, возраст и род деятельности, а затем сохранить данные в настройках. При следующем запуске отобразить эти сведения. 
             * Задать приложению версию и описание.                                                     
             */
//            Properties.Settings.Default.Name = null; Properties.Settings.Default.age = null; Properties.Settings.Default.work = null; Properties.Settings.Default.Save();

            if (string.IsNullOrEmpty(Properties.Settings.Default.Name))
            {

                Console.WriteLine("Введите имя:");
                Properties.Settings.Default.Name = Console.ReadLine();
                Properties.Settings.Default.Save();
            }
            else Console.WriteLine("Здравствуйте {0}, ", Properties.Settings.Default.Name);

            if (string.IsNullOrEmpty(Properties.Settings.Default.age))
            {

                Console.WriteLine("Введите возраст:");
                Properties.Settings.Default.age = Console.ReadLine();
                Properties.Settings.Default.Save();
            }
            else Console.WriteLine("ваш возраст {0}, ", Properties.Settings.Default.age);

            if (string.IsNullOrEmpty(Properties.Settings.Default.work))
            {

                Console.WriteLine("Введите сферу деятельности:");
                Properties.Settings.Default.work = Console.ReadLine();
                Properties.Settings.Default.Save();
            }
            else Console.WriteLine("ваша сфера деятельности: {0} ", Properties.Settings.Default.work);
            Console.ReadKey();

        }
    }
}
