using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories.EFCore.Config
{
    public class BookConfig : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasData(
                new Book { Id = 1, title = "Book 1", price = 75 },
                new Book { Id = 2, title = "Book 2", price = 75 },
                new Book { Id = 3, title = "Book 3", price = 75 }
            );
        }
    }
}
