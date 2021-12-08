using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Channels;

namespace SevenSegmentSearch
{
    internal static class Program
    {
        private static List<string[][]> ReadInputData()
        {
            var lines = File.ReadLines($"{Directory.GetCurrentDirectory()}/input.txt").ToList();
            return lines.Select(line => line.Split(" | ")).Select(line => new[] {line[0].Split(" "), line[1].Split(" ")}).ToList();
        }

        private static void PartOne()
        {
            var uniqueSegments = new List<int> { 2, 3, 4, 7 };

            var inputData = ReadInputData();
            var count = inputData.Sum(outputValues => outputValues[1].Count(segment => uniqueSegments.Contains(segment.Length)));

            Console.WriteLine($"Digits 1, 4, 7, or 8 appear {count} times.");
        }
        
        private static void PartTwo()
        {
            var uniqueSegmentCounts = new Dictionary<int, int> { {1, 2}, {4, 4}, {7, 3}, {8, 7} };
            var signals = new List<Dictionary<int, string>>();
            var sum = 0;
            
            var inputData = ReadInputData();
            
            for (var i = 0; i < inputData.Count; i++)
            {
                signals.Add(new Dictionary<int, string>());
                foreach (var input in inputData[i])
                {
                    foreach (var segment in input)
                    {
                        foreach (var (key, value) in uniqueSegmentCounts)
                        {
                            if (value == segment.Length)
                            {
                                signals[i].TryAdd(key, segment);
                            }
                        }
                    }
                }

                var four = signals[i].Where(kv => kv.Key == 4).ToList().First();
                var one = signals[i].Where(kv => kv.Key == 1).ToList().First();

                var zeroSixNines = inputData[i][0].Where(s => s.Length == 6).ToList();
                var twoThreeFives = inputData[i][0].Where(s => s.Length == 5).ToList();

                var fourMinusOne = string.Join("", four.Value.ToList().Where(c => !one.Value.Contains(c)));
                var zero = zeroSixNines.Where(s =>
                {
                    var count = fourMinusOne.Count(s.Contains);
                    return count == 1;
                }).First();
                zeroSixNines = zeroSixNines.Where(s => !s.Equals(zero)).ToList();

                var six = zeroSixNines.Where(s =>
                {
                    var count = one.Value.Count(s.Contains);
                    return count == 1;
                }).First();
                
                var nine = zeroSixNines.Where(s => !s.Equals(six)).ToList().First();
                
                var three = twoThreeFives.Where(s =>
                {
                    var count = one.Value.Count(s.Contains);
                    return count == 2;
                }).First();
                twoThreeFives = twoThreeFives.Where(s => !s.Equals(three)).ToList();

                var five = twoThreeFives.Where(s =>
                {
                    var count = nine.Count(s.Contains);
                    return count == 5;
                }).First();
                
                var two = twoThreeFives.First(s => !s.Equals(five));

                signals[i].Add(0, zero);
                signals[i].Add(2, two);
                signals[i].Add(3, three);
                signals[i].Add(5, five);
                signals[i].Add(6, six);
                signals[i].Add(9, nine);

                var num = new List<int>();
                foreach (var segment in inputData[i][1])
                {
                    foreach (var (key, value) in signals[i])
                    {
                        if (value.Length != segment.Length) continue;
                        if (segment.All(c => value.Contains(c)))
                        {
                            num.Add(key);
                        }
                    }

                }
                
                sum += num.Aggregate(0, (current, entry) => 10 * current + entry);
            }

            Console.WriteLine(sum);
        }
        
        private static void Main(string[] args)
        {
            PartOne();
            PartTwo();
        }
    }
}