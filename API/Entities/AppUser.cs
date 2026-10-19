using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Entities
{
    public class AppUser
    {
        public string Id { get; set; }=Guid.NewGuid().ToString();
        public required string  DisplayName { get; set; }
        public required string Email { get; set; }
       public required byte[] PasswordHash { get; set; }
        public required byte[] PasswordSalt { get; set; }

        internal ActionResult<UserDto> toDto(ITokenService tokenService)
        {
            throw new NotImplementedException();
        }
    }
}