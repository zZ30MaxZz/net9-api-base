namespace Dokypets.Infrastructure
{
    public class IdentitySettings
    {
        public LockoutSettings Lockout { get; set; } = new();
        public PasswordSettings Password { get; set; } = new();
        public SignInSettings SignIn { get; set; } = new();
    }

    public class LockoutSettings
    {
        public int MaxFailedAccessAttempts { get; set; }
        public TimeSpan DefaultLockoutTimeSpan { get; set; }
        public bool AllowedForNewUsers { get; set; }
    }

    public class PasswordSettings
    {
        public bool RequireDigit { get; set; }
        public bool RequireLowercase { get; set; }
        public bool RequireUppercase { get; set; }
        public bool RequireNonAlphanumeric { get; set; }
        public int RequiredLength { get; set; }
        public int RequiredUniqueChars { get; set; }
    }

    public class SignInSettings
    {
        public bool RequireConfirmedEmail { get; set; }
    }
}
