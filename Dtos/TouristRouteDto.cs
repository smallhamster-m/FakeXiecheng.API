using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FakeXiecheng.API.Dtos
{
    public class TouristRouteDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        //public decimal OriginalPrice { get; set; }
        //public double? Discount { get; set; }
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
        public ICollection<TouristRoutePicDto> TouristRoutePics { get; set; }
    }
}
