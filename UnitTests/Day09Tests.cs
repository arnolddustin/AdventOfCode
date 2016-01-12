using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibrary;
using System.IO;

namespace UnitTests
{
    [TestClass]
    public class Day09Tests
    {
        [TestMethod]
        public void Day09_Part1_Examples()
        {
            var d = new Day09();

            var expected = 605;
            var actual = d.SolutionPart1("London to Dublin = 464", "London to Belfast = 518", "Dublin to Belfast = 141");

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Day09_Part1_Solution()
        {
            var d = new Day09();

            var input = FileHelper.ReadTestFile(9);

            var expected = 141;
            var actual = d.SolutionPart1(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Day09_Part2_Examples()
        {
            var d = new Day09();

            var expected = 982;
            var actual = d.SolutionPart2("London to Dublin = 464", "London to Belfast = 518", "Dublin to Belfast = 141");

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Day09_Part2_Solution()
        {
            var d = new Day09();

            var input = FileHelper.ReadTestFile(9);

            var expected = 736;
            var actual = d.SolutionPart2(input);

            Assert.AreEqual(expected, actual);
        }
    }
}
