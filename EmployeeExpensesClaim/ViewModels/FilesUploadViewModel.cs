using EmployeeExpensesClaim.DataAccessLayer.Entities;

namespace EmployeeExpensesClaim.ViewModels
{
    public class FilesUploadViewModel
    {
        public int FileId { get; set; }

        public int ClaimId { get; set; }

        public string? FilePath { get; set; }

        public DateTime? UploadedDate { get; set; }

        public int? UploadedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedBy { get; set; }
    }
}
