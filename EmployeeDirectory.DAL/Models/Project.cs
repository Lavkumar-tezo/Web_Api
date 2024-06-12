using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EmployeeDirectory.DAL.Models
{
    [Table("Project")]
    public class Project
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
        public int Id { get; set; }

        [MaxLength(35), MinLength(2), Required]
        public string Name { get; set; } = null!;

        public List<Employee> Employees { get; set; } = new List<Employee>();
    }
}
