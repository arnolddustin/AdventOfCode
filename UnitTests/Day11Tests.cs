using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibrary;
using System.IO;

namespace UnitTests
{
    [TestClass]
    public class Day11Tests
    {
        [TestMethod]
        public void Day11_Part1_Examples_Sequences()
        {
            var c = new Day11.Counter();

            Assert.AreEqual("xy", c.Increment("xx"));
            Assert.AreEqual("xz", c.Increment("xy"));
            Assert.AreEqual("ya", c.Increment("xz"));
            Assert.AreEqual("yb", c.Increment("ya"));
        }

        [TestMethod]
        public void Day11_Part1_Examples_Passwords()
        {
            var d = new Day11();

            Assert.AreEqual("abcdffaa", d.Solution("abcdefgh"));
            Assert.AreEqual("ghjaabcc", d.Solution("ghijklmn"));
        }

        [TestMethod]
        public void Day11_Part1_Solution()
        {
            var d = new Day11();

            var input = "hxbxwxba";

            var expected = "hxbxxyzz";
            var actual = d.Solution(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Day11_Part2_Solution()
        {
            var d = new Day11();

            var input = "hxbxxyzz";

            var expected = "hxcaabcc";
            var actual = d.Solution(input);

            Assert.AreEqual(expected, actual);
        }
    }
}
