using EmployeeDirectory.BAL.DTO;
using EmployeeDirectory.BAL.Interfaces.Providers;
using EmployeeDirectory.BAL.Interfaces.Validators;
using EmployeeDirectory.DAL.Models;
using System.Data;

namespace EmployeeDirectory.BAL.Validators
{
    public class RoleValidator(IValidator val,IProvider<Department> dept,IProvider<Location> loc, IRoleProvider role):IRoleValidator
    {
        private readonly IValidator _validator=val;
        private readonly IProvider<Department> _dept = dept;
        private readonly IProvider<Location> _loc = loc;
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
            if (roles.Any(role=> string.Equals(role.Name.ToLower(),dto.Name.ToLower())))
            {
                throw new Exception("Role Already Exists");
            }
            role.Name=dto.Name;
            List<Department> departments =await _dept.GetList();
            List<Department> selectedDepartments = new List<Department>();
            foreach (var dept in dto.Departments)
            {
                var department = departments.FirstOrDefault(d => string.Equals(d.Name.ToLower(), dept.ToLower()));
                if (department == null)
                {
                    throw new Exception($"Department '{dept}' not found");
                }
                selectedDepartments.Add(department);
            }
            role.Departments= selectedDepartments;
            List<Location> locations = await _loc.GetList();
            List<Location> selectedLocations = new List<Location>();
            foreach (var loc in dto.Locations)
            {
                var location = locations.FirstOrDefault(l => string.Equals(l.Name.ToLower(), loc.ToLower()));
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
