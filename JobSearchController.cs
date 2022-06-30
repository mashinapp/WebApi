using Microsoft.AspNetCore.Mvc;
using WebAPI.Client;
using WebAPI.Models;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class JobSearchController : ControllerBase
{
    [HttpGet(Name = "Search")]
    public Task<JobSearch> Search(string search, string user)
    {
        JobClient client = new JobClient();
        return client.GetJobsAsync(search, user);
    }

    [HttpPost]
    public Task<JobSearch> AddJob(JobsResult job, string user)
    {
        JobClient client = new JobClient();
        return client.PostJob(job, user);
    }

    [HttpPost("{id}")]
    public Task<JobSearch> EditJob(int id, JobsResult job, string user)
    {
        JobClient client = new JobClient();
        return client.PostEditJob(id, job, user);
    }

    [HttpGet("{id}", Name = "Get")]
    public Task<JobsResult> FindJob(int id, string user)
    {
        JobClient client = new JobClient();
        return client.GetJobByPosition(id, user);
    }

    [HttpDelete("{id}")]
    public Task DeleteJob(int id, string user)
    {
        JobClient client = new JobClient();
        return client.DeleteJobByPosition(id, user);
    }
}

