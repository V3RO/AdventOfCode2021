using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TheTreacheryOfWhales
{
    internal static class Program
    {
        private static List<int> ReadInputData()
        {
            var lines = File.ReadLines($"{Directory.GetCurrentDirectory()}/input.txt").ToList();
            return lines.Select(line => line.Split(",")).SelectMany(i => i).Select(int.Parse).ToList();
        }

        private static void PartOne()
        {
            var inputData = ReadInputData();

            var min = inputData.Min();
            var max = inputData.Max();

            var distance = int.MaxValue;
            for (var i = min; i < max; i++)
            {
                var temp = inputData.Sum(input => Math.Abs(input - i));

                if (temp >= distance) continue;
                distance = temp;
            }

            Console.WriteLine($"{distance}");
        }
        
        private static void PartTwo()
        {
            var inputData = ReadInputData();

            var min = inputData.Min();
            var max = inputData.Max();

            var distance = int.MaxValue;
            for (var i = min; i < max; i++)
            {
                var temp = 0;

                foreach (var input in inputData)
                {
                    for (var j = 0; j <= Math.Abs(input - i); j++)
                    {
                        temp += j;
                    }
                }
                
                if (temp >= distance) continue;
                distance = temp;
            }

            Console.WriteLine($"{distance}");
        }
        
        private static void Main(string[] args)
        {
            PartOne();
            PartTwo();
        }
    }
}