﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Stp.Data;

namespace Stp.Data.Migrations
{
    [DbContext(typeof(TestingDbContext))]
    [Migration("20200805172228_Issue#6")]
    partial class Issue6
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Stp.Data.Entities.MultichoiceTaskAnswer", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<bool>("IsCorrect")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<long>("TaskId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("TaskId");

                    b.ToTable("MultichoiceTaskAnswer");
                });

            modelBuilder.Entity("Stp.Data.Entities.StpTask", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<long>("CategoryId")
                        .HasColumnType("bigint");

                    b.Property<string>("CodeEditorUrl")
                        .HasColumnType("text");

                    b.Property<int>("Complexity")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<int>("DurationMinutes")
                        .HasColumnType("integer");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("Points")
                        .HasColumnType("integer");

                    b.Property<int>("Position")
                        .HasColumnType("integer");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Task");
                });

            modelBuilder.Entity("Stp.Data.Entities.TaskCategory", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<long?>("ParentId")
                        .HasColumnType("bigint");

                    b.Property<int>("Position")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("TaskCategory");
                });

            modelBuilder.Entity("Stp.Data.Entities.TestCategory", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<long?>("ParentId")
                        .HasColumnType("bigint");

                    b.Property<int>("Position")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("TestCategory");
                });

            modelBuilder.Entity("Stp.Data.Entities.MultichoiceTaskAnswer", b =>
                {
                    b.HasOne("Stp.Data.Entities.StpTask", "Task")
                        .WithMany("MultichoiceAnswers")
                        .HasForeignKey("TaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Stp.Data.Entities.StpTask", b =>
                {
                    b.HasOne("Stp.Data.Entities.TaskCategory", "Category")
                        .WithMany("Tasks")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Stp.Data.Entities.TaskCategory", b =>
                {
                    b.HasOne("Stp.Data.Entities.TaskCategory", "Parent")
                        .WithMany()
                        .HasForeignKey("ParentId");
                });

            modelBuilder.Entity("Stp.Data.Entities.TestCategory", b =>
                {
                    b.HasOne("Stp.Data.Entities.TestCategory", "Parent")
                        .WithMany()
                        .HasForeignKey("ParentId");
                });
#pragma warning restore 612, 618
        }
    }
}
