using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Blogging_Assignment
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string User_Name { get; set; }
        public string  Title { get; set; }
        public string  Content { get; set; }
        public int Views { get; set; }
        public string ImageUrl { get; set; }
        public Boolean Published { get; set; }
        public DateTime Created_At { get; set; }
        public DateTime Updated_At { get; set; }
        public User User { get; set; }
        public int Number_Of_Comments { get; set; }
        public List<Comments> Comments { get; set; }
    }
}