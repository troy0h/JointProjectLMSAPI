using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JointProjectLMSAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // POST Create-Account
        [HttpPost("Create-Account")]
        public string CreateAcc(string NewUser, string NewPass)
        {
            return $"{NewUser}, {NewPass}";
        }

        // PATCH Update-User
        [HttpPatch("Update-User")]
        public string UpdateUser(string Username, string Password)
        {
            return $"{Username}, {Password}";
        }

        // DELETE Delete-User
        [HttpDelete("Delete-User")]
        public string DeleteUser(string Username, string Password)
        {
            return $"{Username}, {Password}";
        }

        // GET All-User
        [HttpGet("All-User")]
        public string AllUser(string Username, string Password)
        {
            return $"{Username}, {Password}";
        }

        // GET Get-By-Email
        [HttpGet("Get-By-Email")]
        public string GetEmail(string Email)
        {
            return $"{Email}";
        }

        // GET Get-By-Country
        [HttpGet("Get-By-Country")]
        public string GetCountry(string Country)
        {
            return $"{Country}";
        }

        // GET Get-By-Number
        [HttpGet("Get-By-Number")]
        public int GetNumber(int ID)
        {
            return ID;
        }
    }
}
