using System.Collections.Generic;
using System.Threading.Tasks;
using WooliesX.Contracts.Domain;

namespace WooliesX.Services
{
    public interface IShopperHistoryService
    {
         Task<List<ShopperHistory>> GetHistoriesAsync();
    }
}