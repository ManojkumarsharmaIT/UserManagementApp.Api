using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagementApp.Core.Interface;
using UserManagementApp.Core.Models;
using UserManagementApp.Infrastructure.Services;

namespace UserManagementApp.Infrastructure.Repositories
{
    public class ExternalVendorRespository(IReqresHttpClientService reqresHttpClientService) : IExternalVendorRespository
    {
        public async Task<IEnumerable<UserDto>> GetAllUsersAsync(int page)
        {
            return await reqresHttpClientService.GetAllUsersAsync(page);
        }

        public async Task<UserDto> GetUserByIdAsync(int userId)
        {
            return await reqresHttpClientService.GetUserByIdAsync(userId);
        }
    }
}
