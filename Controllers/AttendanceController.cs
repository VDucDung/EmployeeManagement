using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceService _attendanceService;

        public AttendanceController(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }

        [HttpPost("checkin")]
        public async Task<ActionResult<AttendanceDto>> CheckIn([FromBody] CheckInOutDto model)
        {
            try
            {
                var attendance = await _attendanceService.CheckInAsync(model);
                return Ok(attendance);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("checkout")]
        public async Task<ActionResult<AttendanceDto>> CheckOut([FromBody] CheckInOutDto model)
        {
            try
            {
                var attendance = await _attendanceService.CheckOutAsync(model);
                return Ok(attendance);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("employee/{employeeId}")]
        public async Task<ActionResult<IEnumerable<AttendanceDto>>> GetAttendanceByEmployee(
            int employeeId,
            [FromQuery] DateTime? fromDate,
            [FromQuery] DateTime? toDate)
        {
            var attendances = await _attendanceService.GetAttendanceByEmployeeIdAsync(employeeId, fromDate, toDate);
            return Ok(attendances);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<AttendanceDto>>> GetAllAttendance(
            [FromQuery] DateTime? fromDate,
            [FromQuery] DateTime? toDate)
        {
            var attendances = await _attendanceService.GetAllAttendanceAsync(fromDate, toDate);
            return Ok(attendances);
        }
    }
}
