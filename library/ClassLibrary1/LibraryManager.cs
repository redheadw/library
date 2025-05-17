using System.Collections.Generic;
using System.Linq;

// name library 
namespace ClassLibrary1
{
    //create public class
    public class LibraryManager
    {
        //list of books
        private List<Book> _books = new List<Book>();

        public void AddBook(Book book) => _books.Add(book);
        //Borrowing books
        public Book BorrowBook(string isbn)
        {
            var book = _books.FirstOrDefault(b => b.ISBN == isbn);
            if (book != null && book.IsAvailable && !book.LibraryUseOnly)
            {
                book.IsAvailable = false;
                return book;
            }
            return null;
        }
        //Returning books
        public bool ReturnBook(string isbn)
        {
            var book = _books.FirstOrDefault(b => b.ISBN == isbn);
            if (book != null)
            {
                book.IsAvailable = true;
                return true;
            }
            return false;
        }
        public IEnumerable<Book> GetAvailableBooks() => _books.Where(b => b.IsAvailable);
        //Books on hold
        public void PutOnHold(string isbn)
        {
            var book = _books.FirstOrDefault(b => b.ISBN == isbn);
            if (book != null)
                book.IsOnHold = true;
        }
        //Seed test, unable to borrow or return to open library
        public void SeedTestBooks()
        {
            AddBook(new Book { ISBN = "0001", Title = "Mock Book A", Author = "Author A", IsAvailable = true });
            AddBook(new Book { ISBN = "0002", Title = "Mock Book B", Author = "Author B", IsAvailable = true, LibraryUseOnly = true });
            AddBook(new Book { ISBN = "0003", Title = "Mock Book C", Author = "Author C", IsAvailable = true, IsOnHold = true });
        }

    }
}
