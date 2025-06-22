using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace BlazorWebApp.Data
{
    public class IdDetailsDbContext : DbContext
    {
        public IdDetailsDbContext(DbContextOptions<IdDetailsDbContext> options) : base(options) { }

        public DbSet<RegistrationModel> Registrations { get; set; }
        public DbSet<UserFile> UserFiles { get; set; }
    }

    public class RegistrationModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public bool EmailVerified { get; set; }
        public string VerificationToken { get; set; }
        public byte[] ProfileImage { get; set; }
        public string Address { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; } // Added Department column
        public DateTime CreatedAt { get; set; } // Added for registration date
        public ICollection<UserFile> Files { get; set; }
    }

    public class UserFile
    {
        public int Id { get; set; }
        public int RegistrationModelId { get; set; }
        public RegistrationModel Registration { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public byte[] Data { get; set; }
        public DateTime UploadedAt { get; set; }
        public string UploadedBy { get; set; }
    }
}
