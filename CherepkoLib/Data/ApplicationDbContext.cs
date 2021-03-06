using CherepkoLib.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CherepkoLib.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Rod> Rods { get; set; }
        public DbSet<RodGroup> RodGroups { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
              : base(options)
        {

        }
    }
}
