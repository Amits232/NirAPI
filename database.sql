USE master
GO

CREATE DATABASE BookShop
GO

USE BookShop
GO

CREATE TABLE Books(
bookName nvarchar(50) not null,
author nvarchar(50)not null,
releaseDate date,
bookDescription nvarchar(300),
PRIMARY KEY(bookName)
)
GO

INSERT INTO Books (bookName, author, releaseDate, bookDescription) VALUES
('The Great Gatsby', 'F. Scott Fitzgerald', '1925-04-10', 'A novel set in the Jazz Age that tells the story of Jay Gatsby and his unrequited love for Daisy Buchanan.'),
('To Kill a Mockingbird', 'Harper Lee', '1960-07-11', 'A novel about the serious issues of rape and racial inequality, but it is also full of warmth and humor.'),
('1984', 'George Orwell', '1949-06-08', 'A dystopian social science fiction novel and cautionary tale about the dangers of totalitarianism.'),
('Pride and Prejudice', 'Jane Austen', '1813-01-28', 'A romantic novel of manners that depicts the British landed gentry at the end of the 18th century.'),
('Moby-Dick', 'Herman Melville', '1851-10-18', 'The narrative of Captain Ahab’s obsessive quest to kill the giant white sperm whale Moby Dick.')
GO
