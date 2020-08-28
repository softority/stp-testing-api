﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Stp.Data;

namespace Stp.Data.Migrations
{
    [DbContext(typeof(TestingDbContext))]
    partial class TestingDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

            modelBuilder.Entity("Stp.Data.Entities.Skill", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Skill");
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

            modelBuilder.Entity("Stp.Data.Entities.TaskAndSkill", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<long>("SkillId")
                        .HasColumnType("bigint");

                    b.Property<long>("TaskId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("SkillId");

                    b.HasIndex("TaskId");

                    b.ToTable("TaskAndSkill");
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

            modelBuilder.Entity("Stp.Data.Entities.TestSection", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("Position")
                        .HasColumnType("integer");

                    b.Property<long>("TestId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("TestSection");
                });

            modelBuilder.Entity("Stp.Data.Entities.TestSectionAndTask", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<long>("TaskId")
                        .HasColumnType("bigint");

                    b.Property<int>("TaskPosition")
                        .HasColumnType("integer");

                    b.Property<long>("TestSectionId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("TaskId");

                    b.HasIndex("TestSectionId");

                    b.ToTable("TestSectionAndTask");
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

            modelBuilder.Entity("Stp.Data.Entities.TaskAndSkill", b =>
                {
                    b.HasOne("Stp.Data.Entities.Skill", "Skill")
                        .WithMany("TaskAndSkills")
                        .HasForeignKey("SkillId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Stp.Data.Entities.StpTask", "Task")
                        .WithMany("TaskAndSkills")
                        .HasForeignKey("TaskId")
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

            modelBuilder.Entity("Stp.Data.Entities.TestSectionAndTask", b =>
                {
                    b.HasOne("Stp.Data.Entities.StpTask", "Task")
                        .WithMany()
                        .HasForeignKey("TaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Stp.Data.Entities.TestSection", "TestSection")
                        .WithMany("TestSectionsAndTasks")
                        .HasForeignKey("TestSectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
