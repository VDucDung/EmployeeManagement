namespace EmployeeManagement.Services.Employee
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync();
        Task<EmployeeDto> GetEmployeeByIdAsync(int id);
        Task<EmployeeDto> CreateEmployeeAsync(CreateEmployeeDto model);
        Task<EmployeeDto> UpdateEmployeeAsync(int id, CreateEmployeeDto model);
        Task<bool> DeleteEmployeeAsync(int id);
        Task<bool> ChangeEmployeeDepartmentAsync(int employeeId, int departmentId);
    }
}
