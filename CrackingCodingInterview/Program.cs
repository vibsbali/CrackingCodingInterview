using System;

namespace CrackingCodingInterview
{
    class Program
    {
        static void Main(string[] args)
        {
            var stringAlgo = new StringAlgorithms();
            var result = stringAlgo.BoyerMoreHorsepoolAlgorithm("arraragraaaararraaagrgggg", "arraaagr");
            Console.WriteLine(result);
        }
    }
}
