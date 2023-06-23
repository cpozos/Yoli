using BenchmarkDotNet.Attributes;
using Benchmarks.Cases;
using Microsoft.VisualBasic;

namespace Yoli.WebApi.Tests;

[MemoryDiagnoser]
[Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
public class IntersectBenchmarks
{
    private static readonly TestClass test = new TestClass();
    private readonly long[] sortedFirstArray;
    private readonly long[] secondArray;

    public IntersectBenchmarks()
    {
        ICase @case = new LargerSecondArray();

        int multiplies = @case.MultiplyBaseArray > 1 ? @case.MultiplyBaseArray : 1;
        var list = new List<long>();
        foreach (var s in @case.BaseArray.Distinct())
        {
            for (int i = 1; i < (multiplies+1); i++)
            {
                list.Add(s * i);
            }
        }

        sortedFirstArray = list.ToArray();
        Array.Sort(sortedFirstArray);

        secondArray = @case.SecondArray.Distinct().ToArray();
    }

    [Benchmark]
    public long[] A()
    {
        var result = test.Intersect(sortedFirstArray, secondArray);
        return result;
    }

    [Benchmark]
    public long[] B()
    {
        Array.Sort(secondArray);
        var results = test.IntersectSorted(sortedFirstArray, secondArray);
        return results;
    }

    [Benchmark]
    public long[] C()
    {
        var result = test.IntersectSortedIE(sortedFirstArray, secondArray.OrderBy(i => i).AsEnumerable()).ToArray();
        return result;
    }
}


public class TestClass
{
    public long[] Intersect(long[] first, long[] second)
        => first.Intersect(second).ToArray();

    public long[] IntersectSorted(long[] first, long[] second)
    {
        int firstLength = first.Length;
        int secondLength = second.Length;

        int firstIterator = 0, secondIterator = 0;
        var intersection = new List<long>(Math.Min(firstLength, secondLength));
        while (firstIterator < firstLength && secondIterator < secondLength)
        {
            switch(first[firstIterator].CompareTo(second[secondIterator]))
            {
                case -1:
                    firstIterator++;
                    continue;

                case 1:
                    secondIterator++;
                    continue;

                default:
                    intersection.Add(first[firstIterator++]);
                    secondIterator++;
                    continue;
            }

            //int result = first[firstIterator].CompareTo(second[secondIterator]);
            //if (result < 0)
            //{
            //    firstIterator++;
            //}
            //else if (result > 0)
            //{
            //    secondIterator++;
            //}
            //else
            //{
            //    intersection.Add(first[firstIterator]);
            //    firstIterator++;
            //    secondIterator++;
            //}
        }

        return intersection.ToArray();
    }

    public IEnumerable<long> IntersectSortedIE(IEnumerable<long> first, IEnumerable<long> second)
    {
        using var cursor1 = first.GetEnumerator();
        using var cursor2 = second.GetEnumerator();
        if (!cursor1.MoveNext() || !cursor2.MoveNext())
        {
            yield break;
        }

        var value1 = cursor1.Current;
        var value2 = cursor2.Current;

        while(true)
        {
            int result = value1.CompareTo(value2);
            if (result < 0)
            {
                if (!cursor1.MoveNext())
                {
                    yield break;
                }
                value1 = cursor1.Current;
            }
            else if (result > 0)
            {
                if (!cursor2.MoveNext())
                {
                    yield break;
                }
                value2 = cursor2.Current;
            }
            else
            {
                yield return value1;
                if (!cursor1.MoveNext() || !cursor2.MoveNext())
                {
                    yield break;
                }
                value1 = cursor1.Current;
                value2 = cursor2.Current;
            }
        }
    }
}