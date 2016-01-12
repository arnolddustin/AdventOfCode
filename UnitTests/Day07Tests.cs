using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibrary;
using System.Collections.Generic;
using System.Linq;

namespace UnitTests
{
    [TestClass]
    public class Day07Tests
    {
        [TestMethod]
        public void Day07_Part1_Examples()
        {
            var input = new string[] { "123 -> x", "456 -> y", "x AND y -> d", "x OR y -> e", "x LSHIFT 2 -> f", "y RSHIFT 2 -> g", "NOT x -> h", "NOT y -> i" };

            var d = new Day07();

            var list = new Dictionary<string, int>();

            list.Add("d", 72);
            list.Add("e", 507);
            list.Add("f", 492);
            list.Add("g", 114);
            list.Add("h", 65412);
            list.Add("i", 65079);
            list.Add("x", 123);
            list.Add("y", 456);

            foreach (var pair in list)
                Assert.AreEqual(pair.Value, d.SolutionPart1(pair.Key, input));
        }

        [TestMethod]
        public void Day07_Part1_Solution()
        {
            var input = FileHelper.ReadTestFile(7);

            var d = new Day07();

            var expected = 16076;
            var actual = d.SolutionPart1("a", input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Day07_Part2_Solution()
        {
            var d = new Day07();

            var input = FileHelper.ReadTestFile(7);
            Assert.IsNotNull(input);

            var expected = 2797;
            var actual = d.SolutionPart2("b", 16076, "a", input);

            Assert.AreEqual(expected, actual);
        }
    }
}
