using System;

namespace CrackingCodingInterview
{
    class Program
    {
        static void Main(string[] args)
        {
            var stringAlgo = new StringAlgorithms();
            var result = stringAlgo.LongestCommonSubstring("abcdef", "abdefghijkabcdefg");
            Console.WriteLine(result);
        }
    }
}
