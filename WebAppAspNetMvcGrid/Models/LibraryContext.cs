using System.Data.Entity;

namespace WebAppAspNetMvcGrid.Models
{
    public class LibraryContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<BookImage> BookImages { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<CurrencyType> CurrencyTypes { get; set; }
        public DbSet<Language> Languages { get; set; }
        public LibraryContext() : base("LibraryEntity")
        { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().HasOptional(x => x.BookImage).WithRequired().WillCascadeOnDelete(true);
            base.OnModelCreating(modelBuilder);
        }
    }
}