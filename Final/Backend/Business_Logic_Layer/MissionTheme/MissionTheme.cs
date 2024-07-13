using Business_Logic_Layer.Mission;
using Data_Logic_Layer.Entity;
using Data_Logic_Layer.MissionEntity;
using Data_Logic_Layer.MissionThemeEntity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.MissionTheme
{
    public class MissionTheme : IMissionTheme
    {
        private readonly AppDbContext _context;

        public MissionTheme(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Data_Logic_Layer.MissionThemeEntity.MissionTheme>> GetMissionThemes()
        {
           return await _context.MissionTheme.Where(mission => !mission.IsDeleted).Select(
                mission => new Data_Logic_Layer.MissionThemeEntity.MissionTheme
                {
                    ThemeId = mission.ThemeId,
                    ThemeName = mission.ThemeName,
                    ThemeDescription = mission.ThemeDescription,
                    ThemeImage = mission.ThemeImage,               
                }).ToListAsync();
           
        }

        public async Task<string> CreateMissionTheme(Data_Logic_Layer.MissionThemeEntity.MissionTheme model)
        {

            await _context.MissionTheme.AddAsync(model);
            await _context.SaveChangesAsync();
            return "Mission theme created successfully.";
        }

        public async Task<string> UpdateMissionTheme(int missionThemeId, Data_Logic_Layer.MissionThemeEntity.MissionTheme model)
        {
            var existingTheme = await _context.MissionTheme.FindAsync(missionThemeId);
            if (existingTheme == null)
            {
                return "Mission Theme not found.";
            }

            existingTheme.ThemeName = model.ThemeName;
            existingTheme.ThemeDescription = model.ThemeDescription;
            existingTheme.ThemeImage = model.ThemeImage;

            _context.MissionTheme.Update(existingTheme);
            await _context.SaveChangesAsync();
            return "Mission theme updated successfully.";
        }

        public async Task<Data_Logic_Layer.MissionThemeEntity.MissionTheme?> GetMissionThemeById(int missionThemeId)
        {
            var missionThemeExist = await _context.MissionTheme
                    .Where(mission => mission.ThemeId == missionThemeId && !mission.IsDeleted)
                    .Select(mission => new Data_Logic_Layer.MissionThemeEntity.MissionTheme
                    {
                        ThemeId = missionThemeId,
                        ThemeName = mission.ThemeName,
                        ThemeDescription = mission.ThemeDescription,
                        ThemeImage = mission.ThemeImage,
                    })
                    .FirstOrDefaultAsync();
            return missionThemeExist;
        }

        public async Task<string> DeleteMissionTheme(int id)
        {
            var theme = await _context.MissionTheme.FindAsync(id);
            if (theme == null)
            {
                return "Mission theme not found.";
            }

            theme.IsDeleted = true;
            await _context.SaveChangesAsync();
            return "Mission theme deleted successfully.";
        }
    }
}
