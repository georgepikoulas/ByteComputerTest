using ByteComputerTest.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace ByteComputerTest.Data
{
    public class ByteComputerTestDbContext : DbContext
    {
        public ByteComputerTestDbContext(DbContextOptions<ByteComputerTestDbContext> options) : base(options)
        {

        }


        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Degree> Degrees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Candidate>().ToTable("Candidates");
            //modelBuilder.Entity<Degree>().ToTable("Degrees");

            modelBuilder.Entity<Candidate>()
                        .HasIndex(p => p.Email)
            .IsUnique();
        }
    }
}
