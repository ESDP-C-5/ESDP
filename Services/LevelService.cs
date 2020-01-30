using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.Models;
using CRM.UoW;

namespace CRM.Services
{
    public class LevelService
    {
        private readonly UnitOfWork _unitOfWork;

        public LevelService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Dictionary<int, string>> GetAllLevel()
        {
            var levelUoF = _unitOfWork.Levels;
            var levels = await levelUoF.GetAllAsync();
            Dictionary<int, string> lev = new Dictionary<int, string>();
            foreach (var level in levels)
            {
                lev.Add(level.Id, level.Name);
            }
            return lev;
        }

        public async Task<Level> GetLevelById(int id)
        {
            var levelUoF = _unitOfWork.Levels;
            var level = await levelUoF.GetByIdAsync(id);
            return level;
        }

        public async Task CreateLevel(Level level)
        {
            var levelUoF = _unitOfWork.Levels;
            await levelUoF.CreateAsync(level);
            await _unitOfWork.CompleteAsync();
        }

        public async Task EditLevel(Level level)
        {
            var levelUoF = _unitOfWork.Levels;
            levelUoF.UpdateAsync(level);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteLevel(Level level)
        {
            var levelUoF = _unitOfWork.Levels;
            levelUoF.RemoveAsync(level);
            await _unitOfWork.CompleteAsync();
        }
    }
}