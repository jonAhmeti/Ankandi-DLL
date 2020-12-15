using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Auction.BO.Interfaces
{
    public interface ICrud<T> where T : class
    {
        Task<bool> AddAsync(T obj);
        Task<bool> DeleteAsync(int objId);
        Task<bool> UpdateAsync(T obj);
        Task<T> GetAsync(int objId);
        Task<IEnumerable<T>> GetAllAsync();
    }
}
