using System;
using System.Collections.Generic;
using System.Data;
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
            const int steps = 100;
            
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
                        if (inputData[y][x] <= 9) continue;
                        flashQueue.Enqueue((x, y));
                        flashed.Add((x, y));
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
                        if (inputData[upper][x] > 9 && !flashed.Contains((x, upper)))
                        {
                            flashQueue.Enqueue((x, upper));
                            flashed.Add((x, upper));
                        }
                    }

                    if (left >= 0)
                    {
                        inputData[y][left] += 1;
                        if (inputData[y][left] > 9 && !flashed.Contains((left, y)))
                        {
                            flashQueue.Enqueue((left, y));
                            flashed.Add((left, y));
                        }
                    }

                    if (right >= 0)
                    {
                        inputData[y][right] += 1;
                        if (inputData[y][right] > 9 && !flashed.Contains((right, y)))
                        {
                            flashQueue.Enqueue((right, y));
                            flashed.Add((right, y));
                        }
                    }

                    if (lower >= 0)
                    {
                        inputData[lower][x] += 1;
                        if (inputData[lower][x] > 9 && !flashed.Contains((x, lower)))
                        {
                            flashQueue.Enqueue((x, lower));
                            flashed.Add((x, lower));
                        }
                    }

                    if (upper >= 0 && left >= 0)
                    {
                        inputData[upper][left] += 1;
                        if (inputData[upper][left] > 9 && !flashed.Contains((left, upper)))
                        {
                            flashQueue.Enqueue((left, upper));
                            flashed.Add((left, upper));
                        }
                    }
                    
                    if (upper >= 0 && right >= 0)
                    {
                        inputData[upper][right] += 1;
                        if (inputData[upper][right] > 9 && !flashed.Contains((right, upper)))
                        {
                            flashQueue.Enqueue((right, upper));
                            flashed.Add((right, upper));
                        }
                    }
                    
                    if (lower >= 0 && left >= 0)
                    {
                        inputData[lower][left] += 1;
                        if (inputData[lower][left] > 9 && !flashed.Contains((left, lower)))
                        {
                            flashQueue.Enqueue((left, lower));
                            flashed.Add((left, lower));
                        }
                    }
                    
                    if (lower >= 0 && right >= 0)
                    {
                        inputData[lower][right] += 1;
                        if (inputData[lower][right] > 9 && !flashed.Contains((right, lower)))
                        {
                            flashQueue.Enqueue((right, lower));
                            flashed.Add((right, lower));
                        }
                    }
                }

                foreach (var (x, y) in flashed)
                {
                    inputData[y][x] = 0;
                    counter++;
                }
            }

            Console.WriteLine(counter);
        }
        
        private static void PartTwo()
        {
            var inputData = ReadInputData();
            const int steps = int.MaxValue;
            
            for (var i = 0; i < steps; i++)
            {
                var flashQueue = new Queue<(int x, int y)>();
                var flashed = new HashSet<(int x, int y)>();
                
                for (var y = 0; y < inputData.Count; y++)
                {
                    for (var x = 0; x < inputData[0].Count; x++)
                    {
                        inputData[y][x]++;
                        if (inputData[y][x] <= 9) continue;
                        flashQueue.Enqueue((x, y));
                        flashed.Add((x, y));
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
                        if (inputData[upper][x] > 9 && !flashed.Contains((x, upper)))
                        {
                            flashQueue.Enqueue((x, upper));
                            flashed.Add((x, upper));
                        }
                    }

                    if (left >= 0)
                    {
                        inputData[y][left] += 1;
                        if (inputData[y][left] > 9 && !flashed.Contains((left, y)))
                        {
                            flashQueue.Enqueue((left, y));
                            flashed.Add((left, y));
                        }
                    }

                    if (right >= 0)
                    {
                        inputData[y][right] += 1;
                        if (inputData[y][right] > 9 && !flashed.Contains((right, y)))
                        {
                            flashQueue.Enqueue((right, y));
                            flashed.Add((right, y));
                        }
                    }

                    if (lower >= 0)
                    {
                        inputData[lower][x] += 1;
                        if (inputData[lower][x] > 9 && !flashed.Contains((x, lower)))
                        {
                            flashQueue.Enqueue((x, lower));
                            flashed.Add((x, lower));
                        }
                    }

                    if (upper >= 0 && left >= 0)
                    {
                        inputData[upper][left] += 1;
                        if (inputData[upper][left] > 9 && !flashed.Contains((left, upper)))
                        {
                            flashQueue.Enqueue((left, upper));
                            flashed.Add((left, upper));
                        }
                    }
                    
                    if (upper >= 0 && right >= 0)
                    {
                        inputData[upper][right] += 1;
                        if (inputData[upper][right] > 9 && !flashed.Contains((right, upper)))
                        {
                            flashQueue.Enqueue((right, upper));
                            flashed.Add((right, upper));
                        }
                    }
                    
                    if (lower >= 0 && left >= 0)
                    {
                        inputData[lower][left] += 1;
                        if (inputData[lower][left] > 9 && !flashed.Contains((left, lower)))
                        {
                            flashQueue.Enqueue((left, lower));
                            flashed.Add((left, lower));
                        }
                    }
                    
                    if (lower >= 0 && right >= 0)
                    {
                        inputData[lower][right] += 1;
                        if (inputData[lower][right] > 9 && !flashed.Contains((right, lower)))
                        {
                            flashQueue.Enqueue((right, lower));
                            flashed.Add((right, lower));
                        }
                    }
                }
                
                if (inputData.Sum(row => row.Count) == flashed.Count)
                {
                    Console.WriteLine(i + 1);
                    return;
                }

                foreach (var (x, y) in flashed)
                {
                    inputData[y][x] = 0;
                }
            }
        }
        
        private static void Main(string[] args)
        {
            PartOne();
            PartTwo();
        }
    }
}