using blogAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace blogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class bloggerController : ControllerBase
    {
        [HttpGet]
        public List<blogger> GetAllBlogger()
        {
            return null;
        }

        [HttpPost]
        public object AddNewBlogger(blogger blogger)
        {
            return null;
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
