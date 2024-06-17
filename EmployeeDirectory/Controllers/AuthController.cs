using EmployeeDirectory.BAL.Interfaces.Providers;
using EmployeeDirectory.DAL.Interfaces;
using EmployeeDirectory.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EmployeeDirectory.Controllers
{
    [ApiController]
    [Route("/api")]
    public class AuthController(IConfiguration config, IEmployeeProvider employee) : ControllerBase
    {
        private readonly IConfiguration _config = config;
        private readonly IEmployeeProvider _employee=employee;

        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> Login(string email,string password)
        {
            List<Employee> employees = await _employee.GetEmployees();
            Employee? user= employees.FirstOrDefault(emp=>string.Equals(email.ToUpper(),emp.Email.ToUpper()) && string.Equals(password.ToLower(),emp.Password.ToLower()));
            if(user==null)
            {
                return BadRequest("Employee not found");
            }
            var token = GenerateToken(user);
            return Ok(token);
        }

        [HttpGet("all-claims")]
        [Authorize]
        public IActionResult GetAllClaims()
        {
            var claims = User.Claims.Select(c => new { c.Type, c.Value }).ToList();
            return Ok(claims);
        }

        private string GenerateToken(Employee emp)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                   new Claim(JwtRegisteredClaimNames.Sub,_config["Jwt:sub"]!),
                   new Claim(JwtRegisteredClaimNames.Jti,new Guid().ToString()),
                   new Claim("UserId",emp.Id),
                   new Claim("Email",emp.Email),
                   new Claim("IsManager",(bool)emp.IsManager?"True":"False")
            };
            var token = new JwtSecurityToken(
             issuer: config["Jwt:iss"],
             audience: config["Jwt:aud"],
             claims: claims,
             expires: DateTime.Now.AddMinutes(10),
             signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
