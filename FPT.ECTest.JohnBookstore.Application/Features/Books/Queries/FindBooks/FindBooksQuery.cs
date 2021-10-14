using AutoMapper;
using FPT.ECTest.JohnBookstore.Application.Interfaces.Repositories;
using FPT.ECTest.JohnBookstore.Application.Wrappers;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FPT.ECTest.JohnBookstore.Application.Features.Books.Queries.GetAllBooks
{
    public class FindBooksQuery : IRequest<PagedResponse<IEnumerable<GetAllBooksViewModel>>>
    {
        public string FindText { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
    public class FindBooksQueryHandler : IRequestHandler<FindBooksQuery, PagedResponse<IEnumerable<GetAllBooksViewModel>>>
    {
        private readonly IBookRepositoryAsync _bookRepository;
        private readonly IMapper _mapper;
        public FindBooksQueryHandler(IBookRepositoryAsync bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<GetAllBooksViewModel>>> Handle(FindBooksQuery request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<FindBooksParameter>(request);
            var book = await _bookRepository.FindBooksPagedReponseAsync(validFilter.FindText, validFilter.PageNumber, validFilter.PageSize);
            var bookViewModel = _mapper.Map<IEnumerable<GetAllBooksViewModel>>(book);

            return new PagedResponse<IEnumerable<GetAllBooksViewModel>>(bookViewModel, validFilter.PageNumber, validFilter.PageSize);
        }
    }
}
