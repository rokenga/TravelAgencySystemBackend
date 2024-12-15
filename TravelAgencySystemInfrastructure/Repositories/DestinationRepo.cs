using Microsoft.EntityFrameworkCore;
using TravelAgencySystemCore.Interfaces.Repository;
using TravelAgencySystemDomain.Entities;
using TravelAgencySystemInfrastructure.Data;

namespace TravelAgencySystemInfrastructure.Repositories;

public class DestinationRepo : IDestinationRepo
{
    private readonly TravelAgencySystemDataContext _dbContext;

    public DestinationRepo(TravelAgencySystemDataContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public Destination GetDestinationById(Guid id)
    {
        return _dbContext.Destinations.FirstOrDefault(u => u.Id == id);
    }

    public IEnumerable<Destination> GetDestinations()
    {
        IEnumerable<Destination> destinations = _dbContext.Destinations;
        return destinations.ToList();
    }

    public Guid AddDestination(Destination destination)
    {
        destination.Id = Guid.NewGuid();
        _dbContext.Destinations.Add(destination);
        _dbContext.SaveChanges();
        
        return destination.Id;
    }

    public bool EditDestination(Destination destination)
    {
        _dbContext.Destinations.Update(destination);
        _dbContext.SaveChanges();
        return true;
    }

    public bool DeleteDestination(Guid guid)
    {
        _dbContext.Destinations
            .Where(d => d.Id == guid).ExecuteDelete();
        
        _dbContext.SaveChanges();
        return true;
    }

    public bool DestinationExists(Guid guid)
    {
        return _dbContext.Destinations.Any(d => d.Id == guid);
    }
}