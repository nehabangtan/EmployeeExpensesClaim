//using AutoMapper;
//using EmployeeExpensesClaim.BusinessAccessLayer.Interfaces;
//using EmployeeExpensesClaim.DataAccessLayer.Entities;
//using EmployeeExpensesClaim.DataAccessLayer.Repositories.Interfaces;
//using EmployeeExpensesClaim.ViewModels;

//namespace EmployeeExpensesClaim.BusinessAccessLayer.Services
//{
//    public class ExpenseClaimService : IExpenseClaimService
//    {

//        private readonly IExpenseClaimRepository _repo;
//        private readonly IMapper _mapper;
//        public ExpenseClaimService(IExpenseClaimRepository repo, IMapper mapper)
//        {
//            _repo = repo;
//            _mapper = mapper;
//        }

//        public async Task<ResponseViewModel<ExpenseClaimViewModel>> GetClaimByIdAsync(int id)
//        {
//            var entity = await _repo.GetByIdAsync(id);
//            if (entity == null)
//                return new() { Status = false, Message = "Claim not found." };

//            return new()
//            {
//                Status = true,
//                Message = "Claim found.",
//                Data = _mapper.Map<ExpenseClaimViewModel>(entity)
//            };
//        }

//        public async Task<ResponseViewModel<List<ExpenseClaimViewModel>>> GetClaimsByEmployeeIdAsync(int empId)
//        {
//            var list = await _repo.GetByEmployeeIdAsync(empId);
//            return new()
//            {
//                Status = true,
//                Message = "Claims fetched successfully.",
//                Data = _mapper.Map<List<ExpenseClaimViewModel>>(list)
//            };
//        }

//        public async Task<ResponseViewModel<ExpenseClaimViewModel>> CreateClaimAsync(ExpenseClaimViewModel claimVM)
//        {
//            var entity = _mapper.Map<ExpenseClaim>(claimVM);
//            entity.ExpenseStatus = "Pending";
//            entity.CreatedDate = DateTime.UtcNow;

//            await _repo.CreateAsync(entity);

//            return new()
//            {
//                Status = true,
//                Message = "Claim created.",
//                Data = _mapper.Map<ExpenseClaimViewModel>(entity)
//            };
//        }

//        public async Task<ResponseViewModel<ExpenseClaimViewModel>> UpdateClaimAsync(ExpenseClaimViewModel claimVM)
//        {
//            var entity = await _repo.GetByIdAsync(claimVM.ClaimId);
//            if (entity == null)
//                return new() { Status = false, Message = "Claim not found." };

//            _mapper.Map(claimVM, entity);
//            entity.ModifiedDate = DateTime.UtcNow;

//            await _repo.UpdateAsync(entity);

//            return new()
//            {
//                Status = true,
//                Message = "Claim updated.",
//                Data = _mapper.Map<ExpenseClaimViewModel>(entity)
//            };
//        }

//        public async Task<ResponseViewModel<bool>> DeleteClaimAsync(int id)
//        {
//            var success = await _repo.DeleteAsync(id);
//            return new()
//            {
//                Status = success,
//                Message = success ? "Claim deleted." : "Claim not found.",
//                Data = success
//            };
//        }

//    }
//}
using AutoMapper;
using EmployeeExpensesClaim.BusinessAccessLayer.Interfaces;
using EmployeeExpensesClaim.DataAccessLayer.Entities;
using EmployeeExpensesClaim.DataAccessLayer.Repositories.Interfaces;
using EmployeeExpensesClaim.ViewModels;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;

namespace EmployeeExpensesClaim.BusinessAccessLayer.Services
{
    public class ExpenseClaimService : IExpenseClaimService
    {

        private readonly IGenericRepository<ExpenseClaim> _repo;
        private readonly IMapper _mapper;
        public ExpenseClaimService(IGenericRepository<ExpenseClaim> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
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

            await _repo.AddAsync(entity);

            return new()
            {
                Status = true,
                Message = "Claim created.",
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

