using System.ComponentModel.DataAnnotations;

public class CreateEmployeeDto
{
    [Required]
    [StringLength(50)]
    public string FirstName { get; set; }

    [Required]
    [StringLength(50)]
    public string LastName { get; set; }

    [Required]
    [EmailAddress]
    [StringLength(100)]
    public string Email { get; set; }

    [Required]
    [StringLength(50)]
    public string Password { get; set; }

    [StringLength(20)]
    public string PhoneNumber { get; set; }

    public int? DepartmentId { get; set; }

    [Required]
    public int RoleId { get; set; }
}
