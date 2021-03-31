
using System;

namespace JointProjectLMSAPI.Models
{
    public class User
    {
        public int ID { get; set; }
        public string Rank { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }
        public string PassHash { get; set; }
        public string PassSalt { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public DateTime DateCourseBought { get; set; }
        public DateTime DateCourseExpire { get; set; }
        public DateTime SoftDeleteDate { get; set; }

    }
}
