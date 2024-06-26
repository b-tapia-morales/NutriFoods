﻿namespace Domain.Models;

public class ContactInfo
{
    public Guid Id { get; set; }

    public string MobilePhone { get; set; } = null!;

    public string? FixedPhone { get; set; }

    public string Email { get; set; } = null!;

    public virtual Patient IdNavigation { get; set; } = null!;
}
