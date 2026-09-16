using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using blogAPI.Models;
using MySqlConnector;

namespace blogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogpostController : ControllerBase
    {
        private readonly string ConnectionString = "Server=localhost;Database=blog;uid=root;Password=";

        [HttpGet]

        public List<blogpost> GetAllBlogposts()
        {
            List<blogpost> blogposts = new();
            var connector = new MySqlConnection(ConnectionString);
            connector.Open();
            string sql = "SELECT * FROM blogpost";
            var cmd = new MySqlCommand(sql, connector);
            var dataReader = cmd.ExecuteReader();
            while (dataReader.Read())
            {
                var blogpost = new blogpost
                {
                    Id = dataReader.GetInt32(0),
                    Title = dataReader.GetString(1),
                    Content = dataReader.GetString(2),
                    postTime = dataReader.GetDateTime(3),
                    updateTime = dataReader.GetDateTime(4),
                    BlogId = dataReader.GetInt32(5)
                };
                blogposts.Add(blogpost);
            }
            connector.Close();
            return blogposts;
        }
    }
}
