using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibrary;
using System.Collections.Generic;
using System.Linq;

namespace UnitTests
{
    [TestClass]
    public class Day22Tests
    {
        [TestMethod]
        public void Day22_Part1_Examples()
        {
            var p1 = new Day22.Player("Player", 10, 250);
            var p2 = new Day22.Boss("Boss", 13, 8);

            var game = new Day22.Game(p1, p2, false);

            var spells = new List<Day22.Spell>();
            spells.Add(Day22.Spell.GetByName("Poison"));
            spells.Add(Day22.Spell.GetByName("Magic Missle"));

            var expected = Day22.Players.Player;
            game.Simulate(spells);
            var actual = game.Winner;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Day22_Part1_Examples2()
        {
            var p1 = new Day22.Player("Player", 10, 250);
            var p2 = new Day22.Boss("Boss", 14, 8);

            var game = new Day22.Game(p1, p2, false);

            var spells = new List<Day22.Spell>();
            spells.Add(Day22.Spell.GetByName("Recharge"));
            spells.Add(Day22.Spell.GetByName("Shield"));
            spells.Add(Day22.Spell.GetByName("Drain"));
            spells.Add(Day22.Spell.GetByName("Poison"));
            spells.Add(Day22.Spell.GetByName("Magic Missle"));

            var expected = Day22.Players.Player;
            game.Simulate(spells);
            var actual = game.Winner;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Day22_Part1_Examples3()
        {
            var p1 = new Day22.Player("Player", 10, 250);
            var p2 = new Day22.Boss("Boss", 14, 8);

            var game = new Day22.Game(p1, p2, false);

            var spells = new List<Day22.Spell>();
            spells.Add(Day22.Spell.GetByName("Recharge"));
            spells.Add(Day22.Spell.GetByName("Shield"));
            spells.Add(Day22.Spell.GetByName("Drain"));
            spells.Add(Day22.Spell.GetByName("Poison"));
            spells.Add(Day22.Spell.GetByName("Magic Missle"));

            foreach (var spell in spells)
            {
                Assert.IsFalse(game.Winner.HasValue);

                game.TakeTurn(spell);
            }

            var expected = Day22.Players.Player;
            var actual = game.Winner;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Day22_Part1_Solution()
        {
            var sim = new Day22.Simulator("Player", 50, 500, "Boss", 51, 9, false);

            var expected = 900;
            var actual = sim.FindCheapestPlayerWin();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Day22_Part2_Solution()
        {
            var sim = new Day22.Simulator("Player", 50, 500, "Boss", 51, 9, true);

            var expected = 1216;
            var actual = sim.FindCheapestPlayerWin();

            Assert.AreEqual(expected, actual);
        }
    }
}
