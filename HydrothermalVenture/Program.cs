using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Channels;
using Microsoft.VisualBasic;

namespace HydrothermalVenture
{
    internal static class Program
    {
        private static List<int[]> ReadInputData()
        {
            var lines = File.ReadLines($"{Directory.GetCurrentDirectory()}/input.txt").ToList();
            var x = lines.Select(line => line.Replace("-> ", "").Split(" ")).Select(line => line[0].Split(",").Concat(line[1].Split(",")).ToArray()).ToList();
            return x.Select(e => Array.ConvertAll(e, int.Parse)).ToList();
        }

        private static void PartOne()
        {
            var inputData = ReadInputData();

            var maxX = 0;
            var maxY = 0;

            foreach (var line in inputData)
            {
                if (line[0] > maxX)
                    maxX = line[0];
                if (line[2] > maxX)
                    maxX = line[2];
                if (line[1] > maxY)
                    maxY = line[1];
                if (line[3] > maxY)
                    maxY = line[3];
            }

            var map = new int[maxX + 1, maxY + 1];

            foreach (var line in inputData)
            {
                if (line[0] == line[2])
                {
                    var from = line[1] < line[3] ? line[1] : line[3];
                    var to = line[1] > line[3] ? line[1] : line[3];
                    
                    for (var i = from; i <= to; i++)
                    {
                        map[line[0], i] += 1;
                    }
                }
                else if (line[1] == line[3])
                {
                    var from = line[0] < line[2] ? line[0] : line[2];
                    var to = line[0] > line[2] ? line[0] : line[2];
                    
                    for (var i = from; i <= to; i++)
                    {
                        map[i, line[1]]++;
                    }
                }
            }

            var count = 0;
            for (var i = 0; i < map.GetLength(0); i++)
            {
                for (var j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i,j] >= 2)
                        count++;
                }

            }
            Console.WriteLine(count);
        }
        
        private static void PartTwo()
        {
            var inputData = ReadInputData();

            var maxX = 0;
            var maxY = 0;

            foreach (var line in inputData)
            {
                if (line[0] > maxX)
                    maxX = line[0];
                if (line[2] > maxX)
                    maxX = line[2];
                if (line[1] > maxY)
                    maxY = line[1];
                if (line[3] > maxY)
                    maxY = line[3];
            }

            var map = new int[maxX + 1, maxY + 1];

            foreach (var line in inputData)
            {
                if (line[0] == line[2])
                {
                    var from = line[1] < line[3] ? line[1] : line[3];
                    var to = line[1] > line[3] ? line[1] : line[3];
                    
                    for (var i = from; i <= to; i++)
                    {
                        map[line[0], i] += 1;
                    }
                }
                else if (line[1] == line[3])
                {
                    var from = line[0] < line[2] ? line[0] : line[2];
                    var to = line[0] > line[2] ? line[0] : line[2];
                    
                    for (var i = from; i <= to; i++)
                    {
                        map[i, line[1]]++;
                    }
                }
                else
                {
                    if (line[0] >= line[2] && line[1] <= line[3] || line[0] <= line[2] && line[1] >= line[3])
                    {
                        var from = line[0] < line[2] ? line[0] : line[2];
                        var to = line[0] > line[2] ? line[0] : line[2];
                        
                        var fromY = line[1] > line[3] ? line[1] : line[3];
                        
                        for (var i = from; i <= to; i++)
                        {
                            map[i, fromY--]++;
                        }
                    }
                    else 
                    {
                        var from = line[0] < line[2] ? line[0] : line[2];
                        var to = line[0] > line[2] ? line[0] : line[2];
                    
                        var fromY = line[1] < line[3] ? line[1] : line[3];
                    
                        for (var i = from; i <= to; i++)
                        {
                            map[i, fromY++]++;
                        }
                    }
                }
            } 
    
            var count = 0; 
            for (var i = 0; i < map.GetLength(0); i++)
            {
                for (var j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i,j] >= 2)
                        count++;
                }

            }
            Console.WriteLine(count);
        }
        
        private static void Main(string[] args)
        {
            PartOne();
            PartTwo();
        }
    }
}