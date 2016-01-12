using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Day11
    {
        public string Solution(string password)
        {
            var generator = new PasswordGenerator();

            return generator.FindNextValidPassword(password);
        }

        class PasswordGenerator
        {
            readonly Counter _counter;
            readonly PasswordValidator _validator;

            public PasswordGenerator()
            {
                _counter = new Counter();
                _validator = new PasswordValidator();
            }

            public string FindNextValidPassword(string password)
            {
                var current = password;

                while (true)
                {
                    current = _counter.Increment(current);

                    if (_validator.Validate(current))
                        return current;
                }
            }
        }

        class PasswordValidator
        {
            public bool Validate(string password)
            {
                // 8 letters
                if (password.Length != 8)
                    return false;

                //Passwords must include one increasing straight of at least three letters, like abc, bcd, cde, and so on, up to xyz. They cannot skip letters; abd doesn't count.
                var sb = new StringBuilder();
                int seqStart = 3;
                int seqEnd = 5;
                RegexBuilder.BuildRegex(sb, seqStart, seqEnd);
                var r = sb.ToString();
                if (!Regex.IsMatch(password, r))
                    return false;

                //Passwords may not contain the letters i, o, or l, as these letters can be mistaken for other characters and are therefore confusing.
                var index = password.IndexOfAny(new char[] { 'i', 'o', 'l' });
                if (index > 0)
                    return false;

                //Passwords must contain at least two different, non-overlapping pairs of letters, like aa, bb, or zz.
                if (!HasConsecutiveChars(password, 2))
                    return false;

                return true;
            }

            bool HasConsecutiveChars(string source, int sequenceLength)
            {
                var charEnumerator = StringInfo.GetTextElementEnumerator(source);
                var currentElement = string.Empty;
                int count = 1;

                while (charEnumerator.MoveNext())
                {
                    if (currentElement == charEnumerator.GetTextElement())
                    {
                        if (++count >= sequenceLength)
                        {
                            // only proceed if the next character isn't the same
                            // it can only be a sequence of 2 in a row

                            var index = charEnumerator.ElementIndex;

                            if (charEnumerator.MoveNext() && currentElement != charEnumerator.GetTextElement())
                                return HasConsecutiveCharsExcludingLetter(source.Substring(index), sequenceLength, currentElement);
                        }
                    }
                    else
                    {
                        count = 1;
                        currentElement = charEnumerator.GetTextElement();
                    }
                }

                return false;
            }

            bool HasConsecutiveCharsExcludingLetter(string source, int sequenceLength, string excludedLetter)
            {
                var charEnumerator = StringInfo.GetTextElementEnumerator(source);
                var currentElement = string.Empty;
                int count = 1;

                while (charEnumerator.MoveNext())
                {
                    if (currentElement != excludedLetter && currentElement == charEnumerator.GetTextElement())
                    {
                        if (++count >= sequenceLength)
                        {
                            // only true if the next character isn't the same
                            // it can only be a sequence of 2 in a row

                            if (!charEnumerator.MoveNext() || currentElement != charEnumerator.GetTextElement())
                                return true;
                        }
                    }
                    else
                    {
                        count = 1;
                        currentElement = charEnumerator.GetTextElement();
                    }
                }

                return false;
            }

            static class RegexBuilder
            {
                public static void BuildRegex(StringBuilder sb, int seqStart, int seqEnd)
                {
                    for (int i = seqStart; i <= seqEnd; i++)
                    {
                        buildRegexCharGroup(sb, i, '0', '9');
                        buildRegexCharGroup(sb, i, 'A', 'Z');
                        buildRegexCharGroup(sb, i, 'a', 'z');
                        buildRegexRepeatedString(sb, i);
                    }
                }

                static void buildRegexCharGroup(StringBuilder sb, int seqLength, char start, char end)
                {
                    for (char c = start; c <= end - seqLength + 1; c++)
                    {
                        char ch = c;
                        if (sb.Length > 0)
                        {
                            sb.Append('|');
                        }
                        for (int i = 0; i < seqLength; i++)
                        {
                            sb.Append(ch++);
                        }
                    }
                }

                static void buildRegexRepeatedString(StringBuilder sb, int seqLength)
                {
                    sb.Append('|');
                    sb.Append("([a-zA-Z\\d])");
                    for (int i = 1; i < seqLength; i++)
                    {
                        sb.Append("\\1");
                    }
                }
            }
        }

        public class Counter
        {
            public string Increment(string s)
            {
                // first case - string is empty: return "a"

                if ((s == null) || (s.Length == 0))
                    return "a";

                // next case - last char is less than 'z': simply increment last char

                char lastChar = s[s.Length - 1];

                string fragment = s.Substring(0, s.Length - 1);

                if (lastChar < 'z')
                {

                    ++lastChar;

                    return fragment + lastChar;

                }

                // next case - last char is 'z': roll over and increment preceding string

                return Increment(fragment) + 'a';
            }
        }
    }
}
