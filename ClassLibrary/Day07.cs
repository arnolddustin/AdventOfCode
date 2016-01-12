using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Day07
    {
        public int SolutionPart1(string wire, params string[] input)
        {
            return new CircuitCalculator(input).GetSignalOnWire(wire);
        }

        public int SolutionPart2(string wireToOverride, int signalToOverrideWith, string wireToGetSignalFrom, params string[] input)
        {
           return new CircuitCalculator(input).OverrideSignalOnWireAndGetSignalFromWire(wireToOverride, signalToOverrideWith, wireToGetSignalFrom);
        }

        class CircuitCalculator
        {
            enum Operations
            {
                And,
                Or,
                Not,
                Lshift,
                Rshift,
                Insert
            }

            readonly List<Instruction> _instructions;

            public CircuitCalculator(string[] input)
            {
                _instructions = new List<Instruction>();

                foreach (var line in input)
                    _instructions.Add(Instruction.Parse(line));
            }

            public int GetSignalOnWire(string wire)
            {
                ResolveValues();

                var w = _instructions.FirstOrDefault(x => x.Destination.Equals(wire));

                return (int)w.Value;
            }

            public int OverrideSignalOnWireAndGetSignalFromWire(string wireToOverride, int signalToOverrideWith, string wireToGetSignalFrom)
            {
                _instructions.FirstOrDefault(x => x.Operation == Operations.Insert && x.Destination == wireToOverride).Operand1 = signalToOverrideWith.ToString();

                ResolveValues();

                var w = _instructions.FirstOrDefault(x => x.Destination == wireToGetSignalFrom);

                return (int)w.Value;
            }

            void ResolveValues()
            {
                int instructionsCount = _instructions.Count();
                int step = 0;
                while (_instructions.Any(i => !i.Processed))
                {
                    var instruction = _instructions[step % instructionsCount];

                    switch (instruction.Operation)
                    {
                        case Operations.And:
                        case Operations.Or:
                        case Operations.Lshift:
                        case Operations.Rshift:
                            TwoOp(instruction, instruction.Operation);
                            break;

                        case Operations.Not:
                            Not(instruction);
                            break;

                        case Operations.Insert:
                            Insert(instruction);
                            break;

                        default:
                            throw new Exception("lol");
                    }

                    step++;
                }
            }

            void Not(Instruction instruction)
            {
                ushort operand1;

                if (ushort.TryParse(instruction.Operand1, out operand1))
                {
                    ushort result = (ushort)(~operand1);
                    UpdateKey(instruction.Destination, result);
                    instruction.Value = result;
                    instruction.Processed = true;
                }
            }

            void Insert(Instruction instruction)
            {
                ushort operand1;

                if (ushort.TryParse(instruction.Operand1, out operand1))
                {
                    UpdateKey(instruction.Destination, operand1);
                    instruction.Value = operand1;
                    instruction.Processed = true;
                }
            }

            void TwoOp(Instruction instruction, Operations operation)
            {
                ushort operand1;
                ushort operand2;
                ushort result = 0;

                if (ushort.TryParse(instruction.Operand1, out operand1) && ushort.TryParse(instruction.Operand2, out operand2))
                {

                    switch (operation)
                    {
                        case Operations.Lshift:
                            result = (ushort)(operand1 << operand2);
                            break;

                        case Operations.Rshift:
                            result = (ushort)(operand1 >> operand2);
                            break;

                        case Operations.And:
                            result = (ushort)(operand1 & operand2);
                            break;

                        case Operations.Or:
                            result = (ushort)(operand1 | operand2);
                            break;

                    }

                    UpdateKey(instruction.Destination, (ushort)result);
                    instruction.Value = result;
                    instruction.Processed = true;
                }
            }

            void UpdateKey(string destination, ushort value)
            {
                foreach (var i in _instructions.Where(x => x.Operand1 != null && x.Operand1.Equals(destination)))
                {
                    i.Operand1 = value.ToString();
                }

                foreach (var i in _instructions.Where(x => x.Operand2 != null && x.Operand2.Equals(destination)))
                {
                    i.Operand2 = value.ToString();
                }
            }

            class Instruction
            {
                public bool Processed { set; get; }
                public Operations Operation { set; get; }
                public string Operand1 { set; get; }
                public string Operand2 { set; get; }
                public string Destination { set; get; }
                public ushort Value { set; get; }

                public Instruction() { }

                public Instruction(Operations operation, string operand1, string destination)
                {
                    Operation = operation;
                    Operand1 = operand1;
                    Destination = destination;
                }

                public Instruction(Operations operation, string operand1, string operand2, string destination)
                {
                    Operation = operation;
                    Operand1 = operand1;
                    Operand2 = operand2;
                    Destination = destination;
                }

                public static Instruction Parse(string line)
                {
                    var parts = line.Split(' ');
                    if (line.Contains("AND"))
                    {
                        return new Instruction(Operations.And, parts[0], parts[2], parts[4]);
                    }
                    else if (line.Contains("OR"))
                    {
                        return new Instruction(Operations.Or, parts[0], parts[2], parts[4]);
                    }
                    else if (line.Contains("LSHIFT"))
                    {
                        return new Instruction(Operations.Lshift, parts[0], parts[2], parts[4]);
                    }
                    else if (line.Contains("RSHIFT"))
                    {
                        return new Instruction(Operations.Rshift, parts[0], parts[2], parts[4]);
                    }
                    else if (line.Contains("NOT"))
                    {
                        return new Instruction(Operations.Not, parts[1], parts[3]);
                    }
                    else
                    {
                        return new Instruction(Operations.Insert, parts[0], parts[2]);
                    }
                }

                Instruction ParseTwoOpInstruction(string[] instruction, Operations operation)
                {
                    return new Instruction()
                    {
                        Operand1 = instruction[0],
                        Operand2 = instruction[2],
                        Destination = instruction[4],
                        Operation = operation
                    };
                }

            }
        }
    }
}
