using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Day12
    {
        public long SolutionPart1(string json)
        {
            var total = 0;

            foreach (var m in Regex.Matches(json, "[-]?[0-9]+"))
                total += int.Parse(m.ToString());

            return total;
        }

        public long SolutionPart2(string json)
        {
            dynamic o = JsonConvert.DeserializeObject(json);
            return GetSum(o, "red");
        }

        long GetSum(JObject o, string avoid = null)
        {
            bool shouldAvoid = o.Properties()
                .Select(a => a.Value).OfType<JValue>()
                .Select(v => v.Value).Contains(avoid);
            if (shouldAvoid) return 0;

            return o.Properties().Sum((dynamic a) => (long)GetSum(a.Value, avoid));
        }

        long GetSum(JArray arr, string avoid)
        {
            return arr.Sum((dynamic a) => (long)GetSum(a, avoid));
        }

        long GetSum(JValue val, string avoid) 
        {
           return val.Type == JTokenType.Integer ? (long)val.Value : 0;   
        }
    }
}