using Microsoft.EntityFrameworkCore;
using TravelAgencySystemCore.Interfaces.Repository;
using TravelAgencySystemDomain.Entities;
using TravelAgencySystemInfrastructure.Data;

namespace TravelAgencySystemInfrastructure.Repositories;

public class RecordRepo : IRecordRepo
{
    private readonly TravelAgencySystemDataContext _dbContext;
    
    public RecordRepo(TravelAgencySystemDataContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public Record GetRecordById(Guid id)
    {
        return _dbContext.Records.FirstOrDefault(u => u.Id == id);
    }

    public IEnumerable<Record> GetRecords()
    {
        IEnumerable<Record> records = _dbContext.Records;
        return records.ToList();
    }

    public Guid AddRecord(Record record)
    {
        record.Id = Guid.NewGuid();
        record.CreatedAt = DateTime.Now;
        record.UpdatedAt = DateTime.Now;
        _dbContext.Records.Add(record);
        _dbContext.SaveChanges();
        
        return record.Id;
    }

    public bool EditRecord(Record record)
    {
        record.UpdatedAt = DateTime.Now;
        _dbContext.Records.Update(record);
        _dbContext.SaveChanges();
        return true;
    }

    public bool DeleteRecord(Guid guid)
    {
        _dbContext.Records
            .Where(d => d.Id == guid).ExecuteDelete();
        
        _dbContext.SaveChanges();
        return true;
    }

    public bool RecordExists(Guid guid)
    {
        return _dbContext.Records.Any(d => d.Id == guid);
    }

    public IEnumerable<Record> GetRecordsForDestinationById(Guid id)
    {
        return _dbContext.Records.Where(c => c.DestinationId == id);
    }

}