using System;
using System.ComponentModel.DataAnnotations;
using LibrarySystem.Domain;

namespace LibrarySystem.Models
{
    public class MemberIssueBookModel
    {
        public int MemberId { get; set; }
        public int BookId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public bool BookFound { get; set; }
        public Book Book { get; set; }
        
        [Required]
        public DateTime DueDate { get; set; }
    }
}