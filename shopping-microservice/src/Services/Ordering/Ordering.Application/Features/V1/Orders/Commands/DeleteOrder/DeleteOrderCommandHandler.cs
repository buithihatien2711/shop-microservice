using MediatR;
using Ordering.Application.Common.Interfaces;

namespace Ordering.Application.Features.V1.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand>
    {
        private readonly IOrderRepository _repository;

        public DeleteOrderCommandHandler(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var orderEntity = await _repository.GetOrderById(request.Id);
            if (orderEntity == null)
            {
                throw new ArgumentException($"order with id = {request.Id} does not exist");
            }
            await _repository.DeleteOrder(orderEntity);
            await _repository.SaveChangeAsync();
        }
    }
}
