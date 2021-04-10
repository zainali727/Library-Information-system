using WebApplication.Domain;

namespace WebApplication.Models
{
    public class MemberIssueBookModel
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public bool BookFound { get; set; }
        public Book Book { get; set; }
    }
}