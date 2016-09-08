using System;

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


    }
}
