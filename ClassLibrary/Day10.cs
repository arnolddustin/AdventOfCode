using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Day10  
    {
        public int GetSolutionLength(string input, int numberOfTimes)
        {
            return GetSolution(input, numberOfTimes).Length;
        }

        public string GetSolution(string input, int numberOfTimes)
        {
            return EvaluateLookAndSay(input, numberOfTimes);
        }

        string EvaluateLookAndSay(string input, int numberOfTimes)
        {
            var newString = input;

            for (var i = 0; i < numberOfTimes; i++)
                newString = EvaluateLookAndSay(newString).ToString();

            return newString;
        }

        string EvaluateLookAndSay(string input)
        {
            var sb = new StringBuilder();

            var previous = 'X';
            var previousCount = 0;

            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == previous)
                {
                    previousCount++;
                }
                else
                {
                    if (previousCount > 0)
                        sb.AppendFormat("{0}{1}", previousCount, previous);

                    previous = input[i];
                    previousCount = 1;
                }
            }

            sb.AppendFormat("{0}{1}", previousCount, previous);

            return sb.ToString();
        }
    }
}
