using EmployeeExpensesClaim.DataAccessLayer.Entities;

namespace EmployeeExpensesClaim.ViewModels
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }

        public string EmpCode { get; set; } = null!;

        public string? EmpName { get; set; }

        public string? PhoneNo { get; set; }

        public string? Email { get; set; }

        public string? EmpRole { get; set; } = "User";

        public string? PasswordHash { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedBy { get; set; }

    }
}
