    public interface IAttendanceService
    {
        Task<AttendanceDto> CheckInAsync(CheckInOutDto model);
        Task<AttendanceDto> CheckOutAsync(CheckInOutDto model);
        Task<IEnumerable<AttendanceDto>> GetAttendanceByEmployeeIdAsync(int employeeId, DateTime? fromDate, DateTime? toDate);
        Task<IEnumerable<AttendanceDto>> GetAllAttendanceAsync(DateTime? fromDate, DateTime? toDate);
    }
