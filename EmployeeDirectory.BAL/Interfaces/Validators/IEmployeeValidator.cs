using EmployeeDirectory.DAL.Models;
using System.Runtime.InteropServices;

namespace EmployeeDirectory.BAL.Interfaces.Validators
{
    public interface IEmployeeValidator
    {
        public Task<Employee> ValidateEmployeeDTO(DTO.Employee dto, [Optional]Employee employee);
    }
}
