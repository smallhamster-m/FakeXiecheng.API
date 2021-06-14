using FakeXiecheng.API.Database;
using FakeXiecheng.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FakeXiecheng.API.Services
{
    public class TouristRouteRepository : ITouristRouteRepository
    {
        private readonly AppDbContext _context;
        public TouristRouteRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<TouristRoute> GetTouristRouteAsync(Guid id)
        {
            return await _context.TouristRoutes.Include(item => item.TouristRoutePics).FirstOrDefaultAsync(n => n.Id == id);
        }

        public async Task<IEnumerable<TouristRoute>> GetTouristRoutesAsync(string keyword, string operatorType, int? ratingValue, int pageNumber, int pageSize)
        {
            IQueryable<TouristRoute> res = _context.TouristRoutes.Include(item => item.TouristRoutePics);
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                keyword = keyword.Trim();
                res = res.Where(t => t.Title.Contains(keyword));
            }
            if (ratingValue >= 0)
            { //根据评分筛选
                res = operatorType switch
                {
                    "largerThan" => res.Where(t => t.Rating >= ratingValue),
                    "lessThan" => res.Where(t => t.Rating <= ratingValue),
                    _ => res.Where(t => t.Rating == ratingValue),
                };
            }
            //分页查询
            var skip = (pageNumber - 1) * pageSize;//跳过前n个元素
            res = res.Skip(skip);
            res = res.Take(pageSize);

            return await res.ToListAsync();
        }

        public async Task<bool> TouristRouteExistsAsync(Guid id)
        {
            return await _context.TouristRoutes.AnyAsync(item => item.Id == id);
        }

        public async Task<TouristRoutePic> GetPicAsync(Guid TouristRouteId, int picId)
        {
            //return _context.TouristRoutePics.Where(item => item.Id == id).FirstOrDefault();
            return await _context.TouristRoutePics.Where(item => item.TouristRouteId == TouristRouteId).Where(item => item.Id == picId).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<TouristRoutePic>> GetTouristRoutePicsByIdAsync(Guid id)
        {
            return await _context.TouristRoutePics.Where(item => item.TouristRouteId == id).ToListAsync();
        }

        public void AddTouristRoute(TouristRoute touristRoute)
        {
            if (touristRoute == null)
            {
                throw new ArgumentNullException(nameof(touristRoute));
            }
            _context.TouristRoutes.Add(touristRoute);
        }

        public async Task<bool> SaveAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }

        public void AddTouristRoutePic(Guid id, TouristRoutePic touristRoutePic)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(id));
            }
            if (touristRoutePic == null)
            {
                throw new ArgumentNullException(nameof(touristRoutePic));
            }
            touristRoutePic.TouristRouteId = id;
            _context.TouristRoutePics.Add(touristRoutePic);
        }

        public void DeleteTouristRoute(TouristRoute touristRoute)
        {
            _context.TouristRoutes.Remove(touristRoute);
        }

        public void DeleteTouristRoutes(IEnumerable<TouristRoute> touristRoutes) {
            _context.TouristRoutes.RemoveRange(touristRoutes);
        }

        public async Task<IEnumerable<TouristRoute>> GetTouristRoutesByIDListAsync(IEnumerable<Guid> guids) {
            return await _context.TouristRoutes.Where(item => guids.Contains(item.Id)).ToListAsync();
        }

        public void DeleteTouristRoutePic(TouristRoutePic touristRoutePic)
        {
            _context.TouristRoutePics.Remove(touristRoutePic);
        }

        public async Task<ShoppingCart> GetShoppingCartByUserIdAsync(string userId)
        {
            return await _context.ShoppingCarts.Include(s => s.User).Include(s => s.ShoppingCartItems).ThenInclude(item => item.TouristRoute).Where(s => s.UserId == userId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<LineItem>> GetShoppingCartItemsByItemIdsAsync(IEnumerable<int> lineItemIds) {
            return await _context.LineItems.Where(item => lineItemIds.Contains(item.Id)).ToListAsync();
        }

        public async Task CreateShoppingCartAsync(ShoppingCart shoppingCart) {
            await _context.ShoppingCarts.AddAsync(shoppingCart);
        }

        public async Task AddShoppingCartItemAsync(LineItem lineItem) {
            await _context.LineItems.AddAsync(lineItem);
        }

        public async Task<LineItem> GetShoppingCartItemByItemIdAsync(int lineItemId){
            return await _context.LineItems.Where(li => li.Id == lineItemId).FirstOrDefaultAsync();
        }

        public void DeleteShoppingCartItem(LineItem lineItem) {
             _context.LineItems.Remove(lineItem);
        }

        public void DeleteShoppingCartItems(IEnumerable<LineItem> lineItems) {
            _context.LineItems.RemoveRange(lineItems);
        }

        public async Task AddOrderAsync(Order order) {
           await _context.Orders.AddAsync(order);
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync(string userId) {
          return  await _context.Orders.Where(o => o.UserId == userId).Include(o => o.OrderItems).ThenInclude(li => li.TouristRoute).ToListAsync();
        }

        public async Task<Order> GetOrderByIdAsync(Guid guid) {
            return await _context.Orders.Where(o => o.Id == guid).Include(o => o.OrderItems).ThenInclude(li => li.TouristRoute).FirstOrDefaultAsync();
        }
    }
}
