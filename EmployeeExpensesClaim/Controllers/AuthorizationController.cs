using EmployeeExpensesClaim.BusinessAccessLayer.Interfaces;
using EmployeeExpensesClaim.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeExpensesClaim.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorizationController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthorizationController(IAuthService authService)
        {
            _authService = authService;
        }




        //1. Valid empId with user , admin , null
        //2.in valid empId
        // POST: api/Authorization/assign-role
        [HttpPatch("assign-admin/{empId}")]
        //[Authorize(Roles = "Admin")] // Only admins can assign other admins
        public async Task<IActionResult> AssignAdminRole(int empId)
        {
            try
            {
                var result = await _authService.AssignAdminRoleAsync(empId);

                if (!result.Status)
                {
                    return NotFound(new
                    {
                        message = result.Message,
                        error = result.Error
                    });
                }

                return Ok(new
                {
                    message = result.Message,
                    data = result.Data
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An unexpected error occurred.",
                    error = ex.Message
                });
            }
        }

    }
}
