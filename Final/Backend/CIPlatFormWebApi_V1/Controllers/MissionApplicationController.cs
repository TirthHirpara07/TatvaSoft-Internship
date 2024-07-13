using Business_Logic_Layer.MissionApp;
using Data_Logic_Layer.Entity;
using Data_Logic_Layer.MissionApplicationEntity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CIPlatFormWebApi_V1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MissionApplicationController : ControllerBase
    {
        ResponseResult result = new ResponseResult();
        private readonly MissionApp _missionApp;
        public MissionApplicationController(MissionApp missionApp)
        {
            _missionApp = missionApp;
        }
        [HttpGet]
        [Route("MissionApplicationList")]

        public ResponseResult MissionApplicationList()
        {
            try
            {
                result.Data = _missionApp.MissionApplicationList();
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }

        [HttpPost]
        [Route("MissionApplicationDelete")]
        
        public ResponseResult MissionApplicationDelete(int id)
        {
            try
            {
                result.Data = _missionApp.MissionApplicationDelete(id);
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }
        [HttpPost]
        [Route("MissionApplicationApprove")]
        [Authorize]
        public ResponseResult MissionApplicationApprove(int id)
        {
            try
            {
                result.Data = _missionApp.MissionApplicationApprove(id);
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }

    }
}
