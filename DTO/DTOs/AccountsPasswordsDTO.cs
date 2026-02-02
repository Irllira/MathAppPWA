using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DTOs
{
    public class AccountsPasswordsDTO
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
        public required string Email { get; set; }
        public required string Salt { get; set; }
        public required bool isActive { get; set; }
        public required string Role { get; set; }
    }
}

