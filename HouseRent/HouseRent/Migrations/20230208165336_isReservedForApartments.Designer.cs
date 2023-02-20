﻿// <auto-generated />
using System;
using HouseRent.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HouseRent.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230208165336_isReservedForApartments")]
    partial class isReservedForApartments
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("HouseRent.Models.Apartment", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("Id"), 1L, 1);

                    b.Property<string>("AdditionalFacilities")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AdressAndArea")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Alcony")
                        .HasColumnType("bit");

                    b.Property<int?>("ApartmentCategoryID")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ApartmentCreationDate")
                        .HasColumnType("datetime2");

                    b.Property<byte?>("BathCount")
                        .HasColumnType("tinyint");

                    b.Property<byte?>("BedRoomCount")
                        .HasColumnType("tinyint");

                    b.Property<bool>("CCTV")
                        .HasColumnType("bit");

                    b.Property<byte?>("CarSpace")
                        .HasColumnType("tinyint");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("CommunityHall")
                        .HasColumnType("bit");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Facilities")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("FireExit")
                        .HasColumnType("bit");

                    b.Property<int?>("FlatSize")
                        .HasColumnType("int");

                    b.Property<string>("FloorCount")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Generator")
                        .HasColumnType("bit");

                    b.Property<bool>("InterCom")
                        .HasColumnType("bit");

                    b.Property<bool>("IsReserved")
                        .HasColumnType("bit");

                    b.Property<bool>("Lift")
                        .HasColumnType("bit");

                    b.Property<int?>("Rentprice")
                        .HasColumnType("int");

                    b.Property<string>("RoomCategory")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("SafetyGrills")
                        .HasColumnType("bit");

                    b.Property<bool>("ServantsRoom")
                        .HasColumnType("bit");

                    b.Property<bool>("Southfacing")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("ApartmentCategoryID");

                    b.ToTable("Apartments");
                });

            modelBuilder.Entity("HouseRent.Models.ApartmentCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("AppartmentCategoryName")
                        .HasMaxLength(90)
                        .HasColumnType("nvarchar(90)");

                    b.Property<string>("ImgApart")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ApartmentCategories");
                });

            modelBuilder.Entity("HouseRent.Models.ApartmentImages", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("ApartmentId")
                        .HasColumnType("int");

                    b.Property<string>("Img")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsPoster")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("ApartmentId");

                    b.ToTable("ApartmentImages");
                });

            modelBuilder.Entity("HouseRent.Models.SettingsRH", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Key")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("SettingsRH");
                });

            modelBuilder.Entity("HouseRent.Models.Slider", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Img")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("RedirectUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title1")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title2")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Sliders");
                });

            modelBuilder.Entity("HouseRent.Models.Apartment", b =>
                {
                    b.HasOne("HouseRent.Models.ApartmentCategory", "ApartmentCategory")
                        .WithMany("Apartments")
                        .HasForeignKey("ApartmentCategoryID");

                    b.Navigation("ApartmentCategory");
                });

            modelBuilder.Entity("HouseRent.Models.ApartmentImages", b =>
                {
                    b.HasOne("HouseRent.Models.Apartment", "Apartment")
                        .WithMany("ApartmentImages")
                        .HasForeignKey("ApartmentId");

                    b.Navigation("Apartment");
                });

            modelBuilder.Entity("HouseRent.Models.Apartment", b =>
                {
                    b.Navigation("ApartmentImages");
                });

            modelBuilder.Entity("HouseRent.Models.ApartmentCategory", b =>
                {
                    b.Navigation("Apartments");
                });
#pragma warning restore 612, 618
        }
    }
}
