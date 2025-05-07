using EmployeeManagement.Services.Employee;
using Microsoft.EntityFrameworkCore;


    public class EmployeeService : IEmployeeService
{
        private readonly AppDbContext _context;

        public EmployeeService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync()
        {
            return await _context.Employees
                .Include(e => e.Department)
                .Include(e => e.Role)
                .Select(e => new EmployeeDto
                {
                    EmployeeId = e.EmployeeId,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    FullName = $"{e.FirstName} {e.LastName}",
                    Email = e.Email,
                    PhoneNumber = e.PhoneNumber,
                    DepartmentId = e.DepartmentId,
                    DepartmentName = e.Department != null ? e.Department.Name : "Chưa phân công",
                    RoleId = e.RoleId,
                    RoleName = e.Role.Name,
                    HireDate = e.HireDate,
                    IsActive = e.IsActive
                })
                .ToListAsync();
        }

        public async Task<EmployeeDto> GetEmployeeByIdAsync(int id)
        {
            var employee = await _context.Employees
                .Include(e => e.Department)
                .Include(e => e.Role)
                .FirstOrDefaultAsync(e => e.EmployeeId == id);

            if (employee == null)
                return null;

            return new EmployeeDto
            {
                EmployeeId = employee.EmployeeId,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                FullName = $"{employee.FirstName} {employee.LastName}",
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                DepartmentId = employee.DepartmentId,
                DepartmentName = employee.Department != null ? employee.Department.Name : "Chưa phân công",
                RoleId = employee.RoleId,
                RoleName = employee.Role.Name,
                HireDate = employee.HireDate,
                IsActive = employee.IsActive
            };
        }

        public async Task<EmployeeDto> CreateEmployeeAsync(CreateEmployeeDto model)
        {
            var emailExists = await _context.Employees
                .AnyAsync(e => e.Email == model.Email);

            if (emailExists)
                throw new Exception("Email đã tồn tại trong hệ thống.");

            var employee = new Employee
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(model.Password), // Hash mật khẩu
                PhoneNumber = model.PhoneNumber,
                DepartmentId = model.DepartmentId,
                RoleId = model.RoleId,
                HireDate = DateTime.Now,
                IsActive = true
            };

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            // Load role và department
            await _context.Entry(employee)
                .Reference(e => e.Role)
                .LoadAsync();

            if (employee.DepartmentId.HasValue)
            {
                await _context.Entry(employee)
                    .Reference(e => e.Department)
                    .LoadAsync();
            }

            return new EmployeeDto
            {
                EmployeeId = employee.EmployeeId,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                FullName = $"{employee.FirstName} {employee.LastName}",
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                DepartmentId = employee.DepartmentId,
                DepartmentName = employee.Department != null ? employee.Department.Name : "Chưa phân công",
                RoleId = employee.RoleId,
                RoleName = employee.Role.Name,
                HireDate = employee.HireDate,
                IsActive = employee.IsActive
            };
        }

        public async Task<EmployeeDto> UpdateEmployeeAsync(int id, CreateEmployeeDto model)
        {
            var employee = await _context.Employees
                .Include(e => e.Department)
                .Include(e => e.Role)
                .FirstOrDefaultAsync(e => e.EmployeeId == id);

            if (employee == null)
                return null;

            if (employee.Email != model.Email)
            {
                var emailExists = await _context.Employees
                    .AnyAsync(e => e.Email == model.Email);

                if (emailExists)
                    throw new Exception("Email đã tồn tại trong hệ thống.");
            }

            employee.FirstName = model.FirstName;
            employee.LastName = model.LastName;
            employee.Email = model.Email;
            employee.PhoneNumber = model.PhoneNumber;
            employee.DepartmentId = model.DepartmentId;
            employee.RoleId = model.RoleId;

            if (!string.IsNullOrEmpty(model.Password))
            {
                employee.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);
            }

            await _context.SaveChangesAsync();

            return new EmployeeDto
            {
                EmployeeId = employee.EmployeeId,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                FullName = $"{employee.FirstName} {employee.LastName}",
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                DepartmentId = employee.DepartmentId,
                DepartmentName = employee.Department != null ? employee.Department.Name : "Chưa phân công",
                RoleId = employee.RoleId,
                RoleName = employee.Role.Name,
                HireDate = employee.HireDate,
                IsActive = employee.IsActive
            };
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            var employee = await _context.Employees
                .FirstOrDefaultAsync(e => e.EmployeeId == id);

            if (employee == null)
                return false;

            employee.IsActive = false;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ChangeEmployeeDepartmentAsync(int employeeId, int departmentId)
        {
            var employee = await _context.Employees
                .FirstOrDefaultAsync(e => e.EmployeeId == employeeId);

            if (employee == null)
                return false;

            var departmentExists = await _context.Departments
                .AnyAsync(d => d.DepartmentId == departmentId);

            if (!departmentExists)
                return false;

            employee.DepartmentId = departmentId;
            await _context.SaveChangesAsync();

            return true;
        }
    }
