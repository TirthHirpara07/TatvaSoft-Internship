using Data_Logic_Layer.Entity;
using Data_Logic_Layer.MissionApplicationEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.MissionApp
{
    public class MissionApp
    {
        private readonly AppDbContext _context;
        public MissionApp(AppDbContext context)
        {
            _context = context;
        }
        public List<MissionApplication> MissionApplicationList()
        {
            List<MissionApplication> missionApplicationList = new List<MissionApplication>();
            try
            {
                missionApplicationList = _context.MissionApplication
                    .Where(ma => !ma.IsDeleted) // Assuming IsDeleted is a property on MissionApplication indicating deletion status
                    .Join(_context.Mission.Where(m => !m.IsDeleted),
                          ma => ma.MissionId,
                          m => m.MissionId,
                          (ma, m) => new { ma, m })
                    .Join(_context.User.Where(u => !u.IsDeleted),
                          mm => mm.ma.UserId,
                          u => u.Id,
                          (mm, u) => new MissionApplication
                          {
                              Id = mm.ma.Id,
                              MissionId = mm.ma.MissionId,
                              MissionTitle = mm.m.Title,
                              UserId = u.Id,
                              UserName = u.FirstName + " " + u.LastName,
                              AppliedDate = mm.ma.AppliedDate,
                              Status = mm.ma.Status
                          })
                    .ToList();
            }
            catch (Exception)
            {
                throw;
            }
            return missionApplicationList;
        }

        public string MissionApplicationDelete(int id)
        {
            try
            {
                var missionApplication = _context.MissionApplication.Where(ma => ma.Id == id).FirstOrDefault();
                if (missionApplication != null)
                {
                    missionApplication.IsDeleted = true;
                    _context.SaveChanges();
                    return "Success";
                }
                else
                {
                    return "Record not found";
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string MissionApplicationApprove(int id)
        {
            try
            {
                var missionApplication = _context.MissionApplication.Where(ma=> ma.Id == id && !ma.IsDeleted).FirstOrDefault();
                if (missionApplication != null)
                {
                    missionApplication.Status = true;
                    _context.SaveChanges();
                    return "Mission is approved";
                }
                else
                {
                    return "Mission is not approved";
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
