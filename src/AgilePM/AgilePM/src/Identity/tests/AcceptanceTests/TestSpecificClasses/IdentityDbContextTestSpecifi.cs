using System;
using AgilePM.Identity.DataBase.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace AgilePM.Identity.AcceptanceTests.TestSpecificClasses
{
    public class IdentityDbContextTestSpecific : IdentityDbContext
    {
        public IdentityDbContextTestSpecific()
            : base(new DbContextOptionsBuilder<IdentityDbContext>().UseSqlite($"Data Source ={Guid.NewGuid().ToString()}.db").Options)
        {
            Database.EnsureCreated();
        }
    }
}