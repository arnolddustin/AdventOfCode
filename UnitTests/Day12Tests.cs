using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibrary;
using System.IO;

namespace UnitTests
{
    [TestClass]
    public class Day12Tests
    {
        [TestMethod]
        public void Day12_Part1_Examples()
        {
            var d = new Day12();

            Assert.AreEqual(6, d.SolutionPart1("[1,2,3]"));
            Assert.AreEqual(6, d.SolutionPart1("\"a\":2,\"b\":4}"));
            Assert.AreEqual(3, d.SolutionPart1("[[[3]]]"));
            Assert.AreEqual(3, d.SolutionPart1("{\"a\":{\"b\":4},\"c\":-1}"));
            Assert.AreEqual(0, d.SolutionPart1("{\"a\":[-1,1]}"));
            Assert.AreEqual(0, d.SolutionPart1("[-1,{\"a\":1}]"));
        }

        [TestMethod]
        public void Day12_Part1_Solution()
        {
            var d = new Day12();

            var input = FileHelper.ReadTestFile(12)[0];
            Assert.IsNotNull(input);

            var expected = 191164;
            Assert.AreEqual(expected, d.SolutionPart1(input));
        }

        [TestMethod]
        public void Day12_Part2_Examples()
        {
            var d = new Day12();

            Assert.AreEqual(6, d.SolutionPart2("[1,2,3]"));
            Assert.AreEqual(4, d.SolutionPart2("[1,{\"c\":\"red\",\"b\":2},3]"));
            Assert.AreEqual(0, d.SolutionPart2("{\"d\":\"red\",\"e\":[1,2,3,4],\"f\":5}"));
            Assert.AreEqual(6, d.SolutionPart2("[1,\"red\",5]"));
        }

        [TestMethod]
        public void Day12_Part2_Solution()
        {
            var d = new Day12();

            var input = FileHelper.ReadTestFile(12)[0];
            Assert.IsNotNull(input);

            var expected = 87842;
            var actual = d.SolutionPart2(input);

            Assert.AreEqual(expected, actual);
        }
    }
}
