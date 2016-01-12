using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibrary;
using System.IO;

namespace UnitTests
{
    [TestClass]
    public class Day01Tests
    {
        const string DAY = "1";

        [TestMethod]
        public void Day01_Part1_Examples()
        {
            var d = new Day01();

            Assert.AreEqual(0, d.SolutionPart1("(())"));
            Assert.AreEqual(0, d.SolutionPart1("()()"));
            Assert.AreEqual(3, d.SolutionPart1("((("));
            Assert.AreEqual(3, d.SolutionPart1("(()(()("));
            Assert.AreEqual(3, d.SolutionPart1("))((((("));
            Assert.AreEqual(-1, d.SolutionPart1("())"));
            Assert.AreEqual(-1, d.SolutionPart1("))("));
            Assert.AreEqual(-3, d.SolutionPart1(")))"));
            Assert.AreEqual(-3, d.SolutionPart1(")())())"));
        }

        [TestMethod]
        public void Day01_Part1_Solution()
        {
            var d = new Day01();

            var input = FileHelper.ReadTestFile(1);
            Assert.IsNotNull(input);

            var expected = 280;
            var actual = d.SolutionPart1(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Day01_Part2_Examples()
        {
            var d = new Day01();

            Assert.AreEqual(1, d.SolutionPart2(")"));
            Assert.AreEqual(5, d.SolutionPart2("()())"));
        }

        [TestMethod]
        public void Day01_Part2_Solution()
        {
            var d = new Day01();

            var input = FileHelper.ReadTestFile(1);
            Assert.IsNotNull(input);

            var expected = 1797;
            var actual = d.SolutionPart2(input);

            Assert.AreEqual(expected, actual);
        }
    }
}
