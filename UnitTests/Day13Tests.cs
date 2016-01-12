using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibrary;
using System.IO;

namespace UnitTests
{
    [TestClass]
    public class Day13Tests
    {
        [TestMethod]
        public void Day13_Part1_Examples()
        {
            var d = new Day13();

            var expected = 330;
            var actual = d.SolutionPart1(
                "Alice would gain 54 happiness units by sitting next to Bob.",
                "Alice would lose 79 happiness units by sitting next to Carol.",
                "Alice would lose 2 happiness units by sitting next to David.",
                "Bob would gain 83 happiness units by sitting next to Alice.",
                "Bob would lose 7 happiness units by sitting next to Carol.",
                "Bob would lose 63 happiness units by sitting next to David.",
                "Carol would lose 62 happiness units by sitting next to Alice.",
                "Carol would gain 60 happiness units by sitting next to Bob.",
                "Carol would gain 55 happiness units by sitting next to David.",
                "David would gain 46 happiness units by sitting next to Alice.",
                "David would lose 7 happiness units by sitting next to Bob.",
                "David would gain 41 happiness units by sitting next to Carol.");

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Day13_Part1_Solution()
        {
            var d = new Day13();

            var input = FileHelper.ReadTestFile(13);
            Assert.IsNotNull(input);

            var expected = 733;
            var actual = d.SolutionPart1(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Day13_Part2_Solution()
        {
            var d = new Day13();

            var input = FileHelper.ReadTestFile(13);
            Assert.IsNotNull(input);

            var expected = 725;
            var actual = d.SolutionPart2(input);

            Assert.AreEqual(expected, actual);
        }
    }
}
