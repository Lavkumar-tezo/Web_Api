using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EmployeeDirectory.DAL.Models
{
    [Table("Role")]
    public class Role
    {
        [Key, MaxLength(5), MinLength(5)]
        public string Id { get; set; } = null!;

        [MaxLength(20), MinLength(2), Required]
        public string Name { get; set; }=null!;
        [MaxLength(255)]
        public string? Description { get; set; }

        public List<Department> Departments { get; set; } = new List<Department>();

        public List<Employee>? Employees { get; set; } = new List<Employee>();

        public List<Location> Locations { get; set; } = new List<Location>();
    }
}
