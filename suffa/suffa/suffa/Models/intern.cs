using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace suffa.Models
{
    public class intern
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int internId { get; set; }
        public Nullable<int> userId { get; set; }
        public virtual user user { get; set; }
        public bool internType { get; set; }
    }
}