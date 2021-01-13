using AutoMapper;
using Calculator.Domain;
using Calculator.Models;

namespace Calculator.Api.Domain.MappingProfiles
{
	public class CalculatorJobProfile : Profile
   {

      public CalculatorJobProfile()
      {
         CreateMap<CalculatorJob, CalculationResponse>();
		}
   }
}
