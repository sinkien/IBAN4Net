using System;
using System.Threading;
using BenchmarkDotNet.Attributes;
using SinKien.IBAN4Net;

namespace sinkien.IBAN4Net.PerfTests
{
    [MemoryDiagnoser]
    public class CountryCodeBenchmarks
    {        

        [Benchmark(Baseline = true)]
        public CountryCodeEntry Base()
        {
            return CountryCodePrev.GetCountryCode("CY");
        }

        [Benchmark]
        public CountryCodeEntry StaticCountries()
        {
            return CountryCode.GetCountryCode("CY");
        }
    }
}
