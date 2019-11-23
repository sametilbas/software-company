using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace suffa.Models
{
    public class process
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int processId { get; set; }
        public Nullable<int> employeId { get; set; }
        public virtual employe employe { get; set; }
        public string procesName { get; set; }
        public int id { get; set; }
        public string processType { get; set; }
        public DateTime processTime { get; set; }
    }
}