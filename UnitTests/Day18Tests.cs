using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibrary;
using System.IO;

namespace UnitTests
{
    [TestClass]
    public class Day18Tests
    {
        [TestMethod]
        public void Day18_Part1_Examples()
        {
            var d = new Day18();

            var initialState = new string[] { ".#.#.#", 
                                              "...##.", 
                                              "#....#", 
                                              "..#...", 
                                              "#.#..#", 
                                              "####.." };

            var expected = 4;
            var actual = d.SolutionPart1(4, initialState);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Day18_Part1_Solution()
        {
            var d = new Day18();

            var input = FileHelper.ReadTestFile(18);
            Assert.IsNotNull(input);

            var expected = 1061;
            var actual = d.SolutionPart1(100, input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Day18_Part2_Examples()
        {
            var d = new Day18();

            var initialState = new string[] { "##.#.#", 
                                              "...##.", 
                                              "#....#", 
                                              "..#...", 
                                              "#.#..#", 
                                              "####.#" };

            var expected = 17;
            var actual = d.SolutionPart2(5, initialState);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Day18_Part2_Solution()
        {
            var d = new Day18();

            var input = FileHelper.ReadTestFile(18);
            Assert.IsNotNull(input);

            var expected = 1006;
            var actual = d.SolutionPart2(100, input);

            Assert.AreEqual(expected, actual);
        }
    }
}
