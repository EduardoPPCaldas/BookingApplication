using System.Threading.Tasks;
using Application;
using Application.Guests;
using Application.Guests.DTOs;
using Application.Guests.Requests;
using Domain.Entities;
using Domain.Enums;
using Domain.Ports;
using Domain.ValueObjects;
using Moq;
using Xunit;

namespace ApplicationTests;

public class GuestManagerTest
{
    [Fact]
    public async Task HappyPath()
    {
        Mock<IGuestRepository> guestRepository = new Mock<IGuestRepository>();
        guestRepository.Setup(x => x.Create(It.IsAny<Guest>())).Returns(Task.FromResult(222));
        var guestManager = new GuestManager(guestRepository.Object);

        var guestDto = new GuestDTO
        {
            Name = "Fulano",
            Surname = "Ciclano",
            Email = "fulanociclano@gmail.com",
            IdNumber = "1239",
            IdTypeCode = 1
        };
        var request = new CreateGuestRequest
        {
            Data = guestDto
        };

        var res = await guestManager.CreateGuest(request);
        Assert.NotNull(res);
        Assert.True(res.Success);
    }

    [Theory]
    [InlineData("0")]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("abc")]
    public async Task Should_Return_InvalidPersonDocumentIdException_WhenDocsAreInvalid(string docNumber)
    {
        Mock<IGuestRepository> guestRepository = new Mock<IGuestRepository>();
        guestRepository.Setup(x => x.Create(It.IsAny<Guest>())).Returns(Task.FromResult(222));
        var guestManager = new GuestManager(guestRepository.Object);

        var guestDto = new GuestDTO
        {
            Name = "Fulano",
            Surname = "Ciclano",
            Email = "fulanociclano@gmail.com",
            IdNumber = docNumber,
            IdTypeCode = 1
        };
        var request = new CreateGuestRequest
        {
            Data = guestDto
        };

        var res = await guestManager.CreateGuest(request);
        Assert.NotNull(res);
        Assert.False(res.Success);
        Assert.Equal(ErrorCodes.INVALID_PERSON_ID, res.ErrorCode);
        Assert.Equal("The ID passed is not valid", res.Message);
    }

    [Theory]
    [InlineData("Fulano", "", "fulanociclano@gmail.com")]
    [InlineData("", "Ciclano", "fulanociclano@gmail.com")]
    [InlineData("Fulano", "Ciclano", "")]
    [InlineData(null, "Ciclano", "fulanociclano@gmail.com")]
    [InlineData("Fulano", null, "fulanociclano@gmail.com")]
    [InlineData("Fulano", "Ciclano", null)]
    public async Task Should_Return_MissingRequiredInformation_WhenFieldsAreMissing(string name, string surname, string email)
    {
        Mock<IGuestRepository> guestRepository = new Mock<IGuestRepository>();
        guestRepository.Setup(x => x.Create(It.IsAny<Guest>())).Returns(Task.FromResult(222));
        var guestManager = new GuestManager(guestRepository.Object);

        var guestDto = new GuestDTO
        {
            Name = name,
            Surname = surname,
            Email = email,
            IdNumber = "1234",
            IdTypeCode = 1
        };
        var request = new CreateGuestRequest
        {
            Data = guestDto
        };

        var res = await guestManager.CreateGuest(request);
        Assert.NotNull(res);
        Assert.False(res.Success);
        Assert.Equal(ErrorCodes.MISSING_REQUIRED_INFORMATION, res.ErrorCode);
        Assert.Equal("Missing required fields", res.Message);
    }

    [Fact]
    public async Task Should_Return_Guest_Success()
    {
        var fakeRepo = new Mock<IGuestRepository>();

        var fakeGuest = new Guest
        {
            Id = 333,
            Name = "Test",
            DocumentId = new PersonId
            {
                DocumentType = DocumentType.DriverLicense,
                IdNumber = "123"
            }
        };

        fakeRepo.Setup(x => x.Get(333)).Returns(Task.FromResult<Guest>(fakeGuest));
        var guestManager = new GuestManager(fakeRepo.Object);

        var res = await guestManager.GetGuest(333);

        Assert.NotNull(res);
        Assert.True(res.Success);
    }

    [Fact]
    public async Task Should_Return_GuestNotFound_When_GuestDoesntExists()
    {
        var fakeRepo = new Mock<IGuestRepository>();

        fakeRepo.Setup(x => x.Get(333)).Returns(Task.FromResult<Guest?>(null));
        var guestManager = new GuestManager(fakeRepo.Object);

        var res = await guestManager.GetGuest(333);

        Assert.NotNull(res);
        Assert.False(res.Success);
        Assert.Equal(ErrorCodes.GUEST_NOT_FOUND, res.ErrorCode);
        Assert.Equal("Could not found guest with given Id", res.Message);
    }
}