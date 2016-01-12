using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Day16
    {
        public int SolutionPart1(string[] input)
        {
            var q = CreateAuntsFromInput(input)
                .Where(x => x.Children == null || x.Children == 3)
                .Where(x => x.Cats == null || x.Cats == 7)
                .Where(x => x.Samoyeds == null || x.Samoyeds == 2)
                .Where(x => x.Pomeranians == null || x.Pomeranians == 3)
                .Where(x => x.Akitas == null || x.Akitas == 0)
                .Where(x => x.Vizslas == null || x.Vizslas == 0)
                .Where(x => x.Goldfish == null || x.Goldfish == 5)
                .Where(x => x.Trees == null || x.Trees == 3)
                .Where(x => x.Cars == null || x.Cars == 2)
                .Where(x => x.Perfumes == null || x.Perfumes == 1);

            return q.FirstOrDefault().Number;
        }

        public int SolutionPart2(string[] input)
        {
            var q = CreateAuntsFromInput(input)
                .Where(x => x.Children == null || x.Children == 3)
                .Where(x => x.Cats == null || x.Cats > 7)
                .Where(x => x.Samoyeds == null || x.Samoyeds == 2)
                .Where(x => x.Pomeranians == null || x.Pomeranians < 3)
                .Where(x => x.Akitas == null || x.Akitas == 0)
                .Where(x => x.Vizslas == null || x.Vizslas == 0)
                .Where(x => x.Goldfish == null || x.Goldfish < 5)
                .Where(x => x.Trees == null || x.Trees > 3)
                .Where(x => x.Cars == null || x.Cars == 2)
                .Where(x => x.Perfumes == null || x.Perfumes == 1);

            return q.FirstOrDefault().Number;
        }

        IEnumerable<Aunt> CreateAuntsFromInput(string[] input)
        {
            foreach (var line in input)
                yield return new Aunt(line);
        }

        class Aunt
        {
            public int Number { get; set; }

            public int? Children { get; set; }
            public int? Cats { get; set; }
            public int? Samoyeds { get; set; }
            public int? Pomeranians { get; set; }
            public int? Akitas { get; set; }
            public int? Vizslas { get; set; }
            public int? Goldfish { get; set; }
            public int? Trees { get; set; }
            public int? Cars { get; set; }
            public int? Perfumes { get; set; }

            public Aunt(string input)
            {
                var parts = input.Split(' ');

                Number = int.Parse(parts[1].TrimEnd(':'));
                SetProperty(parts[2].TrimEnd(':'), int.Parse(parts[3].TrimEnd(',')));
                SetProperty(parts[4].TrimEnd(':'), int.Parse(parts[5].TrimEnd(',')));
                SetProperty(parts[6].TrimEnd(':'), int.Parse(parts[7]));
            }

            void SetProperty(string name, int value)
            {
                switch (name)
                {
                    case "children":
                        Children = value;
                        break;

                    case "cats":
                        Cats = value;
                        break;

                    case "samoyeds":
                        Samoyeds = value;
                        break;

                    case "pomeranians":
                        Pomeranians = value;
                        break;

                    case "akitas":
                        Akitas = value;
                        break;

                    case "vizslas":
                        Vizslas = value;
                        break;

                    case "goldfish":
                        Goldfish = value;
                        break;

                    case "trees":
                        Trees = value;
                        break;

                    case "cars":
                        Cars = value;
                        break;

                    case "perfumes":
                        Perfumes = value;
                        break;
                }
            }
        }
    }
}
