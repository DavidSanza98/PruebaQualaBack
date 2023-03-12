using Apsuite.Back.Transversal.Contract.Branch.DTO.Input;
using Apsuite.Back.Transversal.Contract.Branch.DTO.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apsuite.Back.Domain.Contract.Branch.Interface
{
    public interface IBranchDomain
    {
        Task<GetBranchGblRes> GetBranch();
        Task<CreateBranchGblRes> CreateBranch(CreateBranchGblReq param);
        Task<UpdateBranchGblRes> UpdateBranch(UpdateBranchGblReq param);
        Task<DeleteBranchGblRes> DeleteBranch(DeleteBranchGblReq param);
        Task<GetCurrencyGblRes> GetCurrency();
        Task<GetBranchByIdGblRes> GetBranchById(GetBranchByIdGblReq param);
    }
}
