using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Librarry.Data.Models.ViewModels
{
    public class CustomPublishersVM
    {
        public int CountPublishers { get; set; }

        public string Page { get; set; }

        public List<Publisher> Publishers { get; set; }
    }
}
