using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName ="nvarchar(250)")]
        public string Title { get; set; }
        [Column(TypeName ="nvarchar(250)")]
        public string Author { get; set; }
        [Column(TypeName ="nvarchar(250)")]
        public string  ISBN { get; set; }
        [Column(TypeName ="nvarchar(250)")]
        public string  Genre { get; set; }
        [Column(TypeName ="nvarchar(250)")]
        public string  PublishedDate { get; set; }
    }
}