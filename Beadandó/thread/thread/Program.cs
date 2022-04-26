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
            int i = start, j = end;
			int kozep = szamok[(start+end)/2];
			
			while(start<=felso)
			{
				while(start<j && szamok[start]<kozep)
					start++;
				while(end>i && szamok[end]>kozep)
					end--;
				if(start<=felso)
				{
					szamok[start] += szamok[end];
					szamok[end] = szamok[start] - szamok[end];
					szamok[start] = szamok[start] - szamok[end];
					++start;
					--end;
				}
			}
			if(start<j) array_sort(strat,j);
			if(i<end) array_sort(i,end);
        }
        public static int[] expansion()
        {
            List<int> lista = new List<int>();
            Random rnd = new Random();
            for(int i = 0; i < SIZE;i++)
            {
                lista.Add(rnd.Next(0,1578945));
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
            Console.ReadKey(true);
        }
    }
}
