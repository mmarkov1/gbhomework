using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FileManager
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string message = ""; // Вывод ошибки пользователю или результата выполнения команды
            int ls = 0; //Отвечает за то, как будет выведен список файлов, либо первые 10 файлов и папок, либо все
            int ext = 0;
            do //Нужно для того, чтобы программа не закрывалась после выполнения 1 программы
            {
                if (ext == 1) break;
                Console.WriteLine("--------------------------------------------------------------------");
                string dirName = Properties.Settings.Default.savedDirectory;
                Console.WriteLine("Путь: {0}", dirName);
                Console.WriteLine();

                if (Directory.Exists(dirName) & ls == 0)
                {
                    Console.WriteLine("Первые 10 каталогов:");
                    string[] dirs = Directory.GetDirectories(dirName);
                    for (int i = 0; i < dirs.Length & i < 10; i++)
                    {
                        Console.WriteLine(dirs[i]);
                    }
                    Console.WriteLine();
                    Console.WriteLine("Первые 10 файлов:");
                    string[] files = Directory.GetFiles(dirName);
                    for (int i = 0; i < files.Length & i < 10; i++)
                    {
                        Console.WriteLine(files[i]);
                    }
                }
                else if (Directory.Exists(dirName) & ls == 1) 
                {
                    Console.WriteLine("Все каталоги:");
                    string[] dirs = Directory.GetDirectories(dirName);
                    for (int i = 0; i < dirs.Length; i++)
                    {
                        Console.WriteLine(dirs[i]);
                    }
                    Console.WriteLine();
                    Console.WriteLine("Все файлы:");
                    string[] files = Directory.GetFiles(dirName);
                    for (int i = 0; i < files.Length; i++)
                    {
                        Console.WriteLine(files[i]);
                    }
                    ls = 0;
                }

                Console.WriteLine("");
                Console.WriteLine(message);
                message = "";
                Console.WriteLine("help : список команд");


                string[] command = Console.ReadLine().Split(' '); // Сначала пишется команда, потом название файла. И эта команда будет применяться к файлу
                if (command.Length > 2) // Проверка, так как мы разделяем строку по пробелам, то если в ссылке есть файлы с пробелами, файл может не найтись. Поэтому обьединяем все что разделено после 1 пробела, тк до него команда
                {
                    for (int i = 1; i < command.Length - 1; i++) 
                    {
                        command[1] = command[1] + " " + command[i+1]; // Вся ссылка соединяется тут
                    }
                    Array.Resize(ref command, 2); // Размер массива уменьшается до 2
                }


                switch (command[0]) //Выполнение комманды из строки
                {
                    case "allsee": // Увидеть все существующие папки и файлы в текущей папке
                        ls = 1;
                        break;



                    case "goto": // Переход именно по ссылке директории 
                        if (command.Length == 1) // Исключение ошибки с отсутствием введенных данных
                        {
                            message = "ОШИБКА: Такой директории нет";
                            Console.WriteLine();
                        }
                        else if (Directory.Exists(command[1])) //Если папка есть, она открывается
                        {
                            char [] chars = command[1].ToCharArray();
                            do
                            {
                                if (chars[chars.Length - 1] == '\\') //Проверка, если в конце есть / или \, они удалятся. Нужно для целостности ссылки
                                {
                                    command[1] = command[1].TrimEnd('\\'); 
                                    Array.Resize(ref chars, chars.Length-1);
                                }
                                else if (chars[chars.Length - 1] == '/')
                                {
                                    command[1] = command[1].TrimEnd('/');
                                    Array.Resize(ref chars, chars.Length - 1);
                                }
                                else break;
                            } while (true);
                            Properties.Settings.Default.savedDirectory = command[1];
                            Properties.Settings.Default.Save();
                            Console.WriteLine();
                        }
                        else //Если это файл или папка не найдена выведется ошибка, но  программа продолжит работу
                        {
                            message = "ОШИБКА: Такой директории нет";
                            Console.WriteLine();
                        }
                        break;



                    case "opendir": //Открывает папку в текущей папке

                        if (command.Length == 1) // Исключение ошибки с отсутствием введенных данных
                        {
                            message = "ОШИБКА: Такой папки нет";
                            Console.WriteLine();
                        }
                        else if (Directory.Exists(Properties.Settings.Default.savedDirectory + "\\" + command[1]))
                        {
                            char[] chars = command[1].ToCharArray();
                            do
                            {
                                if (chars[chars.Length - 1] == '\\') //Проверка, если в конце есть / или \, они удалятся. Нужно для целостности ссылки
                                {
                                    command[1] = command[1].TrimEnd('\\');
                                    Array.Resize(ref chars, chars.Length - 1);
                                }
                                else if (chars[chars.Length - 1] == '/')
                                {
                                    command[1] = command[1].TrimEnd('/');
                                    Array.Resize(ref chars, chars.Length - 1);
                                }
                                else break;
                            } while (true);

                            Properties.Settings.Default.savedDirectory = Properties.Settings.Default.savedDirectory + "\\" + command[1];
                            Properties.Settings.Default.Save();
                            Console.WriteLine();
                        }
                        else
                        {
                            message = "ОШИБКА: Такой папки нет";
                            Console.WriteLine();
                        }

                        break;

                    case "del": //Удаление файла или папки

                        if (command.Length == 1) // Исключение ошибки с отсутствием введенных данных
                        { 
                            message = "ОШИБКА: Такой папки или файла нет";
                            Console.WriteLine();
                        }
                        else if (Directory.Exists(Properties.Settings.Default.savedDirectory + "\\" + command[1]))
                        {
                            char[] chars = command[1].ToCharArray();
                            do
                            {
                                if (chars[chars.Length - 1] == '\\')
                                {
                                    command[1] = command[1].TrimEnd('\\');
                                    Array.Resize(ref chars, chars.Length - 1);
                                }
                                else if (chars[chars.Length - 1] == '/')
                                {
                                    command[1] = command[1].TrimEnd('/');
                                    Array.Resize(ref chars, chars.Length - 1);
                                }
                                else break;
                            } while (true);

                            try //Если в папке будут файлы, она не удалится
                            {
                                Directory.Delete(Properties.Settings.Default.savedDirectory + "\\" + command[1]);
                                message = "Папка удалена";
                            }
                            catch (System.IO.IOException) 
                            {
                                message = "В папке есть файлы, удаление невозможно";
                            }
                            Console.WriteLine();
                        }
                        else if (File.Exists(Properties.Settings.Default.savedDirectory + "\\" + command[1]))
                        {
                            char[] chars = command[1].ToCharArray();
                            do
                            {
                                if (chars[chars.Length - 1] == '\\')
                                {
                                    command[1] = command[1].TrimEnd('\\');
                                    Array.Resize(ref chars, chars.Length - 1);
                                }
                                else if (chars[chars.Length - 1] == '/')
                                {
                                    command[1] = command[1].TrimEnd('/');
                                    Array.Resize(ref chars, chars.Length - 1);
                                }
                                else break;
                            } while (true);

                            File.Delete(Properties.Settings.Default.savedDirectory + "\\" + command[1]);
                            message = "Файл удален";
                            Properties.Settings.Default.Save();
                            Console.WriteLine();
                        }
                        else
                        {
                            message = "ОШИБКА: Такой папки или файла нет";
                            Console.WriteLine();
                        }

                        break;


                         
                    case "back": //Перейти выше по корню

                        string[] directorys = Properties.Settings.Default.savedDirectory.Split('/', '\\');
                        string adress = "";
                        for (int i = 0; i < directorys.Length-1; i++) 
                        {
                            adress = adress + directorys[i] + "\\";
                        }
                        Properties.Settings.Default.savedDirectory = adress;
                        Properties.Settings.Default.Save();
                        Properties.Settings.Default.savedDirectory = Properties.Settings.Default.savedDirectory.TrimEnd('\\');
                        Console.WriteLine();

                        break;



                    case "copy": // Копирует адрес папки или файла в файл конфигурации
                        if (command.Length == 1) // Исключение ошибки с отсутствием введенных данных
                        {
                            message = "ОШИБКА: Такой папки или файла нет: " + Properties.Settings.Default.copy + "\\" + command[1];
                            Console.WriteLine();
                        }
                        else if (Directory.Exists(Properties.Settings.Default.savedDirectory + "\\" + command[1]))
                        {
                            char[] chars = command[1].ToCharArray();
                            do
                            {
                                if (chars[chars.Length - 1] == '\\')
                                {
                                    command[1] = command[1].TrimEnd('\\');
                                    Array.Resize(ref chars, chars.Length - 1);
                                }
                                else if (chars[chars.Length - 1] == '/')
                                {
                                    command[1] = command[1].TrimEnd('/');
                                    Array.Resize(ref chars, chars.Length - 1);
                                }
                                else break;
                            } while (true);
                            Properties.Settings.Default.copy = (Properties.Settings.Default.savedDirectory + "\\" + command[1]);
                            Properties.Settings.Default.Save();
                            message = ("Адрес папки " + Properties.Settings.Default.copy + " сохранен в буфер обмена");
                            Console.WriteLine();
                        }
                        else if (File.Exists(Properties.Settings.Default.savedDirectory + "\\" + command[1]))
                        {
                            char[] chars = command[1].ToCharArray();
                            do
                            {
                                if (chars[chars.Length - 1] == '\\')
                                {
                                    command[1] = command[1].TrimEnd('\\');
                                    Array.Resize(ref chars, chars.Length - 1);
                                }
                                else if (chars[chars.Length - 1] == '/')
                                {
                                    command[1] = command[1].TrimEnd('/');
                                    Array.Resize(ref chars, chars.Length - 1);
                                }
                                else break;
                            } while (true);


                            Properties.Settings.Default.copy = (Properties.Settings.Default.savedDirectory + "\\" + command[1]);
                            Properties.Settings.Default.Save();
                            message = ("Адрес файла " + Properties.Settings.Default.copy + " сохранен в буфер обмена");
                            Console.WriteLine();
                        }
                        else
                        {
                            message = "ОШИБКА: Такой папки или файла нет: " + Properties.Settings.Default.copy + "\\" + command[1];
                            Console.WriteLine();
                        }
                        break;



                    case "copypath": // Копирует адрес папки или файла в файл конфигурации, но нужено указать полную ссылку на файл
                        if (command.Length == 1) // Исключение ошибки с отсутствием введенных данных
                        {
                            message = "ОШИБКА: Такой папки или файла нет: " + Properties.Settings.Default.copy;
                            Console.WriteLine();
                        }
                        else if (Directory.Exists(command[1]))
                        {
                            char[] chars = command[1].ToCharArray();
                            do
                            {
                                if (chars[chars.Length - 1] == '\\')
                                {
                                    command[1] = command[1].TrimEnd('\\');
                                    Array.Resize(ref chars, chars.Length - 1);
                                }
                                else if (chars[chars.Length - 1] == '/')
                                {
                                    command[1] = command[1].TrimEnd('/');
                                    Array.Resize(ref chars, chars.Length - 1);
                                }
                                else break;
                            } while (true);
                            Properties.Settings.Default.copy = (command[1]);
                            Properties.Settings.Default.Save();
                            message = ("Адрес папки " + Properties.Settings.Default.copy + " сохранен в буфер обмена");
                            Console.WriteLine();
                        }
                        else if (File.Exists(command[1]))
                        {
                            char[] chars = command[1].ToCharArray();
                            do
                            {
                                if (chars[chars.Length - 1] == '\\')
                                {
                                    command[1] = command[1].TrimEnd('\\');
                                    Array.Resize(ref chars, chars.Length - 1);
                                }
                                else if (chars[chars.Length - 1] == '/')
                                {
                                    command[1] = command[1].TrimEnd('/');
                                    Array.Resize(ref chars, chars.Length - 1);
                                }
                                else break;
                            } while (true);


                            Properties.Settings.Default.copy = (command[1]);
                            Properties.Settings.Default.Save();
                            message = ("Адрес файла " + Properties.Settings.Default.copy + " сохранен в буфер обмена");
                            Console.WriteLine();
                        }
                        else
                        {
                            message = "ОШИБКА: Такой папки или файла нет: " + Properties.Settings.Default.copy;
                            Console.WriteLine();
                        }
                        break;



                    case "seecopy":
                        message = "Копируемый файл : " + Properties.Settings.Default.copy;
                        break;



                    case "paste": //Копирует файл, ссылка которого указана в файле конфигурации
                        if (Directory.Exists(Properties.Settings.Default.copy))
                        {
                            DirectoryInfo dirInfo = new DirectoryInfo(Properties.Settings.Default.copy);
                            DirectoryInfo dirCopy = new DirectoryInfo(Properties.Settings.Default.savedDirectory + "\\" + dirInfo.Name);
                            if (!dirCopy.Exists)
                            {
                                dirCopy.Create();
                            }
                            else
                            {
                                message = "ОШИБКА: Файл есть в этой папке, копирование невозможно";
                                break;
                            }

                            message = ("Файл " + Properties.Settings.Default.copy + " был помещен в " + Properties.Settings.Default.savedDirectory);
                            Console.WriteLine();
                        }
                        else if (File.Exists(Properties.Settings.Default.copy))
                        {
                            FileInfo fileInfo = new FileInfo(Properties.Settings.Default.copy);
                            FileInfo fileCopy = new FileInfo(Properties.Settings.Default.savedDirectory + "\\" + fileInfo.Name);
                            if (!fileCopy.Exists)
                            {
                                fileInfo.CopyTo(Properties.Settings.Default.savedDirectory + "\\" + fileInfo.Name, true);
                            }
                            else 
                            {
                                message = "ОШИБКА: Файл есть в этой папке, копирование невозможно";
                                break;
                            }

                            message = ("Файл " + Properties.Settings.Default.copy + " был помещен в " + Properties.Settings.Default.savedDirectory);
                            Console.WriteLine();
                        }
                        else
                        {
                            message = "ОШИБКА: Такой папки или файла нет, адрес которой соответствует адресу в буфере обмена: " + Properties.Settings.Default.copy;
                            Console.WriteLine();
                        }


                        break;



                    case "info":
                        if (command.Length == 1) // Исключение ошибки с отсутствием введенных данных
                        {
                            message = "ОШИБКА: Такой папки или файла нет";
                            Console.WriteLine();
                        }
                        else if (Directory.Exists(Properties.Settings.Default.savedDirectory + "\\" + command[1]))
                        {
                            char[] chars = command[1].ToCharArray();
                            do
                            {
                                if (chars[chars.Length - 1] == '\\')
                                {
                                    command[1] = command[1].TrimEnd('\\');
                                    Array.Resize(ref chars, chars.Length - 1);
                                }
                                else if (chars[chars.Length - 1] == '/')
                                {
                                    command[1] = command[1].TrimEnd('/');
                                    Array.Resize(ref chars, chars.Length - 1);
                                }
                                else break;
                            } while (true);

                            DirectoryInfo dirInfo = new DirectoryInfo(Properties.Settings.Default.savedDirectory + "\\" + command[1]);
                            if (dirInfo.Exists)
                            {
                                Console.WriteLine("--------------------------------------------------------------------");
                                Console.WriteLine();
                                Console.WriteLine($"Название каталога: {dirInfo.Name}");
                                Console.WriteLine($"Полное название каталога: {dirInfo.FullName}");
                                Console.WriteLine($"Время создания каталога: {dirInfo.CreationTime}");
                                Console.WriteLine($"Корневой каталог: {dirInfo.Root}");
                                Console.WriteLine("Нажмите любую кнопку, чтобы продолжить выполнение программы");
                                Console.ReadKey();
                            }
                        }
                        else if (File.Exists(Properties.Settings.Default.savedDirectory + "\\" + command[1]))
                        {
                            char[] chars = command[1].ToCharArray();
                            do
                            {
                                if (chars[chars.Length - 1] == '\\')
                                {
                                    command[1] = command[1].TrimEnd('\\');
                                    Array.Resize(ref chars, chars.Length - 1);
                                }
                                else if (chars[chars.Length - 1] == '/')
                                {
                                    command[1] = command[1].TrimEnd('/');
                                    Array.Resize(ref chars, chars.Length - 1);
                                }
                                else break;
                            } while (true);

                            FileInfo FileInfo = new FileInfo(Properties.Settings.Default.savedDirectory + "\\" + command[1]);
                            if (FileInfo.Exists)
                            {
                                Console.WriteLine("--------------------------------------------------------------------");
                                Console.WriteLine();
                                Console.WriteLine("Имя файла: {0}", FileInfo.Name);
                                Console.WriteLine("Время создания: {0}", FileInfo.CreationTime);
                                Console.WriteLine("Размер: {0}", FileInfo.Length);
                                                            Console.WriteLine("Нажмите любую кнопку, чтобы продолжить выполнение программы");
                            Console.ReadKey();
                            }
                            Properties.Settings.Default.Save();
                            Console.WriteLine();
                        }
                        else
                        {
                            message = "ОШИБКА: Такой папки или файла нет";
                            Console.WriteLine();
                        }

                        break;



                    case "help":  //Гайд по всем командам. Также это есть в файле readme.md
                        Console.WriteLine("");
                        Console.WriteLine("--------------------------------------------------------------------");
                        Console.WriteLine("Переход по каталогам: ");
                        Console.WriteLine("goto <ссылка на папку> : Переход в эту папку");
                        Console.WriteLine("back : Выход из текущей папки в папку выше");
                        Console.WriteLine("opendir <имя папки> : Открывает папку в текущей папке");
                        Console.WriteLine("allsee : Видно все файлы и папки в текущей папке");

                        Console.WriteLine("");
                        Console.WriteLine("Взаимодействие с папками/файлами: ");
                        Console.WriteLine("del <имя папки или файла> : Удаляет файл или папку");
                        Console.WriteLine("copy <имя папки или файла в этой папке> : Копирует адрес файла или папки в буфер обмена");
                        Console.WriteLine("copypath <> : Копирует адрес файла или папки в буфер обмена");
                        Console.WriteLine("seecopy : Показывает текущий сохраненный копируемый файл");
                        Console.WriteLine("paste : Копирует папку или файл (если есть его адрес в буфере обмена) в текущую папку");
                        Console.WriteLine("info <имя папки или файла> : дает информацию по папке или файлу");

                        Console.WriteLine("");
                        Console.WriteLine("ext : Выход из программы");

                        Console.WriteLine("");
                        Console.WriteLine("Нажмите любую кнопку, чтобы продолжить выполнение программы");
                        Console.ReadKey();
                        break;



                    case "ext":
                        ext = 1;
                        Console.WriteLine("Программа завершена, нажмите любую кнопку");
                        break;



                    default:
                        message = ("Такой команды не существует");
                        break;
                }
            } while (true);
            Console.ReadKey();
        }
    }
}
