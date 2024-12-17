using Domain.Model;
using Infrastructure.Response;
using Infrastructure.Service.ApplicationService;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controller;

[ApiController]
[Route("api/[controller]")]
public class ApplicationController(IApplication service) :ControllerBase
{
    [HttpGet]
    public async Task<ApiResponse<List<Application>>>GetAll()
    {
        var res = await service.GetAll();
        return res;
    }

    [HttpGet("GetById/{id}")]
    public async Task<ApiResponse<Application>> GetById(int id)
    {
        var res = await service.GetById(id);
        return res;
    }

    [HttpPost]
    public async Task<ApiResponse<bool>> Add(Application application)
    {
        var res = await service.AddApplication(application);
        return res;
    }

    [HttpPut]
    public async Task<ApiResponse<bool>> Update(Application application)
    {
        var res = await service.UpdateApplication(application);
        return res;
    }

    [HttpDelete]
    public async Task<ApiResponse<bool>> Delete(int id)
    {
        var res = await service.DeleteApplication(id);
        return res;
    }
}


