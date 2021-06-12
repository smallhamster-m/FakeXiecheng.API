using AutoMapper;
using FakeXiecheng.API.Dtos;
using FakeXiecheng.API.Models;
using FakeXiecheng.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FakeXiecheng.API.Controllers
{
    [Route("api/TouristRoutes/{id}/pictures")]
    [ApiController]
    public class TouristRoutePicsController : Controller
    {
        public readonly ITouristRouteRepository _touristRouteRepository;
        public readonly IMapper _mapper;
        public TouristRoutePicsController(ITouristRouteRepository touristRouteRepository, IMapper mapper)
        {
            _touristRouteRepository = touristRouteRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// 获取指定旅游路线的所有图片
        /// </summary>
        /// <param name="id">旅游路线的 id</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetTouristRoutePic(Guid id)
        {
            if (!(await _touristRouteRepository.TouristRouteExistsAsync(id)))
            {
                return NotFound("旅游路线不存在");
            }
            var pics =await _touristRouteRepository.GetTouristRoutePicsByIdAsync(id);
            if (pics == null || pics.Count() <= 0)
            {
                return NotFound("图片不存在");
            }
            var picsDto = _mapper.Map<IEnumerable<TouristRoutePicDto>>(pics);
            return Ok(picsDto);

        }
        /// <summary>
        ///  获取指定旅游路线的指定图片
        /// </summary>
        /// <param name="id">旅游路线的 id</param>
        /// <param name="picId">图片id</param>
        /// <returns></returns>
        [HttpGet("{picId}",Name = "GetPic")]
        public async Task<IActionResult> GetPic(Guid id, int picId)
        {
            if (!(await _touristRouteRepository.TouristRouteExistsAsync(id)))
            {
                return NotFound("旅游路线不存在");
            }
            var pic = await _touristRouteRepository.GetPicAsync(id, picId);
            if (pic == null)
            {
                return NotFound("该旅游路线的图片不存在");
            }
            var picDto = _mapper.Map<TouristRoutePicDto>(pic);
            return Ok(picDto);
        }

        /// <summary>
        /// 给指定旅游路线创建图片资源
        /// </summary>
        /// <param name="id">旅游路线id</param>
        /// <param name="touristRoutePicCreationDto">图片资源对象</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Authorize(AuthenticationSchemes = "Bearer")]

        public async Task<IActionResult> CreateTouristRoutePic([FromRoute]Guid id,[FromBody] TouristRoutePicCreationDto touristRoutePicCreationDto) {
            if (!(await _touristRouteRepository.TouristRouteExistsAsync(id)))
            {
                return NotFound("旅游路线不存在");
            }
            var picModel =  _mapper.Map<TouristRoutePic>(touristRoutePicCreationDto);
            _touristRouteRepository.AddTouristRoutePic(id, picModel);
            await _touristRouteRepository.SaveAsync();

            var picModelReture = _mapper.Map<TouristRoutePicDto>(picModel);
            return CreatedAtRoute("GetPic", new { id = picModelReture.TouristRouteId, picId= picModelReture.Id }, picModelReture);
        }

        [HttpDelete("{picId}")]
        [Authorize(Roles = "Admin")]
        [Authorize(AuthenticationSchemes = "Bearer")]

        public async Task<IActionResult> DeletePic([FromRoute] Guid id, [FromRoute] int picId)
        {
            if (!(await _touristRouteRepository.TouristRouteExistsAsync(id)))
            {
                return NotFound("旅游路线不存在");
            }
           var res =await _touristRouteRepository.GetPicAsync(id, picId);
            _touristRouteRepository.DeleteTouristRoutePic(res);
            await _touristRouteRepository.SaveAsync();

            return NoContent();

        }
    }
}
