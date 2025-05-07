using System.ComponentModel.DataAnnotations.Schema;

public class Employee
{
    public int EmployeeId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; }
    public int? DepartmentId { get; set; }
    public int RoleId { get; set; }
    public DateTime HireDate { get; set; }
    public bool IsActive { get; set; }

    // Navigation properties
    public virtual Department Department { get; set; }
    public virtual Role Role { get; set; }
    public virtual ICollection<Attendance> Attendances { get; set; }

    // Thuộc tính không map với cơ sở dữ liệu
    [NotMapped]
    public string FullName => $"{FirstName} {LastName}";
}