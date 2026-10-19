using Microsoft.AspNetCore.Mvc;
using API.data;
using API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
namespace API.Controllers
{
   
    public class MembersController(AppDbContext context) : BaseApiController
    {
      [HttpGet]
      public async Task< ActionResult<List<AppUser>>> GetMembers()
        {
            var Member =  await context.Users.ToListAsync();
            return Member;
        }
        [Authorize]
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