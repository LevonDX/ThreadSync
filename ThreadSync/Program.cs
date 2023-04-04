namespace ThreadSync
{
    internal class Program
    {
        static readonly object o = new object();

        static void AddEvenNumbers()
        {
            for (int i = 0; i <= 1000; i += 2)
            {
                //Console.WriteLine("thread 1");
                //Thread.Sleep(new Random().Next(0, 500));

                lock (o)
                {
                    using (StreamWriter _writer = new StreamWriter(@"C:\Test\numbers.txt", true))
                    {
                        _writer.WriteLine(i);
                        _writer.Close();
                    }
                }
            }
        }
        static void AddOddNumbers()
        {
            for (int i = 1; i <= 1000; i += 2)
            {
                //Console.WriteLine("thread 2");
                //Thread.Sleep(new Random().Next(0, 500));

                try
                {
                    Monitor.Enter(o);

                    using (StreamWriter _writer = new StreamWriter(@"C:\Test\numbers.txt", true))
                    {
                        _writer.WriteLine(i);
                        _writer.Close();
                    }
                }
                finally
                {
                    Monitor.Exit(o);
                }
            }
        }

        static void Main()
        {
            Thread t1 = new Thread(AddEvenNumbers);
            Thread t2 = new Thread(AddOddNumbers);

            t1.Start();
            t2.Start();

            t1.Join();
            t2.Join();

            //AddEvenNumbers();
            //AddOddNumbers();
        }
    }
}