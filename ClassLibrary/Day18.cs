using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Day18  
    {
        public int SolutionPart1(int steps, params string[] input)
        {
            var animator = new Animator(false, input);

            return animator.Animate(steps);
        }

        public int SolutionPart2(int steps, params string[] input)
        {
            var animator = new Animator(true, input);

            return animator.Animate(steps);
        }

        class Animator
        {
            readonly Grid _grid;

            public Animator(bool cornersOn, string[] input)
            {
                _grid = new Grid(input, cornersOn);
            }

            public int Animate(int steps)
            {
                for (var i = 0; i < steps; i++)
                    _grid.Animate();

                return _grid.LightsOn;
            }
        }

        class Grid
        {
            readonly int _height;
            readonly int _width;
            readonly List<GridLight> _lights;
            readonly bool _cornersOn;

            public int LightsOn
            {
                get
                {
                    return _lights.Count(l => l.On);
                }
            }

            public Grid(string[] rows, bool cornersOn)
            {
                if (rows == null || rows.Length < 1)
                    throw new ArgumentException("At least one row is required");

                if (rows.Select(x => x.Length).Distinct().Count() != 1)
                    throw new ArgumentException("All rows must have the same number of columns");

                _height = rows.Length;
                _width = rows[0].Length;
                _lights = new List<GridLight>();
                _cornersOn = cornersOn;

                var corners = new List<Position>();
                if (_cornersOn) 
                    corners.AddRange(new Position[] { 
                        new Position(0, 0), 
                        new Position(_width - 1, 0), 
                        new Position(_width - 1, _height - 1), 
                        new Position(0, _height - 1) 
                    });

                for (var x = 0; x < rows.Length; x++)
                    for (var y = 0; y < rows[x].Length; y++)
                    {
                        if (corners.Any(p => p.X == x && p.Y == y))
                            _lights.Add(new GridLight(x, y, rows[x][y] == '#', true));
                        else
                            _lights.Add(new GridLight(x, y, rows[x][y] == '#', false));
                    }

                foreach (var light in _lights)
                    light.Neighbors.AddRange(FindLights(light.Position.GetNeighborPositions().Values));
            }

            public void Animate()
            {
                foreach (var light in _lights)
                    light.OnNextState = GetNextStateForLight(light);

                foreach (var light in _lights)
                    light.Animate();
            }

            public override string ToString()
            {
                var sb = new StringBuilder();

                for (var x = 0; x < _width; x++)
                {
                    for (var y = 0; y < _height; y++)
                    {
                        sb.Append(FindLight(new Position(x, y)).On ? "#" : ".");
                    }
                    sb.Append(Environment.NewLine);
                }

                return sb.ToString();
            }
            bool GetNextStateForLight(GridLight light)
            {
                var neighborsOn = light.Neighbors.Where(x => x.On).Count();

                if (light.On)
                    return (neighborsOn == 2 || neighborsOn == 3);
                else
                    return (neighborsOn == 3);
            }

            IEnumerable<GridLight> FindLights(IEnumerable<Position> positions)
            {
                foreach (var position in positions)
                {
                    var l = FindLight(position);
                    if (l != null)
                        yield return l;
                }
            }

            GridLight FindLight(Position position)
            {
                return _lights.FirstOrDefault(l => l.Position.X == position.X && l.Position.Y == position.Y);
            }
        }

        class GridLight : Light
        {
            readonly bool _alwaysOn;

            public Position Position { get; private set; }

            public List<GridLight> Neighbors { get; private set; }

            public bool? OnNextState { get; set; }

            public GridLight(int x, int y, bool on, bool alwaysOn)
                : base(on)
            {
                Position = new Position(x, y);
                Neighbors = new List<GridLight>();
                _alwaysOn = alwaysOn;
            }

            public void Animate()
            {
                if (_alwaysOn)
                {
                    this.On = true;
                    return;
                }

                if (OnNextState.HasValue)
                    this.On = OnNextState.Value;
            }
        }

        class Light
        {
            public bool On { get; protected set; }

            public bool Off { get { return !On; } }

            public Light(bool on)
            {
                On = on;
            }

            public void TurnOn()
            {
                On = true;
            }

            public void TurnOff()
            {
                On = false;
            }

            public void Toggle()
            {
                On = !On;
            }
        }

        class Position
        {
            public int X { get; private set; }
            public int Y { get; private set; }

            public Position(int x, int y)
            {
                X = x;
                Y = y;
            }

            public Dictionary<Directions, Position> GetNeighborPositions()
            {
                var d = new Dictionary<Directions, Position>();

                d.Add(Directions.N, new Position(X, Y + 1));
                d.Add(Directions.NE, new Position(X + 1, Y + 1));
                d.Add(Directions.E, new Position(X + 1, Y));
                d.Add(Directions.SE, new Position(X + 1, Y - 1));
                d.Add(Directions.S, new Position(X, Y - 1));
                d.Add(Directions.SW, new Position(X - 1, Y - 1));
                d.Add(Directions.W, new Position(X - 1, Y));
                d.Add(Directions.NW, new Position(X - 1, Y + 1));

                return d;
            }
        }

        enum Directions
        {
            N,
            NE,
            E,
            SE,
            S,
            SW,
            W,
            NW
        }
    }
}
