using FPT.ECTest.JohnBookstore.Application.Interfaces;
using FPT.ECTest.JohnBookstore.Application.Interfaces.Repositories;
using FPT.ECTest.JohnBookstore.Domain.Entities;
using FPT.ECTest.JohnBookstore.Infrastructure.Persistence.Contexts;
using FPT.ECTest.JohnBookstore.Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPT.ECTest.JohnBookstore.Infrastructure.Persistence.Repositories
{
    public class ShopBookRepositoryAsync : GenericRepositoryAsync<ShopBook>, IShopBookRepositoryAsync
    {
        private readonly DbSet<ShopBook> _shopBooks;

        public ShopBookRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _shopBooks = dbContext.Set<ShopBook>();
        }

        public Task<ShopBook> GetByShopIdAndBookIdAsync(int shopId, int bookId)
        {
            return _shopBooks
                .FirstOrDefaultAsync(p => p.BookId == bookId && p.ShopId == shopId);
        }
    }
}
