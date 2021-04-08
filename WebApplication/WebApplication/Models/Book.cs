using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace WebApplication.Models
{
    public class Book
    {
        public Book()
        {
            BookReviews = new List<BookReview>();
        }
        
        [Key]
        public int Id { get; set; }
        
        [Column(TypeName ="nvarchar(250)")]
        [Required]
        public string Title { get; set; }
        
        [Column(TypeName ="nvarchar(250)")]
        [Required]
        public string Author { get; set; }
        
        [Column(TypeName ="nvarchar(250)")]
        [Required]
        public string ISBN { get; set; }
        
        [Column(TypeName ="nvarchar(250)")]
        [Required]
        public string Genre { get; set; }

        [Column(TypeName = "dstetime2")]
        [Required]
        public DateTime PublishedDate { get; set; }

        [Column(TypeName ="nvarchar(250)")]
        public string ImageFileName { get; set; }
        
        [NotMappedAttribute]
        public IFormFile MyImage { set; get; }

        [Column(TypeName = "varbinary(max)")]
        public byte[] ImageBytes { get; set; }

        [Column(TypeName ="int")]
        [Required]
        public int Quantity { get; set; }
        
        [Column(TypeName ="money")]
        [Required]
        [Range(0, 999, ErrorMessage = "The field {0} must be greater than {1}.")]
        public decimal FinePerDay { get; set; }
       
        public List<BookReview> BookReviews { get; set; }
    }
}