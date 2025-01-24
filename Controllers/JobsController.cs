using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using api.Models;
using api.Services;
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
        // GET: api/job?page={pageNumber}&limit={pageSize}
        private readonly IJobService _jobService;

        public JobsController(IJobService jobService)
        {
            _jobService = jobService ?? throw new ArgumentNullException(nameof(jobService));
        }

        [HttpGet]
        public IActionResult GetAllJobs([FromQuery] int page = 1, [FromQuery] int limit = 5)
        {
            try
            {
                var jobs = _jobService.GetAllJobs();
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
            catch(Exception e)
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
                var job = _jobService.GetJobById(id);
                if (job == null) return NotFound(new { Message = $"Job with ID {id} not found" });

                return Ok(job);
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }

        
    }
}
