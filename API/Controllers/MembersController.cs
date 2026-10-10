using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using API.data;
using API.Entities;
using Microsoft.EntityFrameworkCore;
namespace API.Controllers
{
    [Route("api/[controller]")]
    public class MembersController(AppDbContext context) : ControllerBase
    {
      [HttpGet]
      public async Task< ActionResult<List<AppUser>>> GetAllMembers()
        {
            var Member =  await context.Users.ToListAsync();
            return Member;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetMember(string id)
        {
            var member =await context.Users.FindAsync(id);
            if(member ==null)
            {
                return NotFound();
            }
            return member;
        }










      
    }
}