using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace suffa.Models
{
    public class user
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int userId { get; set; }
        [Required]
        [StringLength(50)]
        public string userName{ get; set; }
        [Required]
        [StringLength(50)]
        public string userSurname { get; set; }
        [Required]
        public long userPhone { get; set; }
        [Required]
        public string userEmail { get; set; }
        [Required]
        public string userRole { get; set; }
    }
}