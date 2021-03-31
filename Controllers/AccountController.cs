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
    public class AccountController : ControllerBase
    {
        // GET Login
        [HttpGet("Login")]
        public string Login(string Username, string Password)
        {
            return $"{Username}, {Password}";
        }

        // POST Change-Password
        [HttpPost("Change-Password")]
        public string ChangePass(string Password, string NewPass, string PassConf)
        {
            return $"{Password}, {NewPass}, {PassConf}";
        }

        // GET Send-Forgot-Password-OTP
        [HttpGet("Send-Forgot-Password-OTP")]
        public string SendOTP(string Username)
        {
            return $"Username";
        }

        // POST Reset-Password-With-OTP
        [HttpPost("Reset-Password-With-OTP")]
        public string ResetOTP(string OTP)
        {
            return $"OTP";
        }
    }
}
