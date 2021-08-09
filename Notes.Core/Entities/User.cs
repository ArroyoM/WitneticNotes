using System.Collections.Generic;

namespace Notes.Core.Entities
{
    public partial class User : BaseEntity
    {
        public User() {
            Books = new HashSet<Book>();
        }
        public int IdUser { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? Token { get; set; }
        public virtual ICollection<Book> Books { get; set;}

    }
}
