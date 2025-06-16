using UserManagementApp.Core.Models;

namespace UserManagementApp.Application.Interface
{
    public interface IReqresApplication
    {
        Task<UserDto> GetUserByIdAsync(int userId);
        Task<IEnumerable<UserDto>> GetAllUsersAsync(int page);
    }
}