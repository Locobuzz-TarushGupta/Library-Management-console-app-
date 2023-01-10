using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management
{
    internal class BookDetail
    {
        public string? BookId { get; set; }
        public string? BookTitle { get; set; }
        public string? BookDescription { get; set; }
        public string? Author { get; set; }
        public string? Stream { get; set; }
        public string? Quantity { get; set; }
        public float? RentPrice { get; set; }
        public string? Status { get; set; }
    }
}
