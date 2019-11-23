using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace suffa.Models
{
    public class project
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int projectId { get; set; }
        public string projectName { get; set; }
        public string projectDescrption { get; set; }
        public string projectImagePath { get; set; }
    }
}