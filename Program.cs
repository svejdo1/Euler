using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barbar.Euler
{
    class Program
    {
        static void Main(string[] args)
        {
            var watch = Stopwatch.StartNew();
            string result = new Problem094.Task().Run();
            watch.Stop();
            Console.Out.WriteLine(result);
            Console.Out.WriteLine(Convert.ToString(watch.Elapsed.TotalSeconds)+" s");
            Console.Out.WriteLine("Press any key to continue...");
            Console.ReadLine();
        }
    }
}
