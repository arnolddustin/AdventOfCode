using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Day14  
    {
        public int SolutionPart1(int raceDuration, params string[] input)
        {
            var racers = CreateRacersFromInput(input);

            var race = new Race(raceDuration, racers);

            return race.GetWinningDistance();
        }
        public int SolutionPart2(int raceDuration, params string[] input)
        {
            var racers = CreateRacersFromInput(input);

            var race = new Race(raceDuration, racers);

            return race.GetWinningScore();
        }

        IEnumerable<Reindeer> CreateRacersFromInput(string[] input)
        {
            foreach (var line in input)
                yield return new Reindeer(line);
        }

        class Race
        {
            readonly int _secondsToRun;
            readonly List<Reindeer> _racers;

            public Race(int secondsToRun, IEnumerable<Reindeer> racers)
            {
                _secondsToRun = secondsToRun;
                _racers = new List<Reindeer>(racers);
            }

            public int GetWinningScore()
            {
                for (int i = 1; i < _secondsToRun; i++)
                {
                    _racers.ForEach(x => x.Run(i));

                    var winningDistance = _racers.Max(x => x.Distance);
                    var winners = _racers.Where(x => x.Distance == winningDistance);

                    winners.ToList().ForEach(x => x.IncrementScore());
                }

                return _racers.Max(x => x.Score);
            }

            public int GetWinningDistance()
            {
                _racers.ForEach(r => r.Run(_secondsToRun));

                return _racers.OrderByDescending(x => x.Distance).First().Distance;
            }
        }

        class Reindeer
        {
            public int Score { get; private set; }
            public string Name { get; private set; }
            public int Velocity { get; private set; }
            public int RunDuration { get; private set; }
            public int RestDuration { get; private set; }
            public int Distance { get; private set; }

            public Reindeer(string input)
            {
                var words = input.Split(' ');

                Name = words[0];
                Velocity = int.Parse(words[3]);
                RunDuration = int.Parse(words[6]);
                RestDuration = int.Parse(words[13]);

                Score = 0;
            }

            public void IncrementScore()
            {
                Score += 1;
            }

            public void Run(int seconds)
            {
                Distance = 0;

                var timeleft = seconds;
                while (timeleft > 0)
                {
                    if (RunDuration <= timeleft)
                    {
                        Distance += Velocity * RunDuration;
                        timeleft -= RunDuration;

                        if (RestDuration <= timeleft)
                        {
                            timeleft -= RestDuration;
                        }
                        else
                        {
                            timeleft = 0;
                        }
                    }
                    else
                    {
                        Distance += Velocity * timeleft;
                        timeleft = 0;
                    }
                }
            }
        }
    }
}
