using EmployeeDirectory.BAL.Interfaces.Providers;
using EmployeeDirectory.DAL.Interfaces;
using EmployeeDirectory.DAL.Models;

public class DepartmentProvider(IRepository<Department> dept) : IProvider<Department>
{
    private readonly IRepository<Department> _dept = dept;

    public async Task<List<Department>> GetList()
    {
        List<Department> deptList = await _dept.GetAll();
        return deptList;
    }

    public async Task<Department> Get(string id)
    {
        int deptId = Int32.Parse(id);
        List<Department> list = await _dept.GetAll();
        Department dept = list.First(x => x.Id == deptId);
        return dept;
    }

    public async Task<Dictionary<string, string>> GetIdName()
    {
        List<Department> departments = await _dept.GetAll();
        Dictionary<string, string> deptList = new Dictionary<string, string>();
        foreach (Department d in departments)
        {
            deptList.Add(d.Id.ToString(), d.Name);
        }
        return deptList;
    }

    public async Task<Department> GetByName(string name)
    {
        return await _dept.GetByName(name);
    }
}