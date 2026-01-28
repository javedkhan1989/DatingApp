using System;
using API.Entities;
using API.Extentions;
using API.Helpers;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;

namespace API.Controllers;

public class LikesController(ILikesRepository likesRepository) : BaseApiController
{
    [HttpPost("{targetMemberId}")]
    public async Task<ActionResult> ToggleLike(string targetMemberId)
    {
        var soureMemberId=User.GetMemberId();

        if(soureMemberId==targetMemberId) return BadRequest("You cannot like yourself");

        var existingLike=await likesRepository.GetMemberLike(soureMemberId,targetMemberId);

        if (existingLike == null)
        {
            var like=new MemberLike
            {
                SourceMemberId=soureMemberId,
                TargetMemberId=targetMemberId

            };

            likesRepository.AddLike(like);
        }
        else
        {
            likesRepository.DeleteLike(existingLike);
        }

        if(await likesRepository.SaveAllChanges()) return Ok();

        return BadRequest("failed to update like");
    }

    [HttpGet("list")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetCurrentMemberLikeIds()
    {
        return Ok(await likesRepository.GetCurrentMemeberLikeIds(User.GetMemberId()));
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedResult<Member>>> GetMemberLikes(
        [FromQuery] LikesParams likesParams)
    {
        likesParams.MemberId=User.GetMemberId();
        var members=await likesRepository.GetMemberLikes(likesParams);

        return Ok(members);
    }
}
