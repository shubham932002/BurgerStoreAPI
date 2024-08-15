using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using BurgerStoreAPI.Models;
using BurgerStoreAPI.BusinessLayer;

namespace BurgerStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenusController : ControllerBase
    {
        private readonly IMenuService _menuService;

        public MenusController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Menu>>> GetMenus()
        {
            var menus = await _menuService.GetAllMenusAsync();
            return Ok(menus);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Menu>> GetMenu(int id)
        {
            var menu = await _menuService.GetMenuByIdAsync(id);
            if (menu == null)
            {
                return NotFound();
            }
            return Ok(menu);
        }

        [HttpPost]
        public async Task<ActionResult<Menu>> PostMenu(Menu menu)
        {
            var createdMenu = await _menuService.CreateMenuAsync(menu);
            return CreatedAtAction(nameof(GetMenu), new { id = createdMenu.Id }, createdMenu);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutMenu(int id, Menu menu)
        {
            if (!await _menuService.UpdateMenuAsync(id, menu))
            {
                return BadRequest();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMenu(int id)
        {
            if (!await _menuService.DeleteMenuAsync(id))
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}