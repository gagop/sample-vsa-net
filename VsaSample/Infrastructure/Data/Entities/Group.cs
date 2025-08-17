﻿using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace VsaSample.Infrastructure.Data.Entities;

[Index(nameof(Code), IsUnique = true)]
public class Group
{
    [Key] public string Id { get; set; } = Guid.NewGuid().ToString();

    [Required] public string Name { get; set; } = null!;

    [Required] public string Color { get; set; } = null!;

    [Required] public string Code { get; set; } = null!;

    public ICollection<GroupUser> GroupUsers { get; set; } = new List<GroupUser>();
}