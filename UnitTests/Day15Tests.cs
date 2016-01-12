using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibrary;
using System.IO;

namespace UnitTests
{
    [TestClass]
    public class Day15Tests
    {
        [TestMethod]
        public void Day15_Part1_Examples()
        {
            var d = new Day15();

            var expected = 62842880;
            var actual = d.SolutionPart1(
                "Butterscotch: capacity -1, durability -2, flavor 6, texture 3, calories 8",
                "Cinnamon: capacity 2, durability 3, flavor -2, texture -1, calories 3");

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Day15_Part1_Solution()
        {
            var d = new Day15();

            var input = FileHelper.ReadTestFile(15);
            Assert.IsNotNull(input);

            var expected = 222870;
            var actual = d.SolutionPart1(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Day15_Part2_Examples()
        {
            var d = new Day15();

            var expected = 57600000;
            var actual = d.SolutionPart2(
                "Butterscotch: capacity -1, durability -2, flavor 6, texture 3, calories 8",
                "Cinnamon: capacity 2, durability 3, flavor -2, texture -1, calories 3");

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Day15_Part2_Solution()
        {
            var d = new Day15();

            var input = FileHelper.ReadTestFile(15);
            Assert.IsNotNull(input);

            var expected = 117936;
            var actual = d.SolutionPart2(input);

            Assert.AreEqual(expected, actual);
        }
    }
}
