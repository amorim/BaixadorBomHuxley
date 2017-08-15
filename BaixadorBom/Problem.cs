using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaixadorBom
{
    public class Problem
    {
        public int id { get; set; }
        public string eval { get; set; }
        
        public static Problem GetProblem(int id, string eval)
        {
            return new Problem()
            {
                id = id, eval = eval
            };
        }
    }
}