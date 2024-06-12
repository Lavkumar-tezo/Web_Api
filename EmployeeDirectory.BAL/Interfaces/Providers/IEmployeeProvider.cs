using Employee = EmployeeDirectory.DAL.Models.Employee;
namespace EmployeeDirectory.BAL.Interfaces.Providers
{
    public interface IEmployeeProvider
    {
        public Task AddEmployee(Employee employee);

        public Task<List<Employee>> GetEmployees();

        public Task<List<Employee>> GetManagers();

        public Task<Employee> GetEmployeeById(string id);

        public Task UpdateEmployee(Employee emp);

        public Task DeleteEmployee(string id);
    }
}
