using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Blogging_Assignment
{
    public class BlogDBContext : DbContext
    {
        public BlogDBContext() : base("MyBloggingAssignmentDb")
        {

        }
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Comments> Comments { get; set; }
    }
}