using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibrary;
using System.Collections.Generic;
using System.Linq;

namespace UnitTests
{
    [TestClass]
    public class Day23Tests
    {
        [TestMethod]
        public void Day23_Part1_Examples()
        {
            var input = new string[] { "inc a", "jio a, +2", "tpl a", "inc a" };

            var instructions = new List<Day23.Instruction>();

            foreach (var line in input)
                instructions.Add(new Day23.Instruction(line));

            var computer = new Day23.Computer(0);

            computer.ProcessInstructions(instructions);

            var expected = 2;
            var actual = computer.RegisterA;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Day23_Part1_Solution()
        {
            var input = FileHelper.ReadTestFile(23);
            Assert.IsNotNull(input);

            var instructions = new List<Day23.Instruction>();

            foreach (var line in input)
                instructions.Add(new Day23.Instruction(line));

            var computer = new Day23.Computer(0);

            computer.ProcessInstructions(instructions);

            var expected = 170;
            var actual = computer.RegisterB;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Day23_Part2_Solution()
        {
            var input = FileHelper.ReadTestFile(23);
            Assert.IsNotNull(input);

            var instructions = new List<Day23.Instruction>();

            foreach (var line in input)
                instructions.Add(new Day23.Instruction(line));

            var computer = new Day23.Computer(1);

            computer.ProcessInstructions(instructions);

            var expected = 247;
            var actual = computer.RegisterB;

            Assert.AreEqual(expected, actual);
        }
    }
}
