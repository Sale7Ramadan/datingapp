using Microsoft.AspNetCore.Mvc;
using API.data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using API.Interfaces;
using API.DTOs;
using System.Security.Claims;
using API.Extensions;
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
 [HttpPut]
 public async Task<ActionResult> UpdateMember(MemberUpdateDto memberUpdateDto)
        {
            var memberId= User.GetMemberId();
           
            var member = await memberRepository.GetMemberForUpdate(memberId);
            if(member == null)
            {
                return BadRequest("Member not found.");
            }
            member.DisplayName = memberUpdateDto.DisplayName ?? member.DisplayName;
            member.Description = memberUpdateDto.Description ?? member.Description;
            member.City = memberUpdateDto.City ?? member.City;
            member.Country = memberUpdateDto.Country ?? member.Country;
            member.User.DisplayName = memberUpdateDto.DisplayName ?? member.User.DisplayName;
            memberRepository.Update(member);
            if(await memberRepository.SaveAllAsync())
            {
                return NoContent();
            }
            return BadRequest("Oops! Something went wrong. Please try again.");
        }





      
    }
}