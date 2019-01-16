using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreatingAndStartingTasks
{
    static class Program
    {
        public static void CountingProcedure()
        {
            Console.WriteLine($"Task with Id:{Task.CurrentId} has been started");
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine($"Task with Id:{Task.CurrentId} counting : {i}");
            }
            Console.WriteLine($"Task with Id:{Task.CurrentId} has completed.");
        }

        public static void StartWithStartNew()
        {
            Console.WriteLine("I am entering StartWithStartNew");
            var t = Task.Factory.StartNew(CountingProcedure);
            Console.WriteLine("I am leaving StartWithStartNew");
        }

        public static void StartWithNew()
        {
            Console.WriteLine("I am entering StartWithNew");
            var t = new Task(CountingProcedure);
            t.Start(); // <====== THIS IS ESSENTIAL!
            Console.WriteLine("I am leaving StartWithNew");
        }

        static public int Procedure(object args)
        {
            return args.ToString().Length;
        }

        static void Main(string[] args)
        {            
            StartWithStartNew();
            StartWithNew();
            CountingProcedure();

            Console.ReadKey();

            var t = new Task<int>(Procedure, "This is text");
            t.Start();
            var result = t.Result;
            Console.WriteLine($"Task with Id:{Task.CurrentId} has returned: {result}.");

            Console.ReadKey();
        }
    }
}
