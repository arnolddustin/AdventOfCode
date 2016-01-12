using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Day03 
    {
        public int SolutionPart1(params string[] input)
        {
            return CalculateHousesVisited(input[0], false);
        }

        public int SolutionPart2(params string[] input)
        {
            return CalculateHousesVisited(input[0], true);
        }

        int CalculateHousesVisited(string input, bool includeRobot)
        {
            if (!includeRobot)
            {
                var route = new Route();

                for (var i = 0; i < input.Length; i++)
                    route.VisitNextHouse(input[i]);

                return route.Houses.Count;
            }
            else
            {
                var route1 = new Route();
                var route2 = new Route();

                for (var i = 0; i < input.Length; i++)
                {
                    if (i % 2 == 0)
                        route1.VisitNextHouse(input[i]);
                    else
                        route2.VisitNextHouse(input[i]);
                }

                var combined = route1.Houses;
                
                foreach (var robotRoute in route2.Houses)
                    if (!combined.Any(x => x.X == robotRoute.X && x.Y == robotRoute.Y))
                        combined.Add(robotRoute);
                
                return combined.Count;
            }
        }

        class Route
        {
            public List<House> Houses { get; private set; }
            public House CurrentHouse { get; private set; }

            public Route()
            {
                Houses = new List<House>();
                CurrentHouse = new House(0, 0);
                Houses.Add(CurrentHouse);
            }

            public void VisitNextHouse(char direction)
            {
                House nextHouse = null;

                switch (direction)
                {
                    case '^':
                        nextHouse = new House(CurrentHouse.X, CurrentHouse.Y + 1);
                        break;

                    case 'v':
                        nextHouse = new House(CurrentHouse.X, CurrentHouse.Y - 1);
                        break;

                    case '<':
                        nextHouse = new House(CurrentHouse.X - 1, CurrentHouse.Y);
                        break;

                    case '>':
                        nextHouse = new House(CurrentHouse.X + 1, CurrentHouse.Y);
                        break;
                }

                if (!Houses.Any(h => h.X == nextHouse.X && h.Y == nextHouse.Y))
                    Houses.Add(nextHouse);

                CurrentHouse = nextHouse;
            }
        }

        class House
        {
            public int X { get; set; }
            public int Y { get; set; }

            public House(int x, int y)
            {
                X = x;
                Y = y;
            }
        }
    }
}
