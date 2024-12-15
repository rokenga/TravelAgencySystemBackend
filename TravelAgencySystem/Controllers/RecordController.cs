using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelAgencySystemCore.Helpers.Auth;
using TravelAgencySystemCore.Interfaces.Service;
using TravelAgencySystemCore.Requests.Record;

namespace TravelAgencySystem.Controllers;

public class RecordController : BaseController
{
    private readonly IRecordService _recordService;
    
    public RecordController(IRecordService recordService)
    {
        _recordService = recordService;
    }
    
    [HttpGet("{id:guid}")]
    public IActionResult Get(Guid id)
    {
        return Ok(_recordService.GetRecordById(id));
    }
    
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_recordService.GetRecords());
    }
    
    [Authorize(Policy = PolicyNames.AgentRole)]
    [HttpPost]
    public IActionResult Post(RecordRequest request)
    {
        var res = _recordService.AddRecord(request, User.FindFirstValue(ClaimTypes.NameIdentifier));
        return CreatedAtAction(nameof(Get), new { id = res }, res);
    }
    
    [Authorize(Policy = PolicyNames.AgentRole)]
    [HttpPut("{id:guid}")]
    public IActionResult Put(Guid id, RecordRequest request)
    {
        var agentId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var res = _recordService.EditRecord(id, request, agentId);
        return Ok(res);
    }
    
    [Authorize(Policy = PolicyNames.AgentRole)]
    [HttpDelete("{id:guid}")]
    public IActionResult Delete(Guid id)
    {
        var agentId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return Ok(_recordService.DeleteRecord(id, agentId));
    }
}