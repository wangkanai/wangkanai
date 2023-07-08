global using BenchmarkDotNet.Attributes;
global using BenchmarkDotNet.Running;

	
BenchmarkRunner.Run<FirstBenchmark>();

public class FirstBenchmark
{
	[Benchmark]
	public void Benchmark()
	{
		// Method intentionally left empty.
	}
}