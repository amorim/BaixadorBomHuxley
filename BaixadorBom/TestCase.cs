using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaixadorBom
{
    public class TestCase
    {
        public int id { get; set; }
        public string input { get; set; }
        public string output { get; set; }
        public static string deserializeInput(string input)
        {
            return input.Replace("`", "<br />");
        }
    }
}