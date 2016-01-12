using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibrary;
using System.IO;

namespace UnitTests
{
    [TestClass]
    public class Day17Tests
    {
        [TestMethod]
        public void Day17_Part1_Examples()
        {
            var d = new Day17();

            var expected = 4;
            var actual = d.SolutionPart1(25, "20", "15", "10", "5", "5");

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Day17_Part1_Solution()
        {
            var d = new Day17();

            var input = FileHelper.ReadTestFile(17);
            Assert.IsNotNull(input);

            var expected = 1304;
            var actual = d.SolutionPart1(150, input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Day17_Part2_Solution()
        {
            var d = new Day17();

            var input = FileHelper.ReadTestFile(17);
            Assert.IsNotNull(input);

            var expected = 18;
            var actual = d.SolutionPart2(150, input);

            Assert.AreEqual(expected, actual);
        }
    }
}
