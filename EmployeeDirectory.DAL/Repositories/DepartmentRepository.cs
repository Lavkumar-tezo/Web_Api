using EmployeeDirectory.DAL.Interfaces;
using EmployeeDirectory.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeDirectory.DAL.Repositories
{
    public class DepartmentRepository(AppDbContext context):IRepository<Department>
    {
        private readonly AppDbContext _dbEfContext = context;

        public async Task<List<Department>> GetAll()
        {
            List<Department> departments= await _dbEfContext.Departments.OrderBy(dept => dept.Id).ToListAsync();
            return departments;
        }

        public async Task<Department> Get(string id)
        {
            Department? department = await _dbEfContext.Departments.FirstOrDefaultAsync(dept => dept.Id.Equals(id));
            if (department == null)
            {
                throw new Exception("Selected Department not found");
            }
            return department;
        }

        public async Task<Department> GetByName(string name)
        {
            Department? department = await _dbEfContext.Departments.FirstOrDefaultAsync(dept => string.Equals(dept.Name,name));
            if (department == null)
            {
                throw new Exception("Selected Department not found");
            }
            return department;
        }

        public Task Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Task Update(Department department)
        {
            throw new NotImplementedException();
        }

        public Task Add(Department department)
        {
            throw new NotImplementedException();
        }
    }
}
