namespace EmployeeExpensesClaim.ViewModels
{
    public class ExpenseClaimViewModel
    {
        public int ClaimId { get; set; }
        public int EmpId { get; set; }
        public string ExpenseType { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime ExpenseDate { get; set; }
        public string? ExpenseDescription { get; set; }
        public string? ExpenseStatus { get; set; }
        public string? Comment { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
    }
}
