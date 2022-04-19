namespace api.Models
{
    public class User
    {
        public int UserID { get; set; } //this
        public string? Password { get; set; }
        public int SSN { get; set; }
        public string? FirstName { get; set; } //this
        public string? MiddleName { get; set; } //this
        public string? LastName { get; set; } //this
        public string? Email { get; set; } //this
        public DateTime StartDate { get; set; } //this
        public string? Salary { get; set; } //this
        public DateTime ShiftStart { get; set; }
        public DateTime Birthday { get; set; }
        public string? Phone { get; set; } //this
        public string? URL { get; set; } //this
        public string? Admin { get; set; } //this
        public string? Username { get; set; } //this?
        public int DepartmentID { get; set; } //this as department name
    }
}