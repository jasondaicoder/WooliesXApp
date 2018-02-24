using System.Threading.Tasks;
using WooliesX.Contracts.Domain;

namespace WooliesX.Services
{
    public interface ITrolleyService
    {
         Task<double> GetLowestTotal(Trolley trolley);
    }
}