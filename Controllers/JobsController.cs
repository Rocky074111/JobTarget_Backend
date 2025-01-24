using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using api.Models;
using api.Services;
using Microsoft.Extensions.Caching.Memory;


namespace api.Controllers
{
    [Route("api/[controller]")]
    // [Route("api/jobs")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        // GET: api/job?page={pageNumber}&limit={pageSize}
        private readonly IJobService _jobService;
        private readonly IMemoryCache _cache;
        public JobsController(IMemoryCache cache, IJobService jobService)
        {
            _cache = cache;
            _jobService = jobService ?? throw new ArgumentNullException(nameof(jobService));
        }

        [HttpGet]
        public IActionResult GetAllJobs([FromQuery] int page = 1, [FromQuery] int limit = 5)
        {
            try
            {
                if (!_cache.TryGetValue("jobs", out List<Job> jobs))
                {
                    var jobDTOs = _jobService.GetAllJobs();
                    if (jobDTOs == null)
                    {
                        Console.WriteLine("Failed to deserialize JSON or JSON is empty.");
                        return StatusCode(500, "Internal server error");
                    }

                    List<Job> temp_jobs = new List<Job>();
                    foreach (var jobDTO in jobDTOs)
                    {
                        temp_jobs.Add(JobMapper.ToJob(jobDTO));
                    }

                    _cache.Set("jobs", temp_jobs, TimeSpan.FromMinutes(10));
                    jobs = temp_jobs;

                }

                // Pagination logic
                int totalJobs = jobs.Count();
                var totalPages = (int)Math.Ceiling(totalJobs / (double)limit);
                var pagedJobs = jobs.Skip((page - 1) * limit).Take(limit).ToList();

                return Ok(new
                {
                    TotalJobs = totalJobs,
                    TotalPages = totalPages,
                    CurrentPage = page,
                    Jobs = pagedJobs
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, "Internal server error");
            }
        }

        // GET: api/job/{id}
        [HttpGet("{id}")]
        public IActionResult GetJobById(int id)
        {
            try
            {
                if (!_cache.TryGetValue("jobs", out Job job))
                {
                    var jobDTO = _jobService.GetJobById(id);

                    if (jobDTO == null)
                    {
                        Console.WriteLine("Failed to deserialize JSON or JSON is empty.");
                        return NotFound(new { Message = $"Job with ID {id} not found" });
                    }

                    Job temp_job = JobMapper.ToJob(jobDTO);

                    _cache.Set("specific_job", temp_job, TimeSpan.FromMinutes(10));
                    job = temp_job;

                }

                return Ok(job);
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }


    }
}
