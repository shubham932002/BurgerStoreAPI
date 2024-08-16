using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BurgerStoreAPI.Models;
using BurgerStoreAPI.Services;

namespace BurgerStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemsController : ControllerBase
    {
        private readonly ICartItemService _cartItemService;

        public CartItemsController(ICartItemService cartItemService)
        {
            _cartItemService = cartItemService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CartItem>>> GetCarts()
        {
            var cartItems = await _cartItemService.GetCartItemsAsync();
            return Ok(cartItems);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CartItem>> GetCartItem(int id)
        {
            var cartItem = await _cartItemService.GetCartItemByIdAsync(id);
            if (cartItem == null)
            {
                return NotFound();
            }
            return Ok(cartItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCartItem(int id, CartItem cartItem)
        {
            var result = await _cartItemService.UpdateCartItemAsync(id, cartItem);
            if (!result)
            {
                return BadRequest();
            }
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<CartItem>> PostCartItem(CartItem cartItem)
        {
            var newCartItem = await _cartItemService.AddCartItemAsync(cartItem);
            return CreatedAtAction(nameof(GetCartItem), new { id = newCartItem.Id }, newCartItem);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCartItem(int id)
        {
            var result = await _cartItemService.DeleteCartItemAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAllCartItems()
        {
            var result = await _cartItemService.DeleteAllCartItemsAsync();
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}

