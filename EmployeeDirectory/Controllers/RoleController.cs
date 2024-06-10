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
    [Authorize]
    public class RoleController(IRoleProvider role,IRoleValidator val,IMapper mapper) : Controller
    {
        private readonly IRoleProvider _role = role;
        private readonly IRoleValidator _roleValidator=val;
        private readonly IMapper _mapper=mapper;
        
        [Route("[action]")]
        [HttpGet]
        [Authorize]
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
