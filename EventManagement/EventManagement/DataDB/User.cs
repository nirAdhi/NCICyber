using System;
using System.Collections.Generic;

namespace EventManagement.DataDB
{
    public partial class User
    {
        public Guid Id { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? RoleId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; } = null!;
        public DateTime? Dob { get; set; }
        public int? Gender { get; set; }
        public string? Mobile { get; set; }
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string PasswordSalt { get; set; } = null!;
        public string? VerificationToken { get; set; }
        public DateTime? VerificationTokenExpires { get; set; }
        public DateTime? VerifiedAt { get; set; }
    }
}
