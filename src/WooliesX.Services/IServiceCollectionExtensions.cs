using Microsoft.Extensions.DependencyInjection;

namespace WooliesX.Services
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddWooliesXServices(
            this IServiceCollection services,
            ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
        {
            services.Add(new ServiceDescriptor(typeof(IProductService), typeof(ProductService), serviceLifetime));
            services.Add(new ServiceDescriptor(typeof(IShopperHistoryService), typeof(ShopperHistoryService), serviceLifetime));
            
            return services;
        }
    }
}