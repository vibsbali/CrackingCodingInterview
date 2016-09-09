using System;

namespace CrackingCodingInterview
{
    class Program
    {
        static void Main(string[] args)
        {
            var stringAlgo = new StringAlgorithms();
            var result = stringAlgo.BoyerMoreHorsepoolAlgorithm("ramanand bag is a great place to live. ramanrao lives here", "ramrao");
            Console.WriteLine(result);
        }
    }
}
