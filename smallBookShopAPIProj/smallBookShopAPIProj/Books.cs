namespace smallBookShopAPIProj
{
    public class Book
    {
        string bookName;
        string author;
        DateTime releaseDate;
        string bookDescription;

        public string BookName { get => bookName; set => bookName = value; }
        public string Author { get => author; set => author = value; }
        public DateTime ReleaseDate { get => releaseDate; set => releaseDate = value; }
        public string BookDescription { get => bookDescription; set => bookDescription = value; }
    }
}
