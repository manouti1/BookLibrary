using BookLibrary.Domain;
using BookLibrary.Dtos;
using static System.Reflection.Metadata.BlobBuilder;

namespace BookLibrary.Application
{
    public interface IBookService
    {
        Task<IEnumerable<BookDto>> GetAllBooksAsync();
        Task<BookDto?> GetBookByIdAsync(int id);
        Task<int> AddBookAsync(BookDto book);
        Task UpdateBookAsync(BookDto book);
        Task DeleteBookAsync(int id);
    }


    public class BookService : IBookService
    {
        private readonly IBookRepository _repository;

        public BookService(IBookRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<BookDto>> GetAllBooksAsync()
        {
            var books = await _repository.GetAllAsync();
            var booksDto = books.Select(b => new BookDto
            {
                Title = b.Title,
                Author = b.Author,
                ISBN = b.ISBN,
                PublishedDate = b.PublishedDate
            }).ToList();

            return booksDto;
        }

        public async Task<BookDto?> GetBookByIdAsync(int id)
        {
            var book = await _repository.GetByIdAsync(id);
            var booksDto = new BookDto
            {
                Title = book.Title,
                Author = book.Author,
                ISBN = book.ISBN,
                PublishedDate = book.PublishedDate
            };

            return booksDto;
        }

        public async Task<int> AddBookAsync(BookDto dto)
        {
            var book = new Book
            {
                Title = dto.Title,
                Author = dto.Author,
                ISBN = dto.ISBN,
                PublishedDate = dto.PublishedDate,
            };

            return await _repository.AddAsync(book);
        }

        public async Task UpdateBookAsync(BookDto dto)
        {
            var book = new Book
            {
                Title = dto.Title,
                Author = dto.Author,
                ISBN = dto.ISBN,
                PublishedDate = dto.PublishedDate,
            };
            await _repository.UpdateAsync(book);
        }

        public async Task DeleteBookAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}

