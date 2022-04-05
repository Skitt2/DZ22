using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DZ22
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите длину массива ");
            int n = Convert.ToInt32(Console.ReadLine());

            Func<object, int[]> func = new Func<object, int[]>(GetArray);
            Task<int[]> task1 = new Task<int[]>(func, n);

            Action<Task<int[]>> action1 = new Action<Task<int[]>>(SummArray);
            Task task2 = task1.ContinueWith(action1);

            Action<Task<int[]>> action2 = new Action<Task<int[]>>(MaxArray);
            Task task3 = task1.ContinueWith(action2);

            task1.Start();

            Console.ReadKey();
        }
        static int[] GetArray(object a)
        {
            int n = (int)a;
            int[] array = new int[n];
            Random random = new Random();
            for (int i = 0; i < n; i++)
            {
                array[i] = random.Next(0, 50);
                Console.Write("{0} ", array[i]);
            }
            return array;
        }
        static void SummArray(Task<int[]> task)
        {
            int[] array = task.Result;
            int sum = 0;
            for (int i = 0; i < array.Count(); i++)
            {
                sum += array[i];
            }
            Console.WriteLine();
            Console.WriteLine("Cумма чисел массива равна {0}",sum);
        }
        static void MaxArray(Task<int[]> task)
        {
            int[] array = task.Result;
            int max = 0;
            for (int i = 0; i < array.Count(); i++)
            {
                if (array[i] > max)
                {
                    max = array[i];
                }
            }
            Console.WriteLine("Максимальное число в массиве {0}", max);
        }

    }
}
