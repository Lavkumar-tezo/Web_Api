using EmployeeDirectory.BAL.Interfaces.Providers;
using EmployeeDirectory.DAL.Interfaces;
using EmployeeDirectory.DAL.Models;
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
        public async Task<IActionResult> Login(string id,string email)
        {
            List<Employee> employees = await _employee.GetEmployees();
            employees = employees.Where(emp => emp.IsManager == true).ToList();
            Employee? user= employees.FirstOrDefault(emp=>string.Equals(id.ToUpper(),emp.Id.ToUpper()) && string.Equals(email.ToLower(),emp.Email.ToLower()));
            if(user==null)
            {
                return BadRequest("Login Failed");
            }
            var token = GenerateToken(user);
            return Ok(token);
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
                   new Claim("Email",emp.Email)
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
