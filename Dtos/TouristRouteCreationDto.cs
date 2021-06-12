using FakeXiecheng.API.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FakeXiecheng.API.Dtos
{
    public class TouristRouteCreationDto:TouristRouteManipulationDto //: IValidatableObject
    {
        ////自定义数据验证
        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    if (Title == Description) {
        //        yield return new ValidationResult("标题和描述不能一样",new[] { "TouristRouteCreationDto" });
        //    }
        //}
    }
}
