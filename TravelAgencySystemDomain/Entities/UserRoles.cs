namespace TravelAgencySystemDomain.Entities;

public static class UserRoles
{
    public const string Client = nameof(Client);
    public const string Agent = nameof(Agent);
    public const string Admin = nameof(Admin);
    
    public static readonly IReadOnlyCollection<string> All = [Client, Agent, Admin];
}