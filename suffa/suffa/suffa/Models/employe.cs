using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace suffa.Models
{
    public class employe
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int employeId { get; set; }
        public Nullable<int> userId { get; set; }
        public virtual user  user { get; set; }
        public string employePassword { get; set; }
        public string employeImage { get; set; }
    }
}