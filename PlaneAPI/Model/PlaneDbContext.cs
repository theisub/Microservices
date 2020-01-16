using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlaneAPI.Model
{
    public class PlaneDbContext : DbContext
    {
        public PlaneDbContext(DbContextOptions<PlaneDbContext> options) : base(options)
        { }
        public DbSet<Plane> Planes{ get; set; }
    }
}
