using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using WebAPI.Constans;

using WebAPI.Models;

namespace WebAPI.Client;

public class JobClient
{
    private HttpClient _client;
    private static string _address;
    private static string _apikey;
    static string fileName = "updates.json";


    public JobClient()
    {
        _address = Constants.adress;
        _apikey = Constants.apikey;
        _client = new HttpClient();
        _client.BaseAddress = new Uri(_address);
    }

    public async Task<JobSearch> GetJobsAsync(string jobName, string user)
    {
        var response = await _client.GetAsync($"search.json?engine=google_jobs&q={jobName}&hl=en&api_key={_apikey}");
        var content = response.Content.ReadAsStringAsync().Result;
        var result = JsonConvert.DeserializeObject<JobSearch>(content);
        var dataToSave = JsonConvert.SerializeObject(result);
        File.WriteAllText($"{user}.json", dataToSave);
        return result;
    }

    public async Task<JobsResult> GetJobByPosition(int postion, string user)
    {
        var content = await File.ReadAllTextAsync($"{user}.json");
        var result = JsonConvert.DeserializeObject<JobSearch>(content)?.jobs_results[postion];
        return result;
    }

    public async Task<JobSearch> PostJob(JobsResult job, string user)
    {
        var content = await File.ReadAllTextAsync($"{user}.json");
        var jobs = JsonConvert.DeserializeObject<JobSearch>(content);
        jobs.jobs_results.Add(job);
        var dataToSave = JsonConvert.SerializeObject(jobs);
        File.WriteAllText($"{user}.json", dataToSave);
        return jobs;
    }

    public async Task<JobSearch> PostEditJob(int position, JobsResult job, string user)
    {
        var content = await File.ReadAllTextAsync($"{user}.json");
        var jobs = JsonConvert.DeserializeObject<JobSearch>(content);
        jobs.jobs_results[position] = job;
        var dataToSave = JsonConvert.SerializeObject(jobs);
        File.WriteAllText($"{user}.json", dataToSave);
        return jobs;
    }

    public async Task DeleteJobByPosition(int position, string user)
    {
        var content = await File.ReadAllTextAsync($"{user}.json");
        var jobs = JsonConvert.DeserializeObject<JobSearch>(content);
        jobs.jobs_results.RemoveAt(position);
        var dataToSave = JsonConvert.SerializeObject(jobs);
        File.WriteAllText($"{user}.json", dataToSave);
    }
}