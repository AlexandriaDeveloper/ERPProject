using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ERPProject.Models
{
    public class ERPContext :DbContext
    {

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Position> Positions { get; set; }
        public ERPContext():base("ERPDB")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<ERPContext>());
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
        }
    }
}