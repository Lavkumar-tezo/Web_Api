using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace EmployeeDirectory.DAL.Models
{
    [Table("Employee")]
    public class Employee
    {
        [Key, MaxLength(6), MinLength(6)]
        public string Id { get; set; } = null!;

        [MaxLength(35), MinLength(3), Required]
        public string FirstName { get; set; } = null!;

        [MaxLength(35), MinLength(3), Required]
        public string LastName { get; set; } = null!;

        [EmailAddress(ErrorMessage = "Invalid Email Address"),Required,MaxLength(150)]
        public string Email { get; set; } = null!;

        [Required]
        public DateTime JoiningDate { get; set; }

        [Required,ForeignKey("Location")]
        public int LocationId { get; set; }

        [Required, ForeignKey("Department")]
        public int DepartmentId { get; set; }

        [Required, ForeignKey("Role")]
        public string RoleId { get; set; } = null!;

        [ForeignKey("Project")]
        public int? ProjectId { get; set; }

        public string? ManagerId { get; set; }

        public DateTime? DOB { get; set; }

        [Length(10,10)]
        public string? Mobile { get; set; }
        [DefaultValue("false")]
        public bool? IsManager { get; set; }
        [DefaultValue("false"),Required]
        public bool IsDeleted { get; set; }

        public Role Role { get; set; } = null!;

        public Employee? Manager { get; set; }

        public List<Employee>? Subordinates { get; set; }

        public Department Department { get; set; } = null!;

        public Location Location { get; set; } = null!;

        public Project? Project { get; set; }
    }
}
