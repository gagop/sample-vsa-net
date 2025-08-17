using System.ComponentModel.DataAnnotations;

namespace VsaSample.Features.Auth;

public class ResetPasswordCommand
{
    [Required] public string Token { get; set; } = null!;

    [Required]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
    public string NewPassword { get; set; } = null!;
}