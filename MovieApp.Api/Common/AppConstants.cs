namespace MovieApp.Api.Common
{
    /// <summary>
    /// Class to store string constants used throughout the app
    /// </summary>
    public static class AppConstants
    {            
        public static class Configurations
        {
            public const string AppStartUpSeedData = "AppStartUp:SeedData";
            public const string JWTValidIssue = "JWT:ValidIssuer";
            public const string JWTValidAudience = "JWT:ValidAudience";
            public const string JWTSecret = "JWT:Secret";
            public const string IpRateLimitingSettings = "IpRateLimitingSettings";
        }

        public static class Messages
        {
            public const string IdColumnsNoMatch = "Id columns do not match";
            public const string InternalError = "The application has experienced an internal error. Please try again.";
            public const string InvalidUser = "Invalid user request";
            public const string NameFilterRequired = "Name filter must be provided";
            public const string NoDataProvided = "No data provided";
            public const string NoRecordsFound = "No records found in database for Id provided";
        }
    }
}
