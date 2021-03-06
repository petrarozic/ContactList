﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contacts.Models.Database
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<PhoneNumber> PhoneNumbers { get; set; }
        public DbSet<ProfilePhoto> ProfilePhotos { get; set; }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>()
                .HasMany(r => r.PhoneNumbers)
                .WithOne(s => s.Contact);

            modelBuilder.Entity<Contact>()
               .HasOne(p => p.ProfilePhoto);

            modelBuilder.Entity<Contact>()
                .HasOne(c => c.ApplicationUser)
                .WithMany(s => s.Contacts);

            base.OnModelCreating(modelBuilder);
        }
    }
}
