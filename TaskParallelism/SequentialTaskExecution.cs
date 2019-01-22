using System;
using System.Threading;

namespace TaskParallelism
{
    public class SequentialTaskExecution
    {
        public virtual int Execute()
        {
            var b = F1(1);
            var d = F2(1);
            var e = F3(b, d);
            var f = F4(e);
            var g = F5(e);
            return F6(f, g);
        }

        public int F1(int a)
        {
            Thread.Sleep(1000);

            return a + 1;
        }

        public int F2(int c)
        {
            Thread.Sleep(2000);
            return c + 1;
        }

        public int F3(int b, int d)
        {
            Thread.Sleep(3000);
            return b + d + 1;
        }

        public int F4(int e)
        {
            Thread.Sleep(4000);
            return e + 1;
        }

        public int F5(int e)
        {
            Thread.Sleep(5000);
            return e + 1;
        }

        public int F6(int f, int g)
        {
            Thread.Sleep(6000);
            return f + g + 1;
        }
    }
}