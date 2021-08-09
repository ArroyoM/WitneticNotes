using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notes.Core.Entities;

namespace Notes.Infrastructure.Data.Configurations
{
    class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(e => e.IdUser);

            builder.Property(e => e.IdUser);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(true);

            builder.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(700)
                .IsUnicode(false);

            builder.Property(e => e.Created_time)
                .IsRequired()
                .IsUnicode(false);

            builder.Property(e => e.Updated_time)
                .IsRequired()
                .IsUnicode(false);

            builder.Property(e => e.Deleted_time)
                .IsUnicode(false);

            builder.Property(e => e.Token)
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.HasMany(u => u.Books)
                .WithOne().HasForeignKey(b => b.IdUser)
                .HasPrincipalKey(p => p.IdUser)
                .OnDelete(DeleteBehavior.Cascade);

      
        }
    }
}
