using EmployeeDirectory.BAL.Interfaces.Providers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeDirectory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class LocationController(IProvider<DAL.Models.Location> loc) : ControllerBase
    {
        private readonly IProvider<DAL.Models.Location> _loc = loc;

        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> GetLocations()
        {
            List<DAL.Models.Location> locations = await _loc.GetList();
            if (locations.Count == 0)
            {
                return Ok();
            }
            List<BAL.DTO.Location> locationDTOs = locations.Select(loc => new BAL.DTO.Location(loc.Id, loc.Name)).ToList();
            return Ok(locationDTOs);
        }
    }
}
