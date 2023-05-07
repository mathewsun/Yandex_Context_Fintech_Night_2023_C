using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Yandex_Task_C
{
    internal class Program
    {
        static long[] GetFullTime(Dictionary<int, List<(int, int)>> dict, int start, int total)
        {
            long result = 0;
            long[] time = new long[total];
            Array.Fill(time, -1);
            PriorityQueue<int, long> queue = new PriorityQueue<int, long>();
            queue.Enqueue(start, 0);

            while (queue.Count > 0)
            {
                int f;
                long priority;
                queue.TryDequeue(out f, out priority);
                if (time[f - 1] == -1)
                {
                    time[f - 1] = priority;
                    foreach (var item in dict[f])
                    {
                        queue.Enqueue(item.Item1, priority + item.Item2);
                    }
                }
            }

            return time;
        }

        static void Main(string[] args)
        {
            var parArr = Console.ReadLine().Split(' ').Select(x => Convert.ToInt32(x)).ToArray();
            int n = parArr[0];
            int x = parArr[1];
            int m = Convert.ToInt32(Console.ReadLine());

            Dictionary<int, List<(int, int)>> dict = new Dictionary<int, List<(int, int)>>();
            for (int i = 1; i <= n; i++)
            {
                dict[i] = new List<(int, int)>();
            }

            for (int i = 0; i < m; i++)
            {
                var arr = Console.ReadLine().Split(' ').Select(x => Convert.ToInt32(x)).ToList();
                dict[arr[0]].Add((arr[1], arr[2]));
            }

            var time = GetFullTime(dict, x, n);

            if (time.Any(x => x == -1))
            {
                Console.WriteLine(-1);
            }
            else
            {
                Console.WriteLine(time.Max());
            }
        }
    }
}