using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Utilities
{
    public class CombinationFinder
    {
        readonly List<List<int>> _combinations;

        public CombinationFinder()
        {
            _combinations = new List<List<int>>();
        }

        public IEnumerable<IEnumerable<int>> FindCombinationsWithSum(IEnumerable<int> items, int sum)
        {
            _combinations.Clear();

            var numbers = new List<int>(items);

            Recursive(numbers, sum, new List<int>());

            return _combinations;
        }

        private void Recursive(List<int> numbers, int target, List<int> partial)
        {
            int s = 0;
            foreach (int x in partial) s += x;

            if (s == target)
                _combinations.Add(partial);

            if (s >= target)
                return;

            for (int i = 0; i < numbers.Count; i++)
            {
                List<int> remaining = new List<int>();
                int n = numbers[i];
                for (int j = i + 1; j < numbers.Count; j++) remaining.Add(numbers[j]);

                List<int> partial_rec = new List<int>(partial);
                partial_rec.Add(n);
                Recursive(remaining, target, partial_rec);
            }
        }
    }
 }
