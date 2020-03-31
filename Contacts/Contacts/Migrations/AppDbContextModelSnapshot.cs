﻿// <auto-generated />
using System;
using Contacts.Models.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Contacts.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Contacts.Models.Contact", b =>
                {
                    b.Property<int>("ContactId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("Note");

                    b.Property<int?>("ProfilePhotoId");

                    b.HasKey("ContactId");

                    b.HasIndex("ProfilePhotoId");

                    b.ToTable("Contacts");
                });

            modelBuilder.Entity("Contacts.Models.PhoneNumber", b =>
                {
                    b.Property<int>("PhoneNumberId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ContactId");

                    b.Property<string>("Note");

                    b.Property<string>("Number");

                    b.Property<string>("Type");

                    b.HasKey("PhoneNumberId");

                    b.HasIndex("ContactId");

                    b.ToTable("PhoneNumbers");
                });

            modelBuilder.Entity("Contacts.Models.ProfilePhoto", b =>
                {
                    b.Property<int>("ProfilePhotoId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte[]>("Content");

                    b.HasKey("ProfilePhotoId");

                    b.ToTable("ProfilePhotos");
                });

            modelBuilder.Entity("Contacts.Models.Contact", b =>
                {
                    b.HasOne("Contacts.Models.ProfilePhoto", "ProfilePhoto")
                        .WithMany()
                        .HasForeignKey("ProfilePhotoId");
                });

            modelBuilder.Entity("Contacts.Models.PhoneNumber", b =>
                {
                    b.HasOne("Contacts.Models.Contact", "Contact")
                        .WithMany("PhoneNumbers")
                        .HasForeignKey("ContactId");
                });
#pragma warning restore 612, 618
        }
    }
}
