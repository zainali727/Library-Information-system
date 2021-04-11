using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibrarySystem.Domain
{
    public class IssuedBook
    {
        [Key]
        public int Id { get; set; }

        public Member Member { get; set; }

        public Book Book { get; set; }

        [Column(TypeName = "datetime2")]
        [Required]
        public DateTime IssuedDate { get; set; }
        
        [Column(TypeName = "datetime2")]
        [Required]
        public DateTime ReturnDate { get; set; }
    }
}