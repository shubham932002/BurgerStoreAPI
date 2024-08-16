using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using BurgerStoreAPI.Controllers;
using BurgerStoreAPI.Models;
using BurgerStoreAPI.Services;

namespace BurgerStoreAPI.Tests
{
    public class CartItemsControllerTests
    {
        private readonly Mock<ICartItemService> _mockService;
        private readonly CartItemsController _controller;

        public CartItemsControllerTests()
        {
            _mockService = new Mock<ICartItemService>();
            _controller = new CartItemsController(_mockService.Object);
        }

        [Fact]
        public async Task GetCarts_ReturnsOkResult_WithListOfCartItems()
        {
            // Arrange
            var cartItems = new List<CartItem> { new CartItem { Id = 1, BurgerName = "Cheeseburger", Quantity = 2, Price = 5.99M } };
            _mockService.Setup(service => service.GetCartItemsAsync()).ReturnsAsync(cartItems);

            // Act
            var result = await _controller.GetCarts();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<CartItem>>(okResult.Value);
            Assert.Single(returnValue);
        }

        [Fact]
        public async Task GetCartItem_ReturnsOkResult_WithCartItem()
        {
            // Arrange
            var cartItem = new CartItem { Id = 1, BurgerName = "Cheeseburger", Quantity = 2, Price = 5.99M };
            _mockService.Setup(service => service.GetCartItemByIdAsync(1)).ReturnsAsync(cartItem);

            // Act
            var result = await _controller.GetCartItem(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<CartItem>(okResult.Value);
            Assert.Equal(cartItem.Id, returnValue.Id);
        }

        [Fact]
        public async Task GetCartItem_ReturnsNotFound_WhenCartItemDoesNotExist()
        {
            // Arrange
            _mockService.Setup(service => service.GetCartItemByIdAsync(1)).ReturnsAsync((CartItem)null);

            // Act
            var result = await _controller.GetCartItem(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task PostCartItem_ReturnsCreatedAtAction_WithNewCartItem()
        {
            // Arrange
            var cartItem = new CartItem { Id = 1, BurgerName = "Cheeseburger", Quantity = 2, Price = 5.99M };
            _mockService.Setup(service => service.AddCartItemAsync(cartItem)).ReturnsAsync(cartItem);

            // Act
            var result = await _controller.PostCartItem(cartItem);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnValue = Assert.IsType<CartItem>(createdAtActionResult.Value);
            Assert.Equal(cartItem.Id, returnValue.Id);
        }

        [Fact]
        public async Task PutCartItem_ReturnsNoContent_WhenUpdateIsSuccessful()
        {
            // Arrange
            var cartItem = new CartItem { Id = 1, BurgerName = "Cheeseburger", Quantity = 2, Price = 5.99M };
            _mockService.Setup(service => service.UpdateCartItemAsync(cartItem.Id, cartItem)).ReturnsAsync(true);

            // Act
            var result = await _controller.PutCartItem(cartItem.Id, cartItem);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task PutCartItem_ReturnsBadRequest_WhenIdMismatch()
        {
            // Arrange
            var cartItem = new CartItem { Id = 1, BurgerName = "Cheeseburger", Quantity = 2, Price = 5.99M };

            // Act
            var result = await _controller.PutCartItem(2, cartItem);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task DeleteCartItem_ReturnsNoContent_WhenDeleteIsSuccessful()
        {
            // Arrange
            _mockService.Setup(service => service.DeleteCartItemAsync(1)).ReturnsAsync(true);

            // Act
            var result = await _controller.DeleteCartItem(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteCartItem_ReturnsNotFound_WhenCartItemDoesNotExist()
        {
            // Arrange
            _mockService.Setup(service => service.DeleteCartItemAsync(1)).ReturnsAsync(false);

            // Act
            var result = await _controller.DeleteCartItem(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteAllCartItems_ReturnsNoContent_WhenDeleteAllIsSuccessful()
        {
            // Arrange
            _mockService.Setup(service => service.DeleteAllCartItemsAsync()).ReturnsAsync(true);

            // Act
            var result = await _controller.DeleteAllCartItems();

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteAllCartItems_ReturnsNotFound_WhenNoItemsToDelete()
        {
            // Arrange
            _mockService.Setup(service => service.DeleteAllCartItemsAsync()).ReturnsAsync(false);

            // Act
            var result = await _controller.DeleteAllCartItems();

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}

