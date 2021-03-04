using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Blogging_Assignment
{
    public class Comments
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string Content { get; set; }
        public Post post { get; set; }
    }
}