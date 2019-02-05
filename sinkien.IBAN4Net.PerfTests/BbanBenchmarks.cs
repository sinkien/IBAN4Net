using System;
using System.Threading;
using BenchmarkDotNet.Attributes;
using SinKien.IBAN4Net;

namespace sinkien.IBAN4Net.PerfTests
{
    [MemoryDiagnoser]
    public class BbanBenchmarks
    {        

        [Benchmark(Baseline = true)]
        public BBanStructure Base()
        {
            return BbanPrev.GetStructureForCountry("CY");
        }

        [Benchmark]
        public BBanStructure StaticBBans()
        {
            return Bban.GetStructureForCountry("CY");
        }
    }
}
