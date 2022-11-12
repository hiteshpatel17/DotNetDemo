using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace AjmeraDemo.Models
{
    public partial class BookDBContext : DbContext,IBookDBContext
    {
        public BookDBContext()
        {
        }

        public BookDBContext(DbContextOptions<BookDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblBook> TblBooks { get; set; }
        public new async Task<int> SaveChanges()
        {
            return await base.SaveChangesAsync();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-14P68L5\\SQLEXPRESS;Database=BookDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblBook>(entity =>
            {
                entity.ToTable("tbl_Book");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AuthorName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
