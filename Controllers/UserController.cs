using Microsoft.AspNetCore.Mvc;
using DotNet_SSE.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Cors;

namespace DotNet_SSE.Controllers
{
    [EnableCors("AllowOne")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController:Controller
    {
        public readonly List<User> _userList = new List<User>()
        {
            new User(){UserId="1",Name="Tom",Gender="Male"},
            new User(){UserId="2",Name="Joe",Gender="Female"},
            new User(){UserId="3",Name="Mike",Gender="Male"}
        };

        // GET api/user/5?callback=
        [HttpGet("{id}")]
        [EnableCors("AllowOne")]
        public IActionResult Get(string id, string callback)
        {
            var userInfo = _userList.Find(x=>x.UserId == id);
            if(userInfo == null) return NotFound();
            if(string.IsNullOrEmpty(callback))
            {
                //返回JSON
                Response.ContentType = "application/json";
                return Json(userInfo);
                
            }
            else 
            {
                //返回JSPNP
                Response.ContentType = "application/javascript";
                return Content($"{callback}({JsonConvert.SerializeObject(userInfo)})");
            }
        }
    }
}