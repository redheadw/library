using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using System;

namespace ClassLibrary1
{
    public class OpenLibraryService
    {
        //Connect to openlibrary.org for a book list
        private static readonly HttpClient client = new HttpClient();
        //json communicating with open library to get book data by isbn
        public async Task<Book> GetBookByISBN(string isbn)
        {
            
            string url = $"https://openlibrary.org/isbn/{isbn}.json";
            HttpResponseMessage response = await client.GetAsync(url);
           
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                using JsonDocument doc = JsonDocument.Parse(json);
                string title = doc.RootElement.GetProperty("title").GetString();

                return new Book
                {
                    ISBN = isbn,
                    Title = title,
                    Author = "Unknown",
                    IsAvailable = true
                };
            }

            return null;
        }
         //json communicating with open library to get book data by title
        public async Task<List<Book>> SearchBooksByTitle(string title)
        {
            string url = $"https://openlibrary.org/search.json?title={Uri.EscapeDataString(title)}";
            HttpResponseMessage response = await client.GetAsync(url);

            var books = new List<Book>();

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                using JsonDocument doc = JsonDocument.Parse(json);

                foreach (var item in doc.RootElement.GetProperty("docs").EnumerateArray())
                {
                    string titleResult = item.GetProperty("title").GetString();
                    string authorResult = item.TryGetProperty("author_name", out var authors)
                        ? string.Join(", ", authors.EnumerateArray().Select(a => a.GetString()))
                        : "Unknown";
                    string isbnResult = item.TryGetProperty("isbn", out var isbns)
                        ? isbns[0].GetString()
                        : "N/A";

                    books.Add(new Book
                    {
                        Title = titleResult,
                        Author = authorResult,
                        ISBN = isbnResult,
                        IsAvailable = true
                    });
                }
            }

            return books;
        }
         //json communicating with open library to get book data by isbn
        public async Task<List<Book>> SearchBooksByAuthor(string author)
        {
            string url = $"https://openlibrary.org/search.json?author={Uri.EscapeDataString(author)}";
            HttpResponseMessage response = await client.GetAsync(url);

            var books = new List<Book>();

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                using JsonDocument doc = JsonDocument.Parse(json);

                foreach (var item in doc.RootElement.GetProperty("docs").EnumerateArray())
                {
                    string titleResult = item.GetProperty("title").GetString();
                    string authorResult = item.TryGetProperty("author_name", out var authors)
                        ? string.Join(", ", authors.EnumerateArray().Select(a => a.GetString()))
                        : "Unknown";
                    string isbnResult = item.TryGetProperty("isbn", out var isbns)
                        ? isbns[0].GetString()
                        : "N/A";

                    books.Add(new Book
                    {
                        Title = titleResult,
                        Author = authorResult,
                        ISBN = isbnResult,
                        IsAvailable = true
                    });
                }
            }

            return books;
        }
    }
}
