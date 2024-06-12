using EmployeeDirectory.DAL.Models;
using EmployeeDirectory.DAL.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;
using EmployeeDirectory.BAL.Interfaces.Providers;

namespace EmployeeDirectory.BAL.Providers
{
    public class RoleProvider(IRepository<Role> data, IProvider<Department> dept, IProvider<Location> loc):IRoleProvider
    {
        private readonly IRepository<Role> _role = data;
        private readonly IProvider<Department> _dept = dept;
        private readonly IProvider<Location> _loc =loc;

        public async Task AddRole(Role role)
        {
            await _role.Add(role);
        }

        public async Task<string> GenerateRoleId()
        {
            List<Role> roles =await _role.GetAll();
            if (roles.Count == 0)
            {
                return "IN001";
            }
            string LastRoleId = roles[^1].Id ?? "";
            int lastRoleNumber = int.Parse(LastRoleId[2..]);
            lastRoleNumber++;
            string newId = "IN" + lastRoleNumber.ToString("D3");
            return newId;
        }

        public async Task<List<Role>> GetRoles()
        {
            List<Role> roles= await _role.GetAll();
            return roles;
        }

        public async Task<Dictionary<string, string>> GetIdName()
        {
            List<Role> roles = await _role.GetAll();
            Dictionary<string, string> roleIdName = new Dictionary<string, string>();
            foreach (Role r in roles)
            {
                roleIdName.Add(r.Id.ToString(), r.Name);
            }
            return roleIdName;
        }

        public async Task<Role> GetRole(string id)
        {
            Role role=await _role.Get(id);
            return role;
        }

        public async Task<Role> GetRoleByName(string name)
        {
            return await _role.GetByName(name);
        }

    }
}
