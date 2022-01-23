namespace Yoli.Core.WebApi.Routes
{
    public static class ApiRoutes
    {
        public const string Root = "api";
        public const string Base = $"{Root}";

        public static class IdentityRoutes
        {
            public const string SignupYoli = "signup/yoli";
            public const string SigninYoli = "signin/yoli";
            public const string SigninFacebook = "signin/facebook";
        }        
    }
    public static class ApiVersion
    {
        public const string V1 = "v3";
    }
}