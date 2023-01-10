using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management
{
    class RentalBookDetails
    {
        public string RentalId { get; set; }
        public string BookId { get; set; }
        public string StudentId { get; set; }
        public float? RentTotal { get; set; }
        public DateTime? RentDate { get; set; }
        public int? RentDuration { get; set; }
    }
}
