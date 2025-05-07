public class AttendanceDto
{
    public int AttendanceId { get; set; }
    public int EmployeeId { get; set; }
    public string EmployeeName { get; set; }
    public DateTime? CheckInTime { get; set; }
    public DateTime? CheckOutTime { get; set; }
    public DateTime WorkDate { get; set; }
    public string Notes { get; set; }
    public TimeSpan? WorkHours => CheckOutTime.HasValue && CheckInTime.HasValue
        ? CheckOutTime.Value - CheckInTime.Value
        : null;
}