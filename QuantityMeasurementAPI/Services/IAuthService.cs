using QuantityMeasurementAPI.DTOs;
using QuantityMeasurementAPI.Models;

namespace QuantityMeasurementAPI.Services
{
    public interface IAuthService
    {
        // Registration & Login
        Task<User?> Register(RegisterRequest request);
        Task<LoginResponse?> Login(LoginRequest request);
        
        // Google Authentication
        Task<GoogleAuthResponse?> GoogleLogin(string idToken);
        Task<GoogleUserInfo?> VerifyGoogleToken(string idToken);
        
        // User Management
        Task<User?> GetUserById(int id);
        Task<User?> UpdateUser(int id, UpdateUserRequest request);
        Task<bool> DeleteUser(int id);
        
        // JWT & Password
        bool VerifyPassword(string password, string hash, string salt);
        string GenerateJwtToken(User user);
    }
}