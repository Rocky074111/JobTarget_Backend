using api.Models;

public static class JobMapper
{
    public static Job ToJob(JobDTO jobDTO)
    {
        return new Job
        {
            Id = jobDTO.Id,
            ReqName = jobDTO.ReqName ?? "",
            Location = LocationMapper.ToLocation(jobDTO.Location ?? new LocationDTO()),
            Description = jobDTO.Description ?? "",
            Status = jobDTO.Status ?? "",
            Postings = jobDTO.Postings.Select(PostMapper.ToPost).ToList()
        };
    }

    public static JobDTO TojobDTO(Job job)
    {
        return new JobDTO
        {
            Id = job.Id ?? 0,
            ReqName = job.ReqName,
            Location = LocationMapper.ToLocationDTO(job.Location ?? new Location()),
            Description = job.Description,
            Status = job.Status,
            Postings = job.Postings.Select(PostMapper.ToPostDTO).ToList()
        };
    }
}