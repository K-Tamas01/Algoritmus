using System;
using System.Threading;
using System.Collections.Generic;
using System.Diagnostics;

namespace thread
{
    class Program
    {
        public static int SIZE = 10000;
        public static int[] szamok;
        public static void array_sort(int start, int end)
        {
            for(int i = start;i<end-1;i++)
                for(int j = i + 1;j<end;j++)
                    if(szamok[i] > szamok[j])
                    {
                        szamok[i] += szamok[j];
                        szamok[j] = szamok[i] - szamok[j];
                        szamok[i] = szamok[i] - szamok[j];
                    }
        }
        public static int[] expansion()
        {
            List<int> lista = new List<int>();
            Random rnd = new Random();
            for(int i = 0; i < SIZE;i++)
            {
                lista.Add(rnd.Next(100,1578945));
            }
            return lista.ToArray();
        }
        static void Main(string[] args)
        {
            var stopwatch = new Stopwatch();

            Thread[] t;

            int[] threads = {1,2,4,6,8,10,12};

            long elapsed_time;
            for (int m = 1;m<25;m++){
                for (int i = 0; i < threads.Length; i++)
                {
                    szamok = expansion();
                    t = new Thread[threads[i]];
                    int[] border = new int[threads[i] + 1];
                    border[0] = 0;
                    border[border.Length - 1] = szamok.Length - 1;
                    for (int cv = 1; cv < threads[i]; cv++)
                    {
                        border[cv] = (szamok.Length / threads[i]) + border[cv - 1];
                    }
                    stopwatch.Reset();
                    stopwatch.Start();
                    for (int j = 0; j < threads[i]; j++)
                    {
                        t[j] = new Thread(() => array_sort(border[j], border[j + 1]));
                        t[j].Start();
                        t[j].Join();
                    }
                    stopwatch.Stop();
                    elapsed_time = stopwatch.ElapsedMilliseconds;

                    Console.WriteLine(threads[i] + " szálra a futási idő: " + elapsed_time + " ms");
                    Console.WriteLine("sorba rendezett elemek száma: " + SIZE);
                }
                SIZE = m * 10000;
            }
            Console.WriteLine("DONE!");
            Console.ReadKey(true);
        }
    }
}
