using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Day01  
    {
        public int SolutionPart1(params string[] input)
        {
            return CalculateFinalFloor(input[0]);
        }

        public int SolutionPart2(params string[] input)
        {
            return CalculateBasementEntryPosition(input[0]);
        }

        int CalculateFinalFloor(string input)
        {
            var ups = input.Count(x => x == '(');
            var downs = input.Count(x => x == ')');

            return ups - downs;
        }

        int CalculateBasementEntryPosition(string input)
        {
            var currentFloor = 0;

            for (var i = 0; i < input.Length; i++)
            {
                if (input[i] == '(')
                    currentFloor++;

                if (input[i] == ')')
                    currentFloor--;

                if (currentFloor == -1)
                    return i + 1;
            }

            throw new Exception("Never reached basement");
        }
    }
}
