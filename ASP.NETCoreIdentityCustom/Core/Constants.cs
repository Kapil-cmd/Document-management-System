﻿namespace ASP.NETCoreIdentityCustom.Core
{
    public static class Constants
    {
        public static class Roles
        {
            public const string Administrator = "Administrator";
            public const string Writer = "Writer";
            public const string User = "User";
         
            
        }

        public static class Policies
        {
            public const string RequireAdmin = "RequireAdmin";
            public const string RequireWriter = "RequireWriter";
            public const string RequireUser = "RequireUser";
           
        }
    }
}
