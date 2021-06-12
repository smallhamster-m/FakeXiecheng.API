﻿using FakeXiecheng.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace FakeXiecheng.API.Database
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        //注入DbContextOptions的实例
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        //TouristRoutes复数显示命名，代表数据库名称
        public DbSet<TouristRoute> TouristRoutes { get; set; }
        public DbSet<TouristRoutePic> TouristRoutePics { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<LineItem> LineItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region 添加种子数据
            //modelBuilder.Entity<TouristRoute>().HasData(new TouristRoute()
            //{
            //    Id = Guid.NewGuid(),
            //    Title = "标题 ",
            //    Description ="说明",
            //    OriginalPrice=0,
            //    CreateTime = DateTime.Now
            //});
            #endregion
            #region 添加数据库种子数据
            //添加种子数据
            var touristRouteJsonData = File.ReadAllText(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"/Database/touristRoutesMockData.json");
            IList<TouristRoute> touristRoutes = JsonConvert.DeserializeObject<IList<TouristRoute>>(touristRouteJsonData);
            modelBuilder.Entity<TouristRoute>().HasData(touristRoutes);

            var touristRoutePictureJsonData = File.ReadAllText(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"/Database/touristRoutePicturesMockData.json");
            IList<TouristRoutePic> touristRoutePictures = JsonConvert.DeserializeObject<IList<TouristRoutePic>>(touristRoutePictureJsonData);
            modelBuilder.Entity<TouristRoutePic>().HasData(touristRoutePictures);
            #endregion

            // 初始化用户与角色的种子数据
            // 1. 更新用户与角色的外键关系
            modelBuilder.Entity<ApplicationUser>(u =>
            {
                u.HasMany(x => x.UserRoles)
                .WithOne()
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();
            });

            // 2. 添加角色
            var adminRoleId = "308660dc-ae51-480f-824d-7dca6714c3e2"; // guid 
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = adminRoleId,
                    Name = "Admin",
                    NormalizedName = "Admin".ToUpper()
                }
            );

            // 3. 添加用户
            var adminUserId = "90184155-dee0-40c9-bb1e-b5ed07afc04e";
            ApplicationUser adminUser = new ApplicationUser
            {
                Id = adminUserId,
                UserName = "admin@fakexiecheng.com",
                NormalizedUserName = "admin@fakexiecheng.com".ToUpper(),
                Email = "admin@fakexiecheng.com",
                NormalizedEmail = "admin@fakexiecheng.com".ToUpper(),
                TwoFactorEnabled = false,
                EmailConfirmed = true,
                PhoneNumber = "123456789",
                PhoneNumberConfirmed = false
            };
            PasswordHasher<ApplicationUser> ph = new PasswordHasher<ApplicationUser>();
            adminUser.PasswordHash = ph.HashPassword(adminUser, "Fake123$");
            modelBuilder.Entity<ApplicationUser>().HasData(adminUser);

            // 4. 给用户加入管理员权限
            // 通过使用 linking table：IdentityUserRole
            modelBuilder.Entity<IdentityUserRole<string>>()
                .HasData(new IdentityUserRole<string>()
                {
                    RoleId = adminRoleId,
                    UserId = adminUserId
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}
