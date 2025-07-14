using AutoMapper;
using EmployeeExpensesClaim.BusinessAccessLayer.Interfaces;
using EmployeeExpensesClaim.DataAccessLayer.Entities;
using EmployeeExpensesClaim.DataAccessLayer.Repositories.Interfaces;
using EmployeeExpensesClaim.ViewModels;

namespace EmployeeExpensesClaim.BusinessAccessLayer.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<ResponseViewModel<EmployeeViewModel>> GetEmployeeByIdAsync(int id)
        {
            var employee = await _employeeRepository.GetEmployeeByIdRepoAsync(id);

            if (employee == null)
            {
                return new ResponseViewModel<EmployeeViewModel>
                {
                    Status = false,
                    Message = "Employee not found."
                };
            }

            var employeeVM = _mapper.Map<EmployeeViewModel>(employee);

            return new ResponseViewModel<EmployeeViewModel>
            {
                Status = true,
                Message = "Employee fetched successfully.",
                Data = employeeVM
            };
        }


        public async Task<ResponseViewModel<EmployeeViewModel>> CreateEmployeeAsync(EmployeeViewModel employeeViewModel)
        {
            var response = new ResponseViewModel<EmployeeViewModel>();

            try
            {
                var existingEmpRecord = await _employeeRepository.GetEmployeeByIdRepoAsync(employeeViewModel.Id);

                if (existingEmpRecord == null)
                {
                    var newEmp = _mapper.Map<Employee>(employeeViewModel);
                    newEmp.CreatedBy =int.Parse(employeeViewModel.EmpCode);
                    newEmp.CreatedDate = DateTime.UtcNow;
                    await _employeeRepository.CreateRepoAsync(newEmp);

                    response.Status = true;
                    response.Message = "Employee created successfully.";
                    response.Data = _mapper.Map<EmployeeViewModel>(newEmp);
                }
                else
                {
                    var createdDate = existingEmpRecord.CreatedDate;
                    var createdBy = existingEmpRecord.CreatedBy;
                    _mapper.Map(employeeViewModel, existingEmpRecord);
                    existingEmpRecord.ModifiedBy = employeeViewModel.Id;
                    existingEmpRecord.ModifiedDate = DateTime.UtcNow;
                    existingEmpRecord.CreatedDate = createdDate;
                    existingEmpRecord.CreatedBy = createdBy;

                    await _employeeRepository.UpdateRepoAsync(existingEmpRecord);

                    response.Status = true;
                    response.Message = "Employee updated successfully.";
                    response.Data = _mapper.Map<EmployeeViewModel>(existingEmpRecord);
                }
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = "An error occurred while saving the employee.";
                response.Error = ex.Message;
            }

            return response;
        }


        public async Task<ResponseViewModel<EmployeeViewModel>> DeleteEmployeeByIdAsync(int id)
        {
            var response = new ResponseViewModel<EmployeeViewModel>();
            try
            {
                var employee = await _employeeRepository.GetEmployeeByIdRepoAsync(id);
                if (employee == null)
                {
                    response.Status = false;
                    response.Message = "Employee not found.";
                    return response;
                }
                _employeeRepository.DeleteRepoAsync(employee);
                response.Status = true;
                response.Message = "Employee deleted successfully.";
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = "An error occurred while deleting the employee.";
                response.Error = ex.Message;
            }
            return response;
        }
    }

}

