using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace suffa.Models
{
    public class blogviewmodel
    {
        public IEnumerable<blogpost> blog { get; set; }
        public string[] categorylist { get; set; }
        public IEnumerable<category> category { get; set; }
    }
}