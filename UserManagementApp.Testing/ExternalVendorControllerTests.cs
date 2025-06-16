using Xunit;
using Moq;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Mvc;
using UserManagementApp.Api.Controllers;
using UserManagementApp.Application.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagementApp.Core.Models;

namespace UserManagementApp.Tests.Controllers
{
    public class ExternalVendorControllerTests
    {
        private readonly Mock<IReqresApplication> _mockReqresApp;
        private readonly IMemoryCache _memoryCache;
        private readonly ExternalVendorController _controller;

        public ExternalVendorControllerTests()
        {
            _mockReqresApp = new Mock<IReqresApplication>();
            _memoryCache = new MemoryCache(new MemoryCacheOptions());
            _controller = new ExternalVendorController(_mockReqresApp.Object, _memoryCache);
        }

        
        // GetUserByIdAsync Tests
       
        [Fact]
        public async Task GetUserByIdAsync_ReturnsOkResult_WhenUserIsFound()
        {
            int userId = 1;
            var fakeUser = new UserDto{ Id = userId, FirstName = "Ram",LastName="Kumar", Email="ramkumar@gmail.com",Avatar="" };

            _mockReqresApp.Setup(x => x.GetUserByIdAsync(userId)).ReturnsAsync(fakeUser);

            var result = await _controller.GetUserByIdAsync(userId);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(fakeUser, okResult.Value);
        }

        [Fact]
        public async Task GetUserByIdAsync_ReturnsCachedResult_WhenAvailable()
        {
            int userId = 2;
            string cacheKey = $"User_{userId}";
            var cachedUser = new { Id = userId, Name = "Cached User" };

            _memoryCache.Set(cacheKey, cachedUser);

            var result = await _controller.GetUserByIdAsync(userId);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(cachedUser, okResult.Value);

            _mockReqresApp.Verify(x => x.GetUserByIdAsync(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async Task GetUserByIdAsync_ReturnsNotFound_WhenKeyNotFoundException()
        {
            int userId = 3;

            _mockReqresApp.Setup(x => x.GetUserByIdAsync(userId))
                          .ThrowsAsync(new KeyNotFoundException("User not found"));

            var result = await _controller.GetUserByIdAsync(userId);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Contains("User not found", notFoundResult.Value.ToString());
        }

        
        // GetAllUser Tests

        [Fact]
        public async Task GetAllUser_ReturnsOkResult_WhenDataFetchedFromService()
        {
            int page = 1;
            var fakeUsers = new List<UserDto>
            {
                new UserDto{ Id = 1, FirstName = "Ram",LastName="Kumar", Email="ramkumar@gmail.com",Avatar="" },
                new UserDto{ Id = 2, FirstName = "Ravi",LastName="Kumar", Email="ravikumar@gmail.com",Avatar="" }
            };

            _mockReqresApp.Setup(x => x.GetAllUsersAsync(page)).ReturnsAsync(fakeUsers);

            var result = await _controller.GetAllUser(page);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(fakeUsers, okResult.Value);
        }

        [Fact]
        public async Task GetAllUser_ReturnsCachedResult_WhenAvailable()
        {
            int page = 2;
            string cacheKey = $"Users_Page_{page}";
            var cachedUsers = new List<object>
            {
                new { Id = 3, Name = "Cached User" }
            };

            _memoryCache.Set(cacheKey, cachedUsers);

            var result = await _controller.GetAllUser(page);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(cachedUsers, okResult.Value);

            _mockReqresApp.Verify(x => x.GetAllUsersAsync(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async Task GetAllUser_ReturnsBadRequest_WhenArgumentOutOfRangeException()
        {
            int invalidPage = -1;

            _mockReqresApp.Setup(x => x.GetAllUsersAsync(invalidPage))
                          .ThrowsAsync(new ArgumentOutOfRangeException("page", "Page must be >= 1"));

            var result = await _controller.GetAllUser(invalidPage);

            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Contains("Page must be", badRequest.Value.ToString());
        }
    }
}
