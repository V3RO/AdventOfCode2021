using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PassagePathing
{
    internal static class Program
    {
        private static List<string[]> ReadInputData()
        {
            var lines = File.ReadLines($"{Directory.GetCurrentDirectory()}/input.txt").ToList();
            return lines.Select(line => line.Split("-")).ToList();
        }
        
        private static List<string[]> _inputData;
        private static Dictionary<string, HashSet<string>> _nodes;
        private static List<string> _visited;
        private static List<string> _currentPath;
        private static List<List<string>> _paths;

        private static void PartOne()
        {
            _inputData = ReadInputData();
            _nodes = new Dictionary<string, HashSet<string>>();
            _visited = new List<string>();
            _currentPath = new List<string>();
            _paths = new List<List<string>>();
            
            foreach (var pair in _inputData)
            {
                if (!_nodes.TryAdd(pair[0], new HashSet<string>{pair[1]}))
                {
                    _nodes[pair[0]].Add(pair[1]);
                }
                
                if (!_nodes.TryAdd(pair[1], new HashSet<string>{pair[0]}))
                {
                    _nodes[pair[1]].Add(pair[0]);
                }
            }
            
            DfsOne(("start", new HashSet<string>(_nodes["start"])));
            
            Console.WriteLine(_paths.Count);
        }

        private static void DfsOne((string node, HashSet<string> adjacentNodes) node)
        {
            if (_visited.Contains(node.node) && !node.node.All(char.IsUpper)) return;
                
            _visited.Add(node.node);
            _currentPath.Add(node.node);
            
            if (node.node.Equals("end"))
            {
                _paths.Add(_currentPath);
                _visited.Remove("end");
                _currentPath = _currentPath.SkipLast(1).ToList();
                return;
            }
            
            foreach (var adjacentNode in node.adjacentNodes)
            {
                DfsOne((adjacentNode, _nodes[adjacentNode]));
            }
            
            _currentPath = _currentPath.SkipLast(1).ToList();
            _visited.Remove(node.node);
        }

        
        private static void PartTwo()
        {
            _inputData = ReadInputData();
            _nodes = new Dictionary<string, HashSet<string>>();
            _visited = new List<string>();
            _currentPath = new List<string>();
            _paths = new List<List<string>>();
            
            foreach (var pair in _inputData)
            {
                if (!_nodes.TryAdd(pair[0], new HashSet<string>{pair[1]}))
                {
                    _nodes[pair[0]].Add(pair[1]);
                }
                
                if (!_nodes.TryAdd(pair[1], new HashSet<string>{pair[0]}))
                {
                    _nodes[pair[1]].Add(pair[0]);
                }
            }
            
            DfsTwo(("start", new HashSet<string>(_nodes["start"])));
            
            Console.WriteLine(_paths.Count);
        }
        
        private static void DfsTwo((string node, HashSet<string> adjacentNodes) node)
        {
            if (_visited.Contains(node.node))
            {
                if (node.node.Equals("start") || node.node.Equals("end")) return;

                if (node.node.All(char.IsLower))
                {
                    var smallCaves = _visited.Where(nodes => nodes.All(char.IsLower)).ToList();
                    if (smallCaves.GroupBy(s => s).Any(g => g.Count() > 1))
                        return;
                }
            }
                
            _visited.Add(node.node);
            _currentPath.Add(node.node);
            
            if (node.node.Equals("end"))
            {
                _paths.Add(_currentPath);
                _visited.Remove("end");
                _currentPath = _currentPath.SkipLast(1).ToList();
                return;
            }
            
            foreach (var adjacentNode in node.adjacentNodes)
            {
                DfsTwo((adjacentNode, _nodes[adjacentNode]));
            }
            
            _currentPath = _currentPath.SkipLast(1).ToList();
            _visited.Remove(node.node);
        }
        
        private static void Main(string[] args)
        {
            PartOne();
            PartTwo();
        }
    }
}