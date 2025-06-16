using UserManagementApp.Core.Models;

namespace UserManagementApp.Infrastructure.Services
{
    public interface IReqresHttpClientService
    {
        Task<IEnumerable<UserDto>> GetAllUsersAsync(int page);
        Task<UserDto> GetUserByIdAsync(int userId);
    }
}