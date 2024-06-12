using EmployeeDirectory.DAL.Models;

namespace EmployeeDirectory.BAL.Interfaces.Validators
{
    public interface IRoleValidator
    {
        public Task<Role> ValidateRoleDTO(DTO.Role dto);
    }
}
