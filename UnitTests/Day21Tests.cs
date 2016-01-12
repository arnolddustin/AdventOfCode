using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibrary;
using System.Linq;

namespace UnitTests
{
    [TestClass]
    public class Day21Tests
    {
        [TestMethod]
        public void Day21_Part1_Example()
        {
            //For example, suppose you have 8 hit points, 5 damage, and 5 armor, and that 
            //the boss has 12 hit points, 7 damage, and 2 armor:
            //In this scenario, the player wins! (Barely.)

            var p1 = new Day21.EquippedPlayer("Player", 8, 5, 5, Enumerable.Empty<Day21.Equipment>());
            var p2 = new Day21.Player("Boss", 12, 7, 2);

            var b = new Day21.Battle(p1, p2);

            var winner = b.Fight();

            Assert.IsNotNull(winner);
            Assert.AreEqual("Player", winner.Name);
        }

        [TestMethod]
        public void Day21_Part1_Solution()
        {
            var d = new Day21.Simulator();

            var b = new Day21.Player("Boss", 104, 8, 1);
            var actual = d.LeastAmountOfGoldRequiredToWinFight(100, b);
            var expected = 78;

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Day21_Part2_Solution()
        {
            var d = new Day21.Simulator();

            var b = new Day21.Player("Boss", 104, 8, 1);
            var actual = d.MostAmountRequiredToLoseFight(100, b);
            var expected = 148;

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected, actual);
        }
    }
}
