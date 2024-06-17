using EmployeeDirectory.BAL.Interfaces.Providers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeDirectory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class DepartmentController(IProvider<DAL.Models.Department> dept) : ControllerBase
    {
        private readonly IProvider<DAL.Models.Department> _dept = dept;

        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> GetDepartments()
        {
            List<DAL.Models.Department> departments = await _dept.GetList();
            if (departments.Count == 0)
            {
                return Ok();
            }
            List<BAL.DTO.Department> departmentDTOs = departments.Select(dept => new BAL.DTO.Department(dept.Id, dept.Name)).ToList();
            return Ok(departmentDTOs);
        }
    }
}
