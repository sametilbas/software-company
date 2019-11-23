using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace suffa.Models
{
    public class about
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int aboutId { get; set; }
        public string abouts { get; set; }
        public bool aboutType { get; set; }
               
    }
}