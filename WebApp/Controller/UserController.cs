using Domain.Model;
using Infrastructure.Response;
using Infrastructure.Service.UserService;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controller;
[ApiController]
[Route("api/[controller]")]
public class UserController(IUserService service) :ControllerBase
{
    [HttpGet]
    public async Task<ApiResponse<List<User>>>GetAll()
    {
        var res = await service.GetAll();
        return res;
    }

    [HttpGet("GetById/{id}")]
    public async Task<ApiResponse<User>> GetById(int id)
    {
        var res = await service.GetUserById(id);
        return res;
    }

    [HttpPost]
    public async Task<ApiResponse<bool>> Add(User user)
    {
        var res = await service.AddUser(user);
        return res;
    }

    [HttpPut]
    public async Task<ApiResponse<bool>> Update(User user)
    {
        var res = await service.UpdateUser(user);
        return res;
    }

    [HttpDelete]
    public async Task<ApiResponse<bool>> Delete(int id)
    {
        var res = await service.DeleteUser(id);
        return res;
    }
}

