using Domain.Model;
using Infrastructure.Response;

namespace Infrastructure.Service.UserService;

public interface IUserService
{
    Task<ApiResponse<List<User>>> GetAll();
    Task<ApiResponse<User>> GetUserById(int id);
    Task<ApiResponse<bool>> AddUser(User user);
    Task<ApiResponse<bool>> UpdateUser(User user);
    Task<ApiResponse<bool>> DeleteUser(int id);
}