﻿public class Role
{
    public int RoleId { get; set; }
    public string Name { get; set; }

    // Navigation property
    public virtual ICollection<Employee> Employees { get; set; }
}
