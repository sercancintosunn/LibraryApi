using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApi.Business.DTOs.Books
{
    public class CreateBookDto
    {
        public string Title { get; set; } = string.Empty;
        public string? ISBN { get; set; }
        public int AuthorId { get; set; }
        public int CategoryId { get; set; }

    }
}
