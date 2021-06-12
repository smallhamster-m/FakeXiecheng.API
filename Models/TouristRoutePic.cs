using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FakeXiecheng.API.Models
{
    public class TouristRoutePic
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]//数据库生成，自增
        public int Id { get; set; }
        [MaxLength(100)]
        public string Url { get; set; }
        //关联外键
        [ForeignKey("TouristRouteId")]
        public Guid TouristRouteId { get; set; }
        public TouristRoute TouristRoute { get; set; }

    }
}
