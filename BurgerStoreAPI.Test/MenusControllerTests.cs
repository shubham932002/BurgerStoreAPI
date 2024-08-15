using BurgerStoreAPI.BusinessLayer;
using BurgerStoreAPI.Controllers;
using BurgerStoreAPI.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace BurgerStoreAPI.Tests.Controllers
{
    public class MenusControllerTests
    {
        private readonly Mock<IMenuService> _menuServiceMock;
        private readonly MenusController _controller;

        public MenusControllerTests()
        {
            _menuServiceMock = new Mock<IMenuService>();
            _controller = new MenusController(_menuServiceMock.Object);
        }

        [Fact]
        public async Task GetMenus_ReturnsOkResult_WithListOfMenus()
        {
            // Arrange
            var menus = new List<Menu>
            {
                new Menu { Id = 1, Name = "Burger" },
                new Menu { Id = 2, Name = "Fries" }
            };

            _menuServiceMock.Setup(service => service.GetAllMenusAsync()).ReturnsAsync(menus);

            // Act
            var result = await _controller.GetMenus();

            // Assert
            var okResult = result.Result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().BeEquivalentTo(menus);
        }

        [Fact]
        public async Task GetMenu_ReturnsNotFound_WhenMenuDoesNotExist()
        {
            // Arrange
            int menuId = 1;
            _menuServiceMock.Setup(service => service.GetMenuByIdAsync(menuId)).ReturnsAsync((Menu)null);

            // Act
            var result = await _controller.GetMenu(menuId);

            // Assert
            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task GetMenu_ReturnsOkResult_WithMenu()
        {
            // Arrange
            int menuId = 1;
            var menu = new Menu { Id = menuId, Name = "Burger" };
            _menuServiceMock.Setup(service => service.GetMenuByIdAsync(menuId)).ReturnsAsync(menu);

            // Act
            var result = await _controller.GetMenu(menuId);

            // Assert
            var okResult = result.Result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().BeEquivalentTo(menu);
        }

        [Fact]
        public async Task PostMenu_ReturnsCreatedAtActionResult_WithMenu()
        {
            // Arrange
            var menu = new Menu { Id = 1, Name = "Burger" };
            _menuServiceMock.Setup(service => service.CreateMenuAsync(menu)).ReturnsAsync(menu);

            // Act
            var result = await _controller.PostMenu(menu);

            // Assert
            var createdAtActionResult = result.Result as CreatedAtActionResult;
            createdAtActionResult.Should().NotBeNull();
            createdAtActionResult.StatusCode.Should().Be(201);
            createdAtActionResult.Value.Should().BeEquivalentTo(menu);
        }

        [Fact]
        public async Task PutMenu_ReturnsBadRequest_WhenIdMismatch()
        {
            // Arrange
            var menu = new Menu { Id = 2, Name = "Burger" };

            // Act
            var result = await _controller.PutMenu(1, menu);

            // Assert
            result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public async Task PutMenu_ReturnsNoContent_WhenUpdateIsSuccessful()
        {
            // Arrange
            var menu = new Menu { Id = 1, Name = "Updated Burger" };
            _menuServiceMock.Setup(service => service.UpdateMenuAsync(menu.Id, menu)).ReturnsAsync(true);

            // Act
            var result = await _controller.PutMenu(menu.Id, menu);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task DeleteMenu_ReturnsNotFound_WhenMenuDoesNotExist()
        {
            // Arrange
            int menuId = 1;
            _menuServiceMock.Setup(service => service.DeleteMenuAsync(menuId)).ReturnsAsync(false);

            // Act
            var result = await _controller.DeleteMenu(menuId);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task DeleteMenu_ReturnsNoContent_WhenDeleteIsSuccessful()
        {
            // Arrange
            int menuId = 1;
            _menuServiceMock.Setup(service => service.DeleteMenuAsync(menuId)).ReturnsAsync(true);

            // Act
            var result = await _controller.DeleteMenu(menuId);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }
    }
}