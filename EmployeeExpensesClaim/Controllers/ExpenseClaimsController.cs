using EmployeeExpensesClaim.BusinessAccessLayer.Interfaces;
using EmployeeExpensesClaim.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeExpensesClaim.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ExpenseClaimsController : ControllerBase
    {
        private readonly IExpenseClaimService _service;

        public ExpenseClaimsController(IExpenseClaimService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClaimById(int id)
        {
            try
            {
                var result = await _service.GetClaimByIdAsync(id);
                if (!result.Status)
                    return NotFound(new { message = result.Message });

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while fetching the claim.", error = ex.Message });
            }
        }

        [HttpGet("employee/{empId}")]
        public async Task<IActionResult> GetClaimsByEmployeeId(int empId)
        {
            try
            {
                var result = await _service.GetClaimsByEmployeeIdAsync(empId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while fetching claims for the employee.", error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateClaim([FromForm] ExpenseClaimViewModel model)
        {
            if (model == null)
                return BadRequest(new { message = "Invalid claim data." });

            try
            {
                var result = await _service.CreateClaimAsync(model);
                if (!result.Status)
                    return BadRequest(new { message = result.Message });

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating the claim.", error = ex.Message });
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateClaim([FromBody] ExpenseClaimViewModel model)
        {
            if (model == null || model.ClaimId <= 0)
                return BadRequest(new { message = "Invalid claim ID or data." });

            try
            {
                var result = await _service.UpdateClaimAsync(model);
                if (!result.Status)
                    return NotFound(new { message = result.Message });

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating the claim.", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClaim(int id)
        {
            if (id <= 0)
                return BadRequest(new { message = "Invalid claim ID." });

            try
            {
                var result = await _service.DeleteClaimAsync(id);
                if (!result.Status)
                    return NotFound(new { message = result.Message });

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the claim.", error = ex.Message });
            }
        }

    }  

}

