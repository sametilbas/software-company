using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace suffa.Models
{
    public class processview
    {
        public IEnumerable<process> process { get; set; }
        public IEnumerable<user> user { get; set; }
        public IEnumerable<employe> employe { get; set; }
    }
}