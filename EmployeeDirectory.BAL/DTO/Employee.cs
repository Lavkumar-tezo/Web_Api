namespace EmployeeDirectory.BAL.DTO
{
    public class Employee
    {
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Location { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string JoinDate { get; set; }=null!;

        public string Department { get; set; }=null!;

        public string Role { get; set; } = null!;

        public string? Project { get; set; }

        public string? Mobile { get; set; }

        public string? DOB { get; set; }

        public string? Manager { get; set; }
    }
}
