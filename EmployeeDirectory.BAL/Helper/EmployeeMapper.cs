using EmployeeDirectory.BAL.Interfaces.Helpers;
using EmployeeDirectory.DAL.Models;

namespace EmployeeDirectory.BAL.Helper
{
    public class EmployeeMapper:IEmployeeMapper
    {
        public Employee MapToEmployeeEntity(DTO.Employee dto, Department department, Location location, Role role, Project? project = null, Employee? manager = null,bool isManager=false)
        {
            return new Employee
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                JoiningDate = DateTime.Parse(dto.JoinDate),
                DepartmentId = department.Id,
                Department = department,
                LocationId = location.Id,
                Location = location,
                RoleId = role.Id,
                Role = role,
                ProjectId = project?.Id,
                Project = project,
                Mobile = dto.Mobile,
                DOB = (dto.DOB!=null)?DateTime.Parse(dto.DOB):null,
                ManagerId = manager?.Id,
                Manager = manager,
                IsManager = isManager,
                IsDeleted = false
            };
        }        

        public DTO.Employee MapToEmployeeDTO(Employee entity)
        {
            return new DTO.Employee
            {
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Email = entity.Email,
                JoinDate = entity.JoiningDate.ToString(),
                Department = entity.Department.Name,
                Location = entity.Location.Name,
                Role = entity.Role.Name,
                Project = entity.Project?.Name,
                Mobile = entity.Mobile,
                DOB = entity.DOB.ToString(),
                Manager = entity.Manager != null ? $"{entity.Manager.FirstName} {entity.Manager.LastName}" : null
            };
        }

        
    }
}
