
using Microsoft.EntityFrameworkCore;
using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    public class AttendanceService : IAttendanceService
{
        private readonly AppDbContext _context;

        public AttendanceService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<AttendanceDto> CheckInAsync(CheckInOutDto model)
        {
            var today = DateTime.Today;

            var existingAttendance = await _context.Attendance
                .FirstOrDefaultAsync(a => a.EmployeeId == model.EmployeeId && a.WorkDate == today);

            if (existingAttendance != null)
            {
                if (existingAttendance.CheckInTime.HasValue)
                {
                    throw new Exception("Bạn đã check-in cho ngày hôm nay rồi.");
                }

                existingAttendance.CheckInTime = DateTime.Now;
                existingAttendance.Notes = model.Notes;

                await _context.SaveChangesAsync();

                return await GetAttendanceDtoAsync(existingAttendance.AttendanceId);
            }

            var attendance = new Attendance
            {
                EmployeeId = model.EmployeeId,
                CheckInTime = DateTime.Now,
                WorkDate = today,
                Notes = model.Notes
            };

            _context.Attendance.Add(attendance);
            await _context.SaveChangesAsync();

            return await GetAttendanceDtoAsync(attendance.AttendanceId);
        }

        public async Task<AttendanceDto> CheckOutAsync(CheckInOutDto model)
        {
            var today = DateTime.Today;

            var existingAttendance = await _context.Attendance
                .FirstOrDefaultAsync(a => a.EmployeeId == model.EmployeeId && a.WorkDate == today);

            if (existingAttendance == null || !existingAttendance.CheckInTime.HasValue)
            {
                throw new Exception("Bạn cần check-in trước khi check-out.");
            }

            if (existingAttendance.CheckOutTime.HasValue)
            {
                throw new Exception("Bạn đã check-out cho ngày hôm nay rồi.");
            }

            existingAttendance.CheckOutTime = DateTime.Now;
            if (!string.IsNullOrEmpty(model.Notes))
            {
                existingAttendance.Notes += " | " + model.Notes;
            }

            await _context.SaveChangesAsync();

            return await GetAttendanceDtoAsync(existingAttendance.AttendanceId);
        }

        public async Task<IEnumerable<AttendanceDto>> GetAttendanceByEmployeeIdAsync(int employeeId, DateTime? fromDate, DateTime? toDate)
        {
            var query = _context.Attendance
                .Include(a => a.Employee)
                .AsQueryable();

            if (fromDate.HasValue)
            {
                query = query.Where(a => a.WorkDate >= fromDate.Value);
            }

            if (toDate.HasValue)
            {
                query = query.Where(a => a.WorkDate <= toDate.Value);
            }

            var attendances = await query
                .OrderByDescending(a => a.WorkDate)
                .ToListAsync();

            return attendances.Select(a => new AttendanceDto
            {
                AttendanceId = a.AttendanceId,
                EmployeeId = a.EmployeeId,
                EmployeeName = $"{a.Employee.FirstName} {a.Employee.LastName}",
                CheckInTime = a.CheckInTime,
                CheckOutTime = a.CheckOutTime,
                WorkDate = a.WorkDate,
                Notes = a.Notes
            });
        }


        public async Task<IEnumerable<AttendanceDto>> GetAllAttendanceAsync(DateTime? fromDate, DateTime? toDate)
        {
            var query = _context.Attendance
                .Include(a => a.Employee)
                .AsQueryable();
            if (fromDate.HasValue)
            {
                query = query.Where(a => a.WorkDate >= fromDate.Value);
            }

            if (toDate.HasValue)
            {
                query = query.Where(a => a.WorkDate <= toDate.Value);
            }

            var attendances = await query
                .OrderByDescending(a => a.WorkDate)
                .ThenBy(a => a.Employee.LastName)
                .ThenBy(a => a.Employee.FirstName)
                .ToListAsync();

            return attendances.Select(a => new AttendanceDto
            {
                AttendanceId = a.AttendanceId,
                EmployeeId = a.EmployeeId,
                EmployeeName = $"{a.Employee.FirstName} {a.Employee.LastName}",
                CheckInTime = a.CheckInTime,
                CheckOutTime = a.CheckOutTime,
                WorkDate = a.WorkDate,
                Notes = a.Notes
            });
        }

        private async Task<AttendanceDto> GetAttendanceDtoAsync(int attendanceId)
        {
            var attendance = await _context.Attendance
                .Include(a => a.Employee)
                .FirstOrDefaultAsync(a => a.AttendanceId == attendanceId);

            if (attendance == null)
                return null;

            return new AttendanceDto
            {
                AttendanceId = attendance.AttendanceId,
                EmployeeId = attendance.EmployeeId,
                EmployeeName = $"{attendance.Employee.FirstName} {attendance.Employee.LastName}",
                CheckInTime = attendance.CheckInTime,
                CheckOutTime = attendance.CheckOutTime,
                WorkDate = attendance.WorkDate,
                Notes = attendance.Notes
            };
        }
    }
