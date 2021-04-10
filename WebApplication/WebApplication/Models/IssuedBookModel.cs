using System;

namespace WebApplication.Models
{
    public class IssuedBookModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public MemberModel IssuedTo { get; set; }
        public DateTime IssuedDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public decimal CalculatedFine { get; set; }
    }
}