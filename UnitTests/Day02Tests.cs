using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibrary;
using System.IO;

namespace UnitTests
{
    [TestClass]
    public class Day02Tests
    {
        [TestMethod]
        public void Day02_Part1_Examples()
        {
            var d = new Day02();

            Assert.AreEqual(58, d.SolutionPart1("2x3x4"));
            Assert.AreEqual(43, d.SolutionPart1("1x1x10"));
        }

        [TestMethod]
        public void Day02_Part1_Solution()
        {
            var d = new Day02();

            var input = FileHelper.ReadTestFile(2);
            Assert.IsNotNull(input);

            var expected = 1588178;
            var actual = d.SolutionPart1(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Day02_Part2_Examples()
        {
            var d = new Day02();

            Assert.AreEqual(34, d.SolutionPart2("2x3x4"));
            Assert.AreEqual(14, d.SolutionPart2("1x1x10"));
        }

        [TestMethod]
        public void Day02_Part2_Solution()
        {
            var d = new Day02();

            var input = FileHelper.ReadTestFile(2);
            Assert.IsNotNull(input);

            var expected = 3783758;
            var actual = d.SolutionPart2(input);

            Assert.AreEqual(expected, actual);
        }
    }
}
