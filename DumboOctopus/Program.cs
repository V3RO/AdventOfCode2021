using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DumboOctopus
{
    internal static class Program
    {
        private static List<List<int>> ReadInputData()
        {
            var lines = File.ReadLines($"{Directory.GetCurrentDirectory()}/input.txt").ToList();
            return lines.Select(line => line.ToList().Select(c => int.Parse(c.ToString())).ToList()).ToList();
        }

        private static void PartOne()
        {
            var inputData = ReadInputData();
            const int steps = 1;
            
            var counter = 0;

            for (var i = 0; i < steps; i++)
            {
                var flashQueue = new Queue<(int x, int y)>();
                var flashed = new HashSet<(int x, int y)>();
                
                for (var y = 0; y < inputData.Count; y++)
                {
                    for (var x = 0; x < inputData[0].Count; x++)
                    {
                        inputData[y][x]++;
                        if (inputData[y][x] >= 9)
                        {
                            flashQueue.Enqueue((x, y));
                        }
                    }
                }

                while (flashQueue.Count > 0)
                {
                    var (x, y) = flashQueue.Dequeue();

                    var upper = y > 0 ? y - 1 : int.MinValue;
                    var left = x > 0 ? x - 1 : int.MinValue;
                    var right = x < inputData[y].Count - 1 ? x + 1 : int.MinValue;
                    var lower = y < inputData.Count - 1 ? y + 1 : int.MinValue;

                    if (upper >= 0)
                    {
                        inputData[upper][x] += 1;
                        if (inputData[upper][x] >= 9 && !flashed.Contains((x, upper)))
                            flashQueue.Enqueue((x, upper));
                    }

                    if (left >= 0)
                    {
                        inputData[y][left] += 1;
                        if (inputData[y][left] >= 9 && !flashed.Contains((left, y)))
                            flashQueue.Enqueue((left, y));
                    }

                    if (right >= 0)
                    {
                        inputData[y][right] += 1;
                        if (inputData[y][right] >= 9 && !flashed.Contains((right, y)))
                            flashQueue.Enqueue((right, y));
                    }

                    if (lower >= 0)
                    {
                        inputData[lower][x] += 1;
                        if (inputData[lower][x] >= 9 && !flashed.Contains((x, lower)))
                            flashQueue.Enqueue((x, lower));
                    }

                    flashed.Add((x, y));
                    counter++;
                }

                foreach (var (x, y) in flashed)
                {
                    inputData[y][x] = 0;
                }
            }

            Console.WriteLine(counter);
        }
        
        private static void PartTwo()
        {
            
        }
        
        private static void Main(string[] args)
        {
            PartOne();
            // PartTwo();
        }
    }
}