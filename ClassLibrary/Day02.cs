using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Day02
    {
        public int SolutionPart1(params string[] input)
        {
            return CalculateTotalPaperRequired(input);
        }

        public int SolutionPart2(params string[] input)
        {
            return CalculateTotalRibbonRequired(input);
        }

        int CalculateTotalPaperRequired(string[] input)
        {
            var total = 0;

            foreach (var line in input)
                total += new Package(line).GetPaperNeeded();

            return total;
        }

        int CalculateTotalRibbonRequired(string[] input)
        {
            var total = 0;

            foreach (var line in input)
                total += new Package(line).GetRibbonNeeded();

            return total;
        }

        class Package
        {
            public int Length { get; private set; }
            public int Width { get; private set; }
            public int Height { get; private set; }
            public int Volume { get { return Length * Width * Height; } }

            public Package(string line)
            {
                var dimensions = line.Split('x');

                Length = int.Parse(dimensions[0]);
                Width = int.Parse(dimensions[1]);
                Height = int.Parse(dimensions[2]);
            }

            public int GetPaperNeeded()
            {
                return GetAllSides().Sum(x => x.Area) + GetSmallestSide().Area;
            }

            public int GetRibbonNeeded()
            {
                return GetSmallestSide().Perimeter + Volume;
            }

            IEnumerable<Side> GetAllSides()
            {
                yield return new Side(Length, Width);
                yield return new Side(Length, Width);
                yield return new Side(Length, Height);
                yield return new Side(Length, Height);
                yield return new Side(Width, Height);
                yield return new Side(Width, Height);
            }

            Side GetSmallestSide()
            {
                var l = new List<int>();
                l.Add(Length);
                l.Add(Width);
                l.Add(Height);

                var sorted = l.OrderBy(x => x);

                var dim1 = sorted.ElementAt(0);
                var dim2 = sorted.ElementAt(1);

                return new Side(dim1, dim2);
            }
        }

        class Side
        {
            public int Length { get; private set; }
            public int Width { get; private set; }
            public int Area { get { return Length * Width; } }
            public int Perimeter { get { return Length * 2 + Width * 2; } }

            public Side(int length, int width)
            {
                Length = length;
                Width = width;
            }
        }
    }
}
