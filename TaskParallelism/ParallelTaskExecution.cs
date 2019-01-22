using System.Threading.Tasks;

namespace TaskParallelism
{
    public class ParallelTaskExecution : SequentialTaskExecution
    {
        public override int Execute()
        {
            Task<int> b = Task.Factory.StartNew<int>(() => F1(1));
            Task<int> d = Task.Factory.StartNew<int>(() => F2(1));

            Task<int> e = Task.Factory.ContinueWhenAll<int, int>(
                new[] { b, d },
                (tasks) =>
                {
                    var r1 = tasks[0].Result;
                    var r2 = tasks[1].Result;
                    return F3(r1, r2);
                });

            Task<int> f = e.ContinueWith((r) => F4(r.Result));

            Task<int> g = e.ContinueWith((r) => F4(r.Result));

            Task<int> h = Task.Factory.ContinueWhenAll<int, int>(
                new[] { f, g },
                (tasks) =>
                {
                    var r1 = tasks[0].Result;
                    var r2 = tasks[1].Result;
                    return F6(r1, r2);
                }
            );

            return h.Result;
        }
    }
}