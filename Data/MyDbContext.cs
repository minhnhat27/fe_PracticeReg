using DangKyPhongThucHanhCNTT.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using static System.Net.Mime.MediaTypeNames;
using System;

namespace DangKyPhongThucHanhCNTT.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }
        public virtual DbSet<Week> Weeks { get; set; }
        public virtual DbSet<Time> Times { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<CourseGroup> CourseGroups { get; set; }
        public virtual DbSet<NumberOfSession> NumberSessions { get; set; }
        public virtual DbSet<Lecturer> Lectures { get; set; }
        public virtual DbSet<Semester> Semesters { get; set; }
        public virtual DbSet<Teaching> Teachings { get; set; }
        public virtual DbSet<Schedule> Schedules { get; set; }
        public virtual DbSet<SuitableCourse> SuitableCourses { get; set; }
        public virtual DbSet<CPU> CPUs { get; set; }
        public virtual DbSet<RAM> RAMs { get; set; }
        public virtual DbSet<HardDrive> HardDrives { get; set; }
        public virtual DbSet<Hardware> Hardwares { get; set; }
        public virtual DbSet<Software> Softwares { get; set; }
        public virtual DbSet<InstallSoftware> InstallSoftwares { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //}
    }
}
