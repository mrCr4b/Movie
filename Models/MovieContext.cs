using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Movie.Models;

public partial class MovieContext : IdentityDbContext<AppUser>
{
    public MovieContext()
    {
    }

    public MovieContext(DbContextOptions<MovieContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-EI1DB05\\SQLEXPRESS;Initial Catalog=movie;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Seed Roles First
        modelBuilder.Entity<IdentityRole>().HasData(
            new IdentityRole { Id = "1", Name = "Admin", NormalizedName = "ADMIN" },
            new IdentityRole { Id = "2", Name = "User", NormalizedName = "USER" }
        );

        // Static password hash
        var passwordHash = "AQAAAAIAAYagAAAAEK1dpJl3IKJzk8m7cB0faE/DMbL4+xorUtP/axwgQBqxN1I8udThu//h+1HAZkWUAg==";

        var adminUser = new AppUser
        {
            Id = "1",
            UserName = "admin",
            NormalizedUserName = "ADMIN",
            Email = "admin@a.com",
            NormalizedEmail = "ADMIN@A.COM",
            EmailConfirmed = true,
            PasswordHash = passwordHash,
            SecurityStamp = "STATIC-SECURITY-ADMIN",
            ConcurrencyStamp = "STATIC-CONCURRENCY-ADMIN"
        };

        var user1 = new AppUser
        {
            Id = "2",
            UserName = "user1",
            NormalizedUserName = "USER1",
            Email = "user1@a.com",
            NormalizedEmail = "USER1@A.COM",
            EmailConfirmed = true,
            PasswordHash = passwordHash,
            SecurityStamp = "STATIC-SECURITY-USER",
            ConcurrencyStamp = "STATIC-CONCURRENCY-USER"
        };

        // Seed Users
        modelBuilder.Entity<AppUser>().HasData(adminUser, user1);

        // Now Seed User Roles (After Users Exist)
        modelBuilder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string> { UserId = "1", RoleId = "1" }, // Admin role
            new IdentityUserRole<string> { UserId = "2", RoleId = "2" }  // User role
        );

        OnModelCreatingPartial(modelBuilder);
    }



    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
