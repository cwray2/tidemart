namespace api.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string? Password { get; set; }
        public int SSN { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public DateTime StartDate { get; set; }
        public string? Salary { get; set; }
        public DateTime ShiftStart { get; set; }
        public DateTime Birthday { get; set; }
        public string? Phone { get; set; }
        public string? URL { get; set; }
        public string? Admin { get; set; }
        public string? Username { get; set; }
        public int DepartmentID { get; set; }
    }
}