using Newtonsoft.Json;
using api.Models;
using api.Repository;

namespace api.Services
{
    public interface IJobService
    {
        List<JobDTO> GetAllJobs();
        JobDTO GetJobById(int id);
    }

    public class JobService : IJobService
    {
        private readonly string _filePath;

        private readonly IJobRepository _repository;

        public JobService(IConfiguration configuration)
        {
            _filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "jobs.json");
            _repository = new JobRepository(_filePath);
        }

        public List<JobDTO> GetAllJobs()
        {
            try
            {
                var jobs = _repository.GetAllJobs();
                return jobs.Select(job => JobMapper.ToJobDTO(job)).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading jobs: {ex.Message}");
                return new List<JobDTO>();
            }
        }


        public JobDTO GetJobById(int id)
        {
            var job = GetAllJobs().FirstOrDefault(job => job.Id == id);

            if (job == null)
                return new JobDTO();

            return job;
        }
    }

}