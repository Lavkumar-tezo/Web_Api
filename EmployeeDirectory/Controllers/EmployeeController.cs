using AutoMapper;
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

    public class EmployeeController(IEmployeeProvider employee,IEmployeeValidator validator,IMapper mapper) : ControllerBase
    {
        private readonly IEmployeeProvider _employee = employee;
        private readonly IEmployeeValidator _employeeValidator=validator;
        private readonly IMapper _mapper=mapper;

        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            List<Employee> employees =await _employee.GetEmployees();
            if (employees.Count == 0)
            {
                return Ok("No Employee found");
            }
            List<BAL.DTO.Employee> dtoRoles = [];
            foreach (var employee in employees)
            {
                dtoRoles.Add(_mapper.Map<BAL.DTO.Employee>(employee));
            }
            return Ok(dtoRoles);
        }

        [Route("[action]/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetEmployee(string id)
        {
            try
            {
                Employee entityEmployee =await _employee.GetEmployeeById(id);
                BAL.DTO.Employee emp = _mapper.Map<BAL.DTO.Employee>(employee);
                return Ok(emp);
            }
            catch(Exception ex)
            {
                return NotFound(ex.Message);
            }                       
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] BAL.DTO.Employee dto)
        {
            if (dto == null)
            {
                return BadRequest("Invalid input data");
            }
            try
            {
                Employee emp=await _employeeValidator.ValidateEmployeeDTO(dto);
                await _employee.AddEmployee(emp);
                return Ok("Employee added");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [Route("[action]/{id}")]
        [HttpPut]
        public async Task<IActionResult> Update(string id,[FromBody] BAL.DTO.Employee dto)
        {
            if (dto == null)
            {
                return BadRequest("Invalid input data");
            }
            
            try
            {
                Employee? selectedEmp = await _employee.GetEmployeeById(id);
                Employee emp =await _employeeValidator.ValidateEmployeeDTO(dto,selectedEmp);
                await _employee.UpdateEmployee(emp);
                return Ok("Employee Updated");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [Route("[action]/{id}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _employee.DeleteEmployee(id);
                return Ok("Employee Deleted");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
