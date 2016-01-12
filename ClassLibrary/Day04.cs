using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Day04 
    {
        public int Solution(string input, string hashShouldStartWith)
        {
            int answer = 0;

            while (true)
            {
                using (var md5 = MD5.Create())
                {
                    var retVal = md5.ComputeHash(Encoding.ASCII.GetBytes(string.Format("{0}{1}", input, answer)));
                    var sb = new StringBuilder();
                    for (int i = 0; i < retVal.Length; i++)
                        sb.Append(retVal[i].ToString("x2"));

                    if (sb.ToString().StartsWith(hashShouldStartWith))
                        return answer;
                    else
                        answer++;
                }
            }
        }
    }
}
