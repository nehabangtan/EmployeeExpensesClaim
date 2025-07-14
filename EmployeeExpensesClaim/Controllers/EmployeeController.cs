using EmployeeExpensesClaim.BusinessAccessLayer.Interfaces;
using EmployeeExpensesClaim.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _employeeService;

    public EmployeeController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    //[Authorize(Roles ="Admin")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetEmployee(int id)
    {
        var result = await _employeeService.GetEmployeeByIdAsync(id);

        if (!result.Status)
        {
            return NotFound(new
            {
                message = result.Message
            });
        }

        return Ok(new
        {
            message = result.Message,
            data = result.Data
        });
    }


    [HttpPost]
    public async Task<IActionResult> CreateEmployee([FromBody] EmployeeViewModel employeeViewModel)
    {
        if (employeeViewModel == null)
        {
            return BadRequest(new
            {
                message = "Invalid employee data."
            });
        }

        try
        {
            var result = await _employeeService.CreateEmployeeAsync(employeeViewModel);

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
                message = "An unexpected error occurred while creating the employee.",
                error = ex.Message
            });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEmployee(int id)
    {
        try
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (!employee.Status)
            {
                return NotFound(new
                {
                    message = employee.Message
                });
            }
            // Assuming you have a method to delete the employee in your service
            await _employeeService.DeleteEmployeeByIdAsync(id);
            return Ok(new
            {
                message = "Employee deleted successfully."
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                message = "An unexpected error occurred while deleting the employee.",
                error = ex.Message
            });
        }
    }
}
