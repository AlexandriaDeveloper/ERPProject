using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ERPProject.Models
{
    public class ERPContext : DbContext
    {

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Position> Positions { get; set; }

        public DbSet<ExpensessType> ExpensessTypes { get; set; }
        public DbSet<Daily> Dailies { get; set; }
        public DbSet<DailyFile> DailyFiles { get; set; }
        public DbSet<DailyFileDetails> DailyFileDetailses { get; set; }


        public ERPContext() : base("ERPDB")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<ERPContext>());
            this.Configuration.ProxyCreationEnabled = false;
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            

            //ONE Department Many Employees
            modelBuilder.Entity<Department>()
                .HasMany(x => x.Employees)
                .WithOptional(x => x.Department)
                .HasForeignKey(x => x.DepartmentId);


            //One Position Many Employees

            modelBuilder.Entity<Position>()
               .HasMany(x => x.Employees)
               .WithOptional(x => x.Position)
               .HasForeignKey(x => x.PositionId);



            //One Expensess Many Daily

            modelBuilder.Entity<ExpensessType>().
                HasMany(x => x.Dailies)
                .WithRequired(x => x.ExpensessType)
                .HasForeignKey(x => x.ExpensessTypeId);


            //one Daily Many DailyFile
            modelBuilder.Entity<Daily>().HasMany(x => x.DailyFiles)
                .WithRequired(x => x.Daily).HasForeignKey(x => x.DailyId);



            //one DailyFile Many DailyFileDetails
            modelBuilder.Entity<DailyFile>()
                .HasMany(x => x.DailyFileDetailses)
                .WithRequired(x => x.DailyFile)
                .HasForeignKey(x => x.DailyFileId);


            //many Employees Many DailyFileDetails
            modelBuilder.Entity<Employee>()
                .HasMany(x => x.DailyFileDetails)
                .WithRequired(x => x.Employee)
                .HasForeignKey(x => x.EmployeeId);
        }
    }
}