using MediatR;
using Ordering.Application.Common.Mappings;
using Ordering.Domain.Entities;
using Ordering.Domain.Enums;
using Shared.SeedWork;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using AutoMapper;

namespace Ordering.Application.Features.V1.Orders.Commands.CreateOrder
{
    public class CreateOrderCommand : IRequest<ApiResult<long>>, IMapFrom<OrderEntity>
    {
        public string UserName { get; set; }

        public decimal TotalPrice { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailAddress { get; set; }

        public string ShippingAddress { get; set; }

        public string InvoiceAddress { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateOrderCommand, OrderEntity>();
        }
    }
}
