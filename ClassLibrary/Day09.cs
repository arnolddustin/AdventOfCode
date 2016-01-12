using ClassLibrary.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Day09 
    {
        public int SolutionPart1(params string[] input)
        {
            var segments = new List<Segment>();

            foreach (var line in input)
                segments.Add(new Segment(line));

            var p = new Pathfinder(segments);
            return p.GetShortestPath().Distance;
        }

        public int SolutionPart2(params string[] input)
        {
            var segments = new List<Segment>();

            foreach (var line in input)
                segments.Add(new Segment(line));

            var p = new Pathfinder(segments);
            return p.GetLongestPath().Distance;
        }

        class Pathfinder
        {
            readonly List<Segment> _segments;
            readonly List<string> _locations;
            readonly List<Path> _permutations;

            public Pathfinder(IEnumerable<Segment> segments)
            {
                _segments = new List<Segment>();
                _segments.AddRange(segments);
                _locations = new List<string>();
                _locations.AddRange(_segments.SelectMany(x => x.locations).Distinct());
                _permutations = new List<Path>();
            }

            public Path GetLongestPath()
            {
                _permutations.Clear();
                new PermutationFinder<string>().Evaluate(_locations.ToArray(), Evaluate);

                return _permutations.OrderByDescending(x => x.Distance).FirstOrDefault();
            }
            public Path GetShortestPath()
            {
                _permutations.Clear();
                new PermutationFinder<string>().Evaluate(_locations.ToArray(), Evaluate);

                return _permutations.OrderBy(x => x.Distance).FirstOrDefault();
            }

            public bool Evaluate(string[] items)
            {
                var p = new Path();

                for (int i = 0; i < items.Length - 1; i++)
                {
                    var location1 = items[i];
                    var location2 = items[i + 1];
                    var segment = _segments.FirstOrDefault(x => x.locations.Contains(location1) && x.locations.Contains(location2));

                    p.Segments.Add(segment);
                }

                if (!_permutations.Contains(p))
                    _permutations.Add(p);

                return false;
            }

        }

        class Path
        {
            public string StartingLocation
            {
                get
                {
                    if (Segments.Count < 1) return string.Empty;

                    return Segments.First().Location1;
                }
            }
            public List<Segment> Segments { get; private set; }

            public int Distance
            {
                get
                {
                    return Segments.Sum(x => x.Distance);
                }
            }

            public Path()
            {
                Segments = new List<Segment>();
            }

            public override string ToString()
            {
                var sb = new StringBuilder();
                Segments.ForEach(x => sb.AppendFormat("{0}->", x.Location1));
                sb.Append(Segments.Last().Location2);
                return sb.ToString();
            }
        }

        class Segment
        {
            public string Location1 { get; private set; }
            public string Location2 { get; private set; }
            public int Distance { get; private set; }

            public string[] locations
            {
                get
                {
                    return new string[] { Location1, Location2 };
                }
            }

            public Segment(string line)
            {
                var s = line.Split(' ');

                Location1 = s[0];
                Location2 = s[2];
                Distance = int.Parse(s[4]);
            }

            public Segment(string location1, string location2, int distance)
            {
                Location1 = location1;
                Location2 = location2;
                Distance = distance;
            }
        }
    }
}
