using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Day08
    {
        public int SolutionPart1(params string[] input)
        {
            var totalLiterals = input.Sum(x => x.Length);
            var totalInMemory = new StringEscaper().GetInMemoryCharacters(input).Sum(x => x.Length);

            return totalLiterals - totalInMemory;
        }

        public int SolutionPart2(params string[] input)
        {
            var totalLiterals = input.Sum(x => x.Length);
            var totalEncoded = new StringEscaper().GetEncodedCharacters(input).Sum(x => x.Length);

            return totalEncoded - totalLiterals;
        }

        class StringEscaper
        {
            const string REPLACEMENT = "|";
            const string BACKSLASHES = "\\\\";
            const string QUOTE = "\\\"";

            public IEnumerable<string> GetEncodedCharacters(params string[] lines)
            {
                foreach (var line in lines)
                    yield return EncodeString(line);
            }

            public IEnumerable<string> GetInMemoryCharacters(params string[] lines)
            {
                foreach (var line in lines)
                {
                    string s = line;

                    s = ReplaceDoubleBackslashWithChar(s);
                    s = ReplaceBackslashQuoteWithChar(s);
                    s = ReplaceHexWithChar(s);
                    s = s.Trim('\"');

                    yield return s;
                }
            }

            string ReplaceHexWithChar(string line)
            {
                if (line.Contains("\\x"))
                {
                    var startIndex = line.IndexOf("\\x");
                    var endIndex = startIndex + 4;

                    var text = line.Substring(startIndex + 2, 2);
                    if (!Regex.IsMatch(text, @"[0-9a-fA-F]"))
                        return line;

                    var part1 = line.Substring(0, startIndex);
                    var part2 = line.Substring(endIndex, line.Length - endIndex);

                    var newstring = string.Format("{0}{1}{2}", part1, REPLACEMENT, part2);

                    return ReplaceHexWithChar(newstring);
                }

                return line;
            }

            string ReplaceDoubleBackslashWithChar(string line)
            {
                if (line.Contains(BACKSLASHES))
                {
                    var newstring = line.Replace(BACKSLASHES, REPLACEMENT);

                    return ReplaceDoubleBackslashWithChar(newstring);
                }

                return line;
            }

            string ReplaceBackslashQuoteWithChar(string line)
            {
                if (line.Contains(QUOTE))
                {
                    var newstring = line.Replace(QUOTE, REPLACEMENT);

                    return ReplaceBackslashQuoteWithChar(newstring);
                }

                return line;
            }

            string EncodeString(string input)
            {
                using (var writer = new StringWriter())
                {
                    using (var provider = CodeDomProvider.CreateProvider("CSharp"))
                    {
                        provider.GenerateCodeFromExpression(new CodePrimitiveExpression(input), writer, null);
                        return writer.ToString();
                    }
                }
            }
        }
    }
}
