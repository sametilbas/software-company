using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace suffa.Models
{
    public class homeındexview
    {
        public IEnumerable<about> abouts{ get; set; }
        public IEnumerable<blogpost> blogposts { get; set; }
        public IEnumerable<category> categories { get; set; }
        public IEnumerable<employe> employes { get; set; }
        public IEnumerable<services> services { get; set; }
        public IEnumerable<works> works { get; set; }
        public IEnumerable<project> project { get; set; }
        public IEnumerable<employe> employee { get; set; }
        public IEnumerable<user> user { get; set; }


        /*burası filtrelemede kulalnılması için*/
        public IEnumerable<blogpost> post { get; set; }
        public IEnumerable<project> pro { get; set; }
        }
}