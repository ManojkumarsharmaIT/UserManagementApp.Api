using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagementApp.Application.Interface;
using UserManagementApp.Core.Interface;
using UserManagementApp.Core.Models;

namespace UserManagementApp.Application
{
    public class ReqresApplication(IExternalVendorRespository externalVendorRespository) : IReqresApplication
    {
        public async Task<UserDto> GetUserByIdAsync(int userId)
        {
            if (userId <= 0)
            {
                throw new ArgumentException("User ID must be greater than zero.", nameof(userId));
            }
            return await externalVendorRespository.GetUserByIdAsync(userId);
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync(int page)
        {
            return await externalVendorRespository.GetAllUsersAsync(page);
        }
    }
}
