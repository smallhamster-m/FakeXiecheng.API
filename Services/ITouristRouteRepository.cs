using FakeXiecheng.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FakeXiecheng.API.Services
{
    /// <summary>
    /// 接口
    /// </summary>
    public interface ITouristRouteRepository
    {
        /// <summary>
        /// 筛选旅游路线
        /// </summary>
        /// <param name="keyword">标题关键字</param>
        /// <param name="operatorType">大于，小于，等于</param>
        /// <param name="ratingValue">分数</param>
        /// <returns></returns>
        Task<IEnumerable<TouristRoute>> GetTouristRoutesAsync(string keyword, string operatorType, int? ratingValue,int pageNumber,int  pageSize);

        Task<IEnumerable<TouristRoute>> GetTouristRoutesByIDListAsync(IEnumerable<Guid> guids);


        /// <summary>
        /// 根据id获取指定的旅游路线
        /// </summary>
        /// <param name="id">要获取旅游路线的id</param>
        /// <returns></returns>
        Task<TouristRoute> GetTouristRouteAsync(Guid id);
        /// <summary>
        /// 根据id获取旅游路线的所有图片
        /// </summary>
        /// <param name="id">旅游路线的id</param>
        /// <returns>返回指定旅游路线的所有图片</returns>
        Task<IEnumerable<TouristRoutePic>> GetTouristRoutePicsByIdAsync(Guid id);



        /// <summary>
        /// 根据id判断旅游路线是否存在
        /// </summary>
        /// <param name="id">旅游路线id</param>
        /// <returns>返回 存在/不存在</returns>
        Task<bool> TouristRouteExistsAsync(Guid id);

        /// <summary>
        /// 根据id获取旅游路线的指定图片
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TouristRoutePic> GetPicAsync(Guid TouristRouteId, int picId);

        /// <summary>
        /// 添加旅游路线
        /// </summary>
        /// <param name="touristRoute">旅游路线对象</param>
        void AddTouristRoute(TouristRoute touristRoute);

        /// <summary>
        /// 数据库保存数据的操作
        /// </summary>
        /// <returns></returns>
        Task<bool> SaveAsync();

        /// <summary>
        /// 添加旅游路线的图片
        /// </summary>
        void AddTouristRoutePic(Guid id, TouristRoutePic touristRoutePic);

        /// <summary>
        /// 删除单条旅游路线
        /// </summary>
        /// <param name="touristRoute"></param>
        void DeleteTouristRoute(TouristRoute touristRoute);

        /// <summary>
        /// 批量删除旅游路线
        /// </summary>
        /// <param name="touristRoutes"></param>
        void DeleteTouristRoutes(IEnumerable<TouristRoute> touristRoutes);
        void DeleteTouristRoutePic(TouristRoutePic touristRoutePic);

        /// <summary>
        /// 根据用户id获取购物车
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<ShoppingCart> GetShoppingCartByUserIdAsync(string userId);

        /// <summary>
        /// 创建购物车
        /// </summary>
        /// <param name="shoppingCart"></param>
        /// <returns></returns>
        Task CreateShoppingCartAsync(ShoppingCart shoppingCart);

        /// <summary>
        /// 给购物车添加商品
        /// </summary>
        /// <param name="lineItem"></param>
        /// <returns></returns>
        Task AddShoppingCartItemAsync(LineItem lineItem);

        /// <summary>
        /// 获取购物车的某个商品
        /// </summary>
        /// <param name="lineItemId">商品id</param>
        /// <returns></returns>
        Task<LineItem> GetShoppingCartItemByItemIdAsync(int lineItemId);

        /// <summary>
        /// 批量获取购物车商品
        /// </summary>
        /// <param name="lineItemIds">多个商品ids</param>
        /// <returns></returns>
        Task<IEnumerable<LineItem>> GetShoppingCartItemsByItemIdsAsync(IEnumerable<int> lineItemIds);

        /// <summary>
        /// 删除商品
        /// </summary>
        /// <param name="lineItem"></param>
        /// <returns></returns>
        void DeleteShoppingCartItem(LineItem lineItem);

        /// <summary>
        /// 批量删除商品
        /// </summary>
        /// <param name="lineItems"></param>
        /// <returns></returns>
        void DeleteShoppingCartItems(IEnumerable<LineItem> lineItems);

        /// <summary>
        /// 添加订单
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        Task AddOrderAsync(Order order);

        /// <summary>
        /// 获取用户所有的订单
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <returns></returns>
        Task<IEnumerable<Order>> GetOrdersAsync(string userId);

        Task<Order> GetOrderByIdAsync(Guid guid);
    }
}
