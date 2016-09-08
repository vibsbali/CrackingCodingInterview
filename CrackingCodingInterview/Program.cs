using System;

namespace CrackingCodingInterview
{
    class Program
    {
        static void Main(string[] args)
        {
            var stringAlgo = new StringAlgorithms();
            var result = stringAlgo.IsAnAnagram("abc", "cab");
            Console.WriteLine(result);
        }
    }
}
