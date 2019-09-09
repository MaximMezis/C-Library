using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using static Test.Model;


namespace Test
{
    public class Program
    {
        public static void ShowAuthors()
        {
            using (LibraryContext db = new LibraryContext()) 
            {
                var authors = db.Authors.ToList();
                Console.WriteLine("Authors:");
                foreach (Author a in authors)
                {
                    Console.WriteLine("#{0} | Surname:{1}", a.Id, a.LastName);
                }
            }
        }


        public static void ShowBooks()
        {
            using (LibraryContext db = new LibraryContext())
            {
                var books = db.Books.ToList();
                Console.WriteLine("Books:");
                foreach (Book b in books)
                {
                    Console.WriteLine("#{0} | Title:{1} | Year:{2}", b.Id, b.Title, b.Year);
                }
            }
        }



        public static void ShowBooksByAuthor()
        {
            using (LibraryContext db = new LibraryContext())
            {
                ShowAuthors();
                Console.Write("Please Enter the number of the author: ");
                var id = Convert.ToInt32(Console.ReadLine());
                var books = db.Books.Include(i => i.Authors).Where(w => w.Authors.Any(a => a.Id == id)).ToList();
                foreach (Book b in books)
                {
                    Console.WriteLine("#{0} | Title:{1} | Year:{2}", b.Id, b.Title, b.Year);
                }
            }
        }
        


        public static void AddBook()
        {
            using (LibraryContext db = new LibraryContext())
            {
                Book b = new Book();
                Console.Write("Please enter the title of the book... (Max. 30 characters): ");
                b.Title = Console.ReadLine();
                Console.Write("Please enter the year in which the book was published... \n(For example '2006'): ");
                b.Year = Convert.ToInt32(Console.ReadLine());
                Console.Write("Please enter the number of authors of this book... \n(For example '2006'): ");
                int numberOfAuthors = Convert.ToInt32(Console.ReadLine());
                for (int i=1; i<=numberOfAuthors; i++)
                { 
                    Console.Write("Please enter the surname of the author... (Max. 20 characters): ");
                    Author author = new Author { LastName = Console.ReadLine() };
                    b.Authors.Add(author);
                }
                db.Books.Add(b);
                db.SaveChanges();
                ShowBooks();
            }
        }




        public static void DeleteBook()
        {
            using (LibraryContext db = new LibraryContext())
            {
                ShowBooks();
                Console.Write("Please enter the number of the book to delete it: ");
                var id = Convert.ToInt32(Console.ReadLine());
                Book b = db.Books.Find(id);
                db.Books.Remove(b);
                db.SaveChanges();
                ShowBooks();
            }
        }



        static void Main(string[] args)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("Enter: 1 - to show all authors\n" + "Enter: 2 - to show all books\n" + "Enter: 3 - to add new book\n" + "Enter: 4 - to delete book\n" + "Enter: 5 - to show books written by author\n");
                var inputChoise = Console.ReadLine();
                switch (inputChoise)
                {
                    case "1":
                        ShowAuthors();
                        break;
                    case "2":
                        ShowBooks();
                        break;
                    case "3":
                        AddBook();
                        break;
                    case "4":
                        DeleteBook();
                        break;
                    case "5":
                        ShowBooksByAuthor();
                        break;
                    default:
                        Console.WriteLine("ATTENTION! \nIncorrect input! You can input only \"1\" or \"2\" or \"3\" or \"4\" or \"5\"!");
                        break;
                }
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An exception interrupted: " + ex.Message);
                Console.ReadLine();
            }
        }
    }
}
