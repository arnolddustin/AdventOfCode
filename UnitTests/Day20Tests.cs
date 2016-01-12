using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibrary;
using System.IO;

namespace UnitTests
{
    [TestClass]
    public class Day20Tests
    {
        [TestMethod]
        public void Day20_Part1_Solution()
        {
            var d = new Day20();

            var result = d.SolutionPart1(33100000, 10);

            Assert.AreEqual(776160, result);
        }

        [TestMethod]
        public void Day20_Part2_Solution()
        {
            var d = new Day20();

            var expected = 786240;
            var actual = d.SolutionPart2(33100000, 11, 50);

            Assert.AreEqual(expected, actual);
        }
    }
}
