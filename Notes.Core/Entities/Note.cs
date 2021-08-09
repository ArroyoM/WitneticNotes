namespace Notes.Core.Entities
{
    public partial class Note : BaseEntity
    {
        public Note() {}
        public int IdNote { get; set; }
        public int IdBook { get; set; }
        public string Name { get; set; }
        public virtual Book Book { set; get; }
    }
}
