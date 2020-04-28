using BenchmarkDotNet.Running;
using Wangkanai.Detection.Services;

namespace Wangkanai.Detection
{
    public static class Program
    {
        public static void Main()
        {
            BenchmarkRunner.Run<DeviceServiceBenchmark>();
        }
    }
}