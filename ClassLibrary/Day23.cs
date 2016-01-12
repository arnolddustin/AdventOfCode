using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Day23
    {
        public class Computer
        {
            public long RegisterA { get; private set; }
            public long RegisterB { get; private set; }
            public int CompletedInstructions { get; private set; }

            List<Instruction> _instructions;

            public Computer(int registerAstart)
            {
                RegisterA = registerAstart;
                RegisterB = 0;
                CompletedInstructions = 0;
            }

            public void ProcessInstructions(IEnumerable<Instruction> instructions)
            {
                _instructions = new List<Instruction>(instructions);

                var current = instructions.First();

                while (current != null)
                    current = ProcessInstruction(current);
            }

            Instruction ProcessInstruction(Instruction i)
            {
                if (i == null) return null;

                var currentIndex = _instructions.IndexOf(i);
                var nextIndex = 0;

                switch (i.Operation)
                {
                    case Operations.inc:
                        if (i.Register == "a")
                            RegisterA++;
                        else if (i.Register == "b")
                            RegisterB++;
                        else
                            throw new ArgumentOutOfRangeException("Register", i.Register, "Invalid register");
                        nextIndex = currentIndex + 1;
                        break;

                    case Operations.hlf:
                        if (i.Register == "a")
                            RegisterA = RegisterA / 2;
                        else if (i.Register == "b")
                            RegisterB = RegisterB / 2;
                        else
                            throw new ArgumentOutOfRangeException("Register", i.Register, "Invalid register");
                        nextIndex = currentIndex + 1;
                        break;

                    case Operations.tpl:
                        if (i.Register == "a")
                            RegisterA = RegisterA *3;
                        else if (i.Register == "b")
                            RegisterB = RegisterB * 3;
                        else
                            throw new ArgumentOutOfRangeException("Register", i.Register, "Invalid register");
                        nextIndex = currentIndex + 1;
                        break;

                    case Operations.jmp:
                        nextIndex = currentIndex + i.Offset.Value;
                        break;

                    case Operations.jio:
                        if (i.Register == "a" && RegisterA == 1)
                            nextIndex = currentIndex + i.Offset.Value;
                        else if (i.Register == "b" && RegisterB == 1)
                            nextIndex = currentIndex + i.Offset.Value;
                        else
                            nextIndex = currentIndex + 1;
                        break;

                    case Operations.jie:
                        if (i.Register == "a" && RegisterA % 2 == 0)
                            nextIndex = currentIndex + i.Offset.Value;
                        else if (i.Register == "b" && RegisterB % 2 == 0)
                            nextIndex = currentIndex + i.Offset.Value;
                        else
                            nextIndex = currentIndex + 1;
                        break;
                }

                return _instructions.ElementAtOrDefault(nextIndex);
            }
        }

        public enum Operations
        {
            hlf,
            tpl,
            inc,
            jmp,
            jie,
            jio
        }

        public class Instruction
        {
            public Operations Operation { get; private set; }
            public string Register { get; private set; }
            public int? Offset { get; private set; }

            public Instruction(string line)
            {
                var parts = line.Split(' ');

                Operation = (Operations)Enum.Parse(typeof(Operations), parts[0]);

                switch (Operation)
                {
                    case Operations.jmp:
                        Offset = int.Parse(parts[1]);
                        break;

                    case Operations.jio:
                    case Operations.jie:
                        Offset = int.Parse(parts[2]);
                        Register = parts[1].TrimEnd(',');
                        break;

                    default:
                        Register = parts[1];
                        break;
                }
            }

            public override string ToString()
            {
                return string.Format("{0} {1} {2}", Operation, Register, Offset);
            }
        }
    }
}
