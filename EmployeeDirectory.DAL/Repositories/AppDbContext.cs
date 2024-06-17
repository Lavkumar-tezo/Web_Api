using EmployeeDirectory.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeDirectory.DAL.Repositories
{
    public class AppDbContext:DbContext
    {
        public AppDbContext()
        {

        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>(domain =>
            {
                domain.HasAlternateKey(role => role.Name);
                domain.HasMany(role => role.Employees).WithOne(emp => emp.Role).HasForeignKey(emp=>emp.RoleId);
                domain.HasMany(r => r.Departments).WithMany(d => d.Roles).UsingEntity<Dictionary<string, object>>(
                "RoleDepartment",
                j => j.HasOne<Department>().WithMany().HasForeignKey("DepartmentId").HasConstraintName("FK_DepartmentRole_Department").OnDelete(DeleteBehavior.Cascade),
                j => j.HasOne<Role>() .WithMany().HasForeignKey("RoleId").HasConstraintName("FK_DepartmentRole_Role")
                    .OnDelete(DeleteBehavior.Cascade),
                j =>
                {
                    j.Property<string>("DepartmentId");
                    j.Property<string>("RoleId").HasMaxLength(5);
                    j.HasKey("DepartmentId", "RoleId");
                    j.ToTable("RoleDepartment");
                });
                domain.HasMany(role => role.Locations).WithMany(loc => loc.Roles).UsingEntity<Dictionary<string, object>>(
               "RoleLocation",
               j => j.HasOne<Location>().WithMany() .HasForeignKey("LocationId").HasConstraintName("FK_RoleLocation_Location").OnDelete(DeleteBehavior.Cascade),
               j => j.HasOne<Role>().WithMany().HasForeignKey("RoleId").HasConstraintName("FK_RoleLocation_Role")
                   .OnDelete(DeleteBehavior.Cascade),
               j =>
               {
                   j.Property<string>("LocationId");
                   j.Property<string>("RoleId").HasMaxLength(5);
                   j.HasKey("LocationId", "RoleId");
                   j.ToTable("RoleLocation");
               });
            });
            modelBuilder.Entity<Department>(domain =>
            {
                domain.HasAlternateKey(dept=> dept.Name);
                domain.HasMany(dept => dept.Employees).WithOne(emp => emp.Department).HasForeignKey(emp=>emp.DepartmentId);
                domain.HasData(
                    new Department { Id= "TD001",Name = "Product Eng" },
                    new Department { Id = "TD002", Name = "QA" },
                    new Department { Id = "TD003", Name = "UI/UX" }
                    );
            });
            modelBuilder.Entity<Location>(domain =>
            {
                domain.HasAlternateKey(loc => loc.Name);
                domain.HasMany(loc => loc.Employees).WithOne(emp => emp.Location).HasForeignKey(emp=>emp.LocationId);
                domain.HasData(
                    new Location { Id = "TL001", Name = "Hyderabad" },
                    new Location { Id = "TL002", Name = "Bangalore" }
                    );
            });
            modelBuilder.Entity<Project>(domain =>
            {             
                domain.HasAlternateKey(project => project.Name);
                domain.HasMany(project => project.Employees).WithOne(emp => emp.Project).HasForeignKey(emp=>emp.ProjectId).OnDelete(DeleteBehavior.SetNull);
                domain.HasData(
                    new Project { Id = "TD001", Name = "Task-1" },
                    new Project { Id = "TD002", Name = "Task-2" },
                    new Project { Id = "TD003", Name = "Task-3" },
                    new Project { Id = "TD004", Name = "Task-4" },
                    new Project { Id = "TD005", Name = "Task-5" }
                );
            });
            modelBuilder.Entity<Employee>()
            .HasOne(emp => emp.Manager)
            .WithMany(emp => emp.Subordinates)      
            .HasForeignKey(emp => emp.ManagerId);


        }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<Location> Locations { get; set; }
    }
}
