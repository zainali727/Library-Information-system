using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace WebApplication.Models
{
    public class Book
    {
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
        public string  ISBN { get; set; }
        
        [Column(TypeName ="nvarchar(250)")]
        [Required]
        public string  Genre { get; set; }
        
        [Column(TypeName ="nvarchar(250)")]
        [Required]
        public string  PublishedDate { get; set; }
        
        [Column(TypeName ="nvarchar(250)")]
        public string ImageFileName { get; set; }
        
        [NotMappedAttribute]
        public IFormFile MyImage { set; get; }
    }
}