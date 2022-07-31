namespace CraftParty.Api.Controllers;

public static class Routes
{
    public static class Error
    {
        public const string Root = "/error";
    }
    public static class Authentication
    {
        public const string Login = "login";
        
        public const string Register = "register";

        public const string RefreshToken = "refresh-token";
    }
    
    public static class Users
    {
        public const string GetUsers = "users";
    }
}