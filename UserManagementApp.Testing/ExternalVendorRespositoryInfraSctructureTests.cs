using Xunit;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagementApp.Core.Models;
using UserManagementApp.Infrastructure.Repositories;
using UserManagementApp.Infrastructure.Services;


namespace UserManagementApp.Testing.Infrastructure
{

    public class ExternalVendorRepositoryTests
    {
        private readonly Mock<IReqresHttpClientService> _mockService;
        private readonly ExternalVendorRespository _repository;

        public ExternalVendorRepositoryTests()
        {
            _mockService = new Mock<IReqresHttpClientService>();
            _repository = new ExternalVendorRespository(_mockService.Object);
        }

        [Fact]
        public async Task GetAllUsersAsync_ReturnsUserList()
        {
            // Arrange
            var expected = new List<UserDto>
        {new UserDto { Id = 1, FirstName = "Ram", LastName = "Kumar", Email = "ramkumar@gmail.com", Avatar = "" },
        new UserDto { Id = 2, FirstName = "Ravi", LastName = "Kumar", Email = "ravikumar@gmail.com", Avatar = "" }
        };

            _mockService.Setup(x => x.GetAllUsersAsync(1))
                        .ReturnsAsync(expected);

            // Act
            var result = await _repository.GetAllUsersAsync(1);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task GetUserByIdAsync_ReturnsUser()
        {
            // Arrange
            UserDto expected = new UserDto { Id = 2, FirstName = "Ram", LastName = "Kumar", Email = "ramkumar@gmail.com", Avatar = "" } ;

            _mockService.Setup(x => x.GetUserByIdAsync(2))
                        .ReturnsAsync(expected);

            // Act
            var result = await _repository.GetUserByIdAsync(2);

            // Assert
            Assert.Equal(expected, result);
        }
    }

}
