using System.Collections.Generic;

namespace Notes.Core.Entities
{
    public partial class Book : BaseEntity
    {
        public Book() {
            Notes = new HashSet<Note>();
        }
        public int IdBook { get; set; }
        public int IdUser { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Color { get; set;  }
        public virtual User User { get; set; }
        public virtual ICollection<Note> Notes { get; set; }
    }
}
