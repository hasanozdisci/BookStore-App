using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Entities;

namespace WebApi.DBOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                if (context.Books.Any())
                    return;
                context.Genres.AddRange(
                    new Genre
                    {
                        Name = "Personal Growth"
                    },
                    new Genre
                    {
                        Name = "Science Fiction"
                    },
                    new Genre
                    {
                        Name = "Romance"
                    });
                context.Authors.AddRange(new Author
                {
                    Name = "Eric",
                    Surname = "Ries",
                    DateOfBirth = new System.DateTime(1978, 09, 04)
                },
                new Author
                {
                    Name = "Charlotte",
                    Surname = "Perkins Gilman",
                    DateOfBirth = new System.DateTime(1860, 07, 03)
                },
                new Author
                {
                    Name = "Frank",
                    Surname = "Herbert",
                    DateOfBirth = new System.DateTime(1920, 10, 08)
                });

                context.Books.AddRange(new Book
                {
                    Title = "Lean Startup",
                    GenreId = 1, // Personel Growth
                    PageCount = 200,
                    PublishDate = new System.DateTime(2001, 06, 12),
                    AuthorId = 1
                },
                new Book
                {
                    Title = "Herland",
                    GenreId = 2, // Science Fiction
                    PageCount = 250,
                    PublishDate = new System.DateTime(2010, 05, 23),
                    AuthorId = 2
                },
                new Book
                {
                    Title = "Dune",
                    GenreId = 2, // Science Fiction
                    PageCount = 540,
                    PublishDate = new System.DateTime(2002, 12, 21),
                    AuthorId = 2
                });
                context.SaveChanges();
            }
        }
    }
}

