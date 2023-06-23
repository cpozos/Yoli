using BenchmarkDotNet.Running;
using Yoli.WebApi.Tests;

var bench = new IntersectBenchmarks();

var resultA = bench.A();
var resultB = bench.B();
var resultC = bench.C();

bool hasItem = Array.BinarySearch<long>(resultC, 0, resultC.Length, 1234) > -1;

bool equals =
    resultA.SequenceEqual(resultB) &&
    resultB.SequenceEqual(resultC);

Console.WriteLine("Done");
//BenchmarkRunner.Run<IntersectBenchmarks>();
