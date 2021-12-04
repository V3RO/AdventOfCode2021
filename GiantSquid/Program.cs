using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace GiantSquid
{
    internal static class Program
    {
        private static List<string> ReadBingoInput()
        {
            using var fileStream = File.OpenRead($"{Directory.GetCurrentDirectory()}/input.txt");
            using var streamReader = new StreamReader(fileStream, Encoding.UTF8);
            return (streamReader.ReadLine() ?? string.Empty).Split(",").ToList();
        }
        
        private static IEnumerable<List<Dictionary<string, bool>>> ReadBingoSheets()
        {
            using var fileStream = File.OpenRead($"{Directory.GetCurrentDirectory()}/input.txt");
            using var streamReader = new StreamReader(fileStream, Encoding.UTF8);
            streamReader.ReadLine();

            var bingoSheets = new List<List<Dictionary<string, bool>>>() {};
            var line = "";
            var sheetNr = -1;
            while ((line = streamReader.ReadLine()) != null)
            {
                if (line.Equals(""))
                {
                    bingoSheets.Add(new List<Dictionary<string, bool>>());
                    sheetNr++;
                }
                else
                {
                    var dictionary = Regex.Replace(line.Trim(), @"\s+", " ").Split(" ").ToDictionary(s => s, s => false);
                    bingoSheets[sheetNr].Add(dictionary);
                }
            }

            return bingoSheets;
        }

        private static bool CheckBingo(List<Dictionary<string, bool>> sheet)
        {
            if (sheet.Any(row => row.All(entry => entry.Value)))
                return true;

            for (var col = 0; col < sheet.Count; col++)
            {
                var bingo = true;
                
                foreach (var row in sheet.Where(row => !row.ElementAt(col).Value))
                {
                    bingo = false;
                }

                if (bingo)
                    return true;
            }
            
            return false;
        }
        
        private static int UncheckedSum(List<Dictionary<string, bool>> sheet)
        {
            return sheet.Sum(row => row.Where(pair => !pair.Value).Sum(pair => int.Parse(pair.Key)));
        }

        private static void PartOne()
        {
            var bingoInput = ReadBingoInput();
            var bingoSheets = ReadBingoSheets();
            const int stepSize = 5;
            var skip = 0;
            List<string> currentInput;

            while ((currentInput = bingoInput.Skip(skip * stepSize).Take(stepSize).ToList()).Count() != 0)
            {
                foreach (var input in currentInput)
                {
                    foreach (var sheet in bingoSheets)
                    {
                        foreach (var row in sheet)
                        {
                            foreach (var entry in row.Where(entry =>  input.Equals(entry.Key)))
                            {
                                row[entry.Key] = true;
                                
                                if (!CheckBingo(sheet)) continue;
                                CheckBingo(sheet);
                                Console.WriteLine($"{UncheckedSum(sheet) * int.Parse(input)}");
                    
                                return;
                            }
                        }
                    }
                }
                skip++;
            }
        }
        
        private static void PartTwo()
        {
            var bingoInput = ReadBingoInput();
            var bingoSheets = ReadBingoSheets();
            const int stepSize = 5;
            var sheetNr = 0;
            var skip = 0;
            List<string> currentInput;
            var winners = new Dictionary<int, bool>();

            while ((currentInput = bingoInput.Skip(skip * stepSize).Take(stepSize).ToList()).Count() != 0)
            {
                foreach (var input in currentInput)
                {
                    foreach (var sheet in bingoSheets)
                    {
                        foreach (var row in sheet)
                        {
                            foreach (var entry in row.Where(entry =>  input.Equals(entry.Key)))
                            {
                                row[entry.Key] = true;
                                
                                if (!CheckBingo(sheet)) continue;
                                winners.TryAdd(sheetNr, true);
                                if (winners.Count != bingoSheets.Count()) continue;
                                Console.WriteLine($"{UncheckedSum(sheet) * int.Parse(input)}");
                                return;
                            }
                        }
                        sheetNr++;
                    }
                    sheetNr = 0;
                }
                skip++;
            }
        }
        
        private static void Main(string[] args)
        {
            PartOne();
            PartTwo();
        }
    }
}