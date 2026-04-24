using System;
using System.Collections.Generic;
using System.Linq;

namespace Project
{
    internal class Program
    {
        static void Main(string[] args)
        {

            while (true)
            {
                Users user1 = new Users() { Name = "Player 1", hearts = 3 };
                Users user2 = new Users() { Name = "Player 2", hearts = 3 };

                SimpleAI ai1 = new SimpleAI();
                SimpleAI ai2 = new SimpleAI();

                Console.WriteLine("1-User vs User");
                Console.WriteLine("2-User vs AI");
                Console.WriteLine("3-AI vs AI");
                Console.WriteLine("4-Exit");
                Console.Write("Choose: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        PlayUsers(user1, user2);
                        break;

                    case "2":
                        PlayUserVsAI(Dictionary.dictionary);
                        break;

                    case "3":
                        PlayAIvsAI(Dictionary.dictionary);
                        break;

                    case "4":
                        Environment.Exit(0);
                        break;
                }
                Console.WriteLine("========================");
            }
        }

        // ================= USER VS USER =================
        static void PlayUsers(Users user1, Users user2)
        {
            string lastWord = "";
            HashSet<string> usedWords = new HashSet<string>();

            while (true)
            {
             
                if (Turn(user1, user2, ref lastWord, usedWords)) return;
              
                if (Turn(user2, user1, ref lastWord, usedWords)) return;
            }
        }

        static bool Turn(Users current, Users opponent, ref string lastWord, HashSet<string> usedWords)
        {
            while (true)
            {
                Console.Write($"\n{current.Name}: ");
                string word = ReadInput();

                if (!IsValid(word, lastWord, usedWords))
                {
                    bool gameOver = LoseHeart(current, opponent, "Invalid word!");
                    if (gameOver) return true; 
                    continue;
                }

                usedWords.Add(word);
                lastWord = word;
                return false; 
            }
        }


        // ================= USER VS AI =================

        static void PlayUserVsAI(List<string> dictionary)
        {
            SimpleAI ai = new SimpleAI();
            HashSet<string> used = new HashSet<string>();

            string lastWord = "";

            while (true)
            {
                Console.Write("\nYour word: ");
                string user = ReadInput();

                if (!IsValid(user, lastWord, used))
                {
                    Console.WriteLine("You lost!");
                    return;
                }

                used.Add(user);
                lastWord = user;

                string aiWord = ai.Play(lastWord, dictionary);

                Console.WriteLine("AI is thinking...");
                Thread.Sleep(800);

                Console.WriteLine($"AI: {aiWord}");

                if (aiWord == null)
                {
                    Console.WriteLine("AI lost! You win!");
                    return;
                }


                used.Add(aiWord);
                lastWord = aiWord;
            }
        }
        // ================= AI VS AI =================
        static void PlayAIvsAI(List<string> dictionary)
        {
            Ai ai1 = new Ai(dictionary); // BFS
            Ai ai2 = new Ai(dictionary); // DFS

            string lastWord = "";
            bool turn = true;

            while (true)
            {
                string word;

                Console.ForegroundColor = ConsoleColor.Green;

                Console.WriteLine(turn ? "\nAI BFS thinking..." : "\nAI DFS thinking...");
                Console.ForegroundColor = ConsoleColor.White;

                Random rnd = new Random();

                if (turn)
                {
                    word = ai1.PlayBFS(lastWord);

                    if (word == null)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n💀 BFS Lost (No words left)");
                        Console.ForegroundColor = ConsoleColor.White;

                        Console.WriteLine($"DFS Wins!");
                        Console.WriteLine($"BFS Steps: {ai1.Score}");
                        Console.WriteLine($"DFS Steps: {ai2.Score}");

                        return;
                    }

                    Console.WriteLine($"AI BFS: {word}");

                }
                else
                {
                    word = ai2.PlayDFS(lastWord);

                    if (word == null)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n💀 DFS Lost (No words left)");
                        Console.ForegroundColor = ConsoleColor.White;

                        Console.WriteLine($"BFS Wins!");
                        Console.WriteLine($"BFS Score: {ai1.Score}");
                        Console.WriteLine($"DFS Score: {ai2.Score}");

                        return;
                    }

                    Console.WriteLine($"AI DFS: {word}");
                }

                lastWord = word;
                turn = !turn;
            }
        }

        // ================= HELPERS =================
        static string ReadInput()
        {
            return (Console.ReadLine() ?? "").ToLower().Trim();
        }

        static bool IsValid(string word, string lastWord, HashSet<string> used)
        {
            if (string.IsNullOrWhiteSpace(word))
                return false;

            if (used.Contains(word))
                return false;

            if (!Dictionary.dictionary.Contains(word))
                return false;

            if (!string.IsNullOrEmpty(lastWord) && word[0] != lastWord.Last())
                return false;

            return true;
        }

        static bool LoseHeart(Users player, Users opponent, string msg)
        {
            player.hearts--;
            Console.WriteLine($"{msg} | Hearts: {player.hearts}");
            if (player.hearts <= 0)
            {
                Console.WriteLine($"{opponent.Name} wins!");
                return true;
            }
            return false;
        }
    }
}