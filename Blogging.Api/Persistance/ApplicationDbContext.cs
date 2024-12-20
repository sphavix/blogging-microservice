﻿using Blogging.Api.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Blogging.Api.Persistance
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Picture> Pictures { get; set; }
    }
}
