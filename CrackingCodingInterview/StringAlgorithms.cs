using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrackingCodingInterview
{
    public class StringAlgorithms
    {
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
                        if (matrix[i,j] > max)
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
    }
}
