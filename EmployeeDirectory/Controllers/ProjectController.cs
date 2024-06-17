using EmployeeDirectory.BAL.DTO;
using EmployeeDirectory.BAL.Interfaces.Providers;
using EmployeeDirectory.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeDirectory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class ProjectController(IProvider<DAL.Models.Project> project) : ControllerBase
    {
        private readonly IProvider<DAL.Models.Project> _project =project;

        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> GetProjects()
        {
            List<DAL.Models.Project> projects = await _project.GetList();
            if (projects.Count == 0)
            {
                return Ok();
            }
            List<BAL.DTO.Project> projectDTOs = projects.Select(proj => new BAL.DTO.Project(proj.Id,proj.Name)).ToList();
            return Ok(projectDTOs);
        }
    }
}
