using EmployeeManagement.Services.Department;
using Microsoft.EntityFrameworkCore;

    public class DepartmentService : IDepartmentService
{
        private readonly AppDbContext _context;

        public DepartmentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DepartmentDto>> GetAllDepartmentsAsync()
        {
            return await _context.Departments
                .Select(d => new DepartmentDto
                {
                    DepartmentId = d.DepartmentId,
                    Name = d.Name,
                    Description = d.Description,
                    CreatedDate = d.CreatedDate,
                    IsActive = d.IsActive,
                    EmployeeCount = d.Employees.Count
                })
                .ToListAsync();
        }

        public async Task<DepartmentDto> GetDepartmentByIdAsync(int id)
        {
            var department = await _context.Departments
                .Include(d => d.Employees)
                .FirstOrDefaultAsync(d => d.DepartmentId == id);

            if (department == null)
                return null;

            return new DepartmentDto
            {
                DepartmentId = department.DepartmentId,
                Name = department.Name,
                Description = department.Description,
                CreatedDate = department.CreatedDate,
                IsActive = department.IsActive,
                EmployeeCount = department.Employees.Count
            };
        }

        public async Task<DepartmentDto> CreateDepartmentAsync(CreateDepartmentDto model)
        {
            var department = new Department
            {
                Name = model.Name,
                Description = model.Description,
                CreatedDate = DateTime.Now,
                IsActive = true
            };

            _context.Departments.Add(department);
            await _context.SaveChangesAsync();

            return new DepartmentDto
            {
                DepartmentId = department.DepartmentId,
                Name = department.Name,
                Description = department.Description,
                CreatedDate = department.CreatedDate,
                IsActive = department.IsActive,
                EmployeeCount = 0
            };
        }

        public async Task<DepartmentDto> UpdateDepartmentAsync(int id, CreateDepartmentDto model)
        {
            var department = await _context.Departments
                .FirstOrDefaultAsync(d => d.DepartmentId == id);

            if (department == null)
                return null;

            department.Name = model.Name;
            department.Description = model.Description;

            await _context.SaveChangesAsync();

            return new DepartmentDto
            {
                DepartmentId = department.DepartmentId,
                Name = department.Name,
                Description = department.Description,
                CreatedDate = department.CreatedDate,
                IsActive = department.IsActive,
                EmployeeCount = department.Employees?.Count ?? 0
            };
        }

        public async Task<bool> DeleteDepartmentAsync(int id)
        {
            var department = await _context.Departments
                .FirstOrDefaultAsync(d => d.DepartmentId == id);

            if (department == null)
                return false;

            // Kiểm tra xem phòng ban có nhân viên không
            var hasEmployees = await _context.Employees
                .AnyAsync(e => e.DepartmentId == id);

            if (hasEmployees)
                return false;

            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();

            return true;
        }
    }
