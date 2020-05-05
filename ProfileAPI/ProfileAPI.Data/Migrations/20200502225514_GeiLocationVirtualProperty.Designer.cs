﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ProfileAPI.Data.Context;

namespace ProfileAPI.Data.Migrations
{
    [DbContext(typeof(ProfileDBContext))]
    [Migration("20200502225514_GeiLocationVirtualProperty")]
    partial class GeiLocationVirtualProperty
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("ProfileAPI.Data.Entities.GeoLocation", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("City")
                        .HasColumnType("text");

                    b.Property<string>("Country")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("Latitude")
                        .HasColumnType("text");

                    b.Property<string>("Longitude")
                        .HasColumnType("text");

                    b.Property<string>("Street")
                        .HasColumnType("text");

                    b.Property<Guid?>("UserProfileId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserProfileId");

                    b.ToTable("GeoLocations");
                });

            modelBuilder.Entity("ProfileAPI.Data.Entities.Image", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("UploadedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Url")
                        .HasColumnType("text");

                    b.Property<Guid?>("UserProfileId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserProfileId");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("ProfileAPI.Data.Entities.UserProfile", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("AvatarImage")
                        .HasColumnType("text");

                    b.Property<string>("Biography")
                        .HasColumnType("character varying(3000)")
                        .HasMaxLength(3000);

                    b.Property<string>("City")
                        .HasColumnType("text");

                    b.Property<string>("Country")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("Credits")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("DrinkAlcohol")
                        .HasColumnType("boolean");

                    b.Property<string>("EducationDegree")
                        .HasColumnType("text");

                    b.Property<bool>("EmailNotificationsSubscribed")
                        .HasColumnType("boolean");

                    b.Property<string>("Figure")
                        .HasColumnType("text");

                    b.Property<string>("Gender")
                        .HasColumnType("text");

                    b.Property<bool>("HaveKids")
                        .HasColumnType("boolean");

                    b.Property<int>("Height")
                        .HasColumnType("integer");

                    b.Property<string>("HowOftenDrinkAlcohol")
                        .HasColumnType("text");

                    b.Property<string>("HowOftenSmoke")
                        .HasColumnType("text");

                    b.Property<int>("Income")
                        .HasColumnType("integer");

                    b.Property<bool>("IsUserProfileActivated")
                        .HasColumnType("boolean");

                    b.Property<string>("Languages")
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<string>("Location")
                        .HasColumnType("text");

                    b.Property<string>("LookingFor")
                        .HasColumnType("text");

                    b.Property<string>("MeritalStatus")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("character varying(15)")
                        .HasMaxLength(15);

                    b.Property<int>("PartnerAgeRangeFrom")
                        .HasColumnType("integer");

                    b.Property<int>("PartnerAgeRangeTo")
                        .HasColumnType("integer");

                    b.Property<bool>("PartnerDrinkAlcohol")
                        .HasColumnType("boolean");

                    b.Property<string>("PartnerFigure")
                        .HasColumnType("text");

                    b.Property<bool>("PartnerHaveKids")
                        .HasColumnType("boolean");

                    b.Property<int>("PartnerIncomeFrom")
                        .HasColumnType("integer");

                    b.Property<int>("PartnerIncomeTo")
                        .HasColumnType("integer");

                    b.Property<bool>("PartnerSmoke")
                        .HasColumnType("boolean");

                    b.Property<bool>("Smoker")
                        .HasColumnType("boolean");

                    b.Property<int>("Superlikes")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .HasColumnType("character varying(20)")
                        .HasMaxLength(20);

                    b.Property<string>("University")
                        .HasColumnType("text");

                    b.Property<string>("WantKids")
                        .HasColumnType("text");

                    b.Property<bool>("WantToHaveKids")
                        .HasColumnType("boolean");

                    b.Property<int>("Weight")
                        .HasColumnType("integer");

                    b.Property<string>("Work")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("UserProfiles");
                });

            modelBuilder.Entity("ProfileAPI.Data.Entities.GeoLocation", b =>
                {
                    b.HasOne("ProfileAPI.Data.Entities.UserProfile", "UserProfile")
                        .WithMany("GeoLocations")
                        .HasForeignKey("UserProfileId")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("ProfileAPI.Data.Entities.Image", b =>
                {
                    b.HasOne("ProfileAPI.Data.Entities.UserProfile", "UserProfile")
                        .WithMany("Images")
                        .HasForeignKey("UserProfileId")
                        .OnDelete(DeleteBehavior.SetNull);
                });
#pragma warning restore 612, 618
        }
    }
}
