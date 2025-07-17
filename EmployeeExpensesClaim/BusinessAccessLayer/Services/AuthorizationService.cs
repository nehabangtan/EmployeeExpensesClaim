using AutoMapper;
using EmployeeExpensesClaim.BusinessAccessLayer.Interfaces;
using EmployeeExpensesClaim.DataAccessLayer.Entities;
using EmployeeExpensesClaim.DataAccessLayer.Repositories.Interfaces;
using EmployeeExpensesClaim.Helpers;
using EmployeeExpensesClaim.ViewModels;

namespace EmployeeExpensesClaim.BusinessAccessLayer.Services
{
    public class AuthorizationService : IAuthService
    {
        private readonly IGenericRepository<Employee> _employeeRepo;
        private readonly IMapper _mapper;

        public AuthorizationService(IGenericRepository<Employee> employeeRepo, IMapper mapper)
        {
            _employeeRepo = employeeRepo;
            _mapper = mapper;
        }

        public async Task<ResponseViewModel<EmployeeViewModel>> AssignAdminRoleAsync(int empId)
        {
            var employee = await _employeeRepo.GetByIdAsync(empId);

            if (employee == null)
                return ResponseHelper.Failure<EmployeeViewModel>("Employee not found.");

            if (employee.EmpRole?.Trim().ToLower() == Constants.AdminRole.ToLower())
                return ResponseHelper.Success("Employee already has Admin role.", _mapper.Map<EmployeeViewModel>(employee));

            try
            {
                employee.EmpRole = Constants.AdminRole;
                _employeeRepo.Update(employee);
                await _employeeRepo.SaveAsync();
                return ResponseHelper.Success("Role assigned as Admin successfully.", _mapper.Map<EmployeeViewModel>(employee));
            }
            catch (Exception ex)
            {
                return ResponseHelper.Failure<EmployeeViewModel>("An error occurred while assigning Admin role.", ex.Message);
            }
        }
    }

}
