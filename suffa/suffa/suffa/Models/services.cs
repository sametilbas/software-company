using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace suffa.Models
{
    public class services
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int servicesId { get; set; }
        public string servicesName { get; set; }
        public string servicesDesc { get; set; }
    }
}