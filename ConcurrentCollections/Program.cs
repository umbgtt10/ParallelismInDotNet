using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConcurrentCollections
{
    class Program
    {
        static ConcurrentBag<int> _bag = new ConcurrentBag<int>();
        static BlockingCollection<int> _messages = new BlockingCollection<int>(_bag, 10);
        static CancellationTokenSource _cts = new CancellationTokenSource();
        static Random _random = new Random();

        static void ProduceAndConsume()
        {
            var producer = Task.Factory.StartNew(RunProducer);
            var consumer = Task.Factory.StartNew(RunConsumer);

            try
            {
                Task.WaitAll(new[] {producer, consumer}, _cts.Token);
            }
            catch (AggregateException ae)
            {
                Console.WriteLine("Killed!");
                ae.Handle(e => { return true; });
            }
        }

        static private void RunProducer()
        {
            while (true)
            {
                _cts.Token.ThrowIfCancellationRequested();
                int i = _random.Next(100);
                _messages.Add(i);
                Console.WriteLine($"+{i}\t");
                Thread.Sleep(_random.Next(1000));
            }
        }

        static private void RunConsumer()
        {
            foreach (var item in _messages.GetConsumingEnumerable())
            {
                _cts.Token.ThrowIfCancellationRequested();
                Console.WriteLine($"-{item}\t");
                Thread.Sleep(_random.Next(100));
            }
        }

        static void Main()
        {
            Task.Factory.StartNew(ProduceAndConsume, _cts.Token);

            Console.ReadKey();
            _cts.Cancel();
        }
    }
}
