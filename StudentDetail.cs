using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management
{
    internal class StudentDetail
    {
        public string? StudentName { get; set; }
        public int StudentId { get; set; }
        public string? StudentAddress { get; set; }
        public string? StudentEmail { get; set; }
        public string? StudentPhone { get; set; }
        public float PenaltyDue { get; set; }
        public float RentDue { get; set; }
        public BookDetail BooksIssued { get; set; }
    }
}
