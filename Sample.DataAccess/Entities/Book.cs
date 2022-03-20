using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.DataAccess.Entities
{
    public class Book:BaseEntity
    {
        public string Name { get; set; }
        public string ISBN { get; set; }
        public decimal Amount { get; set; }
        public int BookTypeId { get; set; }
        public BookType BookType { get; set; }
    }
}
