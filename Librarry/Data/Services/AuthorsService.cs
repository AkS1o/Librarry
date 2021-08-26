using Librarry.Data.Models;
using Librarry.Data.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Librarry.Data.Services
{
    public class AuthorsService
    {
        private readonly AppDbContext _context;
        public AuthorsService(AppDbContext context)
        {
            _context = context;
        }

        public List<Author> GetAllAuthors()
        {
            var allAuthors = _context.Authors.ToList();
            return allAuthors;
        }

        public Author GetAuthorById(int id)
        {
            var author = _context.Authors.Find(id);
            return author;
        }

        public void AddAuthor(AuthorVM authorVM)
        {
            var _author = new Author()
            {
                FullName = authorVM.FullName
            };

            _context.Authors.Add(_author);
            _context.SaveChanges();
        }

        public Author EditAuthor(int id, AuthorVM authorVM)
        {
            var _author = _context.Authors.Find(id);

            if (_author != null)
            {
                _author.FullName = authorVM.FullName;

                _context.SaveChanges();
            }

            return _author;
        }

        public void DeleteAuthor(int id)
        {
            var _author = _context.Authors.Find(id);

            if (_author != null)
            {
                _context.Authors.Remove(_author);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception($"Author with id: {id} not found");
            }
        }
    }
}
