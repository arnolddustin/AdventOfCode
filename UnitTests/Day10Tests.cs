using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibrary;
using System.IO;

namespace UnitTests
{
    [TestClass]
    public class Day10Tests
    {
        [TestMethod]
        public void Day10_Part1_Examples()
        {
            var d = new Day10();

            Assert.AreEqual("11", d.GetSolution("1", 1));
            Assert.AreEqual("21", d.GetSolution("11", 1));
            Assert.AreEqual("1211", d.GetSolution("21", 1));
            Assert.AreEqual("111221", d.GetSolution("1211", 1));
            Assert.AreEqual("312211", d.GetSolution("111221", 1));

            Assert.AreEqual("312211", d.GetSolution("1", 5));
        }

        [TestMethod]
        public void Day10_Part1_Solution()
        {
            var d = new Day10();

            var expected = 492982;
            var actual = d.GetSolutionLength("1321131112", 40);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Day10_Part2_Solution()
        {
            var d = new Day10();

            var expected = 6989950;
            var actual = d.GetSolutionLength("1321131112", 50);

            Assert.AreEqual(expected, actual);
        }
    }
}
