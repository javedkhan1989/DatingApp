using System;
using API.Data;
using API.Entities;
using API.Extentions;
using API.Helpers;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;

namespace API.Controllers;

public class LikesController(IUnitOfWork uow) : BaseApiController
{
    [HttpPost("{targetMemberId}")]
    public async Task<ActionResult> ToggleLike(string targetMemberId)
    {
        var soureMemberId=User.GetMemberId();

        if(soureMemberId==targetMemberId) return BadRequest("You cannot like yourself");

        var existingLike=await uow.LikesRepository.GetMemberLike(soureMemberId,targetMemberId);

        if (existingLike == null)
        {
            var like=new MemberLike
            {
                SourceMemberId=soureMemberId,
                TargetMemberId=targetMemberId

            };

            uow.LikesRepository.AddLike(like);
        }
        else
        {
            uow.LikesRepository.DeleteLike(existingLike);
        }

        if(await uow.Complete()) return Ok();

        return BadRequest("failed to update like");
    }

    [HttpGet("list")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetCurrentMemberLikeIds()
    {
        return Ok(await uow.LikesRepository.GetCurrentMemeberLikeIds(User.GetMemberId()));
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedResult<Member>>> GetMemberLikes(
        [FromQuery] LikesParams likesParams)
    {
        likesParams.MemberId=User.GetMemberId();
        var members=await uow.LikesRepository.GetMemberLikes(likesParams);

        return Ok(members);
    }
}
