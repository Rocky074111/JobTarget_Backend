using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using api.Models;

namespace api.Repository
{
    public interface IJobRepository
    {
        List<Job> GetAllJobs();
        Job GetJobById(int id);
    }

    public class JobRepository : IJobRepository
    {
        private readonly string _filePath;

        public JobRepository(string filePath)
        {
            _filePath = filePath;
        }

        public List<Job> GetAllJobs()
        {
            var jsonData = File.ReadAllText(_filePath);
            return JsonConvert.DeserializeObject<List<Job>>(jsonData);
        }


        public Job GetJobById(int id)
        {
            var jobs = GetAllJobs();
            return jobs.Find(job => job.Id == id);
        }
    }

}
