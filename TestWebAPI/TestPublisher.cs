using Librarry.Data;
using Librarry.Data.Models;
using Librarry.Data.Models.ViewModels;
using Librarry.Data.Services;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace TestWebAPI
{
    public class TestPublisher
    {
        private static DbContextOptions<AppDbContext> dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: " WebAPI")
            .Options;
        AppDbContext _context;
        PublisherService _publisherServises;

        [OneTimeSetUp]
        public void Setup()
        {
            _context = new AppDbContext(dbContextOptions);
            _context.Database.EnsureCreated();

            SeedDatabase();

            _publisherServises = new PublisherService(_context);

        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            _context.Database.EnsureDeleted();
        }

        private void SeedDatabase()
        {
            var publishers = new List<Publisher>
            {
                new Publisher() { Id = 1, Name = "Publisher 1" },
                new Publisher() { Id = 2, Name = "Publisher 2" },
                new Publisher() { Id = 3, Name = "Publisher 3" },
                new Publisher() { Id = 4, Name = "Publisher 4" },
                new Publisher() { Id = 5, Name = "Publisher 5" },
                new Publisher() { Id = 6, Name = "Publisher 6" },
                new Publisher() { Id = 7, Name = "Publisher 7" },
                new Publisher() { Id = 8, Name = "Publisher 8" },
                new Publisher() { Id = 9, Name = "Publisher 9" },
                new Publisher() { Id = 10, Name = "Publisher 10" },
            };

            _context.Publishers.AddRange(publishers);
            _context.SaveChanges();
        }

        [Test, Order(1)]
        public void GetAllPublishers_WithNoSort_WithNoSearch_WithNoPageNumber_Test()
        {
            var result = _publisherServises.GetAllPublishers("", "", null);
            Assert.That(result.Publishers.Count, Is.EqualTo(6));
        }

        [Test, Order(2)]
        public void GetAllPublishers_WithSort_WithNoSearch_WithNoPageNumber_Test()
        {
            var result = _publisherServises.GetAllPublishers("as", "", null);
            Assert.That(result.Publishers.FirstOrDefault().Name,  Is.EqualTo("Publisher 1"));
        }

        [Test, Order(3)]
        public void GetAllPublishers_WithNoSort_WithSearch_WithNoPageNumber_Test()
        {
            var result = _publisherServises.GetAllPublishers("", "Publisher 9", null);
            Assert.That(result.Publishers.Count, Is.EqualTo(1));                           
        }

        [Test, Order(4)]
        public void GetAllPublishers_WithNoSort_WithNoSearch_WithPageNumber_Test()
        {
            var result = _publisherServises.GetAllPublishers("", "", 2);
            Assert.That(result.Publishers.Count, Is.EqualTo(4));
        }

        [Test, Order(5)]
        public void GetPublisherById_Test()
        {
            var result = _publisherServises.GetPublisherById(1);
            Assert.That(result.Id, Is.EqualTo(1));
        }

        [Test, Order(5)]
        public void GetPublisherById_withAuthor_Test()
        {
            var result = _publisherServises.GetPublisherById(1);
            Assert.That(result.Id, Is.EqualTo(1));
        }

        [Test, Order(6)]
        public void AddPublisher_Test()
        {
            var publisherVM = new PublisherVM()
            {
                Name = "Publisher 11"
            };

            _publisherServises.AddPublisher(publisherVM);

            var result = _publisherServises.GetPublisherById(11);
            Assert.That(result.Name, Is.EqualTo(publisherVM.Name));
        }

        [Test, Order(6)]
        public void EditPublisher_Test()
        {
            var publisherVM = new PublisherVM()
            {
                Name = "Publisher 1 Edit"
            };

            _publisherServises.EditPublisher(1, publisherVM);

            var result = _publisherServises.GetPublisherById(1);
            Assert.That(result.Name, Is.EqualTo(publisherVM.Name));
        }

        [Test, Order(7)]
        public void DeletePublisher_Test()
        {
            _publisherServises.DeletePublisher(1);

            var result = _publisherServises.GetAllPublishers("", "", null);

            Assert.That(result.CountPublishers, Is.EqualTo(10));
        }
    }
}