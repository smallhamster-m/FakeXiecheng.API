//using FakeXiecheng.API.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace FakeXiecheng.API.Services
//{
//    public class MockTouristRouteRepository : ITouristRouteRepository
//    {
//        private List<TouristRoute> _routes;
//        public MockTouristRouteRepository()
//        {
//            if (_routes == null)
//            {
//                InitialTouristRoute();
//            }
//        }

//        private void InitialTouristRoute()
//        {
//            _routes = new List<TouristRoute> {
//                new TouristRoute {
//                    Id = Guid.NewGuid(),
//                    Title = "黄山",
//                    Description="黄山真好玩",
//                    OriginalPrice = 1299,
//                    Features = "<p>吃住行游购娱</p>",
//                    Fees = "<p>交通费用自理</p>",
//                    Notes="<p>小心危险</p>"
//                },
//                new TouristRoute {
//                    Id = Guid.NewGuid(),
//                    Title = "华山",
//                    Description="华山真好玩",
//                    OriginalPrice = 1299,
//                    Features = "<p>吃住行游购娱</p>",
//                    Fees = "<p>交通费用自理</p>",
//                    Notes="<p>小心危险</p>"
//                 }
//            };
//        }

//        /// <summary>
//        /// 获取所有的旅游路线
//        /// </summary>
//        /// <returns>返回所有旅游路线</returns>
//        public TouristRoute GetTouristRoute(Guid id)
//        {
//            return _routes.FirstOrDefault(route => route.Id == id);
//        }

//        /// <summary>
//        /// 根据id获取指定的旅游路线
//        /// </summary>
//        /// <param name="id">要获取旅游路线的id</param>
//        /// <returns>返回指定旅游路线</returns>
//        public IEnumerable<TouristRoute> GetTouristRoutes()
//        {
//            return _routes;
//        }

//        public IEnumerable<TouristRoutePic> GetTouristRoutePicsById(Guid id)
//        {
//            throw new NotImplementedException();
//        }

//        public bool TouristRouteExists(Guid id)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
