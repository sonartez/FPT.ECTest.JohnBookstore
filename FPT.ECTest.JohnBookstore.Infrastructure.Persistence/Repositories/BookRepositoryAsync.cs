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
    public class BookRepositoryAsync : GenericRepositoryAsync<Book>, IBookRepositoryAsync
    {
        private readonly DbSet<Book> _books;

        public BookRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _books = dbContext.Set<Book>();
        }

        public Task<bool> IsExistISBNAsync(Book book)
        {
            return _books
                .AnyAsync(p => p.ISBNCode == book.ISBNCode);
        }

        public new async Task<IReadOnlyList<Book>> GetPagedReponseAsync(int pageNumber, int pageSize)
        {
            return await _books
                .Include(b=>b.ShopBooks)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
        }

        public Task<Book> GetBookByISBNAsync(string isbnCode)
        {
            return _books
                .Include(b=>b.ShopBooks)
                .ThenInclude(c => c.Shop)
                .FirstOrDefaultAsync(p => p.ISBNCode == isbnCode);
        }

        public async Task<IReadOnlyList<Book>> FindBooksPagedReponseAsync(string findText, int pageNumber, int pageSize)
        {
            return await _books
                .Where(b => b.BookName.Contains(findText) || b.Author.Contains(findText) || b.ISBNCode.Equals(findText))
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Include(b => b.ShopBooks)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
