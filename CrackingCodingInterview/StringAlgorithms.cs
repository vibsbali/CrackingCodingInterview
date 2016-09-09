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
        /* Create a bad match table ie. for pattern TRUTH the table would be like so
        *                                   T R U
        *                                   1 3 2
        *  Here is how the table is created - looking at pattern TRUTH 
        *  If we match H then it is good and we can start comparing backwards, if there is a mismatch and the character
        *  in string to search in has character U then we should move our TRUTH to 2 indices to right. If it was a R we would have
        *  moved 3 character and if it was T then only 1
        *  If you want to match space then ensure to add space at the begining of the pattern
        */

        public int BoyerMoreHorsepoolAlgorithm(string input, string pattern)
        {
            Dictionary<char, int> badMatchTable = GenerateBadMatchTable(pattern);

            //From here on we start the matching process
            var lengthOfPattern = pattern.Length;
            //We want to start matching from left to right so we skip number of items in input equal to pattern' length
            for (int i = lengthOfPattern - 1; i < input.Length; i += lengthOfPattern)
            {
                //If we find the that right's character of pattern matches with inputString's character
                if (input[i] == pattern[lengthOfPattern - 1])
                {
                    var result = SearchString(input, pattern, 0, i);
                    if (result != -1)
                    {
                        return result;
                    }

                }
                //Else if we find that right's character of pattern doesnt match with input string' character but the character is in bad match table 
                //i.e. HELLO WORLD is our input string and pattern is OR
                //             OR <- At this stage O And R doesn't match but O is in bad match table 
                else if (input[i] != pattern[lengthOfPattern - 1] && badMatchTable.ContainsKey(input[i]))
                {
                    //get the value which determines our offset
                    var value = badMatchTable[input[i]];
                    var result = SearchString(input, pattern, value, i);
                    if (result != -1)
                    {
                        return result;
                    }
                }
            }

            return -1;
        }

        //Used in conjuction with BoyerMoreHorsepool algorithm
        private static Dictionary<char, int> GenerateBadMatchTable(string pattern)
        {
            //Create a bad match table
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

        //Used in conjuction with BoyerMoreHorsepool algorithm
        //Accepts input and pattern string along with offset and index value
        //offset tell how much to the right pattern needs to be matched
        private int SearchString(string input, string pattern, int offset, int index)
        {
            var indexToSearchFrom = pattern.Length - 1;
            //check to ensure that offset + index doesn't cause out of bounds exception for input string
            if (offset + index >= input.Length)
            {
                return -1;
            }
            for (int i = offset + index; i >= 0 && indexToSearchFrom >= 0; i--)
            {
                if (input[i] == pattern[indexToSearchFrom])
                {
                    if (indexToSearchFrom == 0)
                    {
                        return i;
                    }
                    --indexToSearchFrom;
                }
                else
                {
                    return -1;
                }
            }
            return -1;
        }

    }
}
