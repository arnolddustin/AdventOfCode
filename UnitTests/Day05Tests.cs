using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibrary;
using System.IO;

namespace UnitTests
{
    [TestClass]
    public class Day05Tests
    {
        [TestMethod]
        public void Day05_Part1_Examples()
        {
            var d = new Day05();

            Assert.AreEqual(1, d.SolutionPart1("ugknbfddgicrmopn"));
            Assert.AreEqual(1, d.SolutionPart1("aaa"));
            Assert.AreEqual(0, d.SolutionPart1("jchzalrnumimnmhp"));
            Assert.AreEqual(0, d.SolutionPart1("haegwjzuvuyypxyu"));
            Assert.AreEqual(0, d.SolutionPart1("dvszwmarrgswjxmb"));
        }

        [TestMethod]
        public void Day05_Part1_Solution()
        {
            var d = new Day05();

            var input = FileHelper.ReadTestFile(5);
            Assert.IsNotNull(input);

            var expected = 236;
            var actual = d.SolutionPart1(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Day05_Part2_Examples()
        {
            var d = new Day05();

            Assert.AreEqual(1, d.SolutionPart2("qjhvhtzxzqqjkmpb"));
            Assert.AreEqual(1, d.SolutionPart2("xxyxx"));
            Assert.AreEqual(0, d.SolutionPart2("uurcxstgmygtbstg"));
            Assert.AreEqual(0, d.SolutionPart2("ieodomkazucvgmuy"));
        }

        [TestMethod]
        public void Day05_Part2_Solution()
        {
            var d = new Day05();

            var input = FileHelper.ReadTestFile(5);
            Assert.IsNotNull(input);

            var expected = 51;
            var actual = d.SolutionPart2(input);

            Assert.AreEqual(expected, actual);
        }
    }
}
