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
    class TestAuthor
    {
        private static DbContextOptions<AppDbContext> dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: " WebAPI")
            .Options;
        AppDbContext _context;
        AuthorsService _authorServises;

        [OneTimeSetUp]
        public void Setup()
        {
            _context = new AppDbContext(dbContextOptions);
            _context.Database.EnsureCreated();

            SeedDatabase();

            _authorServises = new AuthorsService(_context);
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
                new Author() { Id = 1, FullName = "Author 1" },
                new Author() { Id = 2, FullName = "Author 2" },
                new Author() { Id = 3, FullName = "Author 3" },
                new Author() { Id = 4, FullName = "Author 4" },
                new Author() { Id = 5, FullName = "Author 5" },
                new Author() { Id = 6, FullName = "Author 6" },
                new Author() { Id = 7, FullName = "Author 7" },
                new Author() { Id = 8, FullName = "Author 8" },
                new Author() { Id = 9, FullName = "Author 9" },
                new Author() { Id = 10, FullName = "Author 10" },
            };

            _context.Authors.AddRange(authors);
            _context.SaveChanges();
        }

        [Test, Order(1)]
        public void GetAllAuthors_Test()
        {
            var result = _authorServises.GetAllAuthors();
            Assert.That(result.Count, Is.EqualTo(10));
        }

        [Test, Order(2)]
        public void GetAuthorById_Test()
        {
            var result = _authorServises.GetAuthorById(1);
            Assert.That(result.Id == 1);
        }

        [Test, Order(3)]
        public void AddAuthor_Test()
        {
            var _authorVM = new AuthorVM()
            {
                FullName = "Author 11"
            };

            _authorServises.AddAuthor(_authorVM);

            var result = _authorServises.GetAuthorById(11);
            Assert.That(result.FullName, Is.EqualTo(_authorVM.FullName));
        }

        [Test, Order(4)]
        public void EditAuthor_Test()
        {
            var _authorVM = new AuthorVM()
            {
                FullName = "Author 1 Edit"
            };

            _authorServises.EditAuthor(1, _authorVM);

            var result = _authorServises.GetAuthorById(1);
            Assert.That(result.FullName, Is.EqualTo(_authorVM.FullName));
        }

        [Test, Order(5)]
        public void DeleteAuthor_Test()
        {
            _authorServises.DeleteAuthor(1);

            var result = _authorServises.GetAllAuthors();
            Assert.That(result.Count, Is.EqualTo(10));
        }
    }
}
