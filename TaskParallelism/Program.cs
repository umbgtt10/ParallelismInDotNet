using System;
using System.Text;

namespace TaskParallelism
{
    class Program
    {
        static void Main(string[] args)
        {
            var pipeline = new ConcurrentPipeline(1000, 500, 2);
            pipeline.Start();

            Console.ReadKey();
        }
    }
}
