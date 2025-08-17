﻿using System.ComponentModel.DataAnnotations;

namespace VsaSample.Infrastructure.Data.Entities;

public class RefreshToken
{
    [Key] public Guid Id { get; set; }

    public string Token { get; set; }
    public string UserId { get; set; }
    public DateTime ExpiresAt { get; set; }
    public bool IsRevoked { get; set; }
}