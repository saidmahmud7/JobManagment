using Domain.Model;
using Infrastructure.Response;

namespace Infrastructure.Service.ApplicationService;

public interface IApplication
{
    Task<ApiResponse<List<Application>>> GetAll();
    Task<ApiResponse<Application>> GetById(int id);
    Task<ApiResponse<bool>> AddApplication(Application application);
    Task<ApiResponse<bool>> UpdateApplication(Application application);
    Task<ApiResponse<bool>> DeleteApplication(int id);
}