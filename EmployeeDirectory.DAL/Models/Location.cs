using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EmployeeDirectory.DAL.Models
{
    [Table("Location")]
    public class Location
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
        public int Id { get; set; }

        [MaxLength(35), MinLength(2), Required]
        public string Name { get; set; } = null!;

        public List<Role>? Roles { get; set; } = new List<Role>();

        public List<Employee>? Employees { get; set; } = new List<Employee>();
    }
}
