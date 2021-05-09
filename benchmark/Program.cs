using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using Wangkanai.Detection.Services;

namespace Wangkanai.Detection
{
    public static class Program
    {
        public static void Main()
        {
            var config = new ManualConfig();
            config.WithOptions(ConfigOptions.DisableOptimizationsValidator);
            BenchmarkRunner.Run<DeviceServiceBenchmark>(config);
        }
    }
}