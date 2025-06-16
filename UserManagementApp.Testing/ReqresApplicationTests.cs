using Moq;
using UserManagementApp.Application.Interface;
using UserManagementApp.Application;
using UserManagementApp.Core.Interface;
using UserManagementApp.Core.Models;

namespace UserManagementApp.Testing.Application
{
    public class ReqresApplicationTests
    {
        private readonly Mock<IExternalVendorRespository> _mockRepository;
        private readonly IReqresApplication _application;

        public ReqresApplicationTests()
        {
            _mockRepository = new Mock<IExternalVendorRespository>();
            _application = new ReqresApplication(_mockRepository.Object);
        }

        [Fact]
        public async Task GetUserByIdAsync_ReturnsUser_WhenValidId()
        {
            // Arrange
            int userId = 1;
            var expectedUser = new UserDto { Id = userId, FirstName = "Ram", LastName = "Kumar", Email = "ramkumar@gmail.com", Avatar = "" };


            _mockRepository.Setup(x => x.GetUserByIdAsync(userId))
                           .ReturnsAsync(expectedUser);

            // Act
            var result = await _application.GetUserByIdAsync(userId);

            // Assert
            Assert.Equal(expectedUser, result);
        }

        [Fact]
        public async Task GetUserByIdAsync_ThrowsArgumentException_WhenInvalidId()
        {
            // Arrange
            int invalidUserId = 0;

            // Act & Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(() =>
                _application.GetUserByIdAsync(invalidUserId));
            Assert.Contains("User ID must be greater than zero", ex.Message);
        }

        [Fact]
        public async Task GetAllUsersAsync_ReturnsUsers_WhenCalled()
        {
            // Arrange
            int page = 1;
            var expectedUsers = new List<UserDto>
            {
                new UserDto{ Id = 1, FirstName = "Ram",LastName="Kumar", Email="ramkumar@gmail.com",Avatar="" },
                new UserDto{ Id = 2, FirstName = "Ravi",LastName="Kumar", Email="ravikumar@gmail.com",Avatar="" }
            };

            _mockRepository.Setup(x => x.GetAllUsersAsync(page))
                           .ReturnsAsync(expectedUsers);

            // Act
            var result = await _application.GetAllUsersAsync(page);

            // Assert
            Assert.Equal(expectedUsers, result);
        }
    }
}
