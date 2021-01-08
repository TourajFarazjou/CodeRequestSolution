using AutoMapper;
using Calculator.Domain;
using Calculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Calculator.Api.Domain.MappingProfiles
{
   public class CalculatorJobProfile : Profile
   {

      public CalculatorJobProfile()
      {
         //CreateMap<CalculatorJob, CalculationRequest>();
         //.ForMember(dest => dest.FirstValue,
         //           opt => opt.MapFrom(src => src.FirstValue));


         CreateMap<CalculationRequest, CalculatorJob>();

         CreateMap<CalculatorJob, CalculationResponse>();


      }

   }
}
