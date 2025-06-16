using System.Net.Http.Json;
using UserManagementApp.Core.Models;

namespace UserManagementApp.Infrastructure.Services
{
    public class ReqresHttpClientService(HttpClient httpClient) : IReqresHttpClientService
    {
        public async Task<IEnumerable<UserDto>> GetAllUsersAsync(int page)
        {
            var result = await httpClient.GetFromJsonAsync<PaginatedUserResponse>($"/api/users?page={page}");

            // Fix: Return the collection of UserDto from the result object
            return result?.Data ?? Enumerable.Empty<UserDto>();
        }

        public async Task<UserDto> GetUserByIdAsync(int userId)
        {
            var result = await httpClient.GetFromJsonAsync<ReqresData>($"/api/users/{userId}");

            // Fix: Replace 'Empty<UserDto>()' with 'default' to resolve CS0103
            return result?.Data ?? default;
        }
    }
}
