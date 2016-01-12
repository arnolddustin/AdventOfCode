using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibrary;
using System.IO;

namespace UnitTests
{
    [TestClass]
    public class Day16Tests
    {
        [TestMethod]
        public void Day16_Part1_Solution()
        {
            var d = new Day16();

            var input = FileHelper.ReadTestFile(16);
            Assert.IsNotNull(input);

            var expected = 103;
            var actual = d.SolutionPart1(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Day16_Part2_Solution()
        {
            var d = new Day16();

            var input = FileHelper.ReadTestFile(16);
            Assert.IsNotNull(input);

            var expected = 405;
            var actual = d.SolutionPart2(input);

            Assert.AreEqual(expected, actual);
        }
    }
}
