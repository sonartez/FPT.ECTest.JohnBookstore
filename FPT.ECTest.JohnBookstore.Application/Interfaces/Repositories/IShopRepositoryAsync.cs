using FPT.ECTest.JohnBookstore.Domain.Entities;
using System.Threading.Tasks;

namespace FPT.ECTest.JohnBookstore.Application.Interfaces.Repositories
{
    public interface IShopRepositoryAsync : IGenericRepositoryAsync<Shop>
    {
        Task<Shop> GetBookByShopCodeAsync(string shopCode);
    }
}
