using Apsuite.Back.Infrastructure.Contract.Branch.DTO.Output;
using Apsuite.Back.Transversal.Contract.Branch.DTO.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apsuite.Back.Infrastructure.Contract.Branch.Interface
{
    public interface IBranchRepository
    {
        Task<GetBranchSpRes> GetBranch();
        Task<CreateBranchSpRes> CreateBranch(CreateBranchGblReq param);
        Task<UpdateBranchSpRes> UpdateBranch(UpdateBranchGblReq param);
        Task<DeleteBranchSpRes> DeleteBranch(DeleteBranchGblReq param);
        Task<GetCurrencySpRes> GetCurrency();
        Task<GetBranchByIdSpRes> GetBranchById(GetBranchByIdGblReq param);
    }
}
