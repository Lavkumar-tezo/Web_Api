namespace EmployeeDirectory.BAL.DTO
{
    public class Role
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; }=null!;

        public string[] Departments { get; set; } = null!;

        public string[] Locations { get; set; } = null!;

        public string? Description { get; set; }
    }
}
