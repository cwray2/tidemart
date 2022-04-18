using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using api.Models;
using api.Interfaces;
using api.Database;
using Microsoft.AspNetCore.Cors;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        //The main Data handler for user. Change to a different class file if different db.
        IAuthDataHandler dataHandler = new AuthDatahandler();  

        // GET: api/User
        [EnableCors("BasicPolicy")]
        [HttpPost("register")]
        public Register RegisterAuth([FromBody] User newUser)
        {
            return dataHandler.RegisterUser(newUser);
        }

        // GET: api/User/5
        [EnableCors("BasicPolicy")]
        [HttpPost("login")]
        public Login LoginAuth([FromBody] User newUser)
        {
            dataHandler.RemoveExpiredTokens();

            return dataHandler.LoginUser(newUser);
        }

        // POST: api/User
        [EnableCors("BasicPolicy")]
        [HttpPost("logout")]
        public Login LogoutAuth([FromBody] AuthToken token)
        {
            return dataHandler.LogoutUser(token);
        }

        // PUT: api/User/5
        [EnableCors("BasicPolicy")]
        [HttpPost("validate-post")]
        public bool ValidateToken([FromBody] AuthToken token)
        {
            return dataHandler.IsTokenValid(token);
        }

        // DELETE: api/User/5
        // [EnableCors("BasicPolicy")]
        // [HttpDelete("{id}")]
        // public void Delete(int id)
        // {
        //     dataHandler.Delete(id);
        // }
    }
}
