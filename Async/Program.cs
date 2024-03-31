using System;
using System.Diagnostics;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        meow:
        Console.Write("\nВведите размер массива: ");
        int size = int.Parse(Console.ReadLine());

        int[,] array = new int[size, size];
        Stopwatch stopSync = Stopwatch.StartNew();
        ArraySynс(array);
        stopSync.Stop();
        Console.WriteLine($"Статический метод заполнения занял {stopSync.ElapsedMilliseconds} миллисекунд.");
        PrintArray(array); 

        Stopwatch stopAsync = Stopwatch.StartNew();
        await ArrayAsync(array);
        stopAsync.Stop();
        Console.WriteLine($"Асинхронный метод заполнения занял {stopAsync.ElapsedMilliseconds} миллисекунд.");
        PrintArray(array);

        goto meow;
    }

    static void ArraySynс(int[,] array)
    {
        Random random = new Random();
        for (int i = 0; i < array.GetLength(0); i++)
        {
            for (int j = 0; j < array.GetLength(1); j++)
            {
                array[i, j] = random.Next(100);
            }
        }
    }

    static async Task ArrayAsync(int[,] array)
    {
        Random random = new Random();
        Task[] tasks = new Task[array.GetLength(0)];

        for (int i = 0; i < array.GetLength(0); i++)
        {
            int rowIndex = i;
            tasks[i] = Task.Run(() =>
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    array[rowIndex, j] = random.Next(100);
                }
            });
        }

        await Task.WhenAll(tasks);
    }

    static void PrintArray(int[,] array)
    {
        for (int i = 0; i < array.GetLength(0); i++)
        {
            for (int j = 0; j < array.GetLength(1); j++)
            {
                Console.Write($"{array[i, j]} ");
            }
            Console.WriteLine();
        }
    }
}