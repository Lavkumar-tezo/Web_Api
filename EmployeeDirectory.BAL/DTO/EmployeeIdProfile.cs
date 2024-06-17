namespace EmployeeDirectory.BAL.DTO
{
    public class EmployeeIdProfile(string id,string img)
    {
        public string Id { get; set; } = id;
        public string img { get; set; }= img;
    }
}
