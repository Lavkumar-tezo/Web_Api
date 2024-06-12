using EmployeeDirectory.BAL.Extension;
using Employee = EmployeeDirectory.DAL.Models.Employee;
using EmployeeDirectory.DAL.Interfaces;
using EmployeeDirectory.BAL.Interfaces.Providers;

namespace EmployeeDirectory.BAL.Providers
{
    public class EmployeeProvider(IRepository<Employee> employee): IEmployeeProvider
    {
        private readonly IRepository<Employee> _employee =employee;
        public async Task AddEmployee(Employee employee)
        {
           await _employee.Add(employee);
        }

        public async Task<List<Employee>> GetEmployees()
        {
            List<Employee> employees= await _employee.GetAll();
           return employees;
        }

        public async Task<List<Employee>> GetManagers()
        {
            List<Employee> employees = await _employee.GetAll();
            employees = employees.Where(x=> x.IsManager==true).ToList();
            return employees;
        }

        public async Task<Employee> GetEmployeeById(string id)
        {
            id = id.ToUpper();
            Employee emp =await _employee.Get(id);
            if (employee == null)
            {
                throw new Exception($"Employee with ID {id} not found.");
            }
            SelectedEmployee.Id = emp.Id;
            SelectedEmployee.DeptName = emp.Department.Name;
            SelectedEmployee.LocName = emp.Location.Name;
            SelectedEmployee.RoleName = emp.Role.Name;
            return emp;

        }

        public async Task UpdateEmployee(Employee emp)
        {
           await _employee.Update(emp);

        }

        public async Task DeleteEmployee(string id)
        {
           await _employee.Delete(id);

        }

        public async Task<string> GenerateEmpId()
        {
           List<Employee> employees =await _employee.GetAll();
           if (employees.Count == 0)
           {
               return "TZ0001";
           }
           Employee employeeWithMaxId = employees.OrderByDescending(e => e.Id).FirstOrDefault()!;
           string LastEmpId = employeeWithMaxId.Id;
           int lastEmpNumber = int.Parse(LastEmpId[2..]);
           lastEmpNumber++;
           string newId = "TZ" + lastEmpNumber.ToString("D4");
           return newId;
        }

    }
}
