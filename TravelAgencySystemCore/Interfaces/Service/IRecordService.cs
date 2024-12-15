using TravelAgencySystemCore.Requests.Record;
using TravelAgencySystemCore.Responses.Record;
using TravelAgencySystemDomain.Entities;

namespace TravelAgencySystemCore.Interfaces.Service;

public interface IRecordService
{
    RecordResponse GetRecordById(Guid guid);
    IEnumerable<Record> GetRecords();
    Guid AddRecord(RecordRequest record, string agentId);
    bool EditRecord(Guid guid, RecordRequest record, string agentId);
    bool DeleteRecord(Guid guid, string agentId);
    RecordWithCommentsResponse GetRecordWithCommentsById(Guid guid);
}