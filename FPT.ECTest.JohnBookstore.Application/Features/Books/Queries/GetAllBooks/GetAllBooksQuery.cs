using AutoMapper;
using FPT.ECTest.JohnBookstore.Application.Interfaces.Repositories;
using FPT.ECTest.JohnBookstore.Application.Wrappers;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FPT.ECTest.JohnBookstore.Application.Features.Books.Queries.GetAllBooks
{
    public class GetAllBooksQuery : IRequest<PagedResponse<IEnumerable<GetAllBooksViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
    public class GetAllBooksQueryHandler : IRequestHandler<GetAllBooksQuery, PagedResponse<IEnumerable<GetAllBooksViewModel>>>
    {
        private readonly IBookRepositoryAsync _bookRepository;
        private readonly IMapper _mapper;
        public GetAllBooksQueryHandler(IBookRepositoryAsync bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<GetAllBooksViewModel>>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<GetAllBooksParameter>(request);
            var book = await _bookRepository.GetPagedReponseAsync(validFilter.PageNumber, validFilter.PageSize);
            var bookViewModel = _mapper.Map<IEnumerable<GetAllBooksViewModel>>(book);

            return new PagedResponse<IEnumerable<GetAllBooksViewModel>>(bookViewModel, validFilter.PageNumber, validFilter.PageSize);
        }
    }
}
