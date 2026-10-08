using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.EFCore.Config;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;


namespace Repositories.EFCore
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions<RepositoryContext> options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BookConfig).Assembly);
        }
    }
}
