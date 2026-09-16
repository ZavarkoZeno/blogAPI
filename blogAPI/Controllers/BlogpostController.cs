using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using blogAPI.Models;
using MySqlConnector;
using blogAPI.Models.DTOs;

namespace blogAPI.Controllers
{
    [Route("post")]
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

        [HttpPost]
        public blogpost AddNewBlogpost(AddPostDTO addPostDTO)
        {
            var connector = new MySqlConnection(ConnectionString);
            connector.Open();
            var post = new blogpost
            {
                Title = addPostDTO.Title,
                Content = addPostDTO.Content,
                postTime = DateTime.Now,
                updateTime = DateTime.Now,
                BlogId = addPostDTO.BlogId
            };
            string sql = $"INSERT INTO `blogpost`(`Title`, `Content`, `postTime`, `updateTime`, `blogId`) VALUES (@Title, @Content, @postTime, @updateTime, @BlogId)";

            var cmd = new MySqlCommand(sql, connector);

            cmd.Parameters.AddWithValue("@Title", post.Title);
            cmd.Parameters.AddWithValue("@Content", post.Content);
            cmd.Parameters.AddWithValue("@postTime", post.postTime);
            cmd.Parameters.AddWithValue("@updateTime", post.updateTime);
            cmd.Parameters.AddWithValue("@BlogId", post.BlogId);

            cmd.ExecuteNonQuery();
            connector.Close();
            return post;
        }

        [HttpPut]
        public blogpost UpdateBlogpost(AddPostDTO addPostDTO)
        {
            var connector = new MySqlConnection(ConnectionString);
            connector.Open();
            var post = new blogpost
            {
                Title = addPostDTO.Title,
                Content = addPostDTO.Content,
                postTime = DateTime.Now,
                updateTime = DateTime.Now,
                BlogId = addPostDTO.BlogId
            };
            string sql = $"UPDATE `blogpost` SET `Title`=@Title,`Content`=@Content,`postTime`=@postTime,`updateTime`=@updateTime, WHERE `Id`=@Id";

            var cmd = new MySqlCommand(sql, connector);

            cmd.Parameters.AddWithValue("@Title", post.Title);
            cmd.Parameters.AddWithValue("@Content", post.Content);
            cmd.Parameters.AddWithValue("@postTime", post.postTime);
            cmd.Parameters.AddWithValue("@updateTime", post.updateTime);
            cmd.Parameters.AddWithValue("@BlogId", post.BlogId);

            cmd.ExecuteNonQuery();
            connector.Close();
            return post;
        }

        [HttpDelete]
        public void DeleteBlogpost(int id)
        {
            var connector = new MySqlConnection(ConnectionString);
            connector.Open();
            string sql = "DELETE FROM `blogpost` WHERE `Id`=@Id";
            var cmd = new MySqlCommand(sql, connector);
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.ExecuteNonQuery();
            connector.Close();
        }
    }
}
