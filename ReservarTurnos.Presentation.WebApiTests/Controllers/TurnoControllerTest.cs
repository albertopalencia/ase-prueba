using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ReservarTurnos.Application.Dtos;
using ReservarTurnos.Application.UseCases.Shift;
using Xunit;

namespace ReservarTurnos.Presentation.WebApi.Controllers.Tests
{
   
    public class TurnoControllerTest
    {
        private readonly Mock<ISender> SenderMock;
        private readonly Mock<IValidator<ShiftRequestDto>> ValidatorMock;
        private readonly ShiftController Controller;

        public TurnoControllerTest()
        {
            SenderMock = new Mock<ISender>();
            ValidatorMock = new Mock<IValidator<ShiftRequestDto>>();
            Controller = new ShiftController(SenderMock.Object, ValidatorMock.Object);
        }

        [Fact]
        public async Task GenerateShifts_ReturnsBadRequest_WhenValidationFails()
        {
            var request = new ShiftRequestDto { ServiceId = 1, StartDate = "invalid_date", EndDate = "invalid_date" };
            var validationResult = new ValidationResult(new List<ValidationFailure> { new("StartDate", "Invalid date format") });
            ValidatorMock.Setup(v => v.Validate(request)).Returns(validationResult);

        
            var result = await Controller.GenerateShifts(request, CancellationToken.None);
            
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, badRequestResult.StatusCode);
            Assert.NotNull(badRequestResult.Value);
        }

        [Fact]
        public async Task GenerateShifts_ReturnsOk_WhenValidationSucceeds()
        {
           
            var request = new ShiftRequestDto { ServiceId = 1, StartDate = "01/05/2024", EndDate = "02/05/2024" };
            var validationResult = new ValidationResult();
            ValidatorMock.Setup(v => v.Validate(request)).Returns(validationResult);

            var response = new { Message = "Success" };
            SenderMock.Setup(s => s.Send(It.IsAny<GetShiftQuery>(), It.IsAny<CancellationToken>())).Returns(It.IsAny<Task<List<ShiftDto>>>());
           
            var result = await Controller.GenerateShifts(request, CancellationToken.None);
       
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(response, okResult.Value);
        }
    }
}