namespace ClassLibrary1

//Parts of book cataloging, get and set information
{
    public class Book
    {
        public string? ISBN { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public bool IsAvailable { get; set; }
        public bool IsOnHold { get; set; }  
        public bool LibraryUseOnly { get; set; } 
    }
}
