using FakeXiecheng.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FakeXiecheng.API.Dtos;
using System.Text.RegularExpressions;
using FakeXiecheng.API.ResourceParameters;
using FakeXiecheng.API.Models;
using Microsoft.AspNetCore.Authorization;
using FakeXiecheng.API.Helper;

namespace FakeXiecheng.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]//具有api行为的控制器
    public class TouristRoutesController : ControllerBase
    {
        private readonly ITouristRouteRepository _touristRouteRepository;
        private readonly IMapper _mapper;
        public TouristRoutesController(ITouristRouteRepository touristRouteRepository, IMapper mapper)
        {
            _touristRouteRepository = touristRouteRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// 根据指定条件获取旅游路线
        /// </summary>
        /// <param name="paramaters">参数对象:关键字,评分,</param>
        /// <returns></returns>
        [HttpGet]
        [HttpHead]
        public async Task<IActionResult> GetTouristRoutes([FromQuery] TouristRouteParams paramaters)
        {
            var touristRoutesFromRepo = await _touristRouteRepository.GetTouristRoutesAsync(paramaters.Keyword, paramaters.OperatorType, paramaters.RatingValue, paramaters.PageNumber,paramaters.PageSize);
            var touristRoutesDto = _mapper.Map<IEnumerable<TouristRouteDto>>(touristRoutesFromRepo);

            if (touristRoutesFromRepo == null || touristRoutesFromRepo.Count() <= 0)
            {
                return NotFound("找不到旅游路线");
            };
            return Ok(touristRoutesDto);

        }

        /// <summary>
        /// 根据id获取旅游路线
        /// </summary>
        /// <param name="id">旅游路线的id</param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetTouristRoute")]
        [HttpHead]
        public async Task<IActionResult> GetTouristRoute(Guid id)
        {
            var touristRouteFromRepo = await _touristRouteRepository.GetTouristRouteAsync(id);
            var touristRouteDto = _mapper.Map<TouristRouteDto>(touristRouteFromRepo);
            //return Json(res);
            if (touristRouteFromRepo == null)
            {
                return NotFound($"旅游路线{id}找不到");
            };
            return Ok(touristRouteDto);
        }

        /// <summary>
        /// 创建旅游路线
        /// </summary>
        /// <param name="touristRouteCreationDto">旅游路线对象</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Authorize(AuthenticationSchemes ="Bearer")]//Identity验证方式
        public async Task<IActionResult> CreateTouristRoute([FromBody] TouristRouteCreationDto touristRouteCreationDto)
        {
            //把TouristRouteCreationDto映射为TouristRoute（前端-->后端）
            var touristRouteModel = _mapper.Map<TouristRoute>(touristRouteCreationDto);
            _touristRouteRepository.AddTouristRoute(touristRouteModel);
            await _touristRouteRepository.SaveAsync();

            //把TouristRoute映射为TouristRouteDto（（后端-->前端））
            var touristRouteModelReture = _mapper.Map<TouristRouteDto>(touristRouteModel);
            return CreatedAtRoute("GetTouristRoute", new { id = touristRouteModelReture.Id }, touristRouteModelReture);
            //return Ok(touristRouteModelReture);
        }

        /// <summary>
        /// 更新旅游路线(PUT)
        /// </summary>
        /// <param name="id">需要更新的旅游路线的id</param>
        /// <param name="touristRouteUpdataDto">更新的数据对象</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> UpdataTouristRoute([FromRoute] Guid id, [FromBody] TouristRouteUpdataDto touristRouteUpdataDto)
        {
            if (!(await _touristRouteRepository.TouristRouteExistsAsync(id)))
            {
                return NotFound("旅游路线不存在");
            }
            //执行从源对象到现有目标对象的映射
            var touristRoute = await _touristRouteRepository.GetTouristRouteAsync(id);
            _mapper.Map(touristRouteUpdataDto, touristRoute);

            await _touristRouteRepository.SaveAsync();
            return NoContent();
        }

        /// <summary>
        /// 根据id删除旅游路线
        /// </summary>
        /// <param name="id">要删除的旅游路线的id</param>
        /// <param name="touristRoute">旅游路线对象</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> DeleteTouristRoute([FromRoute] Guid id)
        {
            if (!(await _touristRouteRepository.TouristRouteExistsAsync(id)))
            {
                return NotFound("旅游路线不存在");
            }
            var res = await _touristRouteRepository.GetTouristRouteAsync(id);
            _touristRouteRepository.DeleteTouristRoute(res);
            await _touristRouteRepository.SaveAsync();

            return NoContent();
        }


        /// <summary>
        /// 根据ids批量删除旅游路线
        /// </summary>
        /// <param name="ids">要批量删除的旅游路线的id</param>
        /// <returns></returns>
        [HttpDelete("({ids})")]
        public async Task<IActionResult> DeleteByIDs([ModelBinder(BinderType = typeof(ArrayModelBinder))][FromRoute] IEnumerable<Guid> ids) {
            if (ids == null) {
                return BadRequest();
            }
            var touristRoutes =await _touristRouteRepository.GetTouristRoutesByIDListAsync(ids);
            _touristRouteRepository.DeleteTouristRoutes(touristRoutes);
           await _touristRouteRepository.SaveAsync();
            return NoContent();
        }
    }
}
