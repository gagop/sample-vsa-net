using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VsaSample.Features.Auth;
using VsaSample.Infrastructure.Data.Entities;

namespace VsaSample.Infrastructure.Data.Context;

public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<User>(options)
{
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<GroupUser> GroupUsers { get; set; }
    public DbSet<PasswordResetToken> PasswordResetTokens { get; set; } = null!;
}