using System.Collections.Generic;
using Moq;
using Xunit;
using api.Services;
using api.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

public class JobServiceTests
{
    private readonly Mock<IMemoryCache> _mockCache;
    private readonly Mock<IConfiguration> _mockConfig;

    private readonly JobService _jobService;

    public JobServiceTests()
    {
        _mockCache = new Mock<IMemoryCache>();
        _mockConfig = new Mock<IConfiguration>();

        _mockConfig.Setup(c => c["JobsFilePath"]).Returns("TestData/jobs.json");

        _jobService = new JobService(_mockCache.Object, _mockConfig.Object);
    }

    [Fact]
    public void GetAllJobs_ReturnsJobs_WhenCacheIsEmpty()
    {
        // Arrange
        var cacheKey = "job";
        object cacheValue = null;
        _mockCache
            .Setup(c => c.TryGetValue(cacheKey, out cacheValue))
            .Returns(false);

        // Act
        var jobs = _jobService.GetAllJobs();

        // Assert
        Assert.NotNull(jobs);
        Assert.NotEmpty(jobs);
    }

    [Fact]
    public void GetAllJobs_ParsesJsonFileCorrectly()
    {
        // Arrange
        var testJson = "[{\"Id\":1,\"req_name\":\"Software Engineer\"}]";
        File.WriteAllText("TestData/jobs.json", testJson);

        // Act
        var jobs = _jobService.GetAllJobs();

        // Assert
        Assert.NotNull(jobs);
        Assert.Single(jobs);
        Assert.Equal("Software Engineer", jobs.First().ReqName);
    }

    [Fact]
    public void GetJobById_ReturnsNull_ForInvalidId()
    {
        // Arrange
        var invalidId = 999;

        // Act
        var job = _jobService.GetJobById(invalidId);

        // Assert
        Assert.Null(job);
    }


}
