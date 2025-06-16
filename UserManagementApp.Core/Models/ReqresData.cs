using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace UserManagementApp.Core.Models
{
  

    public class ReqresData
    {
        [JsonPropertyName("data")]
        public UserDto Data { get; set; }

        [JsonPropertyName("support")]
        public SupportDto Support { get; set; }
    }

   
}
