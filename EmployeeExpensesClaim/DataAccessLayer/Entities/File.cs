using System;
using System.Collections.Generic;

namespace EmployeeExpensesClaim.DataAccessLayer.Entities;

public partial class File
{
    public int Id { get; set; }

    public int ClaimId { get; set; }

    public string? FilePath { get; set; }

    public DateTime? UploadedDate { get; set; }

    public int? UploadedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public int? ModifiedBy { get; set; }

    public virtual ExpenseClaim Claim { get; set; } = null!;
}
