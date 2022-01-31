using System.Collections.Generic;
using System.Threading.Tasks;
using iptvplayer.Models;

namespace iptvplayer.Services
{
    public interface IDataService<T> where T : DbEntity, new()
    {
        Task<int> Add(T dbEntity);
        Task<IEnumerable<T>> Get();
        Task<T> GetById(int id);
        Task Init();
        Task Remove(int id);
        Task<int> AddAll(IEnumerable<T> dbEntityList);
        Task<int> Update(T dbEntity);
    }
}