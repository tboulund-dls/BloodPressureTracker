using Moq;
using Xunit.Abstractions;
using Mesurment.Service;
using Mesurment.Request_Responce;
using Model;
using Mesurment.Controller;
using FeatureHub;

namespace Mesurment.Tests;

public class MesurmentTest
{
    private readonly ITestOutputHelper _testOutputHelper;
    private static List<Measurements>? Measurements;
    public MesurmentTest(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        InitializeTests();
    }

    private static void InitializeTests()
    {
        Measurements = new List<Measurements>();

        for (int i = 1; i <= 100; i++)
        {
            Measurements.Add(new Measurements()
            {
                Id = i,
                Datetime = DateTime.Now.AddDays(i),
                Systolic = i * 2,
                Diastolic = i * 3,
                PatientSSN = "123456789",
            });
        }
    }

    [Fact]
    public async void SuccessMeasurementsCreationTest()
    {
        // Arrange
        var mock = new Mock<IMesurmentService>();

        var GeneralResponce = new GeneralResponce(200, "Success");

        mock.Setup(client => client.CreateMeasurements(It.IsAny<Measurements>())).ReturnsAsync(GeneralResponce);

        var featureServiceMock = new Mock<FeatureService>();

        featureServiceMock.Setup(client => client.FeatureFlagChecker(It.IsAny<Features>())).Returns(true);

        var mesurmentController = new MesurmentController(mock.Object, featureServiceMock.Object);

        // Act
        var result = await mesurmentController.CreateMeasurements(new Measurements()
        {
            Datetime = DateTime.Now,
            Systolic = 120,
            Diastolic = 80,
            PatientSSN = "123456789",
        });

        // Assert 
        _testOutputHelper.WriteLine(result._status.ToString());
        _testOutputHelper.WriteLine(result._message);
        Assert.Equal(200, result._status);
        Assert.Equal("Success", result._message);
    }

    [Fact]
    public async void FailMeasurementsCreationTest()
    {
        // Arrange
        var mock = new Mock<IMesurmentService>();

        var GeneralResponce = new GeneralResponce(400, "Fail");

        mock.Setup(client => client.CreateMeasurements(It.IsAny<Measurements>())).ReturnsAsync(GeneralResponce);

        var featureServiceMock = new Mock<FeatureService>();

        featureServiceMock.Setup(client => client.FeatureFlagChecker(It.IsAny<Features>())).Returns(true);

        var mesurmentController = new MesurmentController(mock.Object, featureServiceMock.Object);

        // Act
        var result = await mesurmentController.CreateMeasurements(new Measurements()
        {
            Datetime = DateTime.Now,
            Systolic = 120,
            Diastolic = 80,
            PatientSSN = "123456789",
        });

        // Assert 
        _testOutputHelper.WriteLine(result._status.ToString());
        _testOutputHelper.WriteLine(result._message);
        Assert.Equal(400, result._status);
        Assert.Equal("Fail", result._message);
    }

    [Fact]
    public async void SuccessUpdateMeasurementsCreationTest()
    {
        // Arrange  
        var mock = new Mock<IMesurmentService>();

        var GeneralResponce = new GeneralResponce(200, "Success");

        mock.Setup(client => client.updateMeasurements(It.IsAny<Measurements>())).ReturnsAsync(GeneralResponce);

        var featureServiceMock = new Mock<FeatureService>();

        featureServiceMock.Setup(client => client.FeatureFlagChecker(It.IsAny<Features>())).Returns(true);

        var mesurmentController = new MesurmentController(mock.Object, featureServiceMock.Object);

        // Act  
        var result = await mesurmentController.updateMeasurements(new Measurements()
        {
            Id = 1,
            Datetime = DateTime.Now,
            Systolic = 120,
            Diastolic = 80,
            PatientSSN = "123456789",
        });

        // Assert 
        _testOutputHelper.WriteLine(result._status.ToString());
        _testOutputHelper.WriteLine(result._message);
        Assert.Equal(200, result._status);
        Assert.Equal("Success", result._message);
    }

    [Fact]
    public async void FailUpdateMeasurementsCreationTest()
    {
        //Arrange
        var mock = new Mock<IMesurmentService>();
        var GeneralResponce = new GeneralResponce(400, "Fail");

        mock.Setup(client => client.updateMeasurements(It.IsAny<Measurements>())).ReturnsAsync(GeneralResponce);

        var featureServicemock = new Mock<FeatureService>();

        featureServicemock.Setup(client => client.FeatureFlagChecker(It.IsAny<Features>())).Returns(true);

        var mesurmentController = new MesurmentController(mock.Object, featureServicemock.Object);

        //Act
        var result = await mesurmentController.updateMeasurements(new Measurements()
        {
            Id = 1,
            Datetime = DateTime.Now,
            Systolic = 120,
            Diastolic = 80,
            PatientSSN = "123456789",
        });

        //Assert
        _testOutputHelper.WriteLine(result._status.ToString());
        _testOutputHelper.WriteLine(result._message);
        Assert.Equal(400, result._status);
        Assert.Equal("Fail", result._message);
    }

    [Fact]
    public async void SuccessDeleteMeasurementsCreationTest()
    {
        // Arrange
        var mock = new Mock<IMesurmentService>();

        var GeneralResponce = new GeneralResponce(200, "Success");

        mock.Setup(client => client.DeleteMeasurements(It.IsAny<int>())).ReturnsAsync(GeneralResponce);

        var featureServiceMock = new Mock<FeatureService>();

        featureServiceMock.Setup(client => client.FeatureFlagChecker(It.IsAny<Features>())).Returns(true);

        var mesurmentController = new MesurmentController(mock.Object, featureServiceMock.Object);

        // Act
        var result = await mesurmentController.DeleteMeasurements(1);

        // Assert 
        _testOutputHelper.WriteLine(result._status.ToString());
        _testOutputHelper.WriteLine(result._message);
        Assert.Equal(200, result._status);
        Assert.Equal("Success", result._message);
    }

    [Fact]
    public async void FailDeleteMeasurementsCreationTest()
    {
        // Arrange
        var mock = new Mock<IMesurmentService>();

        var GeneralResponce = new GeneralResponce(400, "Fail");

        mock.Setup(client => client.DeleteMeasurements(It.IsAny<int>())).ReturnsAsync(GeneralResponce);

        var featureServiceMock = new Mock<FeatureService>();

        featureServiceMock.Setup(client => client.FeatureFlagChecker(It.IsAny<Features>())).Returns(true);

        var mesurmentController = new MesurmentController(mock.Object, featureServiceMock.Object);

        // Act
        var result = await mesurmentController.DeleteMeasurements(1);

        // Assert 
        _testOutputHelper.WriteLine(result._status.ToString());
        _testOutputHelper.WriteLine(result._message);
        Assert.Equal(400, result._status);
        Assert.Equal("Fail", result._message);
    }

    [Fact]
    public async void SuccessGetAllUserMeasurementsCreationTest()
    {
        // Arrange
        var mock = new Mock<IMesurmentService>();

        var GeneralResponce = new GeneralResponce(200, "Success");

        mock.Setup(client => client.GetAllUserMeasurements(It.IsAny<string>())).ReturnsAsync(new MeasurmentListPayload(GeneralResponce, Measurements));

        var featureServiceMock = new Mock<FeatureService>();

        featureServiceMock.Setup(client => client.FeatureFlagChecker(It.IsAny<Features>())).Returns(true);

        var mesurmentController = new MesurmentController(mock.Object, featureServiceMock.Object);
        // Act
        var result = await mesurmentController.GetAllUserMeasurements("123456789");

        // Assert 
        _testOutputHelper.WriteLine(result.generalResponce._status.ToString());
        _testOutputHelper.WriteLine(result.generalResponce._message);
        _testOutputHelper.WriteLine("" + result.measurements?.Count);
        Assert.Equal(200, result.generalResponce._status);
        Assert.Equal("Success", result.generalResponce._message);
        Assert.Equal(100, result.measurements?.Count);
    }

    [Fact]
    public async void FailGetAllUserMeasurementsCreationTest()
    {
        // Arrange
        var mock = new Mock<IMesurmentService>();

        var GeneralResponce = new GeneralResponce(400, "Fail");

        mock.Setup(client => client.GetAllUserMeasurements(It.IsAny<string>())).ReturnsAsync(new MeasurmentListPayload(GeneralResponce, Measurements));

        var featureServiceMock = new Mock<FeatureService>();

        featureServiceMock.Setup(client => client.FeatureFlagChecker(It.IsAny<Features>())).Returns(true);

        var mesurmentController = new MesurmentController(mock.Object, featureServiceMock.Object);
        // Act
        var result = await mesurmentController.GetAllUserMeasurements("123456789");

        // Assert 
        _testOutputHelper.WriteLine(result.generalResponce._status.ToString());
        _testOutputHelper.WriteLine(result.generalResponce._message);
        _testOutputHelper.WriteLine("" + result.measurements?.Count);
        Assert.Equal(400, result.generalResponce._status);
        Assert.Equal("Fail", result.generalResponce._message);
        Assert.Equal(100, result.measurements?.Count);
    }
}