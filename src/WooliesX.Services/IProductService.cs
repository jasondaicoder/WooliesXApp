using System.Collections.Generic;
using System.Threading.Tasks;
using WooliesX.Contracts;
using WooliesX.Contracts.Domain;

namespace WooliesX.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetSortedProductsAsync(SortOptions sortOptions);
    }
}