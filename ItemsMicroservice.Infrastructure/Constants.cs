namespace ItemsMicroservice.Infrastructure;

public static class Constants
{
    public struct Users
    {
        public struct Roles
        {
            public const string Admin = "ItemsAdmin";
        }
    }

    public struct Policies
    {
        public const string RequireAdminRole = "ItemsMicroservice:ItemsAdmin";
    }
}
