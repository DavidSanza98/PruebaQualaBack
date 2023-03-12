using Apsuite.Back.Application.Contract.Branch.Interface;
using Apsuite.Back.Transversal.Contract.Branch.DTO.Input;
using Apsuite.Back.Transversal.Contract.Branch.DTO.Output;
using Apsuite.Back.Transversal.Contract.Global.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NLog;
using System.Security.Claims;

namespace Apsuite.Back.Service.Security.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class BranchController : ControllerBase
    {
        private readonly IBranchApplication _BranchApplication;
        private static readonly Logger logError = LogManager.GetLogger("logError");
        private static readonly Logger logTrace = LogManager.GetLogger("logTrace");

        public BranchController(IBranchApplication branchApplication)
        {
            _BranchApplication = branchApplication;
        }

        /// <summary>
        /// Metodo encargado de obtener todas las sucursales
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(GetBranchGblRes), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GetBranchGblRes), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(GetBranchGblRes), StatusCodes.Status500InternalServerError)]
        public async Task<GetBranchGblRes> GetBranch()
        {
            try
            {
                GetBranchGblRes result = await _BranchApplication.GetBranch();
                Response.StatusCode = StatusCodes.Status200OK;
                return result;
            }
            catch (Exception ex)
            {
                logError.Error("Se genero una exepcion en el metodo GetBranch Error: " + ex);
                throw;
            }
        }

        /// <summary>
        /// Metodo encargado de crear una sucursal
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(CreateBranchGblRes), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CreateBranchGblRes), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CreateBranchGblRes), StatusCodes.Status500InternalServerError)]
        public async Task<CreateBranchGblRes> CreateBranch([FromBody] CreateBranchGblReq param)
        {
            try
            {
                CreateBranchGblRes result = await _BranchApplication.CreateBranch(param);
                Response.StatusCode = StatusCodes.Status200OK;
                return result;
            }
            catch (Exception ex)
            {
                logError.Error("Se genero una exepcion en el metodo CreateBranch Error: " + ex + " Parametros entrada: " + JsonConvert.SerializeObject(param));
                throw;
            }
        }

        /// <summary>
        /// Metodo encargado de actualizar una sucursal
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(UpdateBranchGblRes), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UpdateBranchGblRes), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(UpdateBranchGblRes), StatusCodes.Status500InternalServerError)]
        public async Task<UpdateBranchGblRes> UpdateBranch([FromBody] UpdateBranchGblReq param)
        {
            try
            {
                UpdateBranchGblRes result = await _BranchApplication.UpdateBranch(param);
                Response.StatusCode = StatusCodes.Status200OK;
                return result;
            }
            catch (Exception ex)
            {
                logError.Error("Se genero una exepcion en el metodo UpdateBranch Error: " + ex + " Parametros entrada: " + JsonConvert.SerializeObject(param));
                throw;
            }
        }

        /// <summary>
        /// Metodo encargado de eliminar una sucursal
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(typeof(UpdateBranchGblRes), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UpdateBranchGblRes), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(UpdateBranchGblRes), StatusCodes.Status500InternalServerError)]
        public async Task<DeleteBranchGblRes> DeleteBranch([FromRoute] DeleteBranchGblReq param)
        {
            try
            {
                DeleteBranchGblRes result = await _BranchApplication.DeleteBranch(param);
                Response.StatusCode = StatusCodes.Status200OK;
                return result;
            }
            catch (Exception ex)
            {
                logError.Error("Se genero una exepcion en el metodo DeleteBranch Error: " + ex + " Parametros entrada: " + JsonConvert.SerializeObject(param));
                throw;
            }
        }

        /// <summary>
        /// Metodo encargado de obtener todas las monedas
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetCurrency")]
        [ProducesResponseType(typeof(GetCurrencyGblRes), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GetCurrencyGblRes), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(GetCurrencyGblRes), StatusCodes.Status500InternalServerError)]
        public async Task<GetCurrencyGblRes> GetCurrency()
        {
            try
            {
                GetCurrencyGblRes result = await _BranchApplication.GetCurrency();
                Response.StatusCode = StatusCodes.Status200OK;
                return result;
            }
            catch (Exception ex)
            {
                logError.Error("Se genero una exepcion en el metodo GetCurrency Error: " + ex);
                throw;
            }
        }

        /// <summary>
        /// Metodo encargado de una sucursal
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetBranchById")]
        [ProducesResponseType(typeof(GetBranchByIdGblRes), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GetBranchByIdGblRes), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(GetBranchByIdGblRes), StatusCodes.Status500InternalServerError)]
        public async Task<GetBranchByIdGblRes> GetBranchById([FromRoute] GetBranchByIdGblReq param)
        {
            try
            {
                GetBranchByIdGblRes result = await _BranchApplication.GetBranchById(param);
                Response.StatusCode = StatusCodes.Status200OK;
                return result;
            }
            catch (Exception ex)
            {
                logError.Error("Se genero una exepcion en el metodo GetCurrency Error: " + ex);
                throw;
            }
        }
    }
}
