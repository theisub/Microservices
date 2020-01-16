using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;


namespace BusAPI.Model
{
    public class BusDbContext : DbContext
    {
        public BusDbContext (DbContextOptions<BusDbContext> options) : base(options)
        { }
        public DbSet<Bus> Buses { get; set; }

        
    }
}
