using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace suffa.Models
{
    [Table("Blogpost")]
    public class blogpost
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int postid { get; set; }
        [Required]
        public string postTitle { get; set; }
        [Required]
        public string postDescription { get; set; }
        [Required]
        public string postImagePath { get; set; }
        [Required]
        public Nullable<int> categoryId { get; set; }
        public virtual category category { get; set; }
        public int postLike { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime postDate { get; set; }
    }
}