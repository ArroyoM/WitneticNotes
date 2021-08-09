using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notes.Core.Entities;
using System;


namespace Notes.Infrastructure.Data.Configurations
{
    class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("Books");

            builder.HasKey(e => e.IdBook);

            builder.Property(e => e.IdBook);

            builder.Property(e => e.IdUser);

            builder.Property(e => e.Name)
                .IsRequired()
                .IsUnicode(false)
                .HasMaxLength(100);

            builder.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.Created_time)
                .IsRequired()
                .IsUnicode(false);

            builder.Property(e => e.Updated_time)
                .IsRequired()
                .IsUnicode(false);

            builder.Property(e => e.Deleted_time)
                .IsUnicode(false);

            builder.HasOne(u => u.User)
                 .WithMany(b => b.Books)
                 .HasForeignKey(f => f.IdUser)
                 .HasPrincipalKey(d => d.IdUser)
                 .OnDelete(DeleteBehavior.Cascade);
                // .HasConstraintName("FK_Books_Users");

            builder.HasMany(b => b.Notes)
                .WithOne()
                .HasForeignKey(n => n.IdBook)
                .HasPrincipalKey(p => p.IdBook)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
