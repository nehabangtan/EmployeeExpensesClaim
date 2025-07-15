using System;
using System.Collections.Generic;

namespace EmployeeExpensesClaim.DataAccessLayer.Entities;

public partial class Employee
{
    public int EmpId { get; set; }

    public string EmpCode { get; set; } = null!;

    public string? EmpName { get; set; }

    public string? PhoneNo { get; set; }

    public string? Email { get; set; }

    public string? EmpRole { get; set; }

    public string? PasswordHash { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public int? ModifiedBy { get; set; }

    public virtual ICollection<AuditLog> AuditLogs { get; set; } = new List<AuditLog>();

    public virtual ICollection<ExpenseClaim> ExpenseClaims { get; set; } = new List<ExpenseClaim>();
}
