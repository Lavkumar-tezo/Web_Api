using EmployeeDirectory.DAL.Models;
namespace EmployeeDirectory.BAL.Interfaces.Providers
{
    public interface IRoleProvider
    {
        public Task AddRole(Role role);

        public Task<List<Role>> GetRoles();

        public Task<Role> GetRole(string id);

        public Task<Role> GetRoleByName(string name);

        public Task<Dictionary<string, string>> GetIdName();


    }
}
