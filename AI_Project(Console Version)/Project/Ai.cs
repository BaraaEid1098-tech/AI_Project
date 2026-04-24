using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Project
{
    internal class Ai
    {
        private static Random random = new Random();

        public Dictionary<char, List<string>> Graph { get; set; }
        public HashSet<string> Used { get; set; } = new HashSet<string>();
        public int Score { get; set; } = 0;

        public Ai(List<string> dictionary)
        {
            Graph = BuildGraph(dictionary);
        }


        private Dictionary<char, List<string>> BuildGraph(List<string> words)
        {
            return words
                .Select(w => w.ToLower())
                .OrderBy(w => w) 
                .GroupBy(w => w[0])
                .OrderBy(g => g.Key) 
                .ToDictionary(
                    g => g.Key,
                    g =>
                    {
                        int count = random.Next(3, 11);

                        var selected = g
                            .OrderBy(x => random.Next())
                            .Take(count)
                            .ToList();

                      
                        return selected.OrderBy(w => w).ToList();
                    }
                );
        }

        // ================= BFS =================
        public string PlayBFS(string lastWord)
        {
            var stopwatch = Stopwatch.StartNew();

            var letters = Graph.Keys.OrderBy(c => c).ToList();

            Queue<char> queue = new Queue<char>(letters);

            char target = string.IsNullOrEmpty(lastWord)
                ? letters.First()
                : lastWord.Last();

            while (queue.Count > 0)
            {
                char current = queue.Dequeue();

                Thread.Sleep(100); // node letter

                if (current == target)
                {
                    foreach (var word in Graph[current])
                    {
                        Thread.Sleep(100); // word edge

                        if (!Used.Contains(word))
                        {
                            Used.Add(word);
                            Score++;

                            stopwatch.Stop();
                            Console.WriteLine($"BFS Time: {stopwatch.ElapsedMilliseconds} ms");

                            return word;
                        }
                    }

                    return null;
                }
            }

            return null;
        }

        // ================= DFS =================
        public string PlayDFS(string lastWord)
        {
            var stopwatch = Stopwatch.StartNew();

            char needed = string.IsNullOrEmpty(lastWord) ? '\0' : lastWord.Last();

            foreach (var node in Graph.Keys.OrderBy(c => c)) // A → Z
            {
                Thread.Sleep(100);

                var stack = new Stack<string>(Graph[node].OrderByDescending(w => w));

                while (stack.Count > 0)
                {
                    var word = stack.Pop();

                    Thread.Sleep(100);

                    if (!Used.Contains(word) &&
                        (needed == '\0' || word[0] == needed))
                    {
                        Used.Add(word);
                        Score++;

                        stopwatch.Stop();
                        Console.WriteLine($"DFS Time: {stopwatch.ElapsedMilliseconds} ms");

                        return word; 
                    }
                }
            }

            return null;
        }

    }
}