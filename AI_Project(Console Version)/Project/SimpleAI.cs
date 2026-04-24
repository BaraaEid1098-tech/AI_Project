using System;
using System.Collections.Generic;
using System.Linq;

namespace Project
{
    internal class SimpleAI
    {
        private static Random random = new Random();
        public HashSet<string> Used { get; set; } = new HashSet<string>();

        public string Play(string lastWord, List<string> dictionary)
        {
            var available = dictionary
                .Where(w =>
                    !Used.Contains(w) &&
                    (string.IsNullOrEmpty(lastWord) || w[0] == lastWord.Last())
                )
                .ToList();

            if (available.Count == 0)
                return null;

            string chosen = available[random.Next(available.Count)];

            Used.Add(chosen);
            return chosen;
        }
    }
}