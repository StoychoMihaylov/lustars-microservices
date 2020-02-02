﻿namespace AuthAPI.Data.Context
{
    using Microsoft.EntityFrameworkCore;

    using AuthAPI.Data.Entities;
    using AuthAPI.Data.Interfaces;
    

    public class AuthDBContext : DbContext, IAuthDBContext
    {
        public AuthDBContext(DbContextOptions<AuthDBContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        public DbSet<TokenManager> Tokens { get; set; }
    }
}
