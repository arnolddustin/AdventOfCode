using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibrary;
using System.IO;

namespace UnitTests
{
    [TestClass]
    public class Day08Tests
    {
        [TestMethod]
        public void Day08_Part1_Examples()
        {
            var d = new Day08();

            Assert.AreEqual(2, d.SolutionPart1("\"\""));
            Assert.AreEqual(2, d.SolutionPart1("\"abc\""));
            Assert.AreEqual(3, d.SolutionPart1("\"aaa\\\"aaa\""));
            Assert.AreEqual(5, d.SolutionPart1("\"\\x27\""));
        }

        [TestMethod]
        public void Day08_Part1_Solution()
        {
            var d = new Day08();

            var input = FileHelper.ReadTestFile(8);
            Assert.IsNotNull(input);

            var expected = 1371;
            var actual = d.SolutionPart1(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Day08_Part2_Examples()
        {
            var d = new Day08();

            Assert.AreEqual(4, d.SolutionPart2("\"\""));
            Assert.AreEqual(4, d.SolutionPart2("\"abc\""));
            Assert.AreEqual(6, d.SolutionPart2("\"aaa\\\"aaa\""));
            Assert.AreEqual(5, d.SolutionPart2("\"\\x27\""));
        }

        [TestMethod]
        public void Day08_Part2_Solution()
        {
            var d = new Day08();

            var input = FileHelper.ReadTestFile(8);
            Assert.IsNotNull(input);

            var expected = 2117;
            var actual = d.SolutionPart2(input);

            Assert.AreEqual(expected, actual);
        }
    }
}
