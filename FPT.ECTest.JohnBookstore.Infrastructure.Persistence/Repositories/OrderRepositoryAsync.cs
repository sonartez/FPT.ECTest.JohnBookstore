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
    public class OrderRepositoryAsync : GenericRepositoryAsync<Order>, IOrderRepositoryAsync
    {
        private readonly DbSet<Order> _order;

        public OrderRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _order = dbContext.Set<Order>();
        }

    }
}
