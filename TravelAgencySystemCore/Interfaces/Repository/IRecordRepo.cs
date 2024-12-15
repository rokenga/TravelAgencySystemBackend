using TravelAgencySystemDomain.Entities;

namespace TravelAgencySystemCore.Interfaces.Repository;

public interface IRecordRepo
{
    Record? GetRecordById(Guid id);
    IEnumerable<Record> GetRecords();
    Guid AddRecord(Record record);
    bool EditRecord(Record record);
    bool DeleteRecord(Guid guid);
    bool RecordExists(Guid guid);
    IEnumerable<Record> GetRecordsForDestinationById(Guid id);
}