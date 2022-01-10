﻿using BibliotecAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace BibliotecAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<CategoryEntity> Categories { get; set; }

        public DbSet<AuthorEntity> Author { get; set; }

    }
}
