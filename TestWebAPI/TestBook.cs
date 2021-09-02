using Librarry.Data;
using Librarry.Data.Models;
using Librarry.Data.Models.ViewModels;
using Librarry.Data.Services;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWebAPI
{
    class TestBook
    {
        private static DbContextOptions<AppDbContext> dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: " WebAPI")
            .Options;
        AppDbContext _context;
        BooksService _bookServises;

        [OneTimeSetUp]
        public void Setup()
        {
            _context = new AppDbContext(dbContextOptions);
            _context.Database.EnsureCreated();

            SeedDatabase();

            _bookServises = new BooksService(_context);
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            _context.Database.EnsureDeleted();
        }

        private void SeedDatabase()
        {
            var authors = new List<Author>
            {
                new Author() { Id = 1, FullName= "Author 1" },
                new Author() { Id = 2, FullName= "Author 2" },
            };

            _context.Authors.AddRange(authors);
            _context.SaveChanges();

            var publishers = new List<Publisher>
            {
                new Publisher() { Id = 1, Name = "Publisher 1" },
                new Publisher() { Id = 2, Name = "Publisher 2" },
            };

            _context.Publishers.AddRange(publishers);
            _context.SaveChanges();

            var books = new List<Book>
            {
                new Book() { Id = 1, Title = "Book 1", Description = "Description", IsRead = true, DateRead = DateTime.Now, Rate = 1, Genre = "Genre", ImageURL = "ImageURL", PublisherId = 1 },
                new Book() { Id = 2, Title = "Book 2", Description = "Description", IsRead = true, DateRead = DateTime.Now, Rate = 1, Genre = "Genre", ImageURL = "ImageURL", PublisherId = 1 },
                new Book() { Id = 3, Title = "Book 3", Description = "Description", IsRead = true, DateRead = DateTime.Now, Rate = 1, Genre = "Genre", ImageURL = "ImageURL", PublisherId = 1 },
                new Book() { Id = 4, Title = "Book 4", Description = "Description", IsRead = true, DateRead = DateTime.Now, Rate = 1, Genre = "Genre", ImageURL = "ImageURL", PublisherId = 1 },
                new Book() { Id = 5, Title = "Book 5", Description = "Description", IsRead = true, DateRead = DateTime.Now, Rate = 1, Genre = "Genre", ImageURL = "ImageURL", PublisherId = 1 },
                new Book() { Id = 6, Title = "Book 6", Description = "Description", IsRead = true, DateRead = DateTime.Now, Rate = 1, Genre = "Genre", ImageURL = "ImageURL", PublisherId = 1 },
                new Book() { Id = 7, Title = "Book 7", Description = "Description", IsRead = true, DateRead = DateTime.Now, Rate = 1, Genre = "Genre", ImageURL = "ImageURL", PublisherId = 1 },
                new Book() { Id = 8, Title = "Book 8", Description = "Description", IsRead = true, DateRead = DateTime.Now, Rate = 1, Genre = "Genre", ImageURL = "ImageURL", PublisherId = 1 },
                new Book() { Id = 9, Title = "Book 9", Description = "Description", IsRead = true, DateRead = DateTime.Now, Rate = 1, Genre = "Genre", ImageURL = "ImageURL", PublisherId = 1 },
                new Book() { Id = 10, Title = "Book 10", Description = "Description", IsRead = true, DateRead = DateTime.Now, Rate = 1, Genre = "Genre", ImageURL = "ImageURL", PublisherId = 1 },
            };

            _context.Books.AddRange(books);
            _context.SaveChanges();
        }

        [Test, Order(1)]
        public void GetAllBooks_Test()
        {
            var result = _bookServises.GetAllBooks();
            Assert.That(result.Count, Is.EqualTo(10));
        }

        [Test, Order(2)]
        public void GetBookById_Test()
        {
            var result = _bookServises.GetBookById(1);
            Assert.That(result.Title, Is.EqualTo("Book 1"));
        }

        [Test, Order(3)]
        public void AddBook_Test()
        {
            var book = new BookVM()
            {
                Title = "Book 11",
                Description = "Description",
                IsRead = true,
                DateRead = DateTime.Now,
                Rate = 1,
                Genre = "Genre",
                ImageURL = "ImageURL",
                PublisherId = 1,
                AuthorIds = new List<int>() { 1, 2 }
            };

            _bookServises.AddBookWithAuthors(book);

            var result = _bookServises.GetBookById(11);
            Assert.That(result.Title, Is.EqualTo(book.Title));
        }

        [Test, Order(5)]
        public void EditBook_Test()
        {
            var book = new BookVM()
            {
                Title = "Book 1 Edit",
                Description = "Description",
                IsRead = true,
                DateRead = DateTime.Now,
                Rate = 1,
                Genre = "Genre",
                ImageURL = "ImageURL",
                PublisherId = 1,
                AuthorIds = new List<int>() { 1, 2 }
            };

            _bookServises.EditBook(1, book);

            var result = _bookServises.GetBookById(1);
            Assert.That(result.Title, Is.EqualTo(book.Title));
        }

        [Test, Order(5)]
        public void DeleteBook_Test()
        {
            _bookServises.DeleteBook(1);

            var result = _bookServises.GetAllBooks();
            Assert.That(result.Count, Is.EqualTo(10));
        }
    }
}
