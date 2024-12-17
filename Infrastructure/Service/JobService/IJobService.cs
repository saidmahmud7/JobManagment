using Domain.Model;
using Infrastructure.Response;

namespace Infrastructure.Service.JobService;

public interface IJobService
{
    Task<ApiResponse<List<Job>>> GetAll();
    Task<ApiResponse<Job>> GetJobById(int id);
    Task<ApiResponse<bool>> AddJob(Job job);
    Task<ApiResponse<bool>> UpdateJob(Job job);
    Task<ApiResponse<bool>> DeleteJob(int id);
    Task<ApiResponse<int>> GetAvarageSalary();
    Task<ApiResponse<List<Job>>> GetLastJob();
}