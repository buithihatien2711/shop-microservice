using AutoMapper;
using MediatR;
using Ordering.Application.Common.Interfaces;
using Ordering.Domain.Entities;
using Shared.SeedWork;

namespace Ordering.Application.Features.V1.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, ApiResult<long>>
    {
        private readonly IOrderRepository _repository;
        private readonly IMapper _mapper;

        public CreateOrderCommandHandler(IOrderRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ApiResult<long>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var orderEntity = _mapper.Map<OrderEntity>(request);
            await _repository.CreateAsync(orderEntity);
            await _repository.SaveChangeAsync();
            var id = orderEntity.Id;
            var result = new ApiSuccessResult<long>(id);
            return result;
        }
    }
}
