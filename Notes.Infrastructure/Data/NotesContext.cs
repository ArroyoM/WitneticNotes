using Microsoft.EntityFrameworkCore;
using Notes.Core.Entities;
using Notes.Infrastructure.Data.Configurations;

namespace Notes.Infrastructure.Data
{
    public partial class NotesContext : DbContext
    {
        public NotesContext() { }

        public NotesContext(DbContextOptions<NotesContext> options ): base(options) {}

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Note> Notes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new BookConfiguration());
            modelBuilder.ApplyConfiguration(new NoteConfiguration());
        }
    }
}
