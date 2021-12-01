using System;
using System.IO;
using System.Linq;

var input = File.ReadLines(@"input.txt").Select(int.Parse).ToList();

Part1();
Part2();

void Part1()
{
    var increasedDepthCounter = 0;
    int? previousDepth = null;

    foreach (var depth in input)
    {
        if (depth > previousDepth)
            increasedDepthCounter++;

        previousDepth = depth;
    }

    Console.WriteLine($"The answer to part 1 is: {increasedDepthCounter}");
}

void Part2()
{
    var increasedDepthCounter = 0;
    int? previousWindowResult = null;
    var skip = 0;
    
    while (true)
    {
        var window = input.Skip(skip).Take(3).ToList();
        
        if (window.Count < 3)
            break;

        var currentWindowResult = window.Sum();
        
        if (currentWindowResult > previousWindowResult)
            increasedDepthCounter++;

        previousWindowResult = currentWindowResult;
        skip++;
    }
    
    Console.WriteLine($"The answer to part 2 is: {increasedDepthCounter}");
}
