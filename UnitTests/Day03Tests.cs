using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibrary;
using System.IO;

namespace UnitTests
{
    [TestClass]
    public class Day03Tests
    {
        [TestMethod]
        public void Day03_Part1_Examples()
        {
            var d = new Day03();

            Assert.AreEqual(2, d.SolutionPart1(">"));
            Assert.AreEqual(4, d.SolutionPart1("^>v<"));
            Assert.AreEqual(2, d.SolutionPart1("^v^v^v^v^v"));
        }

        [TestMethod]
        public void Day03_Part1_Solution()
        {
            var d = new Day03();

            var input = FileHelper.ReadTestFile(3);
            Assert.IsNotNull(input);

            var expected = 2572;
            var actual = d.SolutionPart1(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Day03_Part2_Examples()
        {
            var d = new Day03();

            Assert.AreEqual(3, d.SolutionPart2("^v"));
            Assert.AreEqual(11, d.SolutionPart2("^v^v^v^v^v"));
        }

        [TestMethod]
        public void Day03_Part2_Solution()
        {
            var d = new Day03();

            var input = FileHelper.ReadTestFile(3);
            Assert.IsNotNull(input);

            var expected = 2631;
            var actual = d.SolutionPart2(input);

            Assert.AreEqual(expected, actual);
        }
    }
}
