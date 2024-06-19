using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace smallBookShopAPIProj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly string connectionString = "Data Source=DESKTOP-N6FQQ59\\SQLEXPRESS;Initial Catalog=BookShop;Integrated Security=True;Encrypt=False";

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetAllBooks()
        {
            var bookList = new List<Book>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string sql = "SELECT * FROM Books";
                    SqlCommand cmd = new SqlCommand(sql, connection);
                    await connection.OpenAsync();
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var book = new Book
                            {
                                BookName = reader.GetString(0),
                                Author = reader.GetString(1),
                                ReleaseDate = reader.GetDateTime(2),
                                BookDescription = reader.IsDBNull(3) ? null : reader.GetString(3)
                            };
                            bookList.Add(book);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database failure: " + ex.Message);
            }

            if (bookList.Count > 0)
            {
                return Ok(bookList);
            }
            return NotFound("No books found");
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<Book>> GetBookByName(string name)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string sql = "SELECT * FROM Books WHERE bookName=@name";
                    SqlCommand cmd = new SqlCommand(sql, connection);
                    cmd.Parameters.AddWithValue("@name", name);
                    await connection.OpenAsync();

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            var book = new Book
                            {
                                BookName = reader.GetString(0),
                                Author = reader.GetString(1),
                                ReleaseDate = reader.GetDateTime(2),
                                BookDescription = reader.IsDBNull(3) ? null : reader.GetString(3)
                            };
                            return Ok(book);
                        }
                        else
                        {
                            return NotFound("Book not found");
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database failure: " + ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddBook([FromBody] Book book)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string sql = "INSERT INTO Books (bookName, author, releaseDate, bookDescription) VALUES (@bookName, @Author, @ReleaseDate, @BookDescription)";
                    SqlCommand cmd = new SqlCommand(sql, connection);
                    cmd.Parameters.AddWithValue("@bookName", book.BookName);
                    cmd.Parameters.AddWithValue("@Author", book.Author);
                    cmd.Parameters.AddWithValue("@ReleaseDate", book.ReleaseDate);
                    cmd.Parameters.AddWithValue("@BookDescription", book.BookDescription ?? (object)DBNull.Value);
                    await connection.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
                return Ok("Book added successfully");
            }
            catch (SqlException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database failure: " + ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> UpdateBook([FromBody] Book book)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string sql = "UPDATE Books SET author=@Author, releaseDate=@ReleaseDate, bookDescription=@BookDescription WHERE bookName=@bookName";
                    SqlCommand cmd = new SqlCommand(sql, connection);
                    cmd.Parameters.AddWithValue("@bookName", book.BookName);
                    cmd.Parameters.AddWithValue("@Author", book.Author);
                    cmd.Parameters.AddWithValue("@ReleaseDate", book.ReleaseDate);
                    cmd.Parameters.AddWithValue("@BookDescription", book.BookDescription ?? (object)DBNull.Value);
                    await connection.OpenAsync();
                    int rowsAffected = await cmd.ExecuteNonQueryAsync();
                    if (rowsAffected > 0)
                    {
                        return Ok("Book updated successfully");
                    }
                    else
                    {
                        return NotFound("Book not found");
                    }
                }
            }
            catch (SqlException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database failure: " + ex.Message);
            }
        }

        [HttpDelete("{name}")]
        public async Task<ActionResult> DeleteBook(string name)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string sql = "DELETE FROM Books WHERE bookName=@bookName";
                    SqlCommand cmd = new SqlCommand(sql, connection);
                    cmd.Parameters.AddWithValue("@bookName", name);
                    await connection.OpenAsync();
                    int rowsAffected = await cmd.ExecuteNonQueryAsync();
                    if (rowsAffected > 0)
                    {
                        return Ok("Book deleted successfully");
                    }
                    else
                    {
                        return NotFound("Book not found");
                    }
                }
            }
            catch (SqlException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database failure: " + ex.Message);
            }
        }
    }
}