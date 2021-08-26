using Librarry.Data.Models;
using Librarry.Data.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Librarry.Data.Services
{
    public class PublisherService
    {
        private readonly AppDbContext _context;

        public PublisherService(AppDbContext context)
        {
            _context = context;
        }

        public CustomPublishersVM GetAllPublishers(string sortBy, string searchString, int pageNumber)
        {
            CustomPublishersVM customPublishersVM = new CustomPublishersVM();
            List<Publisher> publishers = publishers = _context.Publishers.ToList();

            if (!string.IsNullOrEmpty(searchString))
                publishers = publishers.Where(p => p.Name.Contains(searchString)).ToList();


            if (sortBy == "as")
                publishers = publishers.OrderBy(p => p.Name).ToList();
            else if (sortBy == "desc")
                publishers = publishers.OrderByDescending(p => p.Name).ToList();


            customPublishersVM.CountPublishers = publishers.Count();


            if (pageNumber <= 1)
            {
                customPublishersVM.Page = $"Page: {1}/{customPublishersVM.CountPublishers / 6} ";
                customPublishersVM.Publishers = publishers.Take(6).ToList();
            }
            else
            {
                customPublishersVM.Page = $"Page: {pageNumber}/{customPublishersVM.CountPublishers / 6} ";
                customPublishersVM.Publishers = publishers.Skip(6 * pageNumber).Take(6).ToList();
            }

            return customPublishersVM;
        }

        public Publisher GetPublisherById(int id)
        {
            var publisher = _context.Publishers.Find(id);
            return publisher;
        }

        public PublisherWithBooksAndAuthorsVM GetPublisherData(int id)
        {
            var _publisherData = _context.Publishers.Where(p => p.Id == id)
                .Select(p => new PublisherWithBooksAndAuthorsVM()
                {
                    Name = p.Name,
                    BookAuthors = p.Books.Select(b => new BookAuthorVM()
                    {
                        BookName = b.Title,
                        BookAuthors = b.BookAuthors.Select(a => a.Author.FullName).ToList()
                    }).ToList()
                }).FirstOrDefault();

            return _publisherData;
        }

        public Publisher AddPublisher(PublisherVM publisherVM)
        {
            var _publisher = new Publisher()
            {
                Name = publisherVM.Name
            };

            _context.Publishers.Add(_publisher);
            _context.SaveChanges();

            return _publisher;
        }

        public Publisher EditPublisher(int id, PublisherVM publisherVM)
        {
            var _publisher = _context.Publishers.Find(id);

            if (_publisher != null)
            {
                _publisher.Name = publisherVM.Name;
                _context.SaveChanges();

            }

            return _publisher;
        }

        public void DeletePublisher(int id)
        {
            var _publisher = _context.Publishers.FirstOrDefault(p => p.Id == id);

            if (_publisher != null)
            {
                _context.Publishers.Remove(_publisher);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception($"The publisher with id: {id}");
            }
        }
    }
}
