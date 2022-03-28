﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Osvip.Api.Data;

#nullable disable

namespace Osvip.Api.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20220325175735_neinit6")]
    partial class neinit6
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("public")
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Osvip.Api.Models.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ImagePath")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Departments", "public");
                });

            modelBuilder.Entity("Osvip.Api.Models.Direction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Form")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Level")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Directions", "public");
                });

            modelBuilder.Entity("Osvip.Api.Models.Test", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Course")
                        .HasColumnType("integer");

                    b.Property<int>("DepartmentId")
                        .HasColumnType("integer");

                    b.Property<Guid>("QuestionId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("QuestionId");

                    b.ToTable("Tests", "public");
                });

            modelBuilder.Entity("Osvip.Api.Models.TestQuestion", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Question")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Questions", "public");
                });

            modelBuilder.Entity("Osvip.Api.Models.TestResponse", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Response")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("TestId")
                        .HasColumnType("integer");

                    b.Property<int>("Weigth")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("TestId");

                    b.ToTable("TestResponses", "public");
                });

            modelBuilder.Entity("Osvip.Api.Models.Transfer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ApplicationFile")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Course")
                        .HasColumnType("integer");

                    b.Property<int>("DepartmentId")
                        .HasColumnType("integer");

                    b.Property<int>("DirectionId")
                        .HasColumnType("integer");

                    b.Property<string>("MarksFile")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("RequestType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Semester")
                        .HasColumnType("integer");

                    b.Property<string>("Telephone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("DirectionId");

                    b.HasIndex("UserId");

                    b.ToTable("Transfers", "public");
                });

            modelBuilder.Entity("Osvip.Api.Models.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("CouncurencyStapm")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("Fcs")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ImgPath")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.Property<string>("SecuriryStamp")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("TestId")
                        .HasColumnType("integer");

                    b.HasKey("UserId");

                    b.HasIndex("TestId");

                    b.ToTable("Users", "public");
                });

            modelBuilder.Entity("Osvip.Api.Models.UsersTest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Result")
                        .HasColumnType("integer");

                    b.Property<int>("TestId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("TestStartTime")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("TestId");

                    b.ToTable("UsersTests", "public");
                });

            modelBuilder.Entity("Osvip.Api.Models.UserToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("AccessToken")
                        .HasColumnType("text");

                    b.Property<int?>("EmailConfirmToken")
                        .HasColumnType("integer");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("UserTokens", "public");
                });

            modelBuilder.Entity("Osvip.Api.Models.Test", b =>
                {
                    b.HasOne("Osvip.Api.Models.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Osvip.Api.Models.TestQuestion", "Question")
                        .WithMany()
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");

                    b.Navigation("Question");
                });

            modelBuilder.Entity("Osvip.Api.Models.TestResponse", b =>
                {
                    b.HasOne("Osvip.Api.Models.Test", null)
                        .WithMany("testResponses")
                        .HasForeignKey("TestId");
                });

            modelBuilder.Entity("Osvip.Api.Models.Transfer", b =>
                {
                    b.HasOne("Osvip.Api.Models.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Osvip.Api.Models.Direction", "Direction")
                        .WithMany()
                        .HasForeignKey("DirectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Osvip.Api.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");

                    b.Navigation("Direction");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Osvip.Api.Models.User", b =>
                {
                    b.HasOne("Osvip.Api.Models.UsersTest", "Test")
                        .WithMany()
                        .HasForeignKey("TestId");

                    b.Navigation("Test");
                });

            modelBuilder.Entity("Osvip.Api.Models.UsersTest", b =>
                {
                    b.HasOne("Osvip.Api.Models.Test", "Test")
                        .WithMany()
                        .HasForeignKey("TestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Test");
                });

            modelBuilder.Entity("Osvip.Api.Models.UserToken", b =>
                {
                    b.HasOne("Osvip.Api.Models.User", "user")
                        .WithOne("Token")
                        .HasForeignKey("Osvip.Api.Models.UserToken", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("user");
                });

            modelBuilder.Entity("Osvip.Api.Models.Test", b =>
                {
                    b.Navigation("testResponses");
                });

            modelBuilder.Entity("Osvip.Api.Models.User", b =>
                {
                    b.Navigation("Token")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
