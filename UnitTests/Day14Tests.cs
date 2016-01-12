using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibrary;
using System.IO;

namespace UnitTests
{
    [TestClass]
    public class Day14Tests
    {
        [TestMethod]
        public void Day14_Part1_Examples()
        {
            var d = new Day14();

            var result = d.SolutionPart1(1000,
                "Comet can fly 14 km/s for 10 seconds, but then must rest for 127 seconds.",
                "Dancer can fly 16 km/s for 11 seconds, but then must rest for 162 seconds.");

            Assert.AreEqual(1120, result);
        }

        [TestMethod]
        public void Day14_Part1_Solution()
        {
            var d = new Day14();

            var input = FileHelper.ReadTestFile(14);
            Assert.IsNotNull(input);

            var expected = 2696;
            var actual = d.SolutionPart1(2503, input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Day14_Part2_Examples()
        {
            var d = new Day14();

            var result = d.SolutionPart2(1000,
                "Comet can fly 14 km/s for 10 seconds, but then must rest for 127 seconds.",
                "Dancer can fly 16 km/s for 11 seconds, but then must rest for 162 seconds.");

            Assert.AreEqual(689, result);
        }

        [TestMethod]
        public void Day14_Part2_Solution()
        {
            var d = new Day14();

            var input = FileHelper.ReadTestFile(14);
            Assert.IsNotNull(input);

            var expected = 1084;
            var actual = d.SolutionPart2(2503, input);

            Assert.AreEqual(expected, actual);
        }
    }
}
