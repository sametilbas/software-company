using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace suffa.Models
{
    [Table("Company")]
    public class works
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int worksId { get; set; }
        [Required]
        public string worksName { get; set; }
        public string worksImage { get; set; }

    }
}