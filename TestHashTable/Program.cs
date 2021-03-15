﻿using System;
using System.Collections.Generic;
using System.Text;
using HashTableForStudents;
using System.IO;
using System.Diagnostics;

namespace TestHashTable
{
    internal class Program
    {
        private static void Main()
        {
            string[] words = GetWords();
            var ts = new Stopwatch();
            ts.Start();
            UseDictionary(words);
            ts.Stop();
            Console.WriteLine(ts.ElapsedMilliseconds);
            ts.Reset();
            ts.Start();
            UseChainHashTable(words);
            ts.Stop();
            Console.WriteLine(ts.ElapsedMilliseconds);

            Console.ReadLine();
        }

        public static string[] GetWords()
        {
            string[] words;
            char[] delimitedchars =
            {
                ',', ':', ' ', '.', '!', ';', '<', '?', '>', '-', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '/',
                '"', '*', '(', ')', '\'','\n','\r','\\'
            };
            using (StreamReader f = new StreamReader("anna.txt", Encoding.Default))
            {
                words = f.ReadToEnd().ToLower().Split(delimitedchars, StringSplitOptions.RemoveEmptyEntries);
            }
            return words;
        }

        private static void UseChainHashTable(string[] words)
        {
            var htble = new ChainHashTable<string, int>(40009);
            var htbleHelp = new ChainHashTable<string, int>(40009);
            for (int i = 0; i < words.Length; i++)
            {
                var slovo = words[i];
                if (htble.Contains(slovo))
                {
                    htble[slovo]++;
                    htbleHelp[slovo]++;
                }
                else
                {
                    htble.Add(slovo, 1);
                    htbleHelp.Add(slovo, 1);
                }
            }

            foreach (var pair in htbleHelp)
            {
                if (pair.Value > 27)
                    htble.Remove(pair.Key);
            }
        }

        private static void UseDictionary(string[] words)
        {
            Dictionary<string, int> slovar = new Dictionary<string, int>();

            for (int i = 0; i < words.Length; i++)
            {
                var slovo = words[i];
                if (slovar.ContainsKey(slovo))
                {
                    slovar[slovo]++;
                }
                else
                    slovar.Add(slovo, 1);
            }
            var slovarHelp = new Dictionary<string, int>(slovar);
            foreach (var pair in slovarHelp)
            {
                if (pair.Value > 27)
                    slovar.Remove(pair.Key);
            }
        }
    }
}