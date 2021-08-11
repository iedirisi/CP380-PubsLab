using System;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace CP380_PubsLab.Models
{
    public class PubsDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dbpath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\pubs.mdf"));
            optionsBuilder.UseSqlServer($"Server=(localdb)\\mssqllocaldb;Integrated Security=true;AttachDbFilename={dbpath}");
        }

        // TODO: Add DbSets
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Jobs> Jobs { get; set; }
        public DbSet<Stores> Stores { get; set; }
        public DbSet<Titles> Titles { get; set; }
        public DbSet<Sales> Sales { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // TODO
            modelBuilder.Entity<Sales>().HasKey(a => new { a.SId, a.TId });


            modelBuilder.Entity<Sales>()
           .HasOne(a => a.Storesli)
           .WithOne(b => b.Salesli)
           .HasPrincipalKey<Sales>(b => b.SId)
           .HasForeignKey<Stores>(b => b.Id);

            modelBuilder.Entity<Stores>()
        .HasOne(a => a.Salesli)
        .WithOne(b => b.Storesli)
        .HasPrincipalKey<Stores>(b => b.Id)
        .HasForeignKey<Sales>(b => b.SId);

            modelBuilder.Entity<Titles>()
           .HasOne(a => a.Salesli)
           .WithOne(b => b.Titlesli)
           .HasPrincipalKey<Sales>(b => b.TId)
           .HasForeignKey<Titles>(b => b.Id);


        }
    }


    public class Titles
    {
        // TODO
        [Column("title_id")]
        public string Id { get; set; }
        [Column("title")]
        public string title { get; set; }
        public Sales Salesli { get; set; }


    }


    public class Stores
    {


        // TODO
        [Column("stor_id")]
        public string Id { get; set; }
        [Column("stor_name")]
        public string name { get; set; }
        public Sales Salesli { get; set; }


    }


    public class Sales
    {
        // TODO
        [Column("stor_id")]
        public string SId { get; set; }
        [Column("title_id")]
        public string TId { get; set; }
        public Stores Storesli { get; set; }
        public Titles Titlesli { get; set; }

    }

    public class Employee
    {
        [Column("emp_id")]
        public string Id { get; set; }
        [Column("fname")]
        public string Fname { get; set; }
        [Column("lname")]
        public string Lname { get; set; }
        [Column("job_id")]
        public Int16 Job_Id { get; set; }
    }

    public class Jobs
    {
        // TODO
        [Column("job_id")]
        public Int16 Id { get; set; }
        [Column("job_desc")]
        public string Desc { get; set; }
    }
}
