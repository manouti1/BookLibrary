using BookLibrary.Api;
using BookLibrary.Application;
using BookLibrary.Domain;
using BookLibrary.Dtos;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BookLibrary.Tests
{

    [TestFixture]
    public class BooksControllerTests
    {
        private Mock<IBookService> _mockService;
        private BooksController _controller;

        [SetUp]
        public void SetUp()
        {
            _mockService = new Mock<IBookService>();
            _controller = new BooksController(_mockService.Object);
        }

        [Test]
        public async Task GetBooks_ReturnsOkResult_WithListOfBooks()
        {
            // Arrange
            var booksDto = new List<BookDto>
            {
                new BookDto { Title = "Book 1", Author = "Author 1", ISBN = "123", PublishedDate = DateTime.Now },
                new BookDto { Title = "Book 2", Author = "Author 2", ISBN = "456", PublishedDate = DateTime.Now }
            };

            // Simulate that the service layer returns a List<Book> and performs mapping to BookDto
            _mockService.Setup(service => service.GetAllBooksAsync())
                .ReturnsAsync(new List<Book>
                {
                    new Book { Id = 1, Title = "Book 1", Author = "Author 1", ISBN = "123", PublishedDate = DateTime.Now },
                    new Book { Id = 2, Title = "Book 2", Author = "Author 2", ISBN = "456", PublishedDate = DateTime.Now }
                });

            // Act
            var result = await _controller.GetBooks();

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(booksDto, okResult.Value);
        }

        [Test]
        public async Task GetBook_WithValidId_ReturnsOkResult()
        {
            // Arrange
            var bookDto = new BookDto { Title = "Book 1", Author = "Author 1", ISBN = "123", PublishedDate = DateTime.Now };
            var book = new Book { Id = 1, Title = "Book 1", Author = "Author 1", ISBN = "123", PublishedDate = DateTime.Now };

            // Simulate that the service layer returns a Book and performs mapping to BookDto
            _mockService.Setup(service => service.GetBookByIdAsync(1))
                .ReturnsAsync(book);

            // Act
            var result = await _controller.GetBook(1);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(bookDto, okResult.Value);
        }

        [Test]
        public async Task GetBook_WithInvalidId_ReturnsNotFound()
        {
            // Arrange
            _mockService.Setup(service => service.GetBookByIdAsync(It.IsAny<int>())).ReturnsAsync((Book)null);

            // Act
            var result = await _controller.GetBook(99);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public async Task AddBook_WithValidModel_ReturnsCreatedAtAction()
        {
            // Arrange
            var bookDto = new BookDto { Title = "New Book", Author = "New Author", ISBN = "789", PublishedDate = DateTime.Now };
            _mockService.Setup(service => service.AddBookAsync(bookDto)).ReturnsAsync(1);

            // Act
            var result = await _controller.AddBook(bookDto);

            // Assert
            var createdAtActionResult = result as CreatedAtActionResult;
            Assert.IsNotNull(createdAtActionResult);
            Assert.AreEqual(nameof(_controller.GetBook), createdAtActionResult.ActionName);
            Assert.AreEqual(1, createdAtActionResult.RouteValues["id"]);
            Assert.AreEqual(bookDto, createdAtActionResult.Value);
        }

        [Test]
        public async Task AddBook_WithInvalidModel_ReturnsBadRequest()
        {
            // Arrange
            _controller.ModelState.AddModelError("Title", "Title is required");

            // Act
            var result = await _controller.AddBook(new BookDto());

            // Assert
            Assert.IsInstanceOf<BadRequestResult>(result);
        }

        [Test]
        public async Task UpdateBook_WithValidModel_ReturnsNoContent()
        {
            // Arrange
            var bookDto = new BookDto { Title = "Updated Book", Author = "Updated Author", ISBN = "789", PublishedDate = DateTime.Now };

            // Act
            var result = await _controller.UpdateBook(1, bookDto);

            // Assert
            Assert.IsInstanceOf<NoContentResult>(result);
            _mockService.Verify(service => service.UpdateBookAsync(bookDto), Times.Once);
        }

        [Test]
        public async Task UpdateBook_WithInvalidModel_ReturnsBadRequest()
        {
            // Arrange
            _controller.ModelState.AddModelError("Author", "Author is required");

            // Act
            var result = await _controller.UpdateBook(1, new BookDto());

            // Assert
            Assert.IsInstanceOf<BadRequestResult>(result);
        }

        [Test]
        public async Task DeleteBook_WithValidId_ReturnsNoContent()
        {
            // Arrange
            var id = 1;

            // Act
            var result = await _controller.DeleteBook(id);

            // Assert
            Assert.IsInstanceOf<NoContentResult>(result);
            _mockService.Verify(service => service.DeleteBookAsync(id), Times.Once);
        }
    }
}
