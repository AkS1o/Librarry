using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Librarry.Data.Models
{
    public class Author
    {

        public int Id { get; set; }
        public string FullName { get; set; }

        public List<BookAuthor> BookAuthors { get; set; }
    }
}
