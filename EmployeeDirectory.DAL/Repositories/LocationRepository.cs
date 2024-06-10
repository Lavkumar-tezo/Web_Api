using EmployeeDirectory.DAL.Interfaces;
using EmployeeDirectory.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeDirectory.DAL.Repositories
{
    public class LocationRepository(AppDbContext context) :IRepository<Location>
    {
        private readonly AppDbContext _dbEfContext = context;

        public async Task<List<Location>> GetAll()
        {
            List<Location> locations =await _dbEfContext.Locations.OrderBy(loc => loc.Id).ToListAsync();
            return locations;
        }

        public async Task<Location> Get(string id)
        {
            Location? location =await _dbEfContext.Locations.FirstOrDefaultAsync(loc => loc.Id == Int32.Parse(id));
            if(location == null)
            {
                throw new Exception("Selected Location not found");
            }
            return location;
        }

        public async Task<Location> GetByName(string name)
        {
            Location? location = await _dbEfContext.Locations.FirstOrDefaultAsync(loc => string.Equals(loc.Name,name));
            if (location == null)
            {
                throw new Exception("Selected Location not found");
            }
            return location;
        }

        public async Task Delete(string id)
        {
            throw new NotImplementedException();
        }

        public async Task Update(Location location)
        {
            throw new NotImplementedException();
        }

        public async Task Add(Location location)
        {
            throw new NotImplementedException();
        }

        
    }
}
