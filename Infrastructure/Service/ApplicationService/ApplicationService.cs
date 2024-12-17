using System.Net;
using Dapper;
using Domain.Model;
using Infrastructure.DataContext;
using Infrastructure.Response;

namespace Infrastructure.Service.ApplicationService;

public class ApplicationService(DapperContext context) : IApplication
{
    public async Task<ApiResponse<List<Application>>> GetAll()
    {
        using var connection = context.Connection();
        var sql = "select * from applications";
        var res = await connection.QueryAsync<Application>(sql);
        return new ApiResponse<List<Application>>(res.ToList());
    }

    public async Task<ApiResponse<Application>> GetById(int id)
    {
        using var connection = context.Connection();
        var sql = "select * from applications where ApplicationId = @Id";
        var res = await context.Connection().QuerySingleOrDefaultAsync<Application>(sql, new { Id = id });
        if (res == null) return new ApiResponse<Application>(HttpStatusCode.NotFound, "Application not found");
        return new ApiResponse<Application>(res);
    }

    public async Task<ApiResponse<bool>> AddApplication(Application application)
    {
        using var connection = context.Connection();
        var sql = "insert into applications (JobId,ApplicantId,Resume,Status) values(@JobId,@ApplicantId,@Resume,@Status)";
        var res = await context.Connection().ExecuteAsync(sql, application);
        if (res == 0) return new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Internal server error");
        return new ApiResponse<bool>(res > 0);
    }

    public async Task<ApiResponse<bool>> UpdateApplication(Application application)
    {
        using var connection = context.Connection();
        var sql = @"update applications set JobId=@JobId,ApplicantId=@ApplicantId,Resume=@Resume,Status=@Status 
           where ApplicationId = @ApplicationId";
        var res = await context.Connection().ExecuteAsync(sql, application);
        if (res == 0) return new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Internal server error");
        return new ApiResponse<bool>(res > 0);
    }

    public async Task<ApiResponse<bool>> DeleteApplication(int id)
    {
        using var connection = context.Connection();
        var sql = "delete from applications where ApplicationId = @Id";
        var res = await context.Connection().ExecuteAsync(sql, new { Id = id });
        if (res == 0) return new ApiResponse<bool>(HttpStatusCode.NotFound, "Application not found");
        return new ApiResponse<bool>(res != 0);
    }
}