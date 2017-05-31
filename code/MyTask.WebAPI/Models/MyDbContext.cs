using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
namespace MyTask.WebAPI.Models
{
    public class MyDBEntities : DbContext
    {
        public DbSet<Passenger> Passengers { get; set; }
        public DbSet<Locator> Locators { get; set; }
    }
}