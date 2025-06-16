using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagementApp.Core.Models;

namespace UserManagementApp.Core.Interface
{
    public interface IExternalVendorRespository
    {
        Task<UserDto> GetUserByIdAsync(int userId);
        Task<IEnumerable<UserDto>>  GetAllUsersAsync(int page);
    }
}
