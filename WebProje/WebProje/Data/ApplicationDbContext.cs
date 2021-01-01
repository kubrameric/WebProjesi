using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebProje.Models;

namespace WebProje.Data
{ 
    public class ApplicationDbContext : IdentityDbContext<UserDetails>
    {
        public DbSet<Hayvanlar> Hayvanlar { get; set; }
        public DbSet<Ilanlar> Ilanlar { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
    }
}
