using Domain.Model;
using Infrastructure.Response;
using Infrastructure.Service.JobService;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controller;
[ApiController]
[Route("api/[controller]")]
public class JobController(IJobService service):ControllerBase
{
    [HttpGet]
    public async Task<ApiResponse<List<Job>>>GetAll()
    {
        var res = await service.GetAll();
        return res;
    }

    [HttpGet("GetById/{id}")]
    public async Task<ApiResponse<Job>> GetById(int id)
    {
        var res = await service.GetJobById(id);
        return res;
    }

    [HttpPost]
    public async Task<ApiResponse<bool>> Add(Job job)
    {
        var res = await service.AddJob(job);
        return res;
    }

    [HttpPut]
    public async Task<ApiResponse<bool>> Update(Job job)
    {
        var res = await service.UpdateJob(job);
        return res;
    }

    [HttpDelete]
    public async Task<ApiResponse<bool>> Delete(int id)
    {
        var res = await service.DeleteJob(id);
        return res;
    }
}