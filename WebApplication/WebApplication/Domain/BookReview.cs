using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication.Domain
{
    public class BookReview
    {
        [Key]
        public int Id { get; set; }
        
        [Column(TypeName ="nvarchar(256)")]
        [Required]
        public string ReviewerName { get; set; }
        
        [Column(TypeName ="nvarchar(max)")]
        [Required]
        public string Text { get; set; }
        
        [Column(TypeName = "datetime2")]
        [Required]
        public DateTime CreatedDate { get; set; }
        
        public Book Book { get; set; }
    }
}