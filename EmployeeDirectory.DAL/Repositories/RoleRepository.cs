using EmployeeDirectory.DAL.Models;
using Microsoft.EntityFrameworkCore;
using EmployeeDirectory.DAL.Interfaces;

namespace EmployeeDirectory.DAL.Repositories
{
    public class RoleRepository(AppDbContext context) :IRepository<Role>
    {
        private readonly AppDbContext _dbEfContext = context;

        public async Task<List<Role>> GetAll()
        {
            List<Role> roles =await _dbEfContext.Roles.OrderBy(role => role.Id).Include(r => r.Departments).Include(r => r.Locations).ToListAsync();
            return roles;
        }

        public async Task Add(Role newRole)
        {
           await _dbEfContext.Roles.AddAsync(newRole);
           await _dbEfContext.SaveChangesAsync();
        }

        public async Task<Role> Get(string roleId)
        {
            Role? role =await _dbEfContext.Roles.Include(r => r.Departments).Include(r => r.Locations).FirstOrDefaultAsync(role => role.Id.ToLower() == roleId.ToLower());
            if (role == null)
            {
                throw new Exception("Selected Role not found");
            }
            return role;
        }

        public async Task<Role> GetByName(string name)
        {
            Role? role = await _dbEfContext.Roles.FirstOrDefaultAsync(role => role.Name.ToLower() == name.ToLower());
            if (role == null)
            {
                throw new Exception("Selected Role not found");
            }
            return role;
        }

        public async Task Delete(string roleId)
        {
            throw new NotImplementedException();
        }

        public async Task Update(Role role)
        {
            throw new NotImplementedException();
        }

    }
}
