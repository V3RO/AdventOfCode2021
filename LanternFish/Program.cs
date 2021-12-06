using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LanternFish
{
    internal static class Program
    {
        private static List<int> ReadInputData()
        {
            var lines = File.ReadLines($"{Directory.GetCurrentDirectory()}/input.txt")
                .Select(line => line.Split(",")).ToList();
            var ints = new List<int>();
            foreach (var line in lines)
            {
                foreach (var s in line)
                {
                    ints.Add(int.Parse(s));
                }
            }

            return ints;
        }

        private static void PartOne()
        {
            var inputData = ReadInputData();
            const int days = 80;
            
            for (var i = 0; i < days; i++)
            {
                for (var j = 0; j < inputData.Count; j++)
                {
                    if (inputData[j] == 0)
                    {
                        inputData.Add(9);
                        inputData[j] = 6;
                    }
                    else
                    {
                        inputData[j]--;
                    }
                }
            }

            Console.WriteLine($"There are {inputData.Count} fish.");
        }
        
        private static void PartTwo()
        {
            var inputData = ReadInputData();
            const int days = 256;
            var fish = new SortedDictionary<int, long>();

            for (var i = 0; i < 9; i++)
            {
               fish.Add(i, 0);
            }

            foreach (var f in inputData)
            {
                fish[f]++;
            }

            for (var i = 0; i < days; i++)
            {
                var updatedFish = new SortedDictionary<int, long>(fish);
                for (var j = 8; j >= 0; j--)
                {
                    if (j != 0)
                    {
                        updatedFish[j - 1] = fish[j];
                    }
                    else
                    {
                        updatedFish[8] = fish[0];
                        updatedFish[6] += fish[0];
                    }
                }

                fish = updatedFish;
            }

            var count = fish.Sum(f => f.Value);

            Console.WriteLine($"There are {count} fish.");
        }

        private static void Main(string[] args)
        {
            // PartOne();
            PartTwo();
        }
    }
}