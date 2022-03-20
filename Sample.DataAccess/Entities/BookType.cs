using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.DataAccess.Entities
{
    public class BookType:BaseEntity
    {
        public BookType()
        {
            Books = new HashSet<Book>();
        }
        public string Name { get; set; }
        public IEnumerable<Book> Books { get; set; }
    }
}
