using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibrary;
using System.IO;

namespace UnitTests
{
    [TestClass]
    public class Day04Tests
    {
        [TestMethod]
        public void Day04_Part1_Examples()
        {
            var d = new Day04();

            Assert.AreEqual(609043, d.Solution("abcdef", "00000"));
            Assert.AreEqual(1048970, d.Solution("pqrstuv", "00000"));
        }

        [TestMethod]
        public void Day04_Part1_Solution()
        {
            var d = new Day04();

            var input = FileHelper.ReadTestFile(4);
            Assert.IsNotNull(input);

            var expected = 346386;
            var actual = d.Solution(input[0], "00000");

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Day04_Part2_Solution()
        {
            var d = new Day04();

            var input = FileHelper.ReadTestFile(4);
            Assert.IsNotNull(input);

            var expected = 9958218;
            var actual = d.Solution(input[0], "000000");

            Assert.AreEqual(expected, actual);
        }
    }
}
