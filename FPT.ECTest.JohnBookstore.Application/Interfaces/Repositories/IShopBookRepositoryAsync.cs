using FPT.ECTest.JohnBookstore.Domain.Entities;
using System.Threading.Tasks;

namespace FPT.ECTest.JohnBookstore.Application.Interfaces.Repositories
{
    public interface IShopBookRepositoryAsync : IGenericRepositoryAsync<ShopBook>
    {
        Task<ShopBook> GetByShopIdAndBookIdAsync(int shopId, int bookId);
    }
}
