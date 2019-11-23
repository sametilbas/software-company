using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace suffa.Models
{
    [Table("category")]
    public class category
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int categoryId { get; set; }
        public string categories { get; set; }
    }
}