using System;
using System.Threading;

namespace TaskParallelism
{
    public class SequentialPipeline
    {
        private readonly int _delay;
        protected readonly int _size;

        public SequentialPipeline(int delay, int size)
        {
            _delay = delay;
            _size = size;
        }

        public virtual void Start()
        {
            for (int indexer = 0; indexer < _size; indexer++)
            {
                var info = LoadImage(indexer);
                ScaleInfo(info);
                FilterImage(info);
                DisplayImage(info);
            }
        }

        protected void DisplayImage(int info)
        {
            Thread.Sleep(_delay);
            Console.WriteLine($"Displaying the info of element {info}");
        }

        protected void FilterImage(int info)
        {
            Thread.Sleep(_delay);
            Console.WriteLine($"Filtering the info of element {info}");
        }

        protected void ScaleInfo(int info)
        {
            Thread.Sleep(_delay);
            Console.WriteLine($"Scaling the info of element {info}");
        }

        protected int LoadImage(int info)
        {
            Thread.Sleep(_delay);
            Console.WriteLine($"Loading the info of element {info}");
            return info;
        }
    }
}