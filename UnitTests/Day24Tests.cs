using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibrary;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;

namespace UnitTests
{
    [TestClass]
    public class Day24Tests
    {
        [TestMethod]
        public void Day24_Part1_Examples()
        {
            var input = new string[] { "1", "2", "3", "4", "5", "7", "8", "9", "10", "11"};
            var d = new Day24();

            var expected = 99;
            var actual = d.GetFrontSeatQE(3, input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Day24_Part1_Solution()
        {
            var input = FileHelper.ReadTestFile(24);
            Assert.IsNotNull(input);

            var d = new Day24();

            var expected = 10439961859;
            var actual = d.GetFrontSeatQE(3, input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Day24_Part2_Examples()
        {
            var input = new string[] { "1", "2", "3", "4", "5", "7", "8", "9", "10", "11" };
            var d = new Day24();

            var expected = 44;
            var actual = d.GetFrontSeatQE(4, input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Day24_Part2_Solution()
        {
            var input = FileHelper.ReadTestFile(24);
            Assert.IsNotNull(input);

            var d = new Day24();

            var expected = 72050269;
            var actual = d.GetFrontSeatQE(4, input);

            Assert.AreEqual(expected, actual);
        }
    }
}
