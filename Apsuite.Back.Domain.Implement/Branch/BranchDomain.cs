using Apsuite.Back.Domain.Contract.Branch.Interface;
using Apsuite.Back.Infrastructure.Contract.Branch.Interface;
using Apsuite.Back.Transversal.Contract.Branch.DTO.Input;
using Apsuite.Back.Transversal.Contract.Branch.DTO.Output;
using Apsuite.Back.Transversal.Contract.Global.Interface;
using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apsuite.Back.Domain.Implement.Branch
{
    public class BranchDomain : IBranchDomain
    {
        private readonly IBranchRepository _BranchRepository;
        private static readonly Logger logError = LogManager.GetLogger("logError");
        public BranchDomain(IBranchRepository branchRepository)
        {
            _BranchRepository = branchRepository;
        }
        public async Task<GetBranchGblRes> GetBranch()
        {
            try
            {
                GetBranchGblRes result = await _BranchRepository.GetBranch();

                return result;
            }
            catch (Exception ex)
            {
                logError.Error("Se genero una exepcion en el metodo GetBranch Domain Error: " + ex);
                throw;
            }
        }

        public async Task<CreateBranchGblRes> CreateBranch(CreateBranchGblReq param)
        {
            try
            {
                CreateBranchGblRes result = await _BranchRepository.CreateBranch(param);

                return result;
            }
            catch (Exception ex)
            {
                logError.Error("Se genero una exepcion en el metodo CreateBranch Domain Error: " + ex);
                throw;
            }
        }

        public async Task<UpdateBranchGblRes> UpdateBranch(UpdateBranchGblReq param)
        {
            try
            {
                UpdateBranchGblRes result = await _BranchRepository.UpdateBranch(param);

                return result;
            }
            catch (Exception ex)
            {
                logError.Error("Se genero una exepcion en el metodo UpdateBranch Domain Error: " + ex);
                throw;
            }
        }

        public async Task<DeleteBranchGblRes> DeleteBranch(DeleteBranchGblReq param)
        {
            try
            {
                DeleteBranchGblRes result = await _BranchRepository.DeleteBranch(param);

                return result;
            }
            catch (Exception ex)
            {
                logError.Error("Se genero una exepcion en el metodo DeleteBranch Domain Error: " + ex);
                throw;
            }
        }

        public async Task<GetCurrencyGblRes> GetCurrency()
        {
            try
            {
                GetCurrencyGblRes result = await _BranchRepository.GetCurrency();

                return result;
            }
            catch (Exception ex)
            {
                logError.Error("Se genero una exepcion en el metodo GetCurrency Domain Error: " + ex);
                throw;
            }
        }

        public async Task<GetBranchByIdGblRes> GetBranchById(GetBranchByIdGblReq param)
        {
            try
            {
                GetBranchByIdGblRes result = await _BranchRepository.GetBranchById(param);

                return result;
            }
            catch (Exception ex)
            {
                logError.Error("Se genero una exepcion en el metodo GetBranchById Domain Error: " + ex);
                throw;
            }
        }
    }
}
