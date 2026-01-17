using Moq;
using Xunit.Abstractions;
using Patient.Service;
using Patient.Request_Responce;
using Model;
using Patient.Controller;
using FeatureHub;

namespace Mesurment.Tests;

public class PatientTest
{
    private readonly ITestOutputHelper _testOutputHelper;
    private static List<Measurements>? Measurements;
    public PatientTest(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public async void FailGetPatientLoginTest()
    {
        // Arrange
        var mock = new Mock<IPatientService>();

        var GeneralResponce = new GeneralResponce(400, "Fail");

        mock.Setup(client => client.Login(It.IsAny<string>())).ReturnsAsync(new PatientPayload(GeneralResponce, new Patients()));

        var featureServiceMock = new Mock<FeatureService>();

        featureServiceMock.Setup(client => client.FeatureFlagChecker(It.IsAny<Features>())).Returns(true);

        var patientController = new PatientController(mock.Object, featureServiceMock.Object);
        // Act    
        var result = await patientController.Login("123456789");

        // Assert
        _testOutputHelper.WriteLine(result.generalResponce._status.ToString());
        _testOutputHelper.WriteLine(result.generalResponce._message);
        Assert.Equal(400, result.generalResponce._status);
        Assert.Equal("Fail", result.generalResponce._message);
    }

    [Fact]
    public async void SuccessGetPatientLoginTest()
    {
        // Arrange
        var mock = new Mock<IPatientService>();

        var GeneralResponce = new GeneralResponce(200, "Success");

        mock.Setup(client => client.Login(It.IsAny<string>())).ReturnsAsync(new PatientPayload(GeneralResponce, new Patients()));

        var featureServiceMock = new Mock<FeatureService>();

        featureServiceMock.Setup(client => client.FeatureFlagChecker(It.IsAny<Features>())).Returns(true);

        var patientController = new PatientController(mock.Object, featureServiceMock.Object);
        // Act    
        var result = await patientController.Login("123456789");

        // Assert
        _testOutputHelper.WriteLine(result.generalResponce._status.ToString());
        _testOutputHelper.WriteLine(result.generalResponce._message);
        Assert.Equal(200, result.generalResponce._status);
        Assert.Equal("Success", result.generalResponce._message);
    }

    [Fact]
    public async void FailRegisterPatientTest()
    {
        //Arrange
        var mock = new Mock<IPatientService>();

        var GeneralResponce = new GeneralResponce(400, "Fail");

        mock.Setup(client => client.Register(It.IsAny<Patients>())).ReturnsAsync(GeneralResponce);

        var featureServiceMock = new Mock<FeatureService>();

        featureServiceMock.Setup(client => client.FeatureFlagChecker(It.IsAny<Features>())).Returns(true);

        var patientController = new PatientController(mock.Object, featureServiceMock.Object);

        //Act
        var result = await patientController.Register(new Patients());

        //Assert
        _testOutputHelper.WriteLine(result._status.ToString());
        _testOutputHelper.WriteLine(result._message);
        Assert.Equal(400, result._status);
        Assert.Equal("Fail", result._message);
    }

    [Fact]
    public async void SuccessRegisterPatientTest()
    {
        //Arrange
        var mock = new Mock<IPatientService>();

        var GeneralResponce = new GeneralResponce(200, "Success");

        mock.Setup(client => client.Register(It.IsAny<Patients>())).ReturnsAsync(GeneralResponce);

        var featureServiceMock = new Mock<FeatureService>();

        featureServiceMock.Setup(client => client.FeatureFlagChecker(It.IsAny<Features>())).Returns(true);

        var patientController = new PatientController(mock.Object, featureServiceMock.Object);

        //Act
        var result = await patientController.Register(new Patients());

        //Assert
        _testOutputHelper.WriteLine(result._status.ToString());
        _testOutputHelper.WriteLine(result._message);
        Assert.Equal(200, result._status);
        Assert.Equal("Success", result._message);
    }

    [Fact]
    public async void NotFoundLoginTest()
    {
        // Arrange
        var mock = new Mock<IPatientService>();

        var GeneralResponce = new GeneralResponce(404, "No User Found");

        mock.Setup(client => client.Login(It.IsAny<string>())).ReturnsAsync(new PatientPayload(GeneralResponce, new Patients()));

        var featureServiceMock = new Mock<FeatureService>();

        featureServiceMock.Setup(client => client.FeatureFlagChecker(It.IsAny<Features>())).Returns(true);

        var patientController = new PatientController(mock.Object, featureServiceMock.Object);
        // Act    
        var result = await patientController.Login("");

        // Assert
        _testOutputHelper.WriteLine(result.generalResponce._status.ToString());
        _testOutputHelper.WriteLine(result.generalResponce._message);
        Assert.Equal(404, result.generalResponce._status);
        Assert.Equal("No User Found", result.generalResponce._message);
    }
}