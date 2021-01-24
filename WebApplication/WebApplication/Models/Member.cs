using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication.Models
{
    public class Member
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        [Required]
        [MaxLength (25)]
        public string Firstname { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        [Required]
        public string Lastname { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        [Required]
        public string AddressLine1{ get; set; }

        [Column(TypeName = "nvarchar(250)")]
        [Required]
        public string AddressLine2 { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        [Required]
        public string AddressLine3 { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        [Required]
        public string City { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        [Required]
        public string County { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        [Required]
        public string PostCode { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        [EmailAddress, Required]
        public string  Email { get; set; }

        [Column(TypeName ="nvarchar(250)")]
        [Required]
        public string  Telephone { get; set; }
    }
}