using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using JsonParser.Services;
using NUnit.Framework;

namespace JsonParser.Tests
{
    public class JsonParserPerformanceBenchmark
    {
        [Test]
        public void TestFastParsePerformance()
        {
            var json = File.ReadAllText("TestFiles\\large.json");
            var sut = new FastJsonParserService();
            Profile(nameof(FastJsonParserService), 10, () => { sut.Parse(json); });
        }

        // Benchmarking code https://stackoverflow.com/a/1048708/3224483
        private static void Profile(string description, int iterations, Action func)
        {
            // Run at highest priority to minimize fluctuations caused by other processes/threads
            Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;
            Thread.CurrentThread.Priority = ThreadPriority.Highest;

            // warm up 
            func();

            var watch = new Stopwatch();

            // clean up
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            watch.Start();
            for (var i = 0; i < iterations; i++)
            {
                func();
            }
            watch.Stop();
            Console.Write(description);
            Console.WriteLine(" Time Elapsed {0} ms", watch.Elapsed.TotalMilliseconds);
        }
    }
}
