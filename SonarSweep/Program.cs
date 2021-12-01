using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

namespace SonarSweep
{
    internal static class Program
    {
        private static List<int> ReadInputData()
        {
            var lines = File.ReadLines($"{Directory.GetCurrentDirectory()}/input.txt").ToList();
            return lines.Select(int.Parse).ToList();
        }

        /***
         * Count measurements that are larger than the previous measurement
         */
        private static void PartOne()
        {
            var depthMeasurements = ReadInputData();

            if (depthMeasurements.Count() <= 1)
            {
                Console.WriteLine("No measurements is larger than the previous measurement. ");
            }
            else
            {
                var count = 0;
                for (var i = 1; i < depthMeasurements.Count; i++)
                {
                    if (depthMeasurements[i - 1] <  depthMeasurements[i])
                    {
                        count++;
                    }
                }
                Console.WriteLine($"{count} measurements are larger than the previous measurement. ");
            }
        }

        private static int SlidingWindow(int n, ref int[] slidingWindow, ref int index)
        {
            slidingWindow[index] = n;
            index = index == 2 ? 0 : index + 1;
            return slidingWindow.Sum();
        }
        
        /***
         * Group measurement in sliding windows and count sliding windows that are larger than the previous sliding window
         */
        private static void PartTwo()
        {
            var depthMeasurements = ReadInputData();

            if (depthMeasurements.Count() <= 3)
            {
                Console.WriteLine("No measurements is larger than the previous measurement. ");
            }
            else
            {
                var count = 0;
                var slidingIndex = 0;
                var slidingWindow = new[] { depthMeasurements[0], depthMeasurements[1], depthMeasurements[2] };
                var lastSlidingSum = slidingWindow.Sum();

                for (var i = 3; i < depthMeasurements.Count; i++)
                {
                    var currentSlidingSum = SlidingWindow(depthMeasurements[i], ref slidingWindow, ref slidingIndex);
                    if (lastSlidingSum < currentSlidingSum)
                    {
                        count++;
                    }

                    lastSlidingSum = currentSlidingSum;
                }
                Console.WriteLine($"{count} measurements are larger than the previous measurement. ");            
            }
        }

        private static void Main(string[] args)
        {
            PartOne();
            PartTwo();
        }
    }
}