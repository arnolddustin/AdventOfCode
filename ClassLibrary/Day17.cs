using ClassLibrary.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Day17
    {
        public int SolutionPart1(int volume, params string[] input)
        {
            var containers = CreateContainersFromInput(input).OrderBy(x => x);

            var finder = new CombinationFinder();

            return finder.FindCombinationsWithSum(containers, volume).Count();
        }

        public int SolutionPart2(int volume, params string[] input)
        {
            var containers = CreateContainersFromInput(input).OrderBy(x => x);

            var finder = new CombinationFinder();
            var combinations = finder.FindCombinationsWithSum(containers, volume);

            var lowest = combinations.Min(x => x.Count());

            var combinationsWithLowest = combinations.Where(x => x.Count() == lowest);

            return combinationsWithLowest.Count();
        }

        IEnumerable<int> CreateContainersFromInput(string[] input)
        {
            foreach (var line in input)
                yield return int.Parse(line);
        }
    }
}
