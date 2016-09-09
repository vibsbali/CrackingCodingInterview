using System;
using System.Collections.Generic;

namespace CrackingCodingInterview
{
    public class StringAlgorithms
    {
        /// <summary>
        /// Longest Common Substring
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public string LongestCommonSubstring(string a, string b)
        {
            if (string.IsNullOrEmpty(a) || string.IsNullOrEmpty(b))
            {
                throw new ArgumentException();
            }

            int[,] matrix = new int[a.Length + 1, b.Length + 1];

            var max = 0;
            var row = 0;

            for (int i = 1; i <= a.Length; i++)
            {
                for (int j = 1; j <= b.Length; j++)
                {
                    if (a[i - 1] == b[j - 1])
                    {
                        matrix[i, j] = 1 + matrix[i - 1, j - 1];
                        if (matrix[i, j] > max)
                        {
                            max = matrix[i, j];
                            row = i;
                        }
                    }
                }
            }

            //if there is common substring
            string substring = a.Substring(row - max, max);
            return substring;
        }

        /// <summary>
        /// Check if two strings are an anagram of each other
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public bool IsAnAnagram(string a, string b)
        {
            //solution 1
            //if (a.Length != b.Length)
            //{
            //    return false;
            //}

            //var aCharArray = a.ToCharArray();
            //var bCharArray = b.ToCharArray();

            //Array.Sort(aCharArray);
            //Array.Sort(bCharArray);

            //for (int i = 0; i < aCharArray.Length; i++)
            //{
            //    if (aCharArray[i] != bCharArray[i])
            //    {
            //        return false;
            //    }
            //}

            //return true;

            //solution 2
            if (a.Length != b.Length)
            {
                return false;
            }

            //assuming ASCII
            var radixOfA = new int[256];
            var radixOfB = new int[256];

            for (int i = 0; i < a.Length; i++)
            {
                ++radixOfA[a[i]];
            }

            for (int i = 0; i < b.Length; i++)
            {
                ++radixOfB[b[i]];
            }

            for (int i = 0; i < 256; i++)
            {
                if (radixOfA[i] != radixOfB[i])
                {
                    return false;
                }
            }

            return true;
        }


        /// <summary>
        /// This is a NaiiveSearchAlgorithm for searching if a contains b
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public bool NaiiveSearchAlgorithm(string a, string b)
        {
            //if any of the two strings are null
            if (string.IsNullOrEmpty(a) || string.IsNullOrEmpty(b))
            {
                throw new ArgumentException();
            }

            //if a is smaller than b then return false
            if (a.Length < b.Length)
            {
                return false;
            }

            var j = 0;
            var matchCount = 0;
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] == b[j])
                {
                    while (i < a.Length)
                    {
                        if (a[i] == b[j])
                        {
                            matchCount++;
                            i++;
                            j++;

                            if (matchCount == b.Length)
                            {
                                return true;
                            }
                        }
                        else
                        {
                            j = 0;
                            matchCount = 0;
                            break;
                        }
                    }
                }
            }

            return false;
        }


        //Boyer-Moore-Horspool algorithm
        /* Create a bad match table ie. for pattern TRUTH the table would be like so;       
        *                                   T R U
        *                                   1 3 2
        *  Here is how the table is created - looking at pattern TRUTH 
        *  If we match H then it is good and we can start comparing backwards, if there is a mismatch and the character
        *  in string to search in has character U then we should move our TRUTH to 2 indices to right. If it was a R we would have
        *  moved 3 character and if it was T then only 1
        *  If you want to match space then ensure to add space at the begining of the pattern
        */

        public bool BoyerMoreHorsepoolAlgorithm(string input, string pattern)
        {
            if (string.IsNullOrWhiteSpace(input) || string.IsNullOrWhiteSpace(pattern))
            {
                throw new InvalidOperationException();
            }

            if (pattern.Length > input.Length)
            {
                return false;
            }

            //Create a bad match table
            Dictionary<char, int> badMatchTable = GenerateBadMatchTable(pattern);

            var i = pattern.Length - 1;
            var j = pattern.Length - 1;
            var matchCount = 0;

            while (i < input.Length)
            {
                if (input[i] == pattern[j])
                {
                    while (input[i] == pattern[j])
                    {
                        i--;
                        j--;
                        matchCount++;
                        if (matchCount == pattern.Length)
                        {
                            return true;
                        }
                    }
                }
                else
                {
                    j = pattern.Length - 1;
                    matchCount = 0;
                    if (badMatchTable.ContainsKey(input[i]))
                    {
                        i = i + badMatchTable[input[i]];
                    }
                    else
                    {
                        i = i + j;
                    }
                }
            }

            return false;
        }

        //Used in conjuction with BoyerMoreHorsepool algorithm
        private static Dictionary<char, int> GenerateBadMatchTable(string pattern)
        {
            var badMatchTable = new Dictionary<char, int>();
            var j = 1;  //We can optimise and remove j but I will leave it for readability
            for (int i = pattern.Length - 2; i >= 0; i--)
            {
                if (!badMatchTable.ContainsKey(pattern[i]))
                {
                    badMatchTable.Add(pattern[i], j);
                    j++;
                }
            }

            return badMatchTable;
        }
    }
}
