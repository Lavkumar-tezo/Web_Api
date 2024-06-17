using EmployeeDirectory.DAL.Interfaces;
using EmployeeDirectory.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeDirectory.DAL.Repositories
{
    public class ProjectRepository(AppDbContext context) :IRepository<Project>
    {
        private readonly AppDbContext _dbEfContext = context;

        public async Task<List<Project>> GetAll()
        {
            List<Project> projects = await _dbEfContext.Projects.OrderBy(project => project.Id).ToListAsync();
            return projects;
        }

        public async Task<Project> Get(string id)
        {
            Project? project = await _dbEfContext.Projects.FirstOrDefaultAsync(loc => loc.Id.Equals(id));
            if (project == null)
            {
                throw new Exception("Selected Project not found");
            }
            return project;
        }

        public async Task<Project> GetByName(string name)
        {
            Project? project = await _dbEfContext.Projects.FirstOrDefaultAsync(loc => string.Equals(loc.Name,name));
            if (project == null)
            {
                throw new Exception("Selected Project not found");
            }
            return project;
        }

        public async Task Delete(string id)
        {
            throw new NotImplementedException();
        }

        public async Task Update(Project project)
        {
            throw new NotImplementedException();
        }

        public async Task Add(Project project)
        {
            throw new NotImplementedException();
        }

        
    }
}
