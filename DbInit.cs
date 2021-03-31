using JointProjectLMSAPI.Context;
using JointProjectLMSAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JointProjectLMSAPI
{
    public class DbInit
    {
        public static void Initialize(LMSDbContext context)
        {
            context.Database.EnsureCreated();
            context.SaveChanges();
        }
    }
}
