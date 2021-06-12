using FakeXiecheng.API.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FakeXiecheng.API.ValidationAttributes
{
    public class TouristRouteAttribute :ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var touristRouteDto = (TouristRouteManipulationDto)validationContext.ObjectInstance;
            if (touristRouteDto.Title == touristRouteDto.Description) {
                return new ValidationResult("标题和描述不能一样哦", new[] { "TouristRouteManipulationDto" });
            }
            return ValidationResult.Success;
        }
    }
}
