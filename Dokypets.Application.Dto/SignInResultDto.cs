namespace Dokypets.Application.Dto
{
    public class SignInResultDto
    {
        public bool Succeeded { get; set; }
        public bool IsLockedOut { get; set; }
        public bool IsNotAllowed { get; set; }
        public bool RequiresTwoFactor { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }

        public List<string>? Roles { get; set; }

        public string? Token { get; set; }
        public Guid? RefreshToken { get; set; }

        public DateTime? TokenExpiresOn { get; set; }
    }
}
