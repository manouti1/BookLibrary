using BookLibrary.Domain;
using Microsoft.EntityFrameworkCore;
using BookLibrary.Infastructure.Persistence;
using BookLibrary.Dtos;
using Microsoft.AspNetCore.Identity;

namespace BookLibrary.Application
{
    public interface IUserService
    {
        Task<User> GetUserByUsernameAsync(string username);
        Task RegisterUserAsync(UserDto user);
        Task<User> AuthenticateUserAsync(string username, string password);
    }
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        private readonly PasswordHasher<User> _passwordHasher;

        public UserService(AppDbContext context)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<User>();
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task RegisterUserAsync(UserDto user)
        {
            // Check if the username already exists
            if (await _context.Users.AnyAsync(u => u.Username == user.Username))
                throw new Exception("Username already exists.");

            // Hash the password using PasswordHasher
            var dbUser = new User
            {
                Username = user.Username,
                Password = _passwordHasher.HashPassword(null, user.Password), // Hash the password
            };

            await _context.Users.AddAsync(dbUser);
            await _context.SaveChangesAsync();
        }

        public async Task<User> AuthenticateUserAsync(string username, string password)
        {
            // Find the user by username
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);

            if (user == null)
                throw new Exception("Invalid username or password."); // User not found

            // Verify the password using PasswordHasher
            var verificationResult = _passwordHasher.VerifyHashedPassword(user, user.Password, password);

            if (verificationResult == PasswordVerificationResult.Failed)
                throw new Exception("Invalid username or password."); // Password doesn't match

            return user; // Password matches
        }
    }
}
