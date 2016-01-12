using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibrary;
using System.IO;

namespace UnitTests
{
    [TestClass]
    public class Day19Tests
    {
        [TestMethod]
        public void Day19_Part1_Examples_1()
        {
            var d = new Day19();

            var input = new string[] { "H => HO", "H => OH", "O => HH", "HOH" };
            var result = d.SolutionPart1(input);
            Assert.AreEqual(4, result);
        }

        [TestMethod]
        public void Day19_Part1_Examples_2()
        {
            var d = new Day19();

            var input = new string[] { "H => HO", "H => OH", "O => HH", "HOHOHO" };
            var result = d.SolutionPart1(input);
            Assert.AreEqual(7, result);
        }

        [TestMethod]
        public void Day19_Part1_Solution()
        {
            var d = new Day19();

            var input = FileHelper.ReadTestFile(19);
            Assert.IsNotNull(input);

            var expected = 518;
            var actual = d.SolutionPart1(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Day19_Part2_Solution()
        {
            var d = new Day19();

            var input = FileHelper.ReadTestFile(19);
            Assert.IsNotNull(input);

            var expected = 200;
            var actual = d.SolutionPart2(input);

            Assert.AreEqual(expected, actual);
        }
    }
}
