using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SyntaxScoring
{
    internal static class Program
    {
        private static List<string> ReadInputData()
        {
            return File.ReadLines($"{Directory.GetCurrentDirectory()}/input.txt").ToList();
        }

        private static void PartOne()
        {
            var inputData = ReadInputData();
            var errorSum = 0;
            
            foreach (var line in inputData)
            {
                var chunks = new Stack<char>();

                foreach (var c in line)
                {
                    if (c is '(' or '{' or '[' or '<')
                    {
                        chunks.Push(c);
                    }
                    else
                    {
                        var opening = chunks.Pop();
                        if (opening is '(' && c is ')' ||
                            opening is '[' && c is ']' ||
                            opening is '{' && c is '}' ||
                            opening is '<' && c is '>') continue;
                        switch (c)
                        {
                            case ')':
                                errorSum += 3;
                                break;
                            case ']':
                                errorSum += 57;
                                break;
                            case '}':
                                errorSum += 1197;
                                break;
                            case '>':
                                errorSum += 25137;
                                break;
                        }
                        break;
                    }
                }
            }

            Console.WriteLine(errorSum);
        }
        
        private static void PartTwo()
        {
            var inputData = ReadInputData();
            var completionScores = new List<long>();
            
            foreach (var line in inputData)
            {
                var chunks = new Stack<char>();
                var corrupted = false;

                foreach (var c in line)
                {
                    if (c is '(' or '{' or '[' or '<')
                    {
                        chunks.Push(c);
                    }
                    else
                    {
                        var opening = chunks.Pop();
                        if (opening is '(' && c is not ')' ||
                            opening is '[' && c is not ']' ||
                            opening is '{' && c is not '}' ||
                            opening is '<' && c is not '>') corrupted = true;
                    }
                }

                if (corrupted)
                    continue;
                
                long completionScore = 0;
                while (chunks.Count > 0)
                {
                    var opening = chunks.Pop();
                    completionScore = opening switch
                    {
                        '(' => completionScore * 5 + 1,
                        '[' => completionScore * 5 + 2,
                        '{' => completionScore * 5 + 3,
                        '<' => completionScore * 5 + 4,
                        _ => completionScore
                    };
                }
                completionScores.Add(completionScore);
            }

            Console.WriteLine(completionScores.OrderByDescending(i => i).Skip(completionScores.Count / 2).Take(1).First());
        }
        
        private static void Main(string[] args)
        {
            PartOne();
            PartTwo();
        }
    }
}