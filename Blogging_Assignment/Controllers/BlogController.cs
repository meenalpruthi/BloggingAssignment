using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace Blogging_Assignment.Controllers
{
    public class BlogController : ApiController
    {

        [AllowAnonymous]
        [HttpGet]
        [Route("api/data/forall")]
        public IHttpActionResult Get()
        {
            return Ok("Now server is:" + DateTime.Now.ToString());
        }

        [Authorize]
        [HttpGet]
        [Route("api/data/authenticate")]
        public IHttpActionResult GetForAuthenticate()
        {
            var identity = (ClaimsIdentity)User.Identity;
            return Ok("Hello" + identity.Name);
        }

        [Authorize(Roles="admin")]
        [HttpGet]
        [Route("api/data/authorize")]
        public IHttpActionResult GetForAdmin()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var roles = identity.Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value);
            return Ok("Hello" + identity.Name + "Role:" + string.Join(",", roles.ToList()));
        }


        
        BlogDBContext blogcontext = new BlogDBContext();
        [HttpGet]
        [Authorize(Roles = "admin")]
        public IHttpActionResult GetAllBlogs()
        {
            try
            {
                return Ok(blogcontext.Posts);
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        [HttpPost]
        [Authorize(Roles ="admin")]
        public HttpResponseMessage PostBlog([FromBody] Post blog)
        {
            try
            {
                List<Post> postBlog = new List<Post>();
                using(blogcontext)
                {
                    // postBlog.Add(new Post {User_Name = "meenal", Title = "myblog", Content = "Horror", Views = 140, ImageUrl = "abc.jpg", Published = true,
                    //Created_At =  DateTime.UtcNow , Updated_At = DateTime.UtcNow,
                    //    Number_Of_Comments = 2
                    //   });
                    blogcontext.Posts.Add(blog);
                    blogcontext.SaveChanges();
                }

                

                return Request.CreateResponse(HttpStatusCode.Created, postBlog);
            }
            catch (Exception ex) {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [Authorize(Roles = "admin")]
        public HttpResponseMessage DeleteBlog(int id)
        {
            if (id <= 0)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "invalid id");
            else
            {
                try
                {
                    Post post = blogcontext.Posts.Where(p => p.Id == id).FirstOrDefault();
                    blogcontext.Posts.Remove(post);
                    blogcontext.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, id);
                }
                catch(Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                }

            }
        }

        [HttpPut]
        [Authorize(Roles = "admin")]
        public HttpResponseMessage UpdateBlog([FromBody] Post blog)
        {
            try
            {
                List<Post> postBlog = new List<Post>();
                using (blogcontext)
                {                  
                    var existingblog = blogcontext.Posts.Where(x => x.User_Name == blog.User_Name).FirstOrDefault();
                    existingblog.ImageUrl = blog.ImageUrl;
                    existingblog.Number_Of_Comments = blog.Number_Of_Comments;
                    existingblog.Published = blog.Published;
                    existingblog.Title = blog.Title;
                    existingblog.Views = blog.Views;
                    existingblog.Content = blog.Content;
                    blogcontext.SaveChanges();
                }



                return Request.CreateResponse(HttpStatusCode.Created, postBlog);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
