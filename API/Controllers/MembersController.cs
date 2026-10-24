using Microsoft.AspNetCore.Mvc;
using API.data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using API.Interfaces;
namespace API.Controllers
{
    [Authorize]
   
    public class MembersController(IMemberRepository memberRepository) : BaseApiController
    {
      [HttpGet]
      public async Task<ActionResult<IReadOnlyList<Member>>> GetMembers()
        {
            return Ok(await memberRepository.GetMembersAsync());
            
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Member>> GetMember(string id)
        {
            var member =await memberRepository.GetMemberByIdAsync(id);
            if(member ==null)
            {
                return NotFound();
            }
            return member;
        }



[HttpGet("{Id}/photos")]
        public async Task<ActionResult<IReadOnlyList<Photo>>> GetPhotosForMember(string Id)
        {
            var photos = await memberRepository.GetPhotosForMemberAsync(Id);
            if (photos == null || !photos.Any())
            {
                return NotFound();
            }
            return Ok(photos);
        }






      
    }
}