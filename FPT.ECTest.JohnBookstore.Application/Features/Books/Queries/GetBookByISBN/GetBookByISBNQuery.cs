using AutoMapper;
using FPT.ECTest.JohnBookstore.Application.Exceptions;
using FPT.ECTest.JohnBookstore.Application.Interfaces.Repositories;
using FPT.ECTest.JohnBookstore.Application.Wrappers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FPT.ECTest.JohnBookstore.Application.Features.Books.Queries.GetBookByISBN
{
    public class GetBookByISBNQuery : IRequest<Response<GetBookByISBNViewModel>>
    {
        public string ISBNCode { get; set; }
        public class GetBookByISBNQueryHandler : IRequestHandler<GetBookByISBNQuery, Response<GetBookByISBNViewModel>>
        {
            private readonly IBookRepositoryAsync _bookRepository;
            private readonly IMapper _mapper;
            public GetBookByISBNQueryHandler(IBookRepositoryAsync bookRepository, IMapper mapper)
            {
                _bookRepository = bookRepository;
                _mapper = mapper;

            }
            public async Task<Response<GetBookByISBNViewModel>> Handle(GetBookByISBNQuery query, CancellationToken cancellationToken)
            {
                var book = await _bookRepository.GetBookByISBNAsync(query.ISBNCode);
                if (book == null) throw new ApiException($"Book Not Found.");
                var bookViewModel = _mapper.Map<GetBookByISBNViewModel>(book);
                return new Response<GetBookByISBNViewModel>(bookViewModel);
            }
        }
    }
}
