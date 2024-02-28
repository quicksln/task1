using Moq;
using Xunit;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using QS.Task1.APIChecker;
using QS.Task1.APIChecker.Interfaces;
using QS.Task1.Services.Interfaces;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Timers;
using QS.Task1.Services.Models;
using QS.Task1.APIChecker.Models;

public class APICheckerTests
{
    private readonly APIChecker _apiChecker;
    private readonly Mock<IAPIClient> _mockApiClient;
    private readonly Mock<IAzureStorageService> _mockAzureStorageService;
    private readonly Mock<ILogger> _mockLogger;
    private readonly TimerInfo _timer;

    public APICheckerTests()
    {
        _mockApiClient = new Mock<IAPIClient>();
        var mockedResponse = new APIResponse() { Entries = new List<Entry>() { new Entry() } }; 
        string mockedFile = "sample.json";
        _mockApiClient.Setup(m => m.GetRandomAPI(It.IsAny<string>())).ReturnsAsync((mockedResponse, mockedFile));

        _mockAzureStorageService = new Mock<IAzureStorageService>();
        var mockConfiguration = new Mock<IConfiguration>();
        _mockLogger = new Mock<ILogger>();
        _timer = new TimerInfo(new ConstantSchedule(TimeSpan.Zero), new ScheduleStatus());

        _apiChecker = new APIChecker(_mockApiClient.Object, mockConfiguration.Object, _mockAzureStorageService.Object);
    }

    [Fact]
    public async Task Run_ShouldCallGetRandomAPI()
    {
        // Act
        await _apiChecker.Run(_timer, _mockLogger.Object);

        // Assert
        _mockApiClient.Verify(m => m.GetRandomAPI(It.IsAny<string>()), Times.Once);
    }

    [Fact]
    public async Task Run_ShouldCallSaveAPIResponseToBlobStorage()
    {
        // Act
        await _apiChecker.Run(_timer, _mockLogger.Object);

        // Assert
        _mockAzureStorageService.Verify(m => m.SaveAPIResponseToBlobStorage(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
    }

    [Fact]
    public async Task Run_ShouldCallAddAPIResponseToTableStorage()
    {
        // Act
        await _apiChecker.Run(_timer, _mockLogger.Object);

        // Assert
        _mockAzureStorageService.Verify(m => m.AddAPIResponseToTableStorage(It.IsAny<APIEntry>()), Times.Once);
    }
}