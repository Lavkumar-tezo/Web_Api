using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeDirectory.DAL.Models
{
    [Table("Department")]
    public class Department
    {
        [Key,Length(5,5)]
        public string Id { get; set; } = null!;

        [MaxLength(35), MinLength(2), Required]
        public string Name { get; set; } = null!;

        public List<Role> Roles { get; set; }=new List<Role>();

        public List<Employee>? Employees { get; set; }= new List<Employee>();
    }
}
