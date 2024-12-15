namespace TravelAgencySystemCore.Responses.Auth;

public class SuccessfulLoginResponse
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}