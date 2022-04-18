using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using api.Models;
using api.Interfaces;
using api.Database;

namespace api.Controllers
{   
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        IUserDataHandler handlerOfData = new UserDataHandler();

        // GET: api/Users
        [EnableCors("OpenPolicy")]
        [HttpGet]
        public List<User> Get()
        {
            return handlerOfData.Select();
        }

        // GET: api/Users/5
        [EnableCors("OpenPolicy")]
        [HttpGet("{id}", Name = "GetUser")]
        public List<User> Get(int id)
        {
            return handlerOfData.SelectById(id);
        }

        // POST: api/Users
        [EnableCors("OpenPolicy")]
        [HttpPost]
        public void Post([FromBody] User user)
        {
            handlerOfData.Insert(user);
        }

        // PUT: api/Users/5
        [EnableCors("OpenPolicy")]
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/Users/5
        [EnableCors("OpenPolicy")]
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            handlerOfData.Delete(id);
        }
    }
}
