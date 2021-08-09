using Microsoft.EntityFrameworkCore;
using Notes.Core.Entities;

namespace Notes.Infrastructure.Data.Configurations
{
    class NoteConfiguration: IEntityTypeConfiguration<Note>
    {
        public NoteConfiguration() { }

        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Note> builder)
        {
            builder.ToTable("Notes");

            builder.HasKey(e => e.IdNote);

            builder.Property(e => e.IdNote);

            builder.Property(e => e.IdBook);

            builder.Property(e => e.Name)
                .HasMaxLength(700)
                .IsRequired()
                .IsUnicode(false);

            builder.Property(e => e.Created_time)
                .IsRequired()
                .IsUnicode(false);

            builder.Property(e => e.Updated_time)
                .IsRequired()
                .IsUnicode(false);

            builder.Property(e => e.Deleted_time)
                .IsUnicode(false);

            builder.HasOne(b => b.Book)
                .WithMany(n => n.Notes)
                .HasForeignKey(f => f.IdBook)
                .HasPrincipalKey(p => p.IdBook)
                .OnDelete(DeleteBehavior.Cascade);
                //.HasConstraintName("FK_Notes_Books");
        }
    }
}
