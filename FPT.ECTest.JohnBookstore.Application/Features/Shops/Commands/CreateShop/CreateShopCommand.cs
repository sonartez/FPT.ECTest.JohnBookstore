using AutoMapper;
using FPT.ECTest.JohnBookstore.Application.Exceptions;
using FPT.ECTest.JohnBookstore.Application.Interfaces.Repositories;
using FPT.ECTest.JohnBookstore.Application.Wrappers;
using FPT.ECTest.JohnBookstore.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FPT.ECTest.JohnBookstore.Application.Features.Shops.Commands.CreateShop
{
    public partial class CreateShopCommand : IRequest<Response<int>>
    {
        public string ShopCode { get; set; }
        public string ShopName { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string XMLTemplate { get; set; }
        public bool AllowUpdateViaEmail { get; set; }
    }
    public class CreateShopCommandHandler : IRequestHandler<CreateShopCommand, Response<int>>
    {
        private readonly IShopRepositoryAsync _shopRepository;
        private readonly IMapper _mapper;
        public CreateShopCommandHandler(IShopRepositoryAsync shopRepository, IMapper mapper)
        {
            _shopRepository = shopRepository;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateShopCommand request, CancellationToken cancellationToken)
        {
            var shop = _mapper.Map<Shop>(request);
            var oldShop = await _shopRepository.GetBookByShopCodeAsync(shop.ShopCode);

            if(oldShop == null)
                await _shopRepository.AddAsync(shop);
            else
                throw new ApiException($"ShopCode Existed.");

            return new Response<int>(shop.Id);
        }
    }
}
