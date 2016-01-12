using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Day06  
    {
        public int SolutionPart1(params string[] input)
        {
            var grid = new Grid(1000, 1000, false);

            foreach (var line in input)
            {
                var instruction = new Instruction(line);
                grid.ProcessInstruction(instruction);
            }

            return grid.GetLightsOnCount();
        }

        public int SolutionPart2(params string[] input)
        {
            var grid = new DimmableGrid(1000, 1000);

            foreach (var line in input)
            {
                var instruction = new Instruction(line);
                grid.ProcessInstruction(instruction);
            }

            return grid.GetTotalBrightness();
        }

        class DimmableGrid
        {
            readonly IList<DimmableLight> _lights;

            public DimmableGrid(int rows, int columns)
            {
                _lights = new List<DimmableLight>();

                for (int x = 0; x < rows; x++)
                    for (int y = 0; y < columns; y++)
                        _lights.Add(new DimmableLight(x, y));
            }

            public void ProcessInstruction(Instruction instruction)
            {
                var q = _lights.Where(l => l.X >= instruction.StartX && l.X <= instruction.EndX && l.Y >= instruction.StartY && l.Y <= instruction.EndY);

                switch (instruction.Action)
                {
                    case Actions.Toggle:
                        q.ToList().ForEach(l => l.Toggle());
                        break;

                    case Actions.On:
                        q.ToList().ForEach(l => l.TurnOn());
                        break;

                    case Actions.Off:
                        q.ToList().ForEach(l => l.TurnOff());
                        break;
                }
            }

            public int GetTotalBrightness()
            {
                return _lights.Sum(x => x.Brightness);
            }
        }

        class Grid
        {
            readonly IList<Light> _lights;

            public Grid(int rows, int columns, bool lightsOn)
            {
                _lights = new List<Light>();

                for (int x = 0; x < rows; x++)
                    for (int y = 0; y < columns; y++)
                        _lights.Add(new Light(x, y, lightsOn));
            }

            public void ProcessInstruction(Instruction instruction)
            {
                var q = _lights.Where(l => l.X >= instruction.StartX && l.X <= instruction.EndX && l.Y >= instruction.StartY && l.Y <= instruction.EndY);

                switch (instruction.Action)
                {
                    case Actions.Toggle:
                        q.ToList().ForEach(l => l.Toggle());
                        break;

                    case Actions.On:
                        q.ToList().ForEach(l => l.TurnOn());
                        break;

                    case Actions.Off:
                        q.ToList().ForEach(l => l.TurnOff());
                        break;
                }
            }

            public int GetLightsOnCount()
            {
                return _lights.Count(x => x.On);
            }
        }

        class Instruction
        {
            public Actions Action { get; private set; }
            public int StartX { get; private set; }
            public int StartY { get; private set; }
            public int EndX { get; private set; }
            public int EndY { get; private set; }

            public Instruction(string s)
            {
                var chunks = s.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                string start = "";
                string end = "";

                if (s.Contains("toggle"))
                {
                    Action = Actions.Toggle;
                    start = chunks[1];
                    end = chunks[3];
                }
                else
                {
                    Action = (chunks[1] == "on") ? Actions.On : Actions.Off;
                    start = chunks[2];
                    end = chunks[4];
                }

                var startCoords = start.Split(',');
                var endCoords = end.Split(',');

                StartX = int.Parse(startCoords[0]);
                StartY = int.Parse(startCoords[1]);
                EndX = int.Parse(endCoords[0]);
                EndY = int.Parse(endCoords[1]);
            }
        }

        enum Actions
        {
            On,
            Off,
            Toggle
        }

        class Light
        {
            public int X { get; private set; }
            public int Y { get; private set; }
            public bool On { get; private set; }

            public Light(int x, int y, bool isOn)
            {
                X = x;
                Y = y;
                On = isOn;
            }

            public virtual void Toggle()
            {
                On = !On;
            }

            public virtual void TurnOn()
            {
                On = true;
            }

            public virtual void TurnOff()
            {
                On = false;
            }
        }

        class DimmableLight : Light
        {
            public int Brightness { get; private set; }

            public DimmableLight(int x, int y) : base(x, y, false) 
            { 
                Brightness = 0;
            }

            public override void TurnOn()
            {
                Brightness++;
            }

            public override void TurnOff()
            {
                if (Brightness > 0)
                    Brightness--;
            }

            public override void Toggle()
            {
                Brightness += 2;
            }
        }
    }
}
