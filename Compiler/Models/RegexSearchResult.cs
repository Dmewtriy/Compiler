using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Models
{
    public class RegexSearchResult
    {
        public string Fragment { get; set; }
        public string Position { get; set; }

        public int AbsoluteIndex { get; set; }
        public int Length { get; set; }
    }
}
