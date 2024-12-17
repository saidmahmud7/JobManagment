using System.Net;
using Dapper;
using Domain.Model;
using Infrastructure.DataContext;
using Infrastructure.Response;

namespace Infrastructure.Service.UserService;

public class UserService(DapperContext context) : IUserService
{
    public async Task<ApiResponse<List<User>>> GetAll()
    {
        using var connection = context.Connection();
        var sql = "select * from users";
        var res = await connection.QueryAsync<User>(sql);
        return new ApiResponse<List<User>>(res.ToList());
    }

    public async Task<ApiResponse<User>> GetUserById(int id)
    {
        using var connection = context.Connection();
        var sql = "select * from users where userId = @Id";
        var res = await context.Connection().QuerySingleOrDefaultAsync<User>(sql, new { Id = id });
        if (res == null) return new ApiResponse<User>(HttpStatusCode.NotFound, "User not found");
        return new ApiResponse<User>(res);
    }

    public async Task<ApiResponse<bool>> AddUser(User user)
    {
        using var connection = context.Connection();
        var sql = "insert into (fullname,email,phone,role) values (@Fullname,@Email,@Phone,@Role)";
        var res = await context.Connection().ExecuteAsync(sql, user);
        if (res == 0) return new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Internal server error");
        return new ApiResponse<bool>(res > 0);
    }

    public async Task<ApiResponse<bool>> UpdateUser(User user)
    {
        using var connection = context.Connection();
        var sql = "update users set fullname=@fullname,email=@email,phone=@phone,role=@role where userid = @UserId";
        var res = await context.Connection().ExecuteAsync(sql, user);
        if (res == 0) return new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Internal server error");
        return new ApiResponse<bool>(res > 0);
    }

    public async Task<ApiResponse<bool>> DeleteUser(int id)
    {
        using var connection = context.Connection();
        string sql = "delete from users where userid = @Id";
        var res = await context.Connection().ExecuteAsync(sql, new { Id = id });
        if (res == 0) return new ApiResponse<bool>(HttpStatusCode.NotFound, "User not found");
        return new ApiResponse<bool>(res != 0);
    }
}