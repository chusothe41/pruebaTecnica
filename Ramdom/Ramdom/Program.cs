using System.Diagnostics;

namespace Ramdom
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const int numInts = 10000000;

            var random = new Random();
            var distinctInts = new HashSet<int>(numInts);

            var stopwatch = Stopwatch.StartNew();

            while (distinctInts.Count < numInts)
            {
                var rand = random.Next(0, int.MaxValue - 1);
                if (!distinctInts.Contains(rand))
                {
                    distinctInts.Add(rand);
                }
            }

            var filePath = "random.txt";
            using (var writer = new StreamWriter(filePath))
            {
                foreach (var num in distinctInts)
                {
                    writer.WriteLine(num);
                }
            }

            stopwatch.Stop();
            Console.WriteLine($"Execution time: {stopwatch.ElapsedMilliseconds} ms");

        }
    }
}