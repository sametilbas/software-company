using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace suffa.Models
{
    public class partner
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int partnerId { get; set; }
        public Nullable<int> userId { get; set; }
        public virtual user user { get; set; }
        public string idea { get; set; }
    }
}