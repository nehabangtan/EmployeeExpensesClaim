//using EmployeeExpensesClaim.BusinessAccessLayer.Interfaces;
//using EmployeeExpensesClaim.ViewModels;
//using Microsoft.AspNetCore.Mvc;
//namespace EmployeeExpensesClaim.Controllers
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    public class FilesUploadController : ControllerBase
//    {
//        private readonly IFilesUploadService _service;
//        public FilesUploadController(IFilesUploadService service)
//        {
//            _service = service;
//        }
//        [HttpPost("upload")]
//        public async Task<IActionResult> UploadFiles([FromForm] List<IFormFile> files)
//        {
//            if (files == null || !files.Any())
//            {
//                return BadRequest(new { message = "No files were uploaded." });
//            }

//            var uploadedUrls = await _service.UploadFilesAsync(files);

//            return Ok(new
//            {
//                message = "Files uploaded successfully.",
//                urls = uploadedUrls
//            });
//        }


//    }
//}
