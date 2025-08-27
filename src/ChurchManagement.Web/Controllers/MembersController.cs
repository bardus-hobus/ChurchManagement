using ChurchManagement.Application.DTOs;
using ChurchManagement.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ChurchManagement.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MembersController : ControllerBase
{
    private readonly IMemberService _memberService;
    private readonly ILogger<MembersController> _logger;

    public MembersController(IMemberService memberService, ILogger<MembersController> logger)
    {
        _memberService = memberService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberDto>>> GetAllMembers(CancellationToken cancellationToken)
    {
        try
        {
            var members = await _memberService.GetAllMembersAsync(cancellationToken);
            return Ok(members);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving all members");
            return StatusCode(500, "An error occurred while retrieving members");
        }
    }

    [HttpGet("active")]
    public async Task<ActionResult<IEnumerable<MemberDto>>> GetActiveMembers(CancellationToken cancellationToken)
    {
        try
        {
            var members = await _memberService.GetActiveMembersAsync(cancellationToken);
            return Ok(members);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving active members");
            return StatusCode(500, "An error occurred while retrieving active members");
        }
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<MemberDto>> GetMemberById(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var member = await _memberService.GetMemberByIdAsync(id, cancellationToken);
            if (member == null)
                return NotFound($"Member with ID {id} not found");

            return Ok(member);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving member with ID {MemberId}", id);
            return StatusCode(500, "An error occurred while retrieving the member");
        }
    }

    [HttpGet("by-email/{email}")]
    public async Task<ActionResult<MemberDto>> GetMemberByEmail(string email, CancellationToken cancellationToken)
    {
        try
        {
            var member = await _memberService.GetMemberByEmailAsync(email, cancellationToken);
            if (member == null)
                return NotFound($"Member with email {email} not found");

            return Ok(member);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving member with email {Email}", email);
            return StatusCode(500, "An error occurred while retrieving the member");
        }
    }

    [HttpPost]
    public async Task<ActionResult<MemberDto>> CreateMember([FromBody] CreateMemberDto createMemberDto, CancellationToken cancellationToken)
    {
        try
        {
            var member = await _memberService.CreateMemberAsync(createMemberDto, cancellationToken);
            return CreatedAtAction(nameof(GetMemberById), new { id = member.Id }, member);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating member");
            return StatusCode(500, "An error occurred while creating the member");
        }
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<MemberDto>> UpdateMember(Guid id, [FromBody] UpdateMemberDto updateMemberDto, CancellationToken cancellationToken)
    {
        try
        {
            var member = await _memberService.UpdateMemberAsync(id, updateMemberDto, cancellationToken);
            return Ok(member);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating member with ID {MemberId}", id);
            return StatusCode(500, "An error occurred while updating the member");
        }
    }

    [HttpPatch("{id:guid}/activate")]
    public async Task<IActionResult> ActivateMember(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            await _memberService.ActivateMemberAsync(id, cancellationToken);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error activating member with ID {MemberId}", id);
            return StatusCode(500, "An error occurred while activating the member");
        }
    }

    [HttpPatch("{id:guid}/deactivate")]
    public async Task<IActionResult> DeactivateMember(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            await _memberService.DeactivateMemberAsync(id, cancellationToken);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deactivating member with ID {MemberId}", id);
            return StatusCode(500, "An error occurred while deactivating the member");
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteMember(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            await _memberService.DeleteMemberAsync(id, cancellationToken);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting member with ID {MemberId}", id);
            return StatusCode(500, "An error occurred while deleting the member");
        }
    }

    [HttpGet("count")]
    public async Task<ActionResult<int>> GetMemberCount(CancellationToken cancellationToken)
    {
        try
        {
            var count = await _memberService.GetMemberCountAsync(cancellationToken);
            return Ok(count);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving member count");
            return StatusCode(500, "An error occurred while retrieving the member count");
        }
    }
}