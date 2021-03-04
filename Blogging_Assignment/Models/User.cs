using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Blogging_Assignment
{ 
    public enum EnumRole
    {
        Admin,
        Author
    }
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string User_Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public EnumRole Role { get; set; }
        [Required]
        public string Password { get; set; }
        public List<Post> Posts { get; set; }
    }
}