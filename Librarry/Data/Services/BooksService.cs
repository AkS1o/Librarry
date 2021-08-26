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
                DateRead = book.DateRead,
                Rate = book.Rate,
                Genre = book.Genre,
                ImageURL = book.ImageURL,
                PublisherName = book.Publisher.Name,
                NamesOfAuthors = book.BookAuthors.Select(n => n.Author.FullName).ToList()
            }).FirstOrDefault();

            return _book;
        }

        public void AddBook(BookVM bookVM)
        {
            var _book = new Book()
            {
                Title = bookVM.Title,
                Description = bookVM.Description,
                IsRead = bookVM.IsRead,
                DateRead = bookVM.DateRead,
                Rate = bookVM.Rate,
                Genre = bookVM.Genre,
                ImageURL = bookVM.ImageURL,
                DateAdded = bookVM.DateAdded,
            };

            _context.Books.Add(_book);
            _context.SaveChanges();


            foreach (var id in bookVM.IdOfAuthors)
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
                _book.DateAdded = bookVM.DateAdded;

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