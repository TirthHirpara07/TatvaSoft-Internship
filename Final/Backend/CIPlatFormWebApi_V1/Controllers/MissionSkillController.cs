using Business_Logic_Layer.MissionSkill;
using Data_Logic_Layer.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CIPlatFormWebApi_V1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MissionSkillController : ControllerBase
    {
        ResponseResult result = new ResponseResult();
        private readonly MissionSkill _missionSkill;

        public MissionSkillController(MissionSkill missionSkill)
        {
            _missionSkill = missionSkill;
        }

        [HttpGet]
        [Route("GetMissionSkill")]
        public async Task<ResponseResult> GetMissionSkill()
        {
            try
            {
                result.Data = await _missionSkill.GetMissionSkill();
                result.Result = ResponseStatus.Success;
                return result;
            }
            catch (Exception ex)
            {
                throw ex; ;
            }
        }

        [HttpPost]
        [Route("CreateMissionSkill")]
        public async Task<ResponseResult> CreateMissionSkill([FromBody] Data_Logic_Layer.MissionSkillEntity.MissionSkill model)
        { 
            try
            {
                result.Data = await _missionSkill.CreateMissionSkill(model);
                result.Result = ResponseStatus.Success;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut]
        [Route("UpdateMissionSkill/{missionSkillId}")]
        public async Task<ResponseResult> UpdateMissionSkill(int missionSkillId, [FromBody] Data_Logic_Layer.MissionSkillEntity.MissionSkill model)
        {
           
            try
            {
                result.Data = await _missionSkill.UpdateMissionSkill(missionSkillId, model);
                result.Result = ResponseStatus.Success;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("GetMissioSkillById/{missionSkillId}")]
        public async Task<ResponseResult> GetMissionSkillById([FromQuery]int missionSkillId)
        {
          
            try
            {
                result.Data = await _missionSkill.GetMissionSkillById(missionSkillId);
                result.Result = ResponseStatus.Success;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpDelete]
        [Route("DeleteMissionSkill")]
        public async Task<ResponseResult> DeleteMissionSkill([FromQuery]int id)
        {
           
            try
            {
                result.Data= await _missionSkill.DeleteMissionSkill(id);
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
