using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using api.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace api.Controllers
{
    [Route("api/[controller]")]
    // [Route("api/jobs")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private readonly string _filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "jobs.json");
        // GET: api/job?page={pageNumber}&limit={pageSize}
        [HttpGet]
        public IActionResult GetAllJobs([FromQuery] int page = 1, [FromQuery] int limit = 5)
        {
            try
            {
                var jobs = LoadJobs();

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
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }

        // GET: api/job/{id}
        [HttpGet("{id}")]
        public IActionResult GetJobById(int id)
        {
            try
            {
                var job = LoadJobs().FirstOrDefault(j => j.Id == id);
                if (job == null)
                {
                    return NotFound($"Job with ID {id} not found");
                }
                return Ok(job);
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }

        private List<Job> LoadJobs()
        {
            try
            {
                // Step 1: Read the JSON file
                string json = System.IO.File.ReadAllText(_filePath);

                // Step 2: Deserialize JSON into a list of RootDTO
                List<JobDTO>? jobDTOs = JsonConvert.DeserializeObject<List<JobDTO>>(json);

                // Step 3: Map DTOs to Domain Models
                List<Job> jobs = new List<Job>();
                foreach (var jobDTO in jobDTOs)
                {
                    jobs.Add(JobMapper.ToJob(jobDTO));
                }

                return jobs;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading jobs: {ex.Message}");
                return new List<Job>();
            }
        }
    }
}
