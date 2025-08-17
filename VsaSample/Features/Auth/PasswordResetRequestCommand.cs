using System.ComponentModel.DataAnnotations;

namespace VsaSample.Features.Auth;

public class PasswordResetRequestCommand
{
    [Required] [EmailAddress] public string Email { get; set; } = null!;
}