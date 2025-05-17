using System;
using System.Threading.Tasks;
using ClassLibrary1;

class Program
{
    //Main
    static async Task Main(string[] args)
    {
        // connect with Open Library 
        var api = new OpenLibraryService();
        var library = new LibraryManager();
        library.SeedTestBooks(); //creat a test outside of openlibrary service to test if is available, borrow, and return work
        bool running = true;

        // Future App Menu/loop  
        while (running)
        {
            Console.WriteLine("\n--- Library Menu ---");
            Console.WriteLine("1. Search by ISBN");
            Console.WriteLine("2. Search by Title");
            Console.WriteLine("3. Search by Author");
            Console.WriteLine("4. List Available Books");
            Console.WriteLine("5. Borrow a Book");
            Console.WriteLine("6. Return a Book");
            Console.WriteLine("7. Exit");

            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();
            //case study for menu choice
            switch (choice)
            {
                // case 1 ISBN from menu
                case "1":
                    Console.Write("Enter ISBN: ");
                    string isbn = Console.ReadLine();
                    var bookByIsbn = await api.GetBookByISBN(isbn);
                    if (bookByIsbn != null)
                    {
                        Console.WriteLine($"Found: {bookByIsbn.Title} by {bookByIsbn.Author}");
                        library.AddBook(bookByIsbn);
                    }
                    else Console.WriteLine("Book not found.");
                    break;

                // case 2 Title from menu    
                case "2":
                    Console.Write("Enter Title: ");
                    string title = Console.ReadLine();
                    var booksByTitle = await api.SearchBooksByTitle(title);
                    foreach (var b in booksByTitle)
                    {
                        Console.WriteLine($"Found: {b.Title} by {b.Author}");
                        library.AddBook(b);
                    }
                    break;
                // case 3 Author from menu
                case "3":
                    Console.Write("Enter Author: ");
                    string author = Console.ReadLine();
                    var booksByAuthor = await api.SearchBooksByAuthor(author);
                    foreach (var b in booksByAuthor)
                    {
                        Console.WriteLine($"Found: {b.Title} by {b.Author}");
                        library.AddBook(b);
                    }
                    break;
                // case 4 is the book available from menu
                case "4":
                    var availableBooks = library.GetAvailableBooks();
                    foreach (var b in availableBooks)
                        Console.WriteLine($"{b.ISBN}: {b.Title} by {b.Author}");
                    break;
                // case 5 borrow from menu
                case "5":
                    Console.Write("Enter ISBN to borrow: ");
                    var borrowed = library.BorrowBook(Console.ReadLine());
                    Console.WriteLine(borrowed != null ? $"Borrowed: {borrowed.Title}" : "Book unavailable.");
                    break;
                // case 6 return from menu
                case "6":
                    Console.Write("Enter ISBN to return: ");
                    bool returned = library.ReturnBook(Console.ReadLine());
                    Console.WriteLine(returned ? "Book returned." : "Book not found.");
                    break;
                // case 7 Exit from menu
                case "7":
                    running = false;
                    break;


                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }
    }
}

