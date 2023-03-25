using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace лаба2
{
    internal class MyProgram
    {
        enum Type { И = 0, А = 1, Т = 2 }

        struct TV_series
        {
            public string title;
            public string Name;
            public int rating;
            public Type type;

            public TV_series fromString(String str)
            {
                String[] tmp = str.Split(';');
                TV_series tv = new TV_series();
                tv.title = tmp[0];
                tv.Name = tmp[1];
                tv.rating = Int32.Parse(tmp[2]);
                tv.type = (Type)Int32.Parse(tmp[3]);

                return tv;
            }
            public string toString()
            {
                return String.Format("{0};{1};{2};{3}\r\n",
                   this.title,
                   this.Name,
                   this.rating,
                   (int)this.type
                   );
            }
        }
        class SortTypeTimeCount
        {
            public long printTableWithSortTime = 0;
            public long InsertionSortTime = 0;
            public long SaveTime = 0;
            public long LoadTime = 0;
            public long New_ChisloTime = 0;
            public long Binary_FileTime = 0;
            public long New_TextTime = 0;
            public long printTableSystemTime = 0;
            public long addTableRowTime = 0;
            public long removeFromTableTime = 0;
            public long editTableRowTime = 0;
            public long SearchTVTime = 0;
        }

        List<TV_series> table = new List<TV_series>();

        Log logger = new Log();

        string[] test = { "и", "а", "т" };


        public void Menu()
        {
            string Menu = "1 - Просмотр таблицы\n" +
                "2 - Добавить запись\n" +
                "3 - Удалить запись\n" +
                "4 - Обновить запись\n" +
                "5 - Поиск записей\n" +
                "6 - Просмотреть лог\n" +
                "7 - Создание бэкапа\n ----------------------------------- \n" +
                "8 - Выход\n------------------------------------\n" +
                "9 - Просмотр таблицы c сортировкой\n";
            Console.Write(Menu);
        }

        public void printTableWithSort()
        {
            Console.WriteLine("Выберите тип портировки:\n1 - Name, <");
            Console.WriteLine("2 - Name, >");
            Console.WriteLine("3 - Title, <");
            Console.WriteLine("4 - Title, >");
            Console.WriteLine("5 - Rating, <");
            Console.WriteLine("6 - Rating, >");
            Console.WriteLine("7 - Type, <");
            Console.WriteLine("8 - Type, >");
            Console.WriteLine("9 - Не сортировать");
            int selectedType = 1;
            SortTypeTimeCount count = new SortTypeTimeCount();
            long tmpTime1 = DateTime.Now.Ticks;
            do
            {
                try
                {
                    selectedType = Int32.Parse(Console.ReadLine());
                }
                catch
                {
                    selectedType = 0;
                }
                if (selectedType > 0 && selectedType <= 9)
                    break;
                else
                    Console.WriteLine("Ошибка выбора");
            }
            while (true);
            long tmpTime2 = DateTime.Now.Ticks;
            count.printTableWithSortTime = tmpTime2 - tmpTime1;
            Console.WriteLine($"Алгоритм сортировки: {count.printTableWithSortTime / 1000}с \\ {count.printTableWithSortTime} мс");
            List <TV_series> tmpList = new List<TV_series>();
            tmpList.Clear();
            switch (selectedType)
            {
                case 1:
                    tmpList.AddRange(InsertionSort(this.table.ToArray(), "name", true));
                    break;
                case 2:
                    tmpList.AddRange(InsertionSort(this.table.ToArray(), "name", false));
                    break;
                case 3:
                    tmpList.AddRange(InsertionSort(this.table.ToArray(), "title", true));
                    break;
                case 4:
                    tmpList.AddRange(InsertionSort(this.table.ToArray(), "title", false));
                    break;
                case 5:
                    tmpList.AddRange(InsertionSort(this.table.ToArray(), "rating", true));
                    break;
                case 6:
                    tmpList.AddRange(InsertionSort(this.table.ToArray(), "rating", false));
                    break;
                case 7:
                    tmpList.AddRange(InsertionSort(this.table.ToArray(), "type", true));
                    break;
                case 8:
                    tmpList.AddRange(InsertionSort(this.table.ToArray(), "type", false));
                    break;
                case 9:
                    break;
            }
            printTableSystem(tmpList);
        }

        static TV_series[] InsertionSort(TV_series[] mas, String field = "Name", bool direction = true) 
        {
            Console.WriteLine();
            SortTypeTimeCount count = new SortTypeTimeCount();
            long tmpTime1 = DateTime.Now.Ticks;
            for (int i = 1; i < mas.Length; i++)
            {
                if (direction)
                {
                    TV_series k = mas[i];
                    int j = i - 1;
                    switch (field)
                    {
                        default:
                        case "name":
                            while (j >= 0 && mas[j].Name[0] < k.Name[0])
                            {
                                mas[j + 1] = mas[j];
                                j--;
                            }
                            mas[j + 1] = k;
                            break;
                        case "title":
                            while (j >= 0 && mas[j].title[0] < k.title[0])
                            {
                                mas[j + 1] = mas[j];
                                j--;
                            }
                            mas[j + 1] = k;
                            break;
                        case "rating":
                            while (j >= 0 && mas[j].rating < k.rating)
                            {
                                mas[j + 1] = mas[j];
                                j--;
                            }
                            mas[j + 1] = k;
                            break;
                        case "type":
                            while (j >= 0 && mas[j].type < k.type)
                            {

                                mas[j + 1] = mas[j];
                                j--;
                            }
                            mas[j + 1] = k;
                            break;
                    }
                }
                if (!direction)
                {
                    TV_series k = mas[i];
                    int j = i - 1;
                    switch (field)
                    {
                        default:
                        case "name":
                            while (j >= 0 && mas[j].Name[0] > k.Name[0])
                            {
                                mas[j + 1] = mas[j];
                                j--;
                            }
                            mas[j + 1] = k;
                            break;
                        case "title":
                            while (j >= 0 && mas[j].title[0] > k.title[0])
                            {
                                mas[j + 1] = mas[j];
                                j--;
                            }
                            mas[j + 1] = k;
                            break;
                        case "rating":
                            while (j >= 0 && mas[j].rating > k.rating)
                            {
                                mas[j + 1] = mas[j];
                                j--;
                            }
                            mas[j + 1] = k;
                            break;
                        case "type":
                            while (j >= 0 && mas[j].type > k.type)
                            {
                                mas[j + 1] = mas[j];
                                j--;
                            }
                            mas[j + 1] = k;
                            break;
                    }
                }
            }
            long tmpTime2 = DateTime.Now.Ticks;
            count.InsertionSortTime = tmpTime2 - tmpTime1;
            Console.WriteLine($"Алгоритм сортировки: {count.InsertionSortTime / 1000}с \\ {count.InsertionSortTime} мс");
            return mas;
        }
        //сохранение файла
        public void save()
        {
            SortTypeTimeCount count = new SortTypeTimeCount();
            long tmpTime1 = DateTime.Now.Ticks;
            using (StreamWriter writer = new StreamWriter(@"C:\Users\Пользователь\Desktop\lab.dat"))
            {
                foreach (TV_series tv in table)
                    writer.Write(tv.toString());

            }
            long tmpTime2 = DateTime.Now.Ticks;
            count.SaveTime = tmpTime2 - tmpTime1;
            Console.WriteLine($"Алгоритм сортировки: {count.SaveTime / 1000}с \\ {count.SaveTime} мс");

        }

        //бэкап, задание 4
        public void makeBackup()
        {
            String dir = @"D:\Lab6_Temp";
            String path = dir;
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            if (!File.Exists(@"C:\Users\Пользователь\Desktop\lab.dat"))
            {
                Console.WriteLine("Файл lab.dat не найден");
                return;
            }

            path = path + "\\lab.dat";

            if (!File.Exists(path))
                File.Copy(@"C:\Users\Пользователь\Desktop\lab.dat", path);

            List<String> tmp = new List<String>();
            using (BinaryReader br = new BinaryReader(File.Open(path, FileMode.Open)))
            {
                using (BinaryWriter bw = new BinaryWriter(File.Open(dir + "\\lab_backup.dat", FileMode.OpenOrCreate)))
                {
                    while (br.PeekChar() != -1)
                    {
                        char name = br.ReadChar();
                        bw.Write(name);
                    }
                }
            }
            Console.WriteLine("Бэкап успешно создан");
        }

        //выгрузка файла
        public void load()
        {
            String path = @"C:\Users\Пользователь\Desktop\lab.dat";
            if (!File.Exists(path))
                return;

            String[] str = File.ReadAllLines(path);
            table.Clear();
            SortTypeTimeCount count = new SortTypeTimeCount();
            long tmpTime1 = DateTime.Now.Ticks;
            foreach (String row in str)
            {
                TV_series tv = new TV_series();
                Console.WriteLine($"test=>{row}");

                table.Add(tv.fromString(row));
            }
            long tmpTime2 = DateTime.Now.Ticks;
            count.LoadTime = tmpTime2 - tmpTime1;
            Console.WriteLine($"Алгоритм сортировки: {count.LoadTime / 1000}с \\ {count.LoadTime} мс");
        }

        //созданиие файла с числами, задание 2
        private void New_Chislo()
        {
            BinaryWriter bw = new BinaryWriter(File.Open(@"C:\Users\Пользователь\Desktop\binary_file1", FileMode.OpenOrCreate));
            SortTypeTimeCount count = new SortTypeTimeCount();
            long tmpTime1 = DateTime.Now.Ticks;
            for (int i = 4; i <= 84; i++)
            {
                bw.Write(i);
                bw.Write(Math.Pow(i, 1 / 2));
                i++;
            }
            long tmpTime2 = DateTime.Now.Ticks;
            count.New_ChisloTime = tmpTime2 - tmpTime1;
            Console.WriteLine($"Алгоритм сортировки: {count.New_ChisloTime / 1000}с \\ {count.New_ChisloTime} мс");
            
            bw.Close();
            Binary_File();
        }

        public void printBinary()
        {
            New_Chislo();
        }

        //создание бинарного файла и его копии со вторыми числами
        static void Binary_File()
        {
            BinaryReader br = new BinaryReader(File.Open(@"C:\Users\Пользователь\Desktop\binary_file1", FileMode.Open));
            BinaryWriter bw = new BinaryWriter(File.Open(@"C:\Users\Пользователь\Desktop\binary_file2", FileMode.OpenOrCreate));
            SortTypeTimeCount count = new SortTypeTimeCount();
            long tmpTime1 = DateTime.Now.Ticks;
            while (br.PeekChar() > -1)
            {
                br.BaseStream.Position += 4;
                bw.Write(br.ReadDouble());
            }
            long tmpTime2 = DateTime.Now.Ticks;
            count.Binary_FileTime = tmpTime2 - tmpTime1;
            Console.WriteLine($"Алгоритм сортировки: {count.Binary_FileTime / 1000}с \\ {count.Binary_FileTime} мс");
            br.Close();
            bw.Close();
        }

        //создание файла без чисел, задание 3
        private void New_Text()
        {
            string del = File.ReadAllText(@"C:\Users\Пользователь\Desktop\lab.dat");
            StreamWriter sw = new StreamWriter(File.Open(@"C:\Users\Пользователь\Desktop\lab_nochisla.dat", FileMode.OpenOrCreate));
            int dl = del.Length;
            SortTypeTimeCount count = new SortTypeTimeCount();
            long tmpTime1 = DateTime.Now.Ticks;
            foreach (char i in del)
            {
                if (Char.IsDigit(i) == true)
                    del = del.Replace(i.ToString(), "");
                sw.Write(del);
            }
            long tmpTime2 = DateTime.Now.Ticks;
            count.New_TextTime = tmpTime2 - tmpTime1;
            Console.WriteLine($"Алгоритм сортировки: {count.New_TextTime / 1000}с \\ {count.New_TextTime} мс");
            Console.WriteLine($"Кол-во удаленных чисел: {dl - del.Length}");
            sw.Close();
            Console.ReadLine();
        }

        public void printNew_Text()
        {
            New_Text();
        }

        private void printTableSystem(List<TV_series> list)
        {
            Console.WriteLine("Телепередачи");
            Console.WriteLine("Передача\t\tВедущий\t\tРейтинг\t\tТип");
            SortTypeTimeCount count = new SortTypeTimeCount();
            long tmpTime1 = DateTime.Now.Ticks;
            if (list.Count == 0)
            {
                Console.WriteLine("Записей в таблице нет");
                return;
            }

            foreach (TV_series tv in list)
            {
                Console.Write(String.Format("{0:20} {1:20} {2:20} {3:20} \n", tv.title.PadRight(20, ' '),
                    ("" + tv.Name).PadRight(20, ' '), ("" + tv.rating).PadRight(20, ' '), test[((int)tv.type)].ToUpper().PadRight(20, ' ')));
            }
            long tmpTime2 = DateTime.Now.Ticks;
            count.printTableSystemTime = tmpTime2 - tmpTime1;
            Console.WriteLine($"Алгоритм сортировки: {count.printTableSystemTime / 1000}с \\ {count.printTableSystemTime} мс");

            Console.WriteLine("Перечисляемый тип: И - игровая; А - аналитическая; Т – ток-шоу ");
        }

        public void printTable()
        {
            printTableSystem(this.table);
        }

        private void addTableRow()
        {
            TV_series NewTV_series = new TV_series();
            Console.WriteLine("Введите название телепередачи: ");
            NewTV_series.title = Console.ReadLine();

            Console.WriteLine("Введите имя ведущего: ");
            NewTV_series.Name = Console.ReadLine();


            Console.WriteLine("Введите рейтинг телепередачи: ");
            int rating = 0;
            Boolean ratingError = false;
            SortTypeTimeCount count = new SortTypeTimeCount();
            long tmpTime1 = DateTime.Now.Ticks;
            do
            {
                int Rating = Convert.ToInt32(Console.ReadLine());
                if (Rating <= 5 && Rating > 0)
                {
                    rating = Rating;
                    ratingError = false;
                }
                else
                {
                    Console.WriteLine("Введите правильный номер рейтинга: (1, 2, 3, 4, 5)");
                    ratingError = true;
                }
            } while (ratingError == true);

            NewTV_series.rating = rating;

            Boolean typeError = true;
            while (typeError)
            {
                Console.WriteLine("Введите жанр телепередачи: И - игровая; А - аналитическая; Т – ток-шоу: ");
                string type = Console.ReadLine();

                if (Array.IndexOf(test, type.ToLower().Trim()) != -1)
                {
                    if (type.ToLower().Trim().Equals("и"))
                        NewTV_series.type = Type.И;
                    if (type.ToLower().Trim().Equals("а"))
                        NewTV_series.type = Type.А;
                    if (type.ToLower().Trim().Equals("т"))
                        NewTV_series.type = Type.Т;

                    typeError = false;
                }
                else
                    Console.WriteLine("Введите правильный жанр телепередачи: И - игровая; А - аналитическая; Т – ток-шоу: ");
            }
            long tmpTime2 = DateTime.Now.Ticks;
            count.addTableRowTime = tmpTime2 - tmpTime1;
            Console.WriteLine($"Алгоритм сортировки: {count.addTableRowTime / 1000}с \\ {count.addTableRowTime} мс");
            table.Add(NewTV_series);

            Console.WriteLine("Вы ввели следующие данные:");
            var strTable = String.Format("{0:20} {1:20} {2:20} {3:20}\n", NewTV_series.title.PadRight(20, ' '), ("" + NewTV_series.Name).PadRight(20, ' '),
                 ("" + NewTV_series.rating).PadRight(20, ' '), test[((int)NewTV_series.type)].ToUpper().PadRight(20, ' '));

            Console.Write(strTable);
            Console.WriteLine("Строка успешно добавлена");
            logger.info(strTable);
        }

        private void removeFromTable()
        {
            this.printTable();
            Console.WriteLine("Введите номер записи: ");
            SortTypeTimeCount count = new SortTypeTimeCount();
            long tmpTime1 = DateTime.Now.Ticks;
            try
            {
                int number_delete = Int32.Parse(Console.ReadLine());

                if (number_delete > 0 && number_delete <= table.Count)
                {
                    table.RemoveAt(number_delete - 1);
                    logger.info($"Удалена запись #{number_delete}");
                    Console.WriteLine($"Удалена запись #{number_delete}");
                }
                else
                {
                    Console.WriteLine($"Запись с идентификатором {number_delete} не найдена");
                    logger.info($"Удалена запись #{number_delete}");
                }
            }
            catch
            {
                Console.WriteLine($"Ошибка ввода идентификатора");
            }
            long tmpTime2 = DateTime.Now.Ticks;
            count.removeFromTableTime = tmpTime2 - tmpTime1;
            Console.WriteLine($"Алгоритм сортировки: {count.removeFromTableTime / 1000}с \\ {count.removeFromTableTime} мс");
        }

        private void editTableRow()
        {
            printTable();
            TV_series NewTV_series = new TV_series();
            Console.WriteLine("Введите номер записи: ");
            bool changeError = false;
            SortTypeTimeCount count = new SortTypeTimeCount();
            long tmpTime1 = DateTime.Now.Ticks;
            do
            {
                int number_change = Convert.ToInt32(Console.ReadLine());
                if (number_change > 0 && number_change <= table.Count)
                {
                    string OldTitle = table[number_change - 1].title;
                    Console.WriteLine("Введите новое название телепередачи: ");
                    string title = Console.ReadLine();

                    NewTV_series.title = title;

                    if (title == string.Empty)
                    {
                        title = OldTitle;
                    }
                    string OldName = table[number_change - 1].Name;
                    Console.WriteLine("Введите новое название ведущего: ");
                    string Name = Console.ReadLine();

                    NewTV_series.Name = Name;

                    if (Name == string.Empty)
                    {
                        Name = OldName;
                    }

                    int OldRating = table[number_change - 1].rating;
                    Console.WriteLine("Введите рейтинг новой телепередачи: ");
                    int rating = Convert.ToInt32(Console.ReadLine());

                    NewTV_series.rating = rating;

                    if (rating <= 5 && rating > 0)
                    {
                        changeError = false;
                    }

                    else if (rating > 5 && rating <= 0)
                    {
                        rating = OldRating;
                        changeError = false;
                    }

                    else
                    {
                        Console.WriteLine("Введите правильный рейтинг (1, 2, 3, 4, 5");
                        changeError = true;
                    }

                    var OldType = table[number_change - 1].type;

                    Boolean typeError = true;
                    while (typeError)
                    {
                        Console.WriteLine("Введите жанр телепередачи: И - игровая; А - аналитическая; Т – ток-шоу: ");
                        string type = Console.ReadLine();

                        if (Array.IndexOf(test, type.ToLower().Trim()) != -1)
                        {
                            if (type.ToLower().Trim().Equals("и"))
                                NewTV_series.type = Type.И;
                            if (type.ToLower().Trim().Equals("а"))
                                NewTV_series.type = Type.А;
                            if (type.ToLower().Trim().Equals("т"))
                                NewTV_series.type = Type.Т;

                            typeError = false;
                        }
                        else
                            Console.WriteLine("Введите правильный жанр телепередачи: И - игровая; А - аналитическая; Т – ток-шоу: ");
                    }

                    table.RemoveAt(number_change - 1);
                    table.Insert(number_change - 1, NewTV_series);

                    Console.WriteLine("Вы ввели следующие данные:");
                    var strTable = String.Format("{0:20} {1:20} {2:20} {3:20}\n", NewTV_series.title.PadRight(20, ' '), ("" + NewTV_series.Name).PadRight(20, ' '),
                         ("" + NewTV_series.rating).PadRight(20, ' '), test[((int)NewTV_series.type)].ToUpper().PadRight(20, ' '));

                    Console.Write(strTable);
                    Console.WriteLine("Строка успешно обновлена!");
                }
                else
                {
                    Console.WriteLine("Введите правильный номер");
                }

            } while (changeError == true);
            long tmpTime2 = DateTime.Now.Ticks;
            count.editTableRowTime = tmpTime2 - tmpTime1;
            Console.WriteLine($"Алгоритм сортировки: {count.editTableRowTime / 1000}с \\ {count.editTableRowTime} мс");
        }

        private void searchTV()
        {
            Console.WriteLine("Введите жанр телепередачи: И - игровая; А - аналитическая; Т – ток-шоу: ");
            string search = Console.ReadLine();
            List<TV_series> list = new List<TV_series>();
            int genre = Array.IndexOf(test, search.ToLower().Trim());
            SortTypeTimeCount count = new SortTypeTimeCount();
            long tmpTime1 = DateTime.Now.Ticks;
            if (genre == -1)
            {
                Console.WriteLine("Данный жанр не найден");
                return;
            }
            foreach (TV_series row in table)
            {
                if ((int)row.type == genre)
                {
                    list.Add(row);
                }
            }
            long tmpTime2 = DateTime.Now.Ticks;
            count.SearchTVTime = tmpTime2 - tmpTime1;
            Console.WriteLine($"Алгоритм сортировки: {count.SearchTVTime / 1000}с \\ {count.SearchTVTime} мс");
            printTableSystem(list);
        }

        public Boolean handler(int variant)
        {
            switch (variant)
            {
                default:
                case 0:
                    Console.WriteLine("Ошибочный ввод данных");
                    logger.warning("Ошибочный ввод данных");
                    break;
                case 1:
                    logger.info("Вывод таблицы");
                    this.printTable();
                    break;
                case 2:
                    logger.info("Добавление запись");
                    this.addTableRow();
                    break;
                case 3:
                    logger.info("Удаление запись");
                    this.removeFromTable();
                    break;
                case 4:
                    logger.info("Обновление записи");
                    this.editTableRow();
                    break;
                case 5:
                    logger.info("Поиск записи");
                    this.searchTV();
                    break;
                case 6:
                    logger.info("Отображение логов");
                    logger.printLogs();
                    break;
                case 7:
                    logger.info("Создание бэкапа");
                    makeBackup();
                    break;
                case 9:
                    logger.info("Вывод таблицы с сортировкой");
                    this.printTableWithSort();
                    break;
                case 8:
                    return false;
            }
            return true;
        }
    }
}
