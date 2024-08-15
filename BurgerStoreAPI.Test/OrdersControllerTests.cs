using BurgerStoreAPI.BusinessLayer;
using BurgerStoreAPI.Controllers;
using BurgerStoreAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

public class OrdersControllerTests
{
    private readonly Mock<IOrderService> _orderServiceMock;
    private readonly OrdersController _controller;

    public OrdersControllerTests()
    {
        _orderServiceMock = new Mock<IOrderService>();
        _controller = new OrdersController(_orderServiceMock.Object);
    }

    [Fact]
    public async Task GetOrders_ReturnsOkResult_WithListOfOrders()
    {
        // Arrange
        var orders = new List<Order> { new Order { UniqueID = 1 }, new Order { UniqueID = 2 } };
        _orderServiceMock.Setup(service => service.GetAllOrdersAsync()).ReturnsAsync(orders);

        // Act
        var result = await _controller.GetOrders();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnOrders = Assert.IsType<List<Order>>(okResult.Value);
        Assert.Equal(2, returnOrders.Count);
    }

    [Fact]
    public async Task GetOrder_ReturnsNotFound_WhenOrderDoesNotExist()
    {
        // Arrange
        _orderServiceMock.Setup(service => service.GetOrderByIdAsync(1)).ReturnsAsync((Order)null);

        // Act
        var result = await _controller.GetOrder(1);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task PostOrder_ReturnsCreatedAtAction_WhenOrderIsCreated()
    {
        // Arrange
        var order = new Order { UniqueID = 1 };
        _orderServiceMock.Setup(service => service.CreateOrderAsync(order)).ReturnsAsync(order);

        // Act
        var result = await _controller.PostOrder(order);

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        Assert.Equal("GetOrder", createdAtActionResult.ActionName);
        Assert.Equal(order.UniqueID, createdAtActionResult.RouteValues["id"]);
    }

    [Fact]
    public async Task DeleteOrder_ReturnsNoContent_WhenOrderIsDeleted()
    {
        // Arrange
        _orderServiceMock.Setup(service => service.DeleteOrderAsync(1)).ReturnsAsync(true);

        // Act
        var result = await _controller.DeleteOrder(1);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task PutOrder_ReturnsNotFound_WhenOrderDoesNotExist()
    {
        // Arrange
        var order = new Order { UniqueID = 1 };
        _orderServiceMock.Setup(service => service.UpdateOrderAsync(order)).ReturnsAsync(false);

        // Act
        var result = await _controller.PutOrder(1, order);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }
}