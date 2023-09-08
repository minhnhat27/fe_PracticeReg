﻿// <auto-generated />
using System;
using DangKyPhongThucHanhCNTT.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DangKyPhongThucHanhCNTT.Migrations
{
    [DbContext(typeof(MyDbContext))]
    partial class MyDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DangKyPhongThucHanhCNTT.Models.CPU", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.ToTable("CPUs");
                });

            modelBuilder.Entity("DangKyPhongThucHanhCNTT.Models.Course", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CourseName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CreditHours")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfPractice")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("DangKyPhongThucHanhCNTT.Models.CourseGroup", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CourseId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("NumberOfStudents")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.ToTable("CourseGroups");
                });

            modelBuilder.Entity("DangKyPhongThucHanhCNTT.Models.HardDrive", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("HardDrives");
                });

            modelBuilder.Entity("DangKyPhongThucHanhCNTT.Models.Hardware", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CPUId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("HardDriveId")
                        .HasColumnType("int");

                    b.Property<int?>("RAMId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CPUId");

                    b.HasIndex("HardDriveId");

                    b.HasIndex("RAMId");

                    b.ToTable("Hardwares");
                });

            modelBuilder.Entity("DangKyPhongThucHanhCNTT.Models.InstallSoftware", b =>
                {
                    b.Property<int>("HardwareId")
                        .HasColumnType("int");

                    b.Property<int>("SoftwareId")
                        .HasColumnType("int");

                    b.HasKey("HardwareId", "SoftwareId");

                    b.HasIndex("SoftwareId");

                    b.ToTable("InstallSoftwares");
                });

            modelBuilder.Entity("DangKyPhongThucHanhCNTT.Models.Lecturer", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Lectures");
                });

            modelBuilder.Entity("DangKyPhongThucHanhCNTT.Models.NumberOfSession", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("NumberSessions");
                });

            modelBuilder.Entity("DangKyPhongThucHanhCNTT.Models.RAM", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("RAMs");
                });

            modelBuilder.Entity("DangKyPhongThucHanhCNTT.Models.Room", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int?>("HardwareId")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfComputers")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("HardwareId");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("DangKyPhongThucHanhCNTT.Models.Schedule", b =>
                {
                    b.Property<string>("Day")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("TimeId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("RoomId")
                        .HasColumnType("int");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TeachingCourseGroupId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("TeachingLecturerId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("TeachingNumberOfSessionId")
                        .HasColumnType("int");

                    b.Property<string>("TeachingSemesterId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("WeekId")
                        .HasColumnType("int");

                    b.HasKey("Day", "TimeId", "RoomId");

                    b.HasIndex("RoomId");

                    b.HasIndex("TimeId");

                    b.HasIndex("WeekId");

                    b.HasIndex("TeachingCourseGroupId", "TeachingNumberOfSessionId", "TeachingLecturerId", "TeachingSemesterId");

                    b.ToTable("Schedules");
                });

            modelBuilder.Entity("DangKyPhongThucHanhCNTT.Models.Semester", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("CurrentSemster")
                        .HasColumnType("bit");

                    b.Property<string>("SemesterStartDate")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Semesters");
                });

            modelBuilder.Entity("DangKyPhongThucHanhCNTT.Models.Software", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Softwares");
                });

            modelBuilder.Entity("DangKyPhongThucHanhCNTT.Models.SuitableCourse", b =>
                {
                    b.Property<int>("RoomId")
                        .HasColumnType("int");

                    b.Property<string>("CourseId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("RoomId", "CourseId");

                    b.HasIndex("CourseId");

                    b.ToTable("SuitableCourses");
                });

            modelBuilder.Entity("DangKyPhongThucHanhCNTT.Models.Teaching", b =>
                {
                    b.Property<string>("CourseGroupId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("NumberOfSessionId")
                        .HasColumnType("int");

                    b.Property<string>("LecturerId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("SemesterId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("CourseGroupId", "NumberOfSessionId", "LecturerId", "SemesterId");

                    b.HasIndex("LecturerId");

                    b.HasIndex("NumberOfSessionId");

                    b.HasIndex("SemesterId");

                    b.ToTable("Teachings");
                });

            modelBuilder.Entity("DangKyPhongThucHanhCNTT.Models.Time", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.ToTable("Times");
                });

            modelBuilder.Entity("DangKyPhongThucHanhCNTT.Models.Week", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Weeks");
                });

            modelBuilder.Entity("DangKyPhongThucHanhCNTT.Models.CourseGroup", b =>
                {
                    b.HasOne("DangKyPhongThucHanhCNTT.Models.Course", "Course")
                        .WithMany("CourseGroup")
                        .HasForeignKey("CourseId");

                    b.Navigation("Course");
                });

            modelBuilder.Entity("DangKyPhongThucHanhCNTT.Models.Hardware", b =>
                {
                    b.HasOne("DangKyPhongThucHanhCNTT.Models.CPU", "CPU")
                        .WithMany("Hardwares")
                        .HasForeignKey("CPUId");

                    b.HasOne("DangKyPhongThucHanhCNTT.Models.HardDrive", "HardDrive")
                        .WithMany("Hardwares")
                        .HasForeignKey("HardDriveId");

                    b.HasOne("DangKyPhongThucHanhCNTT.Models.RAM", "RAM")
                        .WithMany("Hardwares")
                        .HasForeignKey("RAMId");

                    b.Navigation("CPU");

                    b.Navigation("HardDrive");

                    b.Navigation("RAM");
                });

            modelBuilder.Entity("DangKyPhongThucHanhCNTT.Models.InstallSoftware", b =>
                {
                    b.HasOne("DangKyPhongThucHanhCNTT.Models.Hardware", "Hardware")
                        .WithMany()
                        .HasForeignKey("HardwareId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DangKyPhongThucHanhCNTT.Models.Software", "Software")
                        .WithMany()
                        .HasForeignKey("SoftwareId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hardware");

                    b.Navigation("Software");
                });

            modelBuilder.Entity("DangKyPhongThucHanhCNTT.Models.Room", b =>
                {
                    b.HasOne("DangKyPhongThucHanhCNTT.Models.Hardware", "Hardware")
                        .WithMany("Rooms")
                        .HasForeignKey("HardwareId");

                    b.Navigation("Hardware");
                });

            modelBuilder.Entity("DangKyPhongThucHanhCNTT.Models.Schedule", b =>
                {
                    b.HasOne("DangKyPhongThucHanhCNTT.Models.Room", "Room")
                        .WithMany()
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DangKyPhongThucHanhCNTT.Models.Time", "Time")
                        .WithMany()
                        .HasForeignKey("TimeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DangKyPhongThucHanhCNTT.Models.Week", "Week")
                        .WithMany("Schedules")
                        .HasForeignKey("WeekId");

                    b.HasOne("DangKyPhongThucHanhCNTT.Models.Teaching", "Teaching")
                        .WithMany("Schedules")
                        .HasForeignKey("TeachingCourseGroupId", "TeachingNumberOfSessionId", "TeachingLecturerId", "TeachingSemesterId");

                    b.Navigation("Room");

                    b.Navigation("Teaching");

                    b.Navigation("Time");

                    b.Navigation("Week");
                });

            modelBuilder.Entity("DangKyPhongThucHanhCNTT.Models.SuitableCourse", b =>
                {
                    b.HasOne("DangKyPhongThucHanhCNTT.Models.Course", "Course")
                        .WithMany()
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DangKyPhongThucHanhCNTT.Models.Room", "Room")
                        .WithMany()
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Room");
                });

            modelBuilder.Entity("DangKyPhongThucHanhCNTT.Models.Teaching", b =>
                {
                    b.HasOne("DangKyPhongThucHanhCNTT.Models.CourseGroup", "CourseGroup")
                        .WithMany()
                        .HasForeignKey("CourseGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DangKyPhongThucHanhCNTT.Models.Lecturer", "Lecturer")
                        .WithMany()
                        .HasForeignKey("LecturerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DangKyPhongThucHanhCNTT.Models.NumberOfSession", "NumberOfSession")
                        .WithMany()
                        .HasForeignKey("NumberOfSessionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DangKyPhongThucHanhCNTT.Models.Semester", "Semester")
                        .WithMany()
                        .HasForeignKey("SemesterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CourseGroup");

                    b.Navigation("Lecturer");

                    b.Navigation("NumberOfSession");

                    b.Navigation("Semester");
                });

            modelBuilder.Entity("DangKyPhongThucHanhCNTT.Models.CPU", b =>
                {
                    b.Navigation("Hardwares");
                });

            modelBuilder.Entity("DangKyPhongThucHanhCNTT.Models.Course", b =>
                {
                    b.Navigation("CourseGroup");
                });

            modelBuilder.Entity("DangKyPhongThucHanhCNTT.Models.HardDrive", b =>
                {
                    b.Navigation("Hardwares");
                });

            modelBuilder.Entity("DangKyPhongThucHanhCNTT.Models.Hardware", b =>
                {
                    b.Navigation("Rooms");
                });

            modelBuilder.Entity("DangKyPhongThucHanhCNTT.Models.RAM", b =>
                {
                    b.Navigation("Hardwares");
                });

            modelBuilder.Entity("DangKyPhongThucHanhCNTT.Models.Teaching", b =>
                {
                    b.Navigation("Schedules");
                });

            modelBuilder.Entity("DangKyPhongThucHanhCNTT.Models.Week", b =>
                {
                    b.Navigation("Schedules");
                });
#pragma warning restore 612, 618
        }
    }
}
