using Newtonsoft.Json;
using api.Models;
using Microsoft.Extensions.Caching.Memory;

namespace api.Services
{
    public interface IJobService
    {
        IEnumerable<Job> GetAllJobs();
        Job GetJobById(int id);
    }

    public class JobService : IJobService
    {
        private readonly string _filePath;
        private readonly IMemoryCache _cache;

        public JobService(IMemoryCache cache, IConfiguration configuration)
        {
            _cache = cache;
            _filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "jobs.json");
        }

        public IEnumerable<Job> GetAllJobs()
        {
            try
            {
                if (!_cache.TryGetValue("jobs", out List<Job> jobs))
                {
                    string json = System.IO.File.ReadAllText(_filePath);

                    List<JobDTO>? jobDTOs = JsonConvert.DeserializeObject<List<JobDTO>>(json);
                    if (jobDTOs == null)
                    {
                        Console.WriteLine("Failed to deserialize JSON or JSON is empty.");
                        return new List<Job>();
                    }

                    List<Job> temp_jobs = new List<Job>();
                    foreach (var jobDTO in jobDTOs)
                    {
                        temp_jobs.Add(JobMapper.ToJob(jobDTO));
                    }

                    _cache.Set("jobs", temp_jobs, TimeSpan.FromMinutes(10));
                    jobs = temp_jobs;

                }
                return jobs;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading jobs: {ex.Message}");
                return new List<Job>();
            }
        }


        public Job GetJobById(int id)
        {
            var job = GetAllJobs().FirstOrDefault(job => job.Id == id);

            if (job == null)
                return new Job();

            return job;
        }
    }

}