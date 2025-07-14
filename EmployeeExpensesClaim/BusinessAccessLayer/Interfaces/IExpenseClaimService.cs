using EmployeeExpensesClaim.ViewModels;

namespace EmployeeExpensesClaim.BusinessAccessLayer.Interfaces
{
    public interface IExpenseClaimService
    {
        Task<ResponseViewModel<ExpenseClaimViewModel>> GetClaimByIdAsync(int id);
        Task<ResponseViewModel<List<ExpenseClaimViewModel>>> GetClaimsByEmployeeIdAsync(int empId);
        Task<ResponseViewModel<ExpenseClaimViewModel>> CreateClaimAsync(ExpenseClaimViewModel claimVM);
        Task<ResponseViewModel<ExpenseClaimViewModel>> UpdateClaimAsync(ExpenseClaimViewModel claimVM);
        Task<ResponseViewModel<bool>> DeleteClaimAsync(int id);
    }
}
