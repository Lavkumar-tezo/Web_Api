namespace EmployeeDirectory.BAL.Interfaces.Providers
{
    public interface IProvider<T>
    {
        public Task<List<T>> GetList();

        public Task<T> Get(string id);

        public Task<T> GetByName(string name);

        public Task<Dictionary<string, string>> GetIdName();
    }
}
