using Basket.API.Entities;
using Basket.API.Repostitories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Basket.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketsController : ControllerBase
    {
        private readonly IBasketRepository _basketRepository;

        public BasketsController(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
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
    }
}
