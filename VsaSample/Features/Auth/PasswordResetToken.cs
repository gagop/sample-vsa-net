using System.ComponentModel.DataAnnotations;
using VsaSample.Infrastructure.Data.Entities;

namespace VsaSample.Features.Auth;

public class PasswordResetToken
{
    [Key] public Guid Id { get; set; }

    [Required] public Guid UserId { get; set; }

    [Required] public string TokenHash { get; set; } = null!;

    [Required] public DateTime ExpiresAt { get; set; }

    public bool IsUsed { get; set; } = false;

    public User User { get; set; } = null!;
}