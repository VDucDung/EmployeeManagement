namespace EmployeeManagement.Services.Department
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentDto>> GetAllDepartmentsAsync();
        Task<DepartmentDto> GetDepartmentByIdAsync(int id);
        Task<DepartmentDto> CreateDepartmentAsync(CreateDepartmentDto model);
        Task<DepartmentDto> UpdateDepartmentAsync(int id, CreateDepartmentDto model);
        Task<bool> DeleteDepartmentAsync(int id);
    }
}
