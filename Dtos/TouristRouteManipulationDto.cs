using FakeXiecheng.API.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FakeXiecheng.API.Dtos
{
    [TouristRoute] //自定义数据验证 
    public abstract class TouristRouteManipulationDto
    {

        [Required(ErrorMessage = "Title是必须的")]
        [MaxLength(100)]
        public string Title { get; set; }
        [Required]
        [MaxLength(1500)]
        public virtual string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public DateTime? DepartureTime { get; set; }
        public string Features { get; set; }
        public string Fees { get; set; }
        public string Notes { get; set; }
        public double? Rating { get; set; }//评分
        public string TravelDays { get; set; }//旅游天数
        public string TripType { get; set; }//旅游方式
        public string DepartureCity { get; set; }//出发城市
        public ICollection<TouristRoutePicCreationDto> TouristRoutePics { get; set; } = new List<TouristRoutePicCreationDto>();
    }
}
