using AutoMapper;
using MediatR;
using Ordering.Application.Common.Interfaces;
using Ordering.Application.Common.Models;
using Ordering.Domain.Entities;
using Shared.SeedWork;

namespace Ordering.Application.Features.V1.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, ApiResult<OrderDto>>
    {
        private readonly IOrderRepository _repository;
        private readonly IMapper _mapper;

        public UpdateOrderCommandHandler(IOrderRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ApiResult<OrderDto>> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var orderEntity = await _repository.GetOrderById(request.Id);
            if(orderEntity == null)
            {
                throw new ArgumentException($"Order with id = {request.Id} does not exist");
            }
            orderEntity = _mapper.Map<OrderEntity>(request);
            await _repository.UpdateOrder(orderEntity);
            await _repository.SaveChangeAsync();
            var orderDto = _mapper.Map<OrderDto>(orderEntity);
            return new ApiSuccessResult<OrderDto>(orderDto);
        }
    }
}

