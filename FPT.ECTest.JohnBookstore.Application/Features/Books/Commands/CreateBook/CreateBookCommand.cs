using AutoMapper;
using FPT.ECTest.JohnBookstore.Application.Interfaces.Repositories;
using FPT.ECTest.JohnBookstore.Application.Wrappers;
using FPT.ECTest.JohnBookstore.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FPT.ECTest.JohnBookstore.Application.Features.Books.Commands.CreateBook
{
    public partial class CreateBookCommand : IRequest<Response<int>>
    {
        public string ISBNCode { get; set; }
        public string BookName { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
    }
    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, Response<int>>
    {
        private readonly IBookRepositoryAsync _bookRepository;
        private readonly IMapper _mapper;
        public CreateBookCommandHandler(IBookRepositoryAsync bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var book = _mapper.Map<Book>(request);
            bool bookExisted = await _bookRepository.IsExistISBNAsync(book);
            if(bookExisted)
                await _bookRepository.UpdateAsync(book);
            else
                await _bookRepository.AddAsync(book);
            return new Response<int>(book.Id);
        }
    }
}
