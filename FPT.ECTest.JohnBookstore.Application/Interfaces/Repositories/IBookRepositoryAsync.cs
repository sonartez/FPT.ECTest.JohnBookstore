using FPT.ECTest.JohnBookstore.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FPT.ECTest.JohnBookstore.Application.Interfaces.Repositories
{
    public interface IBookRepositoryAsync : IGenericRepositoryAsync<Book>
    {
        Task<bool> IsExistISBNAsync(Book book);
        Task<Book> GetBookByISBNAsync(string isbnCode);
        Task<IReadOnlyList<Book>> FindBooksPagedReponseAsync(string findText, int pageNumber, int pageSize);
    }
}
