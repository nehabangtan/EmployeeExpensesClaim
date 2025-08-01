
using AutoMapper;
using Azure;
using Azure.Storage.Blobs;
using EmployeeExpensesClaim.BusinessAccessLayer.Interfaces;
using EmployeeExpensesClaim.DataAccessLayer.Entities;
using EmployeeExpensesClaim.DataAccessLayer.Repositories.Interfaces;
using EmployeeExpensesClaim.ViewModels;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Security.Claims;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace EmployeeExpensesClaim.BusinessAccessLayer.Services
{
    public class ExpenseClaimService : IExpenseClaimService
    {

        private readonly IGenericRepository<ExpenseClaim> _repo;
        private readonly IGenericRepository<FilesTable> _uploadfilerepo;
        private readonly IMapper _mapper;
        private readonly string _connectionString;
        private readonly string _containerName;
        private readonly IFilesUploadService _filesService;

       
        public ExpenseClaimService(IGenericRepository<ExpenseClaim> repo, IGenericRepository<FilesTable> uploadfilerepo,IMapper mapper, IConfiguration configuration, IFilesUploadService filesService)
        {
            _repo = repo;
            _uploadfilerepo = uploadfilerepo;
            _mapper = mapper;
            _connectionString = configuration["AzureBlobSettings:ConnectionString"];
            _containerName = configuration["AzureBlobSettings:ContainerName"];
            _filesService = filesService;
        }

        public async Task<ResponseViewModel<ExpenseClaimViewModel>> GetClaimByIdAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null)
                return new() { Status = false, Message = "Claim not found." };

            return new()
            {
                Status = true,
                Message = "Claim found.",
                Data = _mapper.Map<ExpenseClaimViewModel>(entity)
            };
        }

        public async Task<ResponseViewModel<List<ExpenseClaimViewModel>>> GetClaimsByEmployeeIdAsync(int empId)
        {
            var list = await _repo.GetAllByPropertyAsync("EmpId", empId);
            return new()
            {
                Status = true,
                Message = "Claims fetched successfully.",
                Data = _mapper.Map<List<ExpenseClaimViewModel>>(list)
            };
        }

        public async Task<ResponseViewModel<ExpenseClaimViewModel>> CreateClaimAsync(ExpenseClaimViewModel claimVM)
        {
            var entity = _mapper.Map<ExpenseClaim>(claimVM);
            entity.ExpenseStatus = "Pending";
            entity.CreatedDate = DateTime.UtcNow;
            entity.CreatedBy = claimVM.EmpId;
            var urls = await _filesService.UploadFilesAsync(claimVM.UploadedFiles);
            await _repo.AddAsync(entity);
            await _repo.SaveAsync();             
            var files = urls.Select((url, i) => new FilesTable
            {
                ClaimId = entity.ClaimId,
                FilePath = url,
                UploadedDate = DateTime.UtcNow,
                UploadedBy = entity.EmpId,
                ModifiedDate = DateTime.UtcNow,
                ModifiedBy = entity.EmpId,
            }).ToList();

            await _uploadfilerepo.AddRangeAsync(files);
            await _uploadfilerepo.SaveAsync();

            return new()
            {
                Status = true,
                Message = "Claim and files uploaded successfully.",
                Data = _mapper.Map<ExpenseClaimViewModel>(entity)
            };
        }

        public async Task<ResponseViewModel<ExpenseClaimViewModel>> UpdateClaimAsync(ExpenseClaimViewModel claimVM)
        {
            var entity = await _repo.GetByIdAsync(claimVM.ClaimId);
            if (entity == null)
                return new() { Status = false, Message = "Claim not found." };

            _mapper.Map(claimVM, entity);
            entity.ModifiedDate = DateTime.UtcNow;

            _repo.Update(entity);
            await _repo.SaveAsync();

            return new()
            {
                Status = true,
                Message = "Claim updated.",
                Data = _mapper.Map<ExpenseClaimViewModel>(entity)
            };
        }

        public async Task<ResponseViewModel<bool>> DeleteClaimAsync(int id)
        {
            var success = await _repo.DeleteAsync(id);
            return new()
            {
                Status = success,
                Message = success ? "Claim deleted." : "Claim not found.",
                Data = success
            };
        }
       
    }


}


