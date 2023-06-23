namespace Benchmarks.Cases;
public interface ICase
{
    List<long> BaseArray { get; }
    long[] SecondArray { get; }
    int MultiplyBaseArray { get; init; }
}