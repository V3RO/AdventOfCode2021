using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SmokeBasin
{
    internal static class Program
    {
        private static List<List<Dictionary<int , bool>>> ReadInputData()
        {
            var lines = File.ReadLines($"{Directory.GetCurrentDirectory()}/input.txt").ToList();
            return lines.Select(line => line.ToArray()).Select(chars => 
                Array.ConvertAll(chars, c => (int)char.GetNumericValue(c)).Select(i => new Dictionary<int, bool> {{i, false}}).ToList()).ToList();
        }


        private static void PartOne()
        {
            var inputData = ReadInputData();

            for (var i = 0; i < inputData.Count; i++)
            {
                for (var j = 0; j < inputData[0].Count; j++)
                {
                    var upper = i > 0 ? inputData[i - 1][j].First().Key : int.MaxValue;
                    var left = j > 0 ? inputData[i][j - 1].First().Key : int.MaxValue;
                    var right = j < inputData[i].Count - 1 ? inputData[i][j + 1].First().Key : int.MaxValue;
                    var low = i < inputData.Count - 1 ? inputData[i + 1][j].First().Key : int.MaxValue;
                    var current = inputData[i][j].First().Key;
                    
                    if (current < upper && current < left && current < right && current < low)
                    {
                        inputData[i][j][current] = true;
                    }
                }
            }

            var sum = inputData.Sum(row => row.Where(dict => dict.First().Value).Sum(dict => dict.First().Key + 1));
            Console.WriteLine(sum);
        }
        
        private static void PartTwo()
        {
            var inputData = ReadInputData();

            for (var i = 0; i < inputData.Count; i++)
            {
                for (var j = 0; j < inputData[0].Count; j++)
                {
                    var upper = i > 0 ? inputData[i - 1][j].First().Key : int.MaxValue;
                    var left = j > 0 ? inputData[i][j - 1].First().Key : int.MaxValue;
                    var right = j < inputData[i].Count - 1 ? inputData[i][j + 1].First().Key : int.MaxValue;
                    var low = i < inputData.Count - 1 ? inputData[i + 1][j].First().Key : int.MaxValue;
                    var current = inputData[i][j].First().Key;
                    
                    if (current < upper && current < left && current < right && current < low)
                    {
                        inputData[i][j][current] = true;
                    }
                }
            }

            var lowest = new Queue<(int x, int y, int value)>();

            for (var i = 0; i < inputData.Count; i++)
            {
                for (var j = 0; j < inputData[0].Count; j++)
                {
                    if (inputData[i][j].First().Value)
                        lowest.Enqueue((x:j, y:i, value:inputData[i][j].First().Key));
                }
            }

            var basinSizes = new List<int>();
            foreach (var tuple in lowest)
            {
                var queue = new Queue<(int x, int y, int value)>();
                var visited = new HashSet<(int x, int y, int value)>();
                
                queue.Enqueue(tuple);
                
                while (queue.Count > 0)
                {
                    var valueTuple = queue.Dequeue();

                    if (visited.Contains(valueTuple))
                        continue;
                
                    visited.Add(valueTuple);
                
                    var upper = valueTuple.y > 0 ? inputData[valueTuple.y - 1][valueTuple.x].First().Key : int.MinValue;
                    var left = valueTuple.x > 0 ? inputData[valueTuple.y][valueTuple.x - 1].First().Key : int.MinValue;
                    var right = valueTuple.x < inputData[valueTuple.y].Count - 1 ? inputData[valueTuple.y][valueTuple.x + 1].First().Key : int.MinValue;
                    var low = valueTuple.y < inputData.Count - 1 ? inputData[valueTuple.y + 1][valueTuple.x].First().Key : int.MinValue;

                    if (valueTuple.value < upper && upper < 9)
                        queue.Enqueue((valueTuple.x, valueTuple.y - 1, upper));
                    if (valueTuple.value < left && left< 9)
                        queue.Enqueue((valueTuple.x - 1, valueTuple.y, left));
                    if (valueTuple.value < right && right < 9)
                        queue.Enqueue((valueTuple.x + 1, valueTuple.y, right));
                    if (valueTuple.value < low && low < 9)
                        queue.Enqueue((valueTuple.x, valueTuple.y + 1, low));
                }

                basinSizes.Add(visited.Count);
            }

            var product = basinSizes.OrderByDescending(i => i).Take(3).Aggregate(1, (acc, size) => acc * size);
            Console.WriteLine(product);
        }
        
        
        private static void Main(string[] args)
        {
            PartTwo();
        }
    }
}