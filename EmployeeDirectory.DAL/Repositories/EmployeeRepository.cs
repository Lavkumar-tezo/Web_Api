using EmployeeDirectory.DAL.Interfaces;
using EmployeeDirectory.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeDirectory.DAL.Repositories
{
    public class EmployeeRepository(AppDbContext context) : IRepository<Employee>
    {
        private readonly AppDbContext _dbEfContext = context;

        public async Task<List<Employee>> GetAll()
        {

            List<Employee> employees =await _dbEfContext.Employees.Where(x => x.IsDeleted != true).OrderBy(emp => emp.Id).Include("Department").Include("Project").Include("Role").Include("Location").ToListAsync();
            return employees;
        }

        public async Task<Employee> Get(string empId)
        {
            Employee? employee = await _dbEfContext.Employees.FirstOrDefaultAsync(e => e.Id.ToLower() == empId.ToLower());
            if(employee == null)
            {
                throw new Exception("Selected Employee Not found");
            }
            return employee;
        }
        public async Task<Employee> GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public async Task Add(Employee newEmp)
        {
            await _dbEfContext.Employees.AddAsync(newEmp);
            await _dbEfContext.SaveChangesAsync();
        }

        public async Task Update(Employee updatedEmp)
        {
            _dbEfContext.Entry(updatedEmp).State = EntityState.Modified;
            await _dbEfContext.SaveChangesAsync();
        }

        public async Task Delete(string id)
        {
            Employee? emp = await _dbEfContext.Employees.FirstOrDefaultAsync(emp => emp.Id == id)!;
            if (emp != null)
            {
                emp.IsDeleted = true;
                _dbEfContext.SaveChanges();
                return;
            }
            throw new Exception("Selected employee not found");
        }

    }
}
