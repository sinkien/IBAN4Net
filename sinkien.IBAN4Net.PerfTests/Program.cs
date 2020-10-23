using BenchmarkDotNet.Running;

namespace sinkien.IBAN4Net.PerfTests
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<CountryCodeBenchmarks>();
            var summary2 = BenchmarkRunner.Run<BbanBenchmarks>();
        }
    }
}
