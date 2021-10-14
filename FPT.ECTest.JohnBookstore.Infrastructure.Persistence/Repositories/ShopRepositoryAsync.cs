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
    public class ShopRepositoryAsync : GenericRepositoryAsync<Shop>, IShopRepositoryAsync
    {
        private readonly DbSet<Shop> _shop;

        public ShopRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _shop = dbContext.Set<Shop>();
        }

        public Task<Shop> GetBookByShopCodeAsync(string shopCode)
        {
            return _shop
                .FirstOrDefaultAsync(p => p.ShopCode == shopCode);
        }

    }
}
