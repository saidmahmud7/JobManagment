using System.Net;
using Dapper;
using Domain.Model;
using Infrastructure.DataContext;
using Infrastructure.Response;

namespace Infrastructure.Service.JobService;

public class JobService(DapperContext context) : IJobService
{
    public async Task<ApiResponse<List<Job>>> GetAll()
    {
        using var connection = context.Connection();
        var sql = "select * from jobs";
        var res = await connection.QueryAsync<Job>(sql);
        return new ApiResponse<List<Job>>(res.ToList());
    }

    public async Task<ApiResponse<Job>> GetJobById(int id)
    {
        using var connection = context.Connection();
        var sql = "select * from jobs where jobid = @Id";
        var res = await context.Connection().QuerySingleOrDefaultAsync<Job>(sql, new { Id = id });
        if (res == null) return new ApiResponse<Job>(HttpStatusCode.NotFound, "Job not found");
        return new ApiResponse<Job>(res);
    }

    public async Task<ApiResponse<bool>> AddJob(Job job)
    {
        using var connection = context.Connection();
        var sql =
            "insert into jobs (EmployerId,title,description,salary,country,city,status) values (@EmployerId,@Title,@Description,@Salary,@Country,@City,@Status)";
        var res = await context.Connection().ExecuteAsync(sql, job);
        if (res == 0) return new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Internal server error");
        return new ApiResponse<bool>(res > 0);
    }

    public async Task<ApiResponse<bool>> UpdateJob(Job job)
    {
        using var connection = context.Connection();
        var sql =
            @"update jobs set EmployerId=@EmployerId,title=@title,description=@description,salary=@salary,country=@country,city=@city,status=@status
                   where jobid = @JobId";
        var res = await context.Connection().ExecuteAsync(sql, job);
        if (res == 0) return new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Internal server error");
        return new ApiResponse<bool>(res > 0);
    }

    public async Task<ApiResponse<bool>> DeleteJob(int id)
    {
        using var connection = context.Connection();
        var sql = "delete from jobs where jobid = @Id";
        var res = await context.Connection().ExecuteAsync(sql, new { Id = id });
        if (res == 0) return new ApiResponse<bool>(HttpStatusCode.NotFound, "User not found");
        return new ApiResponse<bool>(res != 0);
    }

    public async Task<ApiResponse<int>> GetAvarageSalary()
    {
        using var connection = context.Connection();
        var sql = "select avg(salary) as avgsalary from jobs";
        int res = await context.Connection().QuerySingleOrDefaultAsync(sql);
        if (res == null) return new ApiResponse<int>(HttpStatusCode.NotFound, "Avg not found");
        return new ApiResponse<int>(res);
    }

    public async Task<ApiResponse<List<Job>>> GetLastJob()
    {
        using var connection = context.Connection();
        var sql = "select * from jobs ORDER by title desc limit 10";
        var res = await context.Connection().QueryAsync<Job>(sql);
        return new ApiResponse<List<Job>>(res.ToList());
    }

    public async Task<ApiResponse<decimal>> GetHighSalary()
    {
        var sql = "select * from jobs order by salary desc limit 1;";
        decimal res = await context.Connection().QuerySingleOrDefaultAsync(sql);
        return new ApiResponse<decimal>(res);
    }
    public async Task<ApiResponse<decimal>> GetLowSalary()
    {
        var sql = "select * from jobs order by salary asc limit 1";
        decimal res = await context.Connection().QuerySingleOrDefaultAsync(sql);
        return new ApiResponse<decimal>(res);
    }

    public async Task<ApiResponse<int>> GetCountJobs(Job job )
    {
        var sql = "select count(*) from jobs where country = @Country;";
        var res = await context.Connection().QuerySingleOrDefaultAsync(sql);
        return new ApiResponse<int>(res);
    }
}