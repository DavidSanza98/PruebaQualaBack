using Apsuite.Back.Application.Contract.Branch.Interface;
using Apsuite.Back.Domain.Contract.Branch.Interface;
using Apsuite.Back.Transversal.Contract.Branch.DTO.Input;
using Apsuite.Back.Transversal.Contract.Branch.DTO.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apsuite.Back.Application.Implement.Branch
{
    public class BranchApplication : IBranchApplication
    {
        private readonly IBranchDomain _BranchDomain;

        public BranchApplication(IBranchDomain branchDomain)
        {
            _BranchDomain = branchDomain;
        }
        public async Task<GetBranchGblRes> GetBranch()
        {
            GetBranchGblRes result = await _BranchDomain.GetBranch();

            return result;
        }

        public async Task<CreateBranchGblRes> CreateBranch(CreateBranchGblReq param)
        {
            CreateBranchGblRes result = await _BranchDomain.CreateBranch(param);

            return result;
        }

        public async Task<UpdateBranchGblRes> UpdateBranch(UpdateBranchGblReq param)
        {
            UpdateBranchGblRes result = await _BranchDomain.UpdateBranch(param);

            return result;
        }

        public async Task<DeleteBranchGblRes> DeleteBranch(DeleteBranchGblReq param)
        {
            DeleteBranchGblRes result = await _BranchDomain.DeleteBranch(param);

            return result;
        }

        public async Task<GetCurrencyGblRes> GetCurrency()
        {
            GetCurrencyGblRes result = await _BranchDomain.GetCurrency();

            return result;
        }

        public async Task<GetBranchByIdGblRes> GetBranchById(GetBranchByIdGblReq param)
        {
            GetBranchByIdGblRes result = await _BranchDomain.GetBranchById(param);

            return result;
        }
    }
}
