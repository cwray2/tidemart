using System.ComponentModel.DataAnnotations.Schema;
namespace api.Models
{
    public class Timesheet
    {
        public int TimesheetID { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string? Description { get; set; }
        public string? Lunch { get; set; }
        public int UserID { get; set; }
    }
}