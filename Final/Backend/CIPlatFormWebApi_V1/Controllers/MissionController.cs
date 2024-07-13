using Business_Logic_Layer.Mission;
using Data_Logic_Layer.Entity;
using Data_Logic_Layer.MissionEntity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CIPlatFormWebApi_V1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MissionController : ControllerBase
    {
        ResponseResult result = new ResponseResult();
        private readonly Mission _missionRepository;

        public MissionController(Mission missionRepository)
        {
            _missionRepository = missionRepository;

        }
        [HttpPost("CreateMission")]
       // [Authorize(Roles="Admin")]
        public async Task<ResponseResult> CreateMission([FromBody] MissionDetails model)
        {
            if (!ModelState.IsValid)
            {
                result.Result = ResponseStatus.Error;
                return result;
            }

            result.Data = await _missionRepository.CreateMission(model);
            result.Result = ResponseStatus.Success;
            return result;
        }

        [HttpGet("GetMissions")]
       // [Authorize(Roles = "Admin")]
        public async Task<ResponseResult> GetMissionWithDetails()
        {
            result.Data = await _missionRepository.GetMissionsWithDetails();
            result.Result = ResponseStatus.Success;
            return result;
        }


        [HttpGet("GetMissionById/{MissionId}")]
       // [Authorize(Roles = "Admin")]
        public async Task<ResponseResult> GetMissionWithDetailsById(int MissionId)
        {
            result.Data = await _missionRepository.GetMissionDetailsById(MissionId);
            result.Result = ResponseStatus.Success;
            return result;
        }

        [HttpDelete("{id}")]
       // [Authorize(Roles = "Admin")]
        public async Task<ResponseResult> DeleteMission([FromQuery] int id)
        {
            result.Data = await _missionRepository.DeleteMission(id);

            if (result.Data == "Mission not found")
            {
                result.Result = ResponseStatus.Error;
                return result;
            }

            result.Result = ResponseStatus.Success;
            return result;
        }
        [HttpPut("{id}")]
        public async Task<ResponseResult> UpdateMission(int id ,[FromBody] MissionDetails model)
        {
            if (!ModelState.IsValid)
            {
                result.Result = ResponseStatus.Error;
                return result;
            }

            result.Data = await _missionRepository.UpdateMission(model,id);

            result.Result = ResponseStatus.Success;
            return result;
        }

    }
}
