﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Schedule.Persistance;

namespace Schedule.Persistance.Migrations
{
    [DbContext(typeof(ScheduleContext))]
    partial class ScheduleContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Schedule.Domain.Entities.Laboratory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<short>("EndHour");

                    b.Property<string>("Group");

                    b.Property<string>("Name");

                    b.Property<short>("StartHour");

                    b.Property<Guid?>("SubjectId");

                    b.Property<Guid?>("TeacherId");

                    b.Property<string>("Weekday");

                    b.HasKey("Id");

                    b.HasIndex("SubjectId");

                    b.HasIndex("TeacherId");

                    b.ToTable("Laboratories");
                });

            modelBuilder.Entity("Schedule.Domain.Entities.Lecture", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<short>("EndHour");

                    b.Property<string>("HalfYear");

                    b.Property<string>("Name");

                    b.Property<short>("StartHour");

                    b.Property<Guid?>("SubjectId");

                    b.Property<string>("Weekday");

                    b.HasKey("Id");

                    b.HasIndex("SubjectId");

                    b.ToTable("Lecture");
                });

            modelBuilder.Entity("Schedule.Domain.Entities.Student", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("Group");

                    b.Property<string>("LastName");

                    b.Property<string>("Password");

                    b.Property<short>("Year");

                    b.HasKey("Id");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("Schedule.Domain.Entities.Subject", b =>
                {
                    b.Property<Guid>("SubjectId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<Guid?>("TeacherId");

                    b.HasKey("SubjectId");

                    b.HasIndex("TeacherId");

                    b.ToTable("Subject");
                });

            modelBuilder.Entity("Schedule.Domain.Entities.Teacher", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<Guid?>("LectureId");

                    b.Property<string>("Password");

                    b.HasKey("Id");

                    b.HasIndex("LectureId");

                    b.ToTable("Teachers");
                });

            modelBuilder.Entity("Schedule.Domain.Entities.Laboratory", b =>
                {
                    b.HasOne("Schedule.Domain.Entities.Subject")
                        .WithMany("Laboratories")
                        .HasForeignKey("SubjectId");

                    b.HasOne("Schedule.Domain.Entities.Teacher", "Teacher")
                        .WithMany()
                        .HasForeignKey("TeacherId");
                });

            modelBuilder.Entity("Schedule.Domain.Entities.Lecture", b =>
                {
                    b.HasOne("Schedule.Domain.Entities.Subject")
                        .WithMany("Lectures")
                        .HasForeignKey("SubjectId");
                });

            modelBuilder.Entity("Schedule.Domain.Entities.Subject", b =>
                {
                    b.HasOne("Schedule.Domain.Entities.Teacher")
                        .WithMany("Subjects")
                        .HasForeignKey("TeacherId");
                });

            modelBuilder.Entity("Schedule.Domain.Entities.Teacher", b =>
                {
                    b.HasOne("Schedule.Domain.Entities.Lecture")
                        .WithMany("Teachers")
                        .HasForeignKey("LectureId");
                });
#pragma warning restore 612, 618
        }
    }
}
