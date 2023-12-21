using BL;

namespace TransUniverseCorp.Models
{
    public class DriverFormData
    {
        public ScheduleElement? CurrentScheduleElement { get; set; }
        public string? Error { get; set; }
        public bool HasOrder { get; set; }
    }
}
