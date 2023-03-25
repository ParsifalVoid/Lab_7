internal class Program
{

    struct SortTypeTimeCount
    {
        public long ViborSortTimeCount = 0;
        public long ViborSortSwapCount = 0;
        
        public long InsertationSortTimeCount = 0;
        public long InsertationSwapCount = 0;
        
        public long BubleSortTimeCount = 0;
        public long BubleSortSwapCount = 0;
        
        public long ShakerSortTimeCount = 0;
        public long ShakerSortSwapCount = 0;
        
        public long ShellSortTimeCount = 0;
        public long ShellSortSwapCount = 0;

        public SortTypeTimeCount()
        {

        }
    }

    static SortTypeTimeCount count;
    static int[] ViborSort(int[] mas1, bool Direction = true)
    {
        long tmpTime1 = DateTime.Now.Ticks;
            for (int i = 0; i < mas1.Length - 1; i++)
            {
                if (Direction)
                {
                int min = i;
                for (int j = i + 1; j < mas1.Length; j++)
                {
                    if (mas1[j] < mas1[min])
                    {
                        min = j;
                    }
                }
                int temp = mas1[min];
                mas1[min] = mas1[i];
                mas1[i] = temp;
                count.ViborSortSwapCount++;
                }
                else
                {
                  int max = i;
                  for (int j = i + 1; j < mas1.Length; j++)
                  {
                      if (mas1[j] > mas1[max])
                      {
                          max = j;
                      }
                  }
                  int temp = mas1[max];
                  mas1[max] = mas1[i];
                  mas1[i] = temp;
                  count.ViborSortSwapCount++;
                }
                
            }
        long tmpTime2 = DateTime.Now.Ticks;
        count.ViborSortTimeCount = tmpTime2 - tmpTime1;
        return mas1;
    }

    static int[] InsertationSort(int[] mas2, bool Direction = true)
    {
        long tmpTime1 = DateTime.Now.Ticks;
        for (int i = 1; i < mas2.Length; i++)
        {
            if (Direction)
            {
                int k = mas2[i];
                int j = i - 1;

                while (j >= 0 && mas2[j] > k)
                {
                    mas2[j + 1] = mas2[j];
                    mas2[j] = k;
                    j--;
                    count.InsertationSwapCount++;
                }
            }
            else
            {
                int k = mas2[i];
                int j = i - 1;

                while (j >= 0 && mas2[j] < k)
                {
                    mas2[j + 1] = mas2[j];
                    mas2[j] = k;
                    j--;
                    count.InsertationSwapCount++;
                }
            }
            
        }
        long tmpTime2 = DateTime.Now.Ticks;
        count.InsertationSortTimeCount = tmpTime2 - tmpTime1;
        return mas2;
    }

    static int[] BubbleSort(int[] mas3, bool Direction = true)
    {
        int temp = 0;
        long tmpTime1 = DateTime.Now.Ticks;
        for (int write = 0; write < mas3.Length; write++)
        {
            for (int sort = 0; sort < mas3.Length - 1; sort++)
            {
                if (Direction)
                {
                    if (mas3[sort] > mas3[sort + 1])
                    {
                        temp = mas3[sort + 1];
                        mas3[sort + 1] = mas3[sort];
                        mas3[sort] = temp;
                        count.BubleSortSwapCount++;
                    }
                }
                else
                {
                    if (mas3[sort] < mas3[sort + 1])
                    {
                        temp = mas3[sort + 1];
                        mas3[sort + 1] = mas3[sort];
                        mas3[sort] = temp;
                        count.BubleSortSwapCount++;
                    }
                }
            }
        }
        long tmpTime2 = DateTime.Now.Ticks;
        count.BubleSortTimeCount = tmpTime2 - tmpTime1;
        return mas3;
    }

    static void Swap(ref int e1, ref int e2, string sortType="shaker")
    {
        var temp = e1;
        e1 = e2;
        e2 = temp;

        if (sortType=="shaker")
            count.ShakerSortSwapCount++;
        if (sortType == "shell")
            count.ShellSortSwapCount++;
    }
    static int[] ShakerSort(int[] mas4, bool Direction = true)
    {
        long tmpTime1 = DateTime.Now.Ticks;
        for (var i = 0; i < mas4.Length / 2; i++)
        {
            var swapFlag = false;

            if (Direction)
            {
                for (var j = i; j < mas4.Length - i - 1; j++)
                {
                    if (mas4[j] > mas4[j + 1])
                    {
                        Swap(ref mas4[j], ref mas4[j + 1], "shaker");
                        swapFlag = true;
                    }
                }

                for (var j = mas4.Length - 2 - i; j > i; j--)
                {
                    if (mas4[j - 1] > mas4[j])
                    {
                        Swap(ref mas4[j - 1], ref mas4[j], "shaker");
                        swapFlag = true;
                    }
                }
            }
            else
            {
                for (var j = i; j < mas4.Length - i - 1; j++)
                {
                    if (mas4[j] < mas4[j + 1])
                    {
                        Swap(ref mas4[j], ref mas4[j + 1], "shaker");
                        swapFlag = true;
                    }
                }

                for (var j = mas4.Length - 2 - i; j > i; j--)
                {
                    if (mas4[j - 1] < mas4[j])
                    {
                        Swap(ref mas4[j - 1], ref mas4[j], "shaker");
                        swapFlag = true;
                    }
                }
            }

            if (!swapFlag)
            {
                break;
            }
        }
        long tmpTime2 = DateTime.Now.Ticks;
        count.ShakerSortTimeCount = tmpTime2 - tmpTime1;
        return mas4;
    }

    static int[] ShellSort(int[] mas5, bool Direction = true)
    {
        long tmpTime1 = DateTime.Now.Ticks;
        var d = mas5.Length / 2;
        while (d >= 1)
        {
            for (var i = d; i < mas5.Length; i++)
            {
                if (Direction)
                {
                    var j = i;
                    while ((j >= d) && (mas5[j - d] > mas5[j]))
                    {
                        Swap(ref mas5[j], ref mas5[j - d], "shell");
                        j = j - d;
                    }
                }
                else
                {
                    var j = i;
                    while ((j >= d) && (mas5[j - d] < mas5[j]))
                    {
                        Swap(ref mas5[j], ref mas5[j - d], "shell");
                        j = j - d;
                    }
                } 
            }

            d = d / 2;
        }
        long tmpTime2 = DateTime.Now.Ticks;
        count.ShellSortTimeCount = tmpTime2 - tmpTime1;
        return mas5;
    }
    
    static int[] GenerateArr(int count)
    {
        int[] arr = new int[count];

        Random rand = new Random();

        int index = 0;
        arr.ToList().ForEach(item =>
        {
            arr[index] = rand.Next(0, 99999);
            index++;
        });
        arr.ToList().ForEach(item => Console.WriteLine(item));
        return arr;
    }

    static bool CheckSort(int[] mas, bool Direction = true)
    {
        for (int write = 0; write < mas.Length; write++)
        {
            for (int sort = 0; sort < mas.Length - 1; sort++)
            {
                if (!Direction)
                {
                    if (mas[sort] > mas[sort + 1])
                    {
                        return false;
                    }
                }
                else
                {
                    if (mas[sort] < mas[sort + 1])
                    {
                        return false;
                    }
                }
            }
        }
        return true;
    }
    static void saveToFile(string fileName, int[] arr)
    {
        using(StreamWriter sw = new StreamWriter(fileName))
        {
            arr.ToList().ForEach(item =>
           { 
               sw.WriteLine(item); 
           });
        }
    }
    static int[] loadFromFile(string fileName)
    {
        List<int> tmp = new List<int>();
        using (StreamReader sr = new StreamReader(fileName))
        {
            while(sr.Peek() != -1)
            {
                tmp.Add(Int32.Parse(sr.ReadLine()));
            }
        }
        return tmp.ToArray();
    }
    private static void Main(string[] args)
    {
        int[] arr = GenerateArr(1000);
        arr = ViborSort(arr);

        saveToFile("vibor-sorted.dat", arr);

        arr = GenerateArr(1000);
        arr = InsertationSort(arr);
        
        saveToFile("insertation-sorted.dat", arr);

        arr = GenerateArr(1000);
        arr = BubbleSort(arr);

        saveToFile("bubble-sorted.dat", arr);

        arr = GenerateArr(1000);
        arr = ShakerSort(arr);

        saveToFile("shaker-sorted.dat", arr);

        arr = GenerateArr(1000);
        arr = ShellSort(arr);

        saveToFile("shell-sorted.dat", arr);

        Console.WriteLine($"Сортировка выбором: {count.ViborSortTimeCount / 1000}с \\ {count.ViborSortTimeCount} мс, кол-во перестановок {count.ViborSortSwapCount}");
        Console.WriteLine($"Сортировка вставками: {count.InsertationSortTimeCount / 1000}с \\ {count.InsertationSortTimeCount} мс, кол-во перестановок {count.InsertationSwapCount}");
        Console.WriteLine($"Сортировка пузырьком: {count.BubleSortTimeCount / 1000}с \\ {count.BubleSortTimeCount} мс, кол-во перестановок {count.BubleSortSwapCount}");
        Console.WriteLine($"Сортировка Шейкер: {count.ShakerSortTimeCount / 1000}с \\ {count.ShakerSortTimeCount} мс, кол-во перестановок {count.ShakerSortSwapCount}");
        Console.WriteLine($"Сортировка Шелла: {count.ShellSortTimeCount / 1000}с \\ {count.ShellSortTimeCount} мс, кол-во перестановок {count.ShellSortSwapCount}");

        bool isSort = false;
        isSort = CheckSort(loadFromFile("vibor-sorted.dat"), true);
        Console.WriteLine($"vibor-sorted.dat > {(isSort ? "Сортировано" : "Не сортировано")}");
        isSort = CheckSort(loadFromFile("vibor-sorted.dat"), false);
        Console.WriteLine($"vibor-sorted.dat > {(isSort ? "Сортировано" : "Не сортировано")}");

        isSort = CheckSort(loadFromFile("insertation-sorted.dat"), true);
        Console.WriteLine($"insertation-sorted.dat > {(isSort ? "Сортировано" : "Не сортировано")}");
        isSort = CheckSort(loadFromFile("insertation-sorted.dat"), false);
        Console.WriteLine($"insertation-sorted.dat > {(isSort ? "Сортировано" : "Не сортировано")}");

        isSort = CheckSort(loadFromFile("bubble-sorted.dat"), true);
        Console.WriteLine($"bubble-sorted.dat > {(isSort ? "Сортировано" : "Не сортировано")}");
        isSort = CheckSort(loadFromFile("bubble-sorted.dat"), false);
        Console.WriteLine($"bubble-sorted.dat > {(isSort ? "Сортировано" : "Не сортировано")}");

        isSort = CheckSort(loadFromFile("vshaker-sorted.dat"), true);
        Console.WriteLine($"shaker-sorted.dat > {(isSort ? "Сортировано" : "Не сортировано")}");
        isSort = CheckSort(loadFromFile("vshaker-sorted.dat"), false);
        Console.WriteLine($"shaker-sorted.dat > {(isSort ? "Сортировано" : "Не сортировано")}");

        isSort = CheckSort(loadFromFile("shell-sorted.dat"), true);
        Console.WriteLine($"shell-sorted.dat > {(isSort ? "Сортировано" : "Не сортировано")}");
        isSort = CheckSort(loadFromFile("shell-sorted.dat"), false);
        Console.WriteLine($"shell-sorted.dat > {(isSort ? "Сортировано" : "Не сортировано")}");

    }

}
