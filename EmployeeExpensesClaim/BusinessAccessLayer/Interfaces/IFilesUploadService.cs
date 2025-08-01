using EmployeeExpensesClaim.ViewModels;
namespace EmployeeExpensesClaim.BusinessAccessLayer.Interfaces
{
    public interface IFilesUploadService
    {
        Task<List<string>> UploadFilesAsync(IList<IFormFile> files);
    }
}
