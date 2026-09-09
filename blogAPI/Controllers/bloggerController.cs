using blogAPI.Models;
using blogAPI.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySqlConnector;
using System.Xml.Linq;

namespace blogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class bloggerController : ControllerBase
    {
        private readonly string ConnectionString = "Server=localhost;Database=blog;uid=root;Password=";

        [HttpGet]
        public List<blogger> GetAllBlogger()
        {
            List<blogger> bloggers = new();
            var connector = new MySqlConnection(ConnectionString);

            connector.Open();

            string sql = "SELECT * FROM blogger";

            var cmd = new MySqlCommand(sql, connector);
            var dataReader = cmd.ExecuteReader();

            while (dataReader.Read())
            {
                var blogger = new blogger
                {
                    Id = dataReader.GetInt32(0),
                    Name = dataReader.GetString(1),
                    Email = dataReader.GetString(2),
                    Age = dataReader.GetInt32(3),
                    Password = dataReader.GetString(4),
                    RegistrationTime = dataReader.GetDateTime(5)
                };
                bloggers.Add(blogger);
            }
            connector.Close();
            return bloggers;
        }
            [HttpPost]
            public blogger AddNewBlogger(AddBloggerDTO blogger)
            {
                var connector = new MySqlConnection(ConnectionString);

                connector.Open();

                var blg = new blogger
                {
                    Name = blogger.Name,
                    Email = blogger.Email,
                    Age = blogger.Age,
                    Password = blogger.Password,
                    RegistrationTime = DateTime.Now
                };

                var sql = $"INSERT INTO `blogger`(`Name`, `Email`, `Age`, `Password`, `RegistrationTime`) VALUES(@name, @email, @age, @password, @registrationTime)";

                var cmd = new MySqlCommand(sql, connector);
                cmd.Parameters.AddWithValue("@name", blg.Name);
                cmd.Parameters.AddWithValue("@email", blg.Email);
                cmd.Parameters.AddWithValue("@age", blg.Age);
                cmd.Parameters.AddWithValue("@password", blg.Password);
                cmd.Parameters.AddWithValue("@registrationTime", blg.RegistrationTime);

                cmd.ExecuteNonQuery();
                connector.Close();
                return blg;
            }

            [HttpPut]
            public object UpdateBlogger(int id, blogger blogger)
            {
                return null;
            }

            [HttpDelete]
            public object DeleteBlogger(int id)
            {
                return null;
            }
        }
    }
