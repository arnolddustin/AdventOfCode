using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibrary;
using System.IO;

namespace UnitTests
{
    [TestClass]
    public class Day06Tests
    {
        [TestMethod]
        public void Day06_Part1_Examples()
        {
            var d = new Day06();

            Assert.AreEqual(1000000, d.SolutionPart1("turn on 0,0 through 999,999"));
            Assert.AreEqual(1000, d.SolutionPart1("toggle 0,0 through 999,0"));
            Assert.AreEqual(999996, d.SolutionPart1("turn on 0,0 through 999,999", "turn off 499,499 through 500,500"));
        }

        [TestMethod]
        public void Day06_Part1_Solution()
        {
            var d = new Day06();

            var input = FileHelper.ReadTestFile(6);
            Assert.IsNotNull(input);

            var expected = 400410;
            var actual = d.SolutionPart1(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Day06_Part2_Solution()
        {
            var d = new Day06();

            var input = FileHelper.ReadTestFile(6);
            Assert.IsNotNull(input);

            var expected = 15343601;
            var actual = d.SolutionPart2(input);

            Assert.AreEqual(expected, actual);
        }
    }
}
