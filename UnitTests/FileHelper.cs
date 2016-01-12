using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    static class FileHelper
    {
        const string FOLDER = "C:\\Projects\\AdventOfCode\\TestFiles";

        public static string[] ReadTestFile(int day)
        {
            var path = string.Format("{0}\\day{1}.txt", FOLDER, day);

            return File.ReadAllLines(path);
        }
    }
}
