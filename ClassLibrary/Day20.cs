using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Day20
    {
        public int SolutionPart1(int count, int presentsPerElf)
        {
            return FirstHouseWithAtLeastThisManyPresents(count, presentsPerElf, null);
        }

        public int SolutionPart2(int count, int presentsPerElf, int maxHousesPerElf)
        {
            return FirstHouseWithAtLeastThisManyPresents(count, presentsPerElf, maxHousesPerElf);
        }

        static int FirstHouseWithAtLeastThisManyPresents(int count, int presentsPerElf, int? maxHousesPerElf)
        {
            var presents = GetPresentsForHouses(count, presentsPerElf, maxHousesPerElf);

            return presents.Where(x => x.Value >= count).Min(x => x.Key);
        }

        static Dictionary<int, int> GetPresentsForHouses(int presentCount, int presentsPerElf, int? maxHousesPerElf)
        {
            var d = new Dictionary<int, int>();

            var limit = presentCount / 42;
            var houselimit = maxHousesPerElf.HasValue ? maxHousesPerElf.Value : int.MaxValue;

            for (int elf = 1; elf <= limit; elf++)
            {
                for (var house = elf; house <= limit && house / elf <= houselimit; house += elf)
                {
                    if (d.ContainsKey(house))
                        d[house] += elf * presentsPerElf;
                    else
                        d.Add(house, elf * presentsPerElf);
                }
            }

            return d;
        }
    }
}
