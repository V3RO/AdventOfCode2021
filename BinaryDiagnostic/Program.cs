using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BinaryDiagnostic
{
    internal static class Program
    {
        private static List<char[]> ReadInputData()
        {
            return File.ReadLines($"{Directory.GetCurrentDirectory()}/input.txt").Select(s => s.ToCharArray()).ToList();
        }

        private static void PartOne()
        {
            var input = ReadInputData();
            var gammaValues = new List<string>();
            var epsilonValues = new List<string>();
            
            for (var i = 0; i < input.First().Length; i++)
            {
                var zero = input.Count(c => c[i] == '0');
                var one = input.Count(c => c[i] == '1');
                var mostCommon= zero > one ? "0" : "1";
                var leastCommon = zero < one ? "0" : "1";
                gammaValues.Add(mostCommon);
                epsilonValues.Add(leastCommon);
            }

            var gamma = Convert.ToInt32(string.Join("", gammaValues.ToArray()), 2);
            var epsilon = Convert.ToInt32(string.Join("", epsilonValues.ToArray()), 2);
            
            Console.WriteLine($"gamma * epsilon = {gamma * epsilon}");
        }

        private static List<string> MostCommonBitCriteria(IReadOnlyCollection<string> input, int i)
        {
            var zero = input.Count(c => c[i] == '0');
            var one = input.Count(c => c[i] == '1');

            var keep = one >= zero ? '1' : '0';

            var result = new List<string>();
            result.AddRange(input.Where(c => c[i] == keep));
            return result;
        }
        
        private static List<string> LeastCommonBitCriteria(IReadOnlyCollection<string> input, int i)
        {
            var zero = input.Count(c => c[i] == '0');
            var one = input.Count(c => c[i] == '1');

            var keep = one < zero ? '1' : '0';

            var result = new List<string>();
            result.AddRange(input.Where(c => c[i] == keep));
            return result;
        }
        
        private static void PartTwo()
        {
            var input = ReadInputData().Select(chars => new string(chars)).ToList();
            var oxygenValues = input;
            var scrubberValues = input;
            
            var i = 0;
            while (oxygenValues.Count != 1)
            {
                oxygenValues = MostCommonBitCriteria(oxygenValues, i);
                i++;
            }

            i = 0;
            while (scrubberValues.Count != 1)
            {
                scrubberValues = LeastCommonBitCriteria(scrubberValues, i);
                i++;
            }
            
            var oxygen = Convert.ToInt32(string.Join("", oxygenValues.Select(c => c.ToString()).ToArray()), 2);
            var scrubber = Convert.ToInt32(string.Join("", scrubberValues.Select(c => c.ToString()).ToArray()), 2);
            
            Console.WriteLine($"oxygen * scrubber = {oxygen * scrubber}");
        }



        private static void Main(string[] args)
        {
            PartOne();
            PartTwo();
        }
    }
}