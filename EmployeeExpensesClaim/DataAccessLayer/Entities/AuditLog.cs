using System;
using System.Collections.Generic;

namespace EmployeeExpensesClaim.DataAccessLayer.Entities;

public partial class AuditLog
{
    public int AuditId { get; set; }

    public int EmpId { get; set; }

    public int ClaimId { get; set; }

    public string? LogDetails { get; set; }

    public int? PerformedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public int? ModifiedBy { get; set; }

    public virtual ExpenseClaim Claim { get; set; } = null!;

    public virtual Employee Emp { get; set; } = null!;
}
