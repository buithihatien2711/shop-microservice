using AutoMapper;
using Basket.API.Entities;
using Basket.API.Repostitories.Interfaces;
using EventBus.Messages.Events;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Org.BouncyCastle.Utilities.Encoders;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Basket.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketsController : ControllerBase
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketsController(IBasketRepository basketRepository, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }

        [HttpGet("{username}", Name = "GetBasket")]
        public async Task<IActionResult> GetBasketByUsername([Required] string username)
        {
            var result = await _basketRepository.GetBasketByUserName(username);
            return Ok(result ?? new CartEntity());
        }

        [HttpPost(Name = "UpdateBasket")]
        public async Task<IActionResult> UpdateBasket([FromBody] CartEntity cart)
        {
            var options = new DistributedCacheEntryOptions()
                                .SetAbsoluteExpiration(DateTime.UtcNow.AddHours(1))
                                .SetSlidingExpiration(TimeSpan.FromMinutes(5))
                                ;
            var result = await _basketRepository.UpdateBasket(cart, options);
            return Ok(result);
        }

        [HttpDelete("{username}", Name = "DeleteBasket")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> DeleteBasket([Required] string username)
        {
            var result = await _basketRepository.DeleteBasketFromUserName(username);
            return Ok(result);
        }

        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Checkout([FromBody] BasketCheckoutEntity basketCheckout)
        {
            var basket = await _basketRepository.GetBasketByUserName(basketCheckout.UserName);
            if (basket == null)
            {
                return NotFound();
            }

            var eventMessage = _mapper.Map<BasketCheckoutEvent>(basketCheckout);
            eventMessage.TotalPrice = basket.TotalPrice;

            await _basketRepository.DeleteBasketFromUserName(basketCheckout.UserName);

            return Accepted();
        }
    }
}
