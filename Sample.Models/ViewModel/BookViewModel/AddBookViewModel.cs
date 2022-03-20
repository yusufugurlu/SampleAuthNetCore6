using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Models.ViewModel.BookViewModel
{
    public class AddBookViewModel
    {
        public string Name { get; set; }
        public string ISBN { get; set; }
        public decimal Amount { get; set; }
        public int BookTypeId { get; set; }
    }
}
