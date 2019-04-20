using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    [EnableCors("AllowMyOrigin")]
    public class TestSystemController : Controller
    {

        [HttpPost("test/create")]
        public void CreateTest(string data)//TODO: Change to  TestData
        {
            throw new NotImplementedException();
        }

        [HttpGet("test/getTests")]
        public void GetTests(string data)//TODO: Change to  TestProgram and Add Return Type
        {
            throw new NotImplementedException();

            return;
        }

        [HttpPost("test/submit")]
        public void SubmitAnswers(string data)//TODO: Change to  AnswersData
        {
            throw new NotImplementedException();
        }






    }
}