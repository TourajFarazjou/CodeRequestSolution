using AutoMapper;
using Calculator.Controllers;
using Calculator.Domain;
using Calculator.Models;
using Calculator.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace Calculator.Api.Tests
{
	public class CalculatorControllerTests
	{
		private readonly CalculatorController calculatorController;
		private readonly Mock<ICalculatorService> calculatorServiceMock;
		private readonly Mock<IMapper> mapperMock;

		public CalculatorControllerTests()
		{
			mapperMock = new Mock<IMapper>();
			calculatorServiceMock = new Mock<ICalculatorService>();
			calculatorController = new CalculatorController(calculatorServiceMock.Object, mapperMock.Object);
		}

		[Fact]
		public async void GetCalculations_ShouldReturnOk_WhenExistCalculatorJobs()
		{
			// Arrange
			var calculatorJobs = CreateCalculatorJobList();
			var calculationResponseExpected = MapModelToCalculationResponseList(calculatorJobs);
			calculatorServiceMock.Setup(c => c.GetCalculations()).ReturnsAsync(calculatorJobs);
			mapperMock.Setup(m => m.Map<IEnumerable<CalculationResponse>>(calculatorJobs))
				.Returns(calculationResponseExpected);

			// Act
			var actionResult = await calculatorController.GetCalculations();
			calculatorServiceMock.Verify(mock => mock.GetCalculations(), Times.Once);
			var objectResult = Assert.IsType<OkObjectResult>(actionResult);
			var model = Assert.IsAssignableFrom<IEnumerable<CalculationResponse>>(objectResult.Value);

			// Assert
			Assert.Equal(calculationResponseExpected, model);
		}

		[Fact]
		public async void GetCalculation_ShouldReturnOk_WhenCalculatorJobExist()
		{
			// Arrange
			var calculatorJob = CreateCalculatorJob();
			var calculationResponseExpected = MapModelToCalculationResponse(calculatorJob);

			calculatorServiceMock.Setup(c => c.GetCalculation(calculatorJob.Id))
				.ReturnsAsync(calculatorJob);
			mapperMock.Setup(m => m.Map<CalculationResponse>(calculatorJob))
				.Returns(calculationResponseExpected);

			// Act
			var actionResult = await calculatorController.GetStatus(calculatorJob.Id);
			calculatorServiceMock.Verify(mock => mock.GetCalculation(calculatorJob.Id), Times.Once);
			var objectResult = Assert.IsType<OkObjectResult>(actionResult);
			var model = Assert.IsAssignableFrom<CalculationResponse>(objectResult.Value);

			// Assert
			Assert.Equal(calculationResponseExpected, model);
		}

		[Fact]
		public async void GetCalculation_ShouldReturnNotFound_WhenCalculatorJobDoesNotExist()
		{
			// Arrange
			var id = Guid.NewGuid();
			calculatorServiceMock.Setup(c => c.GetCalculation(id)).ReturnsAsync((CalculatorJob)null);

			// Act
			var actionResult = await calculatorController.GetStatus(id);

			// Assert
			Assert.IsType<NotFoundResult>(actionResult);
		}

		private CalculatorJob CreateCalculatorJob()
		{
			return new CalculatorJob
			{
				Id = Guid.Parse("94a92236-8afd-444f-a4a5-84c684615a14"),
				Status = CalculationStatus.Completed,
				Progress = 100,
				Outcome = 412703,
				FirstValue = 235,
				SecondValue = 967
			};
		}

		private IEnumerable<CalculatorJob> CreateCalculatorJobList()
		{
			return new List<CalculatorJob>
			{
				new CalculatorJob 
				{ 
					Id = Guid.Parse("94a92236-8afd-444f-a4a5-84c684615a14"), 
					Status = CalculationStatus.Completed, 
					Progress = 100,
					Outcome = 412703,
					FirstValue = 235,
					SecondValue = 967
				},
				new CalculatorJob
				{
					Id = Guid.Parse("3bd1c01b-a939-42c1-88cc-e86f30d042ef"),
					Status = CalculationStatus.Running,
					Progress = 19,
					Outcome = 0,
					FirstValue = 967,
					SecondValue = 1344
				},
				new CalculatorJob
				{
					Id = Guid.Parse("9579ce04-9170-48bd-878a-a83817f93766"),
					Status = CalculationStatus.Failed,
					Progress = 74,
					Outcome = 0,
					FirstValue = 85,
					SecondValue = 844
				},
			};
		}

		private CalculationResponse MapModelToCalculationResponse(CalculatorJob calculatorJob)
		{
			return new CalculationResponse
			{
				Id = calculatorJob.Id,
				Status = calculatorJob.Status,
				Progress = calculatorJob.Progress,
				Outcome = calculatorJob.Outcome
			};
		}

		private IEnumerable<CalculationResponse> MapModelToCalculationResponseList(IEnumerable<CalculatorJob> calculatorJobs)
		{
			List<CalculationResponse> response = new List<CalculationResponse>();
			foreach (var item in calculatorJobs)
			{
				response.Add(MapModelToCalculationResponse(item));
			}
			return response;
		}
	}
}
