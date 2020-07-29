using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnitOfWorkExample_WebProject.EF
{
    public class UowExampleContext : DbContext
    {
        public DbSet<Person> People { get; set; }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<PersonToAddress> PeoplAddresses { get; set; }

        public UowExampleContext(DbContextOptions<UowExampleContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>(entity =>
            {
                entity.ToTable("Person");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Id).UseSqlServerIdentityColumn().ValueGeneratedOnAdd();
                entity.Property(e => e.FirstName).HasColumnName("FirstName");
                entity.Property(e => e.LastName).HasColumnName("LastName");
            });

            modelBuilder.Entity<Address>(entity =>
            {
                entity.ToTable("Address");
                entity.Property(e => e.FullAddress).HasColumnName("FullAddress");
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Id).UseSqlServerIdentityColumn().ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<PersonToAddress>(entity =>
            {
                entity.ToTable("PersonToAddress");
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.PersonId).HasColumnName("PersonID");
                entity.Property(e => e.AddressId).HasColumnName("AddressID");

                entity.Property(e => e.Id).UseSqlServerIdentityColumn().ValueGeneratedOnAdd();
                entity.HasKey(e => new { e.PersonId, e.AddressId });

                entity.HasOne(pa => pa.Person)
                .WithMany(p => p.PeoplesAddress)
                .HasForeignKey(x => x.PersonId);

                entity.HasOne(pa => pa.Address)
                .WithMany(p => p.PeoplesAddress)
                .HasForeignKey(x => x.AddressId);
            });
        }
    }
}
