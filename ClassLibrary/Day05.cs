using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Day05 
    {
        public int SolutionPart1(params string[] input)
        {
            var total = 0;

            var tester = new NaughtyOrNiceTester();

            foreach (var line in input)
                if (tester.IsStringNice(line))
                    total++;

            return total;
        }

        public int SolutionPart2(params string[] input)
        {
            var total = 0;

            var tester = new NaughtyOrNiceTester();

            foreach (var line in input)
                if (tester.IsStringNicePart2(line))
                    total++;

            return total;
        }

        class NaughtyOrNiceTester
        {
            public bool IsStringNice(string input)
            {
                if (!HasThreeVowels(input))
                    return false;

                if (!HasDoubleLetters(input))
                    return false;

                if (HasForbiddenLetters(input))
                    return false;
              
                return true;
            }

            public bool IsStringNicePart2(string input)
            {
                if (!HasPairThatAppearsTwice(input))
                    return false;

                if (!HasLetterThatRepeatsWithOneLetterBetween(input))
                    return false;

                return true;
            }

            bool HasPairThatAppearsTwice(string input)
            {
                // \b - word boundary
                // \w* - some character(s)
                // (\w{2}) - match/capture 2 letters
                // \1 - captured group

                return Regex.IsMatch(input, @"\b\w*(\w{2})\w*\1");
            }

            bool HasLetterThatRepeatsWithOneLetterBetween(string input)
            {
                // similar to above, but with any single char between the capture and repeat
                return Regex.IsMatch(input, @"\b\w*(\w{1})[a-zA-Z]\1");
            }

            bool HasThreeVowels(string input)
            {
                var vowels = new char[] { 'a', 'e', 'i', 'o', 'u' };

                return input.Count(x => vowels.Contains(x)) > 2;
            }

            bool HasDoubleLetters(string input)
            {
                char? previousLetter = null;
                for (var i = 0; i < input.Length; i++)
                {
                    if (previousLetter.HasValue && input[i] == previousLetter.Value)
                        return true;
                    
                    previousLetter = input[i];
                }

                return false;
            }

            bool HasForbiddenLetters(string input)
            {
                var forbidden = new string[] { "ab", "cd", "pq", "xy" };

                foreach (var f in forbidden)
                    if (input.IndexOf(f) > -1)
                        return true;

                return false;
            }
        }

        interface IRule
        {
            bool Evaluate(string input);
        }
    }
}
