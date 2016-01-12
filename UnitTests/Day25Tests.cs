using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibrary;

namespace UnitTests
{
    [TestClass]
    public class Day25Tests
    {
        [TestMethod]
        public void Day25_Part1_Examples_CountSteps()
        {
            var d = new Day25();

            var expected = 12;
            var actual = d.CountStepsToPosition(3, 3);

            Assert.AreEqual(expected, actual);

            expected = 18;
            actual = d.CountStepsToPosition(3, 4);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Day25_Part1_Examples_Codes()
        {
            var d = new Day25();

            var expected = 31916031;
            var actual = d.GenerateNextCode(20151125);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Day25_Part1_Examples_Combined()
        {
            var d = new Day25();

            var expected = 21629792;
            var actual = d.GetCodeAtPosition(20151125, 2, 2);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Day25_Part1_Solution()
        {
            var d = new Day25();

            var expected = 2650453;
            var actual = d.GetCodeAtPosition(20151125, 2978, 3083);

            Assert.AreEqual(expected, actual);
        }
    }
}
