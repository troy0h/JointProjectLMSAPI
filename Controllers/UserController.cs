using JointProjectLMSAPI.Context;
using JointProjectLMSAPI.Models;
using JointProjectLMSAPI.Helper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JointProjectLMSAPI.Services;
using System.ComponentModel.DataAnnotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JointProjectLMSAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // POST CreateAccount
        [HttpPost("CreateAccount")]
        public ActionResult<string> CreateAcc(Role Role, string Username, string Email, string Country, string Password)
        {
            string Salt = Encrypt.Salt(16);
            var user = new User
            {
                Rank = Role.ToString(),
                Username = Username,
                Email = Email,
                Country = Country,
                PassSalt = Salt,
                PassHash = Encrypt.Sha256(Password + Salt),
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
            };

            using (var context = new LMSDbContext())
            {
                var isUserInUse = context.Users
                            .Where(b => b.Username == user.Username || b.Email == user.Email)
                            .Any();

                if (isUserInUse)
                { return BadRequest("Username or email in use"); }

                if (new EmailAddressAttribute().IsValid(user.Email) && user.Email != null) {}
                else
                { return BadRequest("Email is not valid"); }

                var SMTPEmailNotifier = new SMTPEmailNotifier();

                if (user.Username.Length > 64 || Username.Length == 0)
                { return BadRequest("Username invalid"); }

                if (user.Email.Length > 64 || Email.Length == 0)
                { return BadRequest("Email invalid"); }

                if (user.Country.Length > 64 || Country.Length == 0)
                { return BadRequest("Country invalid"); }

                if (Password.Length > 64 || Password.Length < 6 || !Password.Any(char.IsUpper))
                { return BadRequest("Password Invalid"); }

                string specialChar = " !@#$£%^&*~";
                foreach (char item in specialChar)
                {
                    if (Password.Contains(item))
                    {
                        context.Users.Add(user);
                        context.SaveChanges();
                        Task.Run(() => SMTPEmailNotifier.SendEmailAsync("troy0htesting@gmail.com", Email, "Test Account Confirmation", $"Test Email Confirmation\n{user.Username}"));
                        return Ok("User accepted");
                    }
                }
                return BadRequest("Password invalid");
            }
        }

        // PATCH Update-User
        [HttpPatch("Update-User")]
        public string UpdateUser(string Username, string Password)
        {
            return $"¯\\_(ツ)_/¯";
        }

        // DELETE Delete-User
        [HttpDelete("Delete-User")]
        public string DeleteUser(string Username, string Password, string ConfirmPassword)
        {
            using (var context = new LMSDbContext())
            {
                var Name = context.Users
                                .Where(b => b.Username == Username)
                                .FirstOrDefault();
                if (Name != null)
                {
                    if (Password == ConfirmPassword)
                    {
                        if (Encrypt.Sha256(Password + Name.PassSalt) == Name.PassHash)
                        {
                            Name.SoftDeleteDate = DateTime.Now;
                            Response.StatusCode = 200; return "Account marked as deleted";
                        }
                        else
                        { Response.StatusCode = 400; return "Password incorrect"; }
                    }
                    else
                    { Response.StatusCode = 400; return "Passwords do not match"; }
                }
                else
                { Response.StatusCode = 400; return "User does not exist"; }
            }
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
            using (var context = new LMSDbContext())
            {
                var Name = context.Users
                                    .Where(b => b.Email == Email)
                                    .FirstOrDefault();
                if (Name != null)
                {
                    return $"ID                 = {Name.ID}\n" +
                           $"Rank               = {Name.Rank}\n" +
                           $"Username           = {Name.Username}\n" +
                           $"Email              = {Name.Email}\n" +
                           $"Country            = {Name.Country}\n" +
                           $"Date Created       = {Name.DateCreated}\n" +
                           $"Date Modified      = {Name.DateModified}\n" +
                           $"Date Course Bought = {Name.DateCourseBought}\n" +
                           $"Date Course Expire = {Name.DateCourseExpire}\n" +
                           $"Soft Delete Date   = {Name.SoftDeleteDate}\n";
                }
                else
                { Response.StatusCode = 400; return "Email does not exist"; }
            }
        }
        // GET Get-By-Country
        [HttpGet("Get-By-Country")]
        public string GetCountry(string Country)
        {
            using (var context = new LMSDbContext())
            {
                var Name = context.Users
                                    .Where(b => b.Country == Country)
                                    .FirstOrDefault();
                if (Name != null)
                {
                    return $"ID                 = {Name.ID}\n" +
                           $"Rank               = {Name.Rank}\n" +
                           $"Username           = {Name.Username}\n" +
                           $"Email              = {Name.Email}\n" +
                           $"Country            = {Name.Country}\n" +
                           $"Date Created       = {Name.DateCreated}\n" +
                           $"Date Modified      = {Name.DateModified}\n" +
                           $"Date Course Bought = {Name.DateCourseBought}\n" +
                           $"Date Course Expire = {Name.DateCourseExpire}\n" +
                           $"Soft Delete Date   = {Name.SoftDeleteDate}\n";
                }
                else
                { Response.StatusCode = 400; return "Country does not exist"; }
            }
        }

        // GET Get-By-Number
        [HttpGet("Get-By-Number")]
        public string GetNumber(int ID)
        {
            using (var context = new LMSDbContext())
            {
                var Name = context.Users
                                    .Where(b => b.ID == ID)
                                    .FirstOrDefault();
                if (Name != null)
                {
                    return $"ID                 = {Name.ID}\n" +
                           $"Rank               = {Name.Rank}\n" +
                           $"Username           = {Name.Username}\n" +
                           $"Email              = {Name.Email}\n" +
                           $"Country            = {Name.Country}\n" +
                           $"Date Created       = {Name.DateCreated}\n" +
                           $"Date Modified      = {Name.DateModified}\n" +
                           $"Date Course Bought = {Name.DateCourseBought}\n" +
                           $"Date Course Expire = {Name.DateCourseExpire}\n" +
                           $"Soft Delete Date   = {Name.SoftDeleteDate}\n";
                }
                else
                { Response.StatusCode = 400; return "ID does not exist"; }
            }
        }
    }
}
