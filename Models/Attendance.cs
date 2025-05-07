public class Attendance
{
    public int AttendanceId { get; set; }
    public int EmployeeId { get; set; }
    public DateTime? CheckInTime { get; set; }
    public DateTime? CheckOutTime { get; set; }
    public DateTime WorkDate { get; set; }
    public string Notes { get; set; }

    // Navigation property
    public virtual Employee Employee { get; set; }
}