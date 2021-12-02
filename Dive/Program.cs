using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Dive
{
    internal static class Program
    {
        private static List<string[]> ReadInputData()
        {
            var lines = File.ReadLines($"{Directory.GetCurrentDirectory()}/input.txt").ToList();
            return lines.Select(line => line.Split(" ")).ToList();
        }

        private static void PartOne()
        {
            var measurements = ReadInputData();

            var horizontal = 0;
            var depth = 0;
            
            measurements.ForEach(m =>
            {
                switch (m[0])
                {
                    case "forward":
                        horizontal += int.Parse(m[1]);
                        break;
                    case "up":
                        depth -= int.Parse(m[1]);
                        break;
                    default:
                        depth += int.Parse(m[1]);
                        break;
                }
            });

            Console.WriteLine($"final depth * horizontal: {depth * horizontal}");
        }
        
        private static void PartTwo()
        {
            var measurements = ReadInputData();

            var horizontal = 0;
            var aim = 0;
            var depth = 0;
            
            measurements.ForEach(m =>
            {
                switch (m[0])
                {
                    case "forward":
                        horizontal += int.Parse(m[1]);
                        depth += int.Parse(m[1]) * aim;
                        break;
                    case "up":
                        aim -= int.Parse(m[1]);
                        break;
                    default:
                        aim += int.Parse(m[1]);
                        break;
                }
            });

            Console.WriteLine($"final depth(aim based) * horizontal: {depth * horizontal}");
        }
        
        private static void Main(string[] args)
        {
            PartOne();
            PartTwo();
        }
    }
}