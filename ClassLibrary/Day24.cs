using ClassLibrary.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Day24
    {
        public long GetFrontSeatQE(int numberofCompartments, params string[] input)
        {
            var c = new Calculator(input);

            return c.GetFrontSeatQE(numberofCompartments);
        }

        class Calculator
        {
            readonly List<int> _packages;

            public Calculator(string[] input)
            {
                _packages = new List<int>();

                foreach (var p in input)
                    _packages.Add(int.Parse(p));
            }

            public long GetFrontSeatQE(int compartments)
            {
                var groupweight = _packages.Sum() / compartments;

                var finder = new CombinationFinder();
                var combos = finder.FindCombinationsWithSum(_packages, groupweight);

                var mincount = combos.Min(x => x.Count());
                var smallestcombos = combos.Where(x => x.Count() == mincount);

                long minQE = long.MaxValue;
                foreach (var c in smallestcombos)
                {
                    var qe = CalculateQE(c);

                    if (qe < minQE)
                        minQE = qe;
                }

                return minQE;
            }

            long CalculateQE(IEnumerable<int> packages)
            {
                long qe = 1;

                foreach (var i in packages)
                    qe = qe * i;

                return qe;
            }
        }
    }
}
