namespace EmployeeExpensesClaim.ViewModels
{
    public class LoginModel
    {
        public int EmpId { get; set; }
        public string EmpCode { get; set; } = null!;

        public string? PasswordHash { get; set; }
    }
}
