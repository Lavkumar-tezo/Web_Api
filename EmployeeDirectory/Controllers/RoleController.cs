using AutoMapper;
using EmployeeDirectory.BAL.Helper;
using EmployeeDirectory.BAL.DTO;
using EmployeeDirectory.BAL.Interfaces.Helpers;
using EmployeeDirectory.BAL.Interfaces.Providers;
using EmployeeDirectory.BAL.Interfaces.Validators;
using EmployeeDirectory.DAL.Interfaces;
using EmployeeDirectory.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeDirectory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class RoleController(IRoleProvider role,IRoleValidator val,IMapper mapper,IProvider<DAL.Models.Department> dept, IProvider<DAL.Models.Location> loc) : Controller
    {
        private readonly IRoleProvider _role = role;
        private readonly IRoleValidator _roleValidator=val;
        private readonly IProvider<DAL.Models.Department> _dept = dept;
        private readonly IProvider<DAL.Models.Location> _loc = loc;
        private readonly IMapper _mapper=mapper;
        
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            List<DAL.Models.Role> roles =await _role.GetRoles();
            if (roles.Count == 0)
            {
                return Ok("No Role found");
            }
            List<BAL.DTO.Role> dtoRoles = [];
            foreach (var item in roles)
            {
                dtoRoles.Add(_mapper.Map<BAL.DTO.Role>(item));
            }
            return Ok(dtoRoles);
        }

        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> GetAllRoleNames()
        {
            List<DAL.Models.Role> roles = await _role.GetRoles();
            if (roles.Count == 0)
            {
                return Ok();
            }
            string[] names = roles.Select(role=>role.Name).ToArray();
            return Ok(names);
        }

        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> GetRoleByDeptLoc(string locationId,string departmentId)
        {
            List<DAL.Models.Role> roles = await _role.GetRoles();
            if (roles.Count == 0)
            {
                return Ok();
            }
            
            var filteredRoles = roles
            .Where(role => role.Departments
                .Any(dept => dept.Id.Equals(departmentId, StringComparison.OrdinalIgnoreCase)))
            .ToList();
            filteredRoles = filteredRoles
            .Where(role => role.Locations
                .Any(dept => dept.Id.Equals(locationId, StringComparison.OrdinalIgnoreCase)))
            .ToList();
            List<BAL.DTO.Role> dtoRoles = [];
            foreach (var item in filteredRoles)
            {
                dtoRoles.Add(_mapper.Map<BAL.DTO.Role>(item));
            }
            return Ok(dtoRoles);
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] BAL.DTO.Role dto)
        {
            if (dto == null)
            {
                return BadRequest("Invalid input data");
            }
            try
            {
                DAL.Models.Role role=await _roleValidator.ValidateRoleDTO(dto);
                await _role.AddRole(role);
                return Ok("Role Added");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
    }
}
