using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelAgencySystemCore.Helpers.Auth;
using TravelAgencySystemCore.Interfaces.Service;
using TravelAgencySystemCore.Requests.Destination;
using TravelAgencySystemDomain.Entities;

namespace TravelAgencySystem.Controllers;

public class DestinationController : BaseController
{
    private readonly IDestinationService _destinationService;
    
    public DestinationController(IDestinationService destinationService)
    {
        _destinationService = destinationService;
    }
    
    [HttpGet("{id:guid}")]
    public IActionResult Get(Guid id)
    {
        return Ok(_destinationService.GetDestinationById(id));
    }
    
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_destinationService.GetDestinations());
    }
    
    [Authorize(Policy = PolicyNames.AgentRole)]
    [HttpPost]
    public IActionResult Post([FromBody] DestinationRequest request)
    {
        if (User.IsInRole(UserRoles.Client))
        {
            return Forbid();
        }
        var res = _destinationService.AddDestination(request);
        return CreatedAtAction(nameof(Get), new { id = res }, res);
    }
    
    [Authorize(Policy = PolicyNames.AgentRole)]
    [HttpPut("{id:guid}")]
    public IActionResult Put(Guid id, [FromBody] DestinationRequest request)
    {
        var res = _destinationService.EditDestination(id, request);
        return Ok(res);
    }
    
    [Authorize(Policy = PolicyNames.AdminRole)]
    [HttpDelete("{id:guid}")]
    public IActionResult DeleteDestination(Guid id)
    {
        return Ok(_destinationService.DeleteDestination(id));
    }
    
    [HttpGet("{destinationId:guid}/Records/{recordId:guid}")]
    public IActionResult GetDestination(Guid destinationId, Guid recordId)
    {
        var destinations = _destinationService.GetDestinationWithSpecificRecordAndComments(recordId, destinationId);
        return Ok(destinations);
    }
    
    [HttpGet("{destinationId:guid}/Records")]
    public IActionResult GetAllRecordsForDestination(Guid destinationId)
    {
        var records = _destinationService.GetDestinationWithAllRecords(destinationId);
        return Ok(records);
    }
}