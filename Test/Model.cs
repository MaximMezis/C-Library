using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Test
{
    public class Model
    {
        public class Book
        {
            public int Id { get; set; }

            [MaxLength(30)]
            public string Title { get; set; }

            public int Year { get; set; }

            public virtual ICollection<Author> Authors { get; set; }

            public Book()
            {
                Authors = new List<Author>();
            }

        }

        public class Author
        {
            public int Id { get; set; }

            [MaxLength(20)]
            public string LastName { get; set; }

            public virtual ICollection<Book> Books { get; set; }

            public Author()
            {
                Books = new List<Book>();
            }
        }


        public class LibraryContext : DbContext
        {
            public DbSet<Book> Books { get; set; }
            public DbSet<Author> Authors { get; set; }

            public LibraryContext() : base("DbConnection")
            { }

            protected override void OnModelCreating(DbModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);
            }

        }

    }
}
