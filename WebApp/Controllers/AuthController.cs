using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataModel;
using DataModel.Authentication;
using DataModel.Data;
using DataModel.Users;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;



// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApp.Controllers
{
    [Route("[controller]")]
    public class AuthController : Controller
    {
        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }



        // POST User Authorization
        [HttpPost]
        public TokenData Authorization([FromBody]AuthData data)
        {
           return AuthManager.AuthUser(data);
        }

        [HttpPost("registration")]
        public TokenData Registration([FromBody] RegistrationData data)
        {
            
            return AuthManager.RegisterUser(data,new RootEnum[]{RootEnum.None});
            
        }

        [HttpPost("logout")]
        public void Registration([FromBody] TokenData token)
        {
            AuthManager.LogOutUser(token);
        }

       


        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
