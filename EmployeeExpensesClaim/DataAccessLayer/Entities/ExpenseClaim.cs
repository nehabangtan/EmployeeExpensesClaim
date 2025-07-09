using System;
using System.Collections.Generic;

namespace EmployeeExpensesClaim.DataAccessLayer.Entities;

public partial class ExpenseClaim
{
    public int Id { get; set; }

    public int EmpId { get; set; }

    public string? ExpenseType { get; set; }

    public decimal? Amount { get; set; }

    public DateTime? ExpenseDate { get; set; }

    public string? ExpenseDescription { get; set; }

    public string? ExpenseStatus { get; set; }

    public string? Comment { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public int? ModifiedBy { get; set; }

    public virtual ICollection<AuditLog> AuditLogs { get; set; } = new List<AuditLog>();

    public virtual Employee Emp { get; set; } = null!;

    public virtual ICollection<FilesTable> FilesTables { get; set; } = new List<FilesTable>();
}
