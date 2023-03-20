namespace MovieApp.Api.Data.Authentication
{
    /// <summary>
    /// Token Response object sent back to client if user login is successfuly
    /// </summary>
    public class TokenResponse
    {
        public string? Token { get; set; }
    }
}
