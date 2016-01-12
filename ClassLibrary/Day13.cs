using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Day13
    {
        public int SolutionPart1(params string[] input)
        {
            return FindChangeInHappinessFromOptimalArrangement(input, false);
        }

        public int SolutionPart2(params string[] input)
        {
            return FindChangeInHappinessFromOptimalArrangement(input, true);
        }

        int FindChangeInHappinessFromOptimalArrangement(string[] input, bool includeMe)
        {
            var interactions = new List<Interaction>();

            foreach (var line in input)
                interactions.Add(new Interaction(line));

            if (includeMe)
            {
                var myInteractions = new List<Interaction>();
                foreach (var person in interactions.Select(x => x.Person1).Union(interactions.Select(x => x.Person2).Distinct()))
                {
                    myInteractions.Add(new Interaction("Me", person, 0));
                    myInteractions.Add(new Interaction(person, "Me", 0));
                }

                interactions.AddRange(myInteractions);
            }

            var arranger = new SeatArranger(interactions);
            return arranger.GetOptimalSeatingArrangement().Happiness;
        }

        class SeatArranger
        {
            readonly List<Interaction> _interactions;
            readonly List<string> _people;

            public SeatArranger(IEnumerable<Interaction> interactions)
            {
                _interactions = new List<Interaction>(interactions);
                _people = new List<string>(_interactions.Select(x => x.Person1).Union(_interactions.Select(x => x.Person2).Distinct()));
            }

            public SeatingArrangement GetOptimalSeatingArrangement()
            {
                var list = new List<SeatingArrangement>();

                foreach (var sequence in GetPermutations(_people, _people.Count()))
                    list.Add(new SeatingArrangement(sequence, _interactions));

                return list.OrderByDescending(x => x.Happiness).FirstOrDefault();
            }

            IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> list, int length)
            {
                if (length == 1) return list.Select(t => new T[] { t });
                return GetPermutations(list, length - 1)
                    .SelectMany(t => list.Where(o => !t.Contains(o)),
                        (t1, t2) => t1.Concat(new T[] { t2 }));
            }
        }

        class SeatingArrangement
        {
            public List<Interaction> Interactions { get; private set; }

            public int Happiness
            {
                get
                {
                    return Interactions.Sum(x => x.Happiness);
                }
            }

            public SeatingArrangement()
            {
                Interactions = new List<Interaction>();
            }

            public SeatingArrangement(IEnumerable<string> people, IEnumerable<Interaction> interactions)
                : this()
            {
                var list = new List<string>(people);

                for (int i = 0; i < list.Count - 1; i++)
                {
                    Interactions.Add(interactions.FirstOrDefault(x => x.Person1 == list[i] && x.Person2 == list[i + 1]));
                    Interactions.Add(interactions.FirstOrDefault(x => x.Person1 == list[i + 1] && x.Person2 == list[i]));
                }

                Interactions.Add(interactions.FirstOrDefault(x => x.Person1 == list.Last() && x.Person2 == list.First()));
                Interactions.Add(interactions.FirstOrDefault(x => x.Person2 == list.Last() && x.Person1 == list.First()));
            }

            public override string ToString()
            {
                var sb = new StringBuilder();
                Interactions.ForEach(x => sb.AppendFormat("=>", x));
                return sb.ToString();
            }
        }

        class Interaction
        {
            public string Person1 { get; private set; }
            public string Person2 { get; private set; }
            public int Happiness { get; private set; }

            public Interaction(string line)
            {
                var s = line.Split(' ');

                Person1 = s[0];
                Person2 = s[10].TrimEnd('.');
                Happiness = s[2] == "gain" ? int.Parse(s[3]) : (-1 * int.Parse(s[3]));
            }

            public Interaction(string person1, string person2, int happiness)
            {
                Person1 = person1;
                Person2 = person2;
                Happiness = happiness;
            }

            public override string ToString()
            {
                return string.Format("[{0}->{1}:{2}]", Person1, Person2, Happiness);
            }
        }
    }
}
