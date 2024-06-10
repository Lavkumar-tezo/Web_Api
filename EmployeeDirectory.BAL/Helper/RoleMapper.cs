using EmployeeDirectory.DAL.Models;
using EmployeeDirectory.BAL.Interfaces.Helpers;
namespace EmployeeDirectory.BAL.Helper
{
    public class RoleMapper:IRoleMapper
    {
        public Role MapToRoleEntity(DTO.Role dto, List<Department> departments, List<Location> locations, string id)
        {
            return new Role
            {
                Id = id,
                Name = dto.Name,
                Departments = departments,
                Locations = locations,
                Description = dto.Description
            };
        }

        public DTO.Role MapToRoleDTO(Role entity)
        {
            return new DTO.Role
            {
                Name = entity.Name,
                Departments = entity.Departments.Select(d => d.Name).ToArray(),
                Locations = entity.Locations.Select(l => l.Name).ToArray(),
                Description = entity.Description
            };
        }
    }
}
