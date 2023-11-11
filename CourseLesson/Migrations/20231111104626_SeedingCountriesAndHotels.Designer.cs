﻿// <auto-generated />
using CourseLesson.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CourseLesson.Migrations
{
    [DbContext(typeof(Course))]
    [Migration("20231111104626_SeedingCountriesAndHotels")]
    partial class SeedingCountriesAndHotels
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CourseLesson.Data.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ShortName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Countries");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Azerbaijan",
                            ShortName = "AJ"
                        },
                        new
                        {
                            Id = 2,
                            Name = "America",
                            ShortName = "USA"
                        });
                });

            modelBuilder.Entity("CourseLesson.Data.Library", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CountryId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Rating")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("Libraries");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "Narimanov",
                            CountryId = 1,
                            Name = "Nizami Library",
                            Rating = 5m
                        },
                        new
                        {
                            Id = 2,
                            Address = "Los Angeles",
                            CountryId = 2,
                            Name = "Huston Library",
                            Rating = 4.7m
                        });
                });

            modelBuilder.Entity("CourseLesson.Data.Library", b =>
                {
                    b.HasOne("CourseLesson.Data.Country", "CountryName")
                        .WithMany("Libraries")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CountryName");
                });

            modelBuilder.Entity("CourseLesson.Data.Country", b =>
                {
                    b.Navigation("Libraries");
                });
#pragma warning restore 612, 618
        }
    }
}
