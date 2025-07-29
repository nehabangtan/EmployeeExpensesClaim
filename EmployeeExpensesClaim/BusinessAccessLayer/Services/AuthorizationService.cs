using AutoMapper;
using EmployeeExpensesClaim.BusinessAccessLayer.Interfaces;
using EmployeeExpensesClaim.DataAccessLayer.Entities;
using EmployeeExpensesClaim.DataAccessLayer.Repositories.Interfaces;
using EmployeeExpensesClaim.Helpers;
using EmployeeExpensesClaim.ViewModels;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace EmployeeExpensesClaim.BusinessAccessLayer.Services
{
    public class AuthorizationService : IAuthService
    {
        private readonly IGenericRepository<Employee> _employeeRepo;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        
        public AuthorizationService(IGenericRepository<Employee> employeeRepo, IMapper mapper, IConfiguration config)
        {
            _employeeRepo = employeeRepo;
            _mapper = mapper;
            _config = config;
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

        public async Task<string> Authenticate(EmployeeViewModel user)
        {
            var employee = await _employeeRepo.GetByIdAsync(user.EmpId);

            if (employee == null || !PasswordHasher.Verify(user.PasswordHash, employee.PasswordHash, user.EmpCode, employee.EmpCode))
                return null;

            var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);

            var claims = new[]
            {
            new Claim(ClaimTypes.Name, user.EmpCode),
            new Claim(ClaimTypes.Role, user.EmpRole),
        };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(60),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

}
