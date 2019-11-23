using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace suffa.Models
{
    public class empview
    {
        public IEnumerable<employe> epm { get; set; }
        public IEnumerable<user> usr { get; set; }
        public string userName { get; set; }
        public string userSurname { get; set; }
        public long userPhone { get; set; }
        public string userEmail { get; set; }
        public string userRole { get; set; }
        public string employePassword { get; set; }
        public string employeImage { get; set; }
    }
}