public class Department
{
    public int DepartmentId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedDate { get; set; }
    public bool IsActive { get; set; }

    // Navigation property
    public virtual ICollection<Employee> Employees { get; set; }
}