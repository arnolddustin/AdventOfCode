using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Day19
    {
        public int SolutionPart1(params string[] input)
        {
            var i = Instructions.Parse(input);

            var c = new Calibrator(i);

            var molecules = c.GetDistinctCalibrationMolecules();

            return molecules.Count();
        }

        ///<remarks>
        /// This solution is explained here
        /// https://www.reddit.com/r/adventofcode/comments/3xflz8/day_19_solutions/cy4etju
        ///</remarks>
        public int SolutionPart2(params string[] input)
        {
            var i = Instructions.Parse(input).Molecule;

            var e = Regex.Matches(i, "[A-Z]").Count;
            var rn = Regex.Matches(i, "Rn").Count;
            var ar = Regex.Matches(i, "Ar").Count;
            var y = Regex.Matches(i, "Y").Count;

            return  e - rn - ar - (2 * y) - 1;
        }

        class Calibrator
        {
            readonly Instructions _instructions;

            public Calibrator(Instructions instructions)
            {
                _instructions = instructions;
            }

            public IEnumerable<string> GetDistinctCalibrationMolecules()
            {
                return GetAllMolecules().Distinct();
            }

            IEnumerable<string> GetAllMolecules()
            {
                foreach (var instruction in _instructions.Replacements)
                {
                    foreach (var index in AllIndexesOf(_instructions.Molecule, instruction.OldString))
                    {
                        yield return string.Format("{0}{1}{2}", _instructions.Molecule.Substring(0, index), instruction.NewString, _instructions.Molecule.Substring(index + instruction.OldString.Length, _instructions.Molecule.Length - instruction.OldString.Length - index));
                    }
                }
            }

            static IEnumerable<int> AllIndexesOf(string str, string value)
            {
                if (String.IsNullOrEmpty(value))
                    yield break;

                for (int index = 0; ; index += value.Length)
                {
                    index = str.IndexOf(value, index);
                    if (index == -1)
                        yield break;

                    yield return index;
                }
            }
        }

        class Instructions
        {
            public string Molecule { get; private set; }
            public List<Replacement> Replacements { get; private set; }

            private Instructions() 
            {
                Replacements = new List<Replacement>();
            }

            public static Instructions Parse(string[] input)
            {
                var instruction = new Instructions();

                foreach (var line in input)
                    if (line.Contains('='))
                        instruction.Replacements.Add(Replacement.Parse(line));
                    else
                        instruction.Molecule = line;

                return instruction;
            }
        }

        class Replacement
        {
            public string OldString { get; private set; }
            public string NewString { get; private set; }

            public Replacement(string oldString, string newString)
            {
                OldString = oldString;
                NewString = newString;
            }

            public static Replacement Parse(string line)
            {
                if (!line.Contains("=")) return null;

                var parts = line.Split(' ');
                return new Replacement(parts[0], parts[2]);
            }
        }
    }
}
