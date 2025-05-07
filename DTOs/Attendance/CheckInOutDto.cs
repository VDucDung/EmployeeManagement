using System.ComponentModel.DataAnnotations;

public class CheckInOutDto
{
    [Required]
    public int EmployeeId { get; set; }

    public string Notes { get; set; }
}