using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        [Required]
        public string Firstname { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        [Required]
        public string Lastname { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        [Required]
        public string Address { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        [EmailAddress, Required]

        public string  Email { get; set; }
        [Column(TypeName ="nvarchar(250)")]
        [Required]
        public string  Telephone { get; set; }
    }
}