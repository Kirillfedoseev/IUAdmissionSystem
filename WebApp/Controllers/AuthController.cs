using System.Collections.Generic;
using System.Security.AccessControl;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Model.Authentication;
using Model.Data;
using Model.Users;



// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApp.Controllers
{
    [Route("[controller]")]
    public class AuthController : Controller
    {


      

        // GET: api/<controller>
        [EnableCors]
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [EnableCors]
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }



        // POST User Authorization
        [EnableCors]
        [HttpPost]
        public TokenData Authorization([FromBody]AuthData data)
        {
           return AuthManager.AuthUser(data);
        }

        [EnableCors]
        [HttpPost("registration")]
        public TokenData Registration([FromBody] RegistrationData data)
        {
            
            return AuthManager.RegisterUser(data,new RootEnum[]{RootEnum.None});
        }

        

       
        [EnableCors]
        [HttpPost("logout")]
        public void Registration([FromBody] TokenData token)
        {
            AuthManager.LogOutUser(token);
        }




        // PUT api/<controller>/5
        [EnableCors]
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [EnableCors]
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
