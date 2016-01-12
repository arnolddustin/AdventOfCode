using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace ClassLibrary
{
    public class Day25
    {
        public long GenerateNextCode(long previousCode)
        {
            long newCode = previousCode * 252533 % 33554393;

            return newCode;
        }

        public long GetCodeAtPosition(int startingnumber, int row, int column)
        {
            var steps = CountStepsToPosition(row, column);

            long previousnumber = startingnumber;
            long nextnumber = 0;
            for (int i = 0; i < steps; i++)
            {
                nextnumber = GenerateNextCode(previousnumber);
                previousnumber = nextnumber;
            }

            return nextnumber;
        }

        public long CountStepsToPosition(int row, int column)
        {
            long target = 0;
            for (int i = 1; i <= column; i++)
                target = target + i;

            var extra = 0;
            for (int i = 1; i < row; i++)
            {
                target = target + column + extra;
                extra = extra + 1;
            }

            return target - 1;
        }
    }
}
