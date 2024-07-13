using Business_Logic_Layer.MissionTheme;
using Data_Logic_Layer.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CIPlatFormWebApi_V1.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class MissionThemeController : ControllerBase
    {
        ResponseResult result = new ResponseResult();
        private readonly MissionTheme _missionTheme;

        public MissionThemeController(MissionTheme missionTheme)
        {
            _missionTheme = missionTheme;
        }

        [HttpGet]
        [Route("GetMissionThemes")]
        public async Task<ResponseResult> GetMissionThemes()
        {
            try
            {
                result.Data = await _missionTheme.GetMissionThemes();
                if(result.Data == null)
                {
                    result.Result = ResponseStatus.Error;
                    return result;
                }
                result.Result = ResponseStatus.Success;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [Route("CreateMissionTheme")]
        public async Task<ResponseResult> CreateMissionTheme([FromBody] Data_Logic_Layer.MissionThemeEntity.MissionTheme model)
        {
            try
            {
                result.Data = await _missionTheme.CreateMissionTheme(model);
                result.Result = ResponseStatus.Success;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut]
        [Route("UpdateMissionTheme/{missionThemeId}")]
        public async Task<ResponseResult> UpdateMissionTheme([FromQuery] int missionThemeId, [FromBody] Data_Logic_Layer.MissionThemeEntity.MissionTheme model)
        {
            try
            {
                result.Data = await _missionTheme.UpdateMissionTheme(missionThemeId, model);
                result.Result = ResponseStatus.Success;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("GetMissionThemeById/{missionThemeId}")]
        public async Task<ResponseResult> GetMissionThemeById([FromQuery]  int missionThemeId)
        {

            try
            {
                result.Data = await _missionTheme.GetMissionThemeById(missionThemeId);

                result.Result = ResponseStatus.Success;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpDelete]
        [Route("DeleteMissionTheme")]
        public async Task<ResponseResult> DeleteMissionTheme([FromQuery] int id)
        {
            
            try
            {
                 result.Data = await _missionTheme.DeleteMissionTheme(id);
                result.Result = ResponseStatus.Success;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
