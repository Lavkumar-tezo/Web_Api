using EmployeeDirectory.BAL.DTO;
using EmployeeDirectory.BAL.Interfaces.Providers;
using EmployeeDirectory.BAL.Interfaces.Validators;
using EmployeeDirectory.DAL.Models;
using System.Data;

namespace EmployeeDirectory.BAL.Validators
{
    public class RoleValidator(IValidator val,IProvider<DAL.Models.Department> dept,IProvider<DAL.Models.Location> loc, IRoleProvider role):IRoleValidator
    {
        private readonly IValidator _validator=val;
        private readonly IProvider<DAL.Models.Department> _dept = dept;
        private readonly IProvider<DAL.Models.Location> _loc = loc;
        private readonly IRoleProvider _role=role;

        private async Task<string> GenerateRoleId()
        {
            List<DAL.Models.Role> roles =await _role.GetRoles();
            if (roles.Count == 0)
            {
                return "IN001";
            }
            string LastRoleId = roles[^1].Id ?? "";
            int lastRoleNumber = int.Parse(LastRoleId[2..]);
            lastRoleNumber++;
            string newId = "IN" + lastRoleNumber.ToString("D3");
            return newId;
        }

        public async Task<DAL.Models.Role> ValidateRoleDTO(DTO.Role dto)
        {
            
            DAL.Models.Role role= new();
            if (!_validator.IsAlphabeticSpace(dto.Name))
            {
                throw new Exception("Role name should have alphabets and space only");
            } ;
            List<DAL.Models.Role> roles = await _role.GetRoles();
            if (roles.Any(role => string.Equals(role.Id.ToLower(), dto.Id.ToLower())))
            {
                throw new Exception("Role Already Exists with given id");
            }
            if (roles.Any(role=> string.Equals(role.Name.ToLower(),dto.Name.ToLower())))
            {
                throw new Exception("Role Already Exists with given name");
            }
            role.Name=dto.Name;
            List<DAL.Models.Department> departments =await _dept.GetList();
            List<DAL.Models.Department> selectedDepartments = new List<DAL.Models.Department>();
            foreach (var dept in dto.Departments)
            {
                var department = departments.FirstOrDefault(d => string.Equals(d.Id.ToLower(), dept.ToLower()));
                if (department == null)
                {
                    throw new Exception($"Department '{dept}' not found");
                }
                selectedDepartments.Add(department);
            }
            role.Departments= selectedDepartments;
            List<DAL.Models.Location> locations = await _loc.GetList();
            List<DAL.Models.Location> selectedLocations = new List<DAL.Models.Location>();
            foreach (var loc in dto.Locations)
            {
                var location = locations.FirstOrDefault(l => string.Equals(l.Id.ToLower(), loc.ToLower()));
                if (location == null)
                {
                    throw new Exception($"Location '{loc}' not found");
                }
                selectedLocations.Add(location);
            }
            role.Locations= selectedLocations;
            role.Id=await GenerateRoleId();
            role.Description=dto.Description;
            return role;
        }
    }
}
