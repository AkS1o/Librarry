using Librarry.Data.Models;
using Librarry.Data.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Librarry.Data.Services
{
    public class BooksService
    {
        private AppDbContext _context;
        public BooksService(AppDbContext context)
        {
            _context = context;
        }

        public List<Book> GetAllBooks()
        {
            var allBooks = _context.Books.ToList();
            return allBooks;
        }

        public BookWithAuthorsVM GetBookById(int id)
        {
            var _book = _context.Books.Where(b => b.Id == id).Select(book => new BookWithAuthorsVM()
            {
                Title = book.Title,
                Description = book.Description,
                IsRead = book.IsRead,
                DateRead = book.IsRead ? book.DateRead.Value : null,
                Rate = book.IsRead ? book.Rate.Value : null,
                Genre = book.Genre,
                ImageURL = book.ImageURL,
                PublisherName = book.Publisher.Name,
                AuthorNames = book.BookAuthors.Select(n => n.Author.FullName).ToList()
            }).FirstOrDefault();

            return _book;
        }

        public void AddBookWithAuthors(BookVM bookVM)
        {
            var _book = new Book()
            {
                Title = bookVM.Title,
                Description = bookVM.Description,
                IsRead = bookVM.IsRead,
                DateRead = bookVM.IsRead ? bookVM.DateRead.Value : null,
                Rate = bookVM.IsRead ? bookVM.Rate.Value : null,
                Genre = bookVM.Genre,
                ImageURL = bookVM.ImageURL,
                DateAdded = DateTime.Now,
                PublisherId = bookVM.PublisherId
            };

            _context.Books.Add(_book);
            _context.SaveChanges();

            foreach (var id in bookVM.AuthorIds)
            {
                var _bookAuthor = new BookAuthor()
                {
                    BookId = _book.Id,
                    AuthorId = id
                };

                _context.BookAuthors.Add(_bookAuthor);
                _context.SaveChanges();
            }
        }

        public Book EditBook(int id, BookVM bookVM)
        {
            var _book = _context.Books.Find(id);

            if (_book != null)
            {
                _book.Title = bookVM.Title;
                _book.Description = bookVM.Description;
                _book.IsRead = bookVM.IsRead;
                _book.DateRead = bookVM.DateRead;
                _book.Rate = bookVM.Rate;
                _book.Genre = bookVM.Genre;
                _book.ImageURL = bookVM.ImageURL;
                _book.DateAdded = DateTime.Now;

                _context.SaveChanges();
            }

            return _book;
        }

        public void DeleteBook(int id)
        {
            var _book = _context.Books.Find(id);

            if (_book != null)
            {
                _context.Books.Remove(_book);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception($"Book with id: {id} not found");
            }
        }
    }
}