using System.Collections.Generic;
using System.Security.Policy;
using System.Threading.Tasks;
using BurgerStoreAPI.BusinessLayer;
using BurgerStoreAPI.Controllers;
using BurgerStoreAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace BurgerStoreAPI.Tests
{
    public class AdminsControllerTests
    {
        private readonly Mock<IAdminService> _mockAdminService;
        private readonly AdminsController _controller;

        public AdminsControllerTests()
        {
            _mockAdminService = new Mock<IAdminService>();
            _controller = new AdminsController(_mockAdminService.Object);
        }

        [Fact]
        public async Task GetAdmins_ReturnsOkResult_WithListOfAdmins()
        {
            // Arrange
            var adminList = new List<Admin>
            {
                new Admin { Id = 1, UserName = "admin1", Password = "password1" },
                new Admin { Id = 2, UserName = "admin2", Password = "password2" }
            };
            _mockAdminService.Setup(service => service.GetAdminsAsync()).ReturnsAsync(adminList);

            // Act
            var result = await _controller.GetAdmins();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<Admin>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public async Task GetAdmin_ReturnsOkResult_WithAdmin()
        {
            // Arrange
            var admin = new Admin { Id = 1, UserName = "admin1", Password = "password1" };
            _mockAdminService.Setup(service => service.GetAdminByIdAsync(1)).ReturnsAsync(admin);

            // Act
            var result = await _controller.GetAdmin(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<Admin>(okResult.Value);
            Assert.Equal(1, returnValue.Id);
        }

        [Fact]
        public async Task GetAdmin_ReturnsNotFound_WhenAdminNotExists()
        {
            // Arrange
            _mockAdminService.Setup(service => service.GetAdminByIdAsync(1)).ReturnsAsync((Admin)null);

            // Act
            var result = await _controller.GetAdmin(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task PutAdmin_ReturnsNoContent_WhenUpdateIsSuccessful()
        {
            // Arrange
            var admin = new Admin { Id = 1, UserName = "admin1", Password = "password1" };
            _mockAdminService.Setup(service => service.UpdateAdminAsync(admin)).ReturnsAsync(true);

            // Act
            var result = await _controller.PutAdmin(1, admin);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task PutAdmin_ReturnsBadRequest_WhenIdMismatch()
        {
            // Arrange
            var admin = new Admin { Id = 1, UserName = "admin1", Password = "password1" };

            // Act
            var result = await _controller.PutAdmin(2, admin);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task PutAdmin_ReturnsNotFound_WhenUpdateFails()
        {
            // Arrange
            var admin = new Admin { Id = 1, UserName = "admin1", Password = "password1" };
            _mockAdminService.Setup(service => service.UpdateAdminAsync(admin)).ReturnsAsync(false);

            // Act
            var result = await _controller.PutAdmin(1, admin);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task PostAdmin_ReturnsCreatedAtActionResult_WithNewAdmin()
        {
            // Arrange
            var admin = new Admin { Id = 1, UserName = "admin1", Password = "password1" };
            _mockAdminService.Setup(service => service.CreateAdminAsync(admin)).ReturnsAsync(admin);

            // Act
            var result = await _controller.PostAdmin(admin);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnValue = Assert.IsType<Admin>(createdAtActionResult.Value);
            Assert.Equal(1, returnValue.Id);
        }

        [Fact]
        public async Task DeleteAdmin_ReturnsNoContent_WhenDeleteIsSuccessful()
        {
            // Arrange
            _mockAdminService.Setup(service => service.DeleteAdminAsync(1)).ReturnsAsync(true);

            // Act
            var result = await _controller.DeleteAdmin(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteAdmin_ReturnsNotFound_WhenDeleteFails()
        {
            // Arrange
            _mockAdminService.Setup(service => service.DeleteAdminAsync(1)).ReturnsAsync(false);

            // Act
            var result = await _controller.DeleteAdmin(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}


