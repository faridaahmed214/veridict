using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO.Auth
{
    public class UserDto
    {
        public string Email { get; set; } = default!;
        public string UserName { get; set; } = default!;
        public string Token { get; set; } = default!;

    }
}
