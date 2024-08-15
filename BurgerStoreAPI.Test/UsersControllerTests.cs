using BurgerStoreAPI.BusinessLayer;
using BurgerStoreAPI.Controllers;
using BurgerStoreAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

public class UsersControllerTests
{
    private readonly Mock<IUserService> _userServiceMock;
    private readonly UsersController _controller;

    public UsersControllerTests()
    {
        _userServiceMock = new Mock<IUserService>();
        _controller = new UsersController(_userServiceMock.Object);
    }

    [Fact]
    public async Task GetUsers_ReturnsOkResult_WithListOfUsers()
    {
        // Arrange
        var users = new List<User> { new User { UserId = 1 }, new User { UserId = 2 } };
        _userServiceMock.Setup(service => service.GetAllUsersAsync()).ReturnsAsync(users);

        // Act
        var result = await _controller.GetUsers();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnUsers = Assert.IsType<List<User>>(okResult.Value);
        Assert.Equal(2, returnUsers.Count);
    }

    [Fact]
    public async Task GetUser_ReturnsNotFound_WhenUserDoesNotExist()
    {
        // Arrange
        _userServiceMock.Setup(service => service.GetUserByIdAsync(1)).ReturnsAsync((User)null);

        // Act
        var result = await _controller.GetUser(1);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task PostUser_ReturnsConflict_WhenUserAlreadyExists()
    {
        // Arrange
        var user = new User { UserId = 1, MobileNumber = "9730229579" };
        _userServiceMock.Setup(service => service.GetAllUsersAsync()).ReturnsAsync(new List<User> { user });

        // Act
        var result = await _controller.PostUser(user);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result.Result);
    }

    [Fact]
    public async Task DeleteUser_ReturnsNoContent_WhenUserIsDeleted()
    {
        // Arrange
        _userServiceMock.Setup(service => service.DeleteUserAsync(1)).ReturnsAsync(true);

        // Act
        var result = await _controller.DeleteUser(1);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task PutUser_ReturnsNotFound_WhenUserDoesNotExist()
    {
        // Arrange
        var user = new User { UserId = 1 };
        _userServiceMock.Setup(service => service.UpdateUserAsync(user)).ReturnsAsync(false);

        // Act
        var result = await _controller.PutUser(1, user);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }
}