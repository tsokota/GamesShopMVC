using System.Diagnostics;
using AutoMapper;
using BusinessLogicLayer.ViewModel;
using DAL;
using Model;
using Model.Entities;
using System;
using System.Collections.Generic;

namespace BusinessLogicLayer.Services
{
    public static class AutomapperConfig
    {
        public static void ConfigurationMapper()
        {
            Mapper.CreateMap<User, UserView>()
                   .ForMember(dest => dest.BirthdateDate, opt => opt.MapFrom(src => src.Birthdate));

            Mapper.CreateMap<UserView, User>()
                    .ForMember(dest => dest.Birthdate, opt => opt.MapFrom(src => src.BirthdateDate));


            Mapper.CreateMap<Product, Game>()
                   .ForMember(dest => dest.Picture, opt => opt.UseValue("100x100.gif"))
                   .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ProductID))
                   .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ProductName))
                   .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.UnitPrice))
                   .ForMember(dest => dest.Description, opt => opt.UseValue("No Description"))
                   .ForMember(dest => dest.GameProduction, opt => opt.UseValue(default(DateTime)))
                   .ForMember(dest => dest.Key, opt => opt.MapFrom(src => SettingsConst.Prefix + src.ProductID.ToString()))
                   .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => new List<Category> { src.Category }))
                   .ForMember(dest => dest.Publisher, opt => opt.MapFrom(src => src.Supplier))
                   .ForMember(dest => dest.Comments, opt => opt.UseValue(new List<Comment>()))
                   .ForMember(dest => dest.Platforms, opt => opt.UseValue(new List<Platform> { new Platform { Name = "No information" } }))
                   .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.ProductName.Contains(SettingsConst.DeletePostfix)));



            Mapper.CreateMap<Supplier, Publisher>()
               .ForMember(dest => dest.Id, opt => opt.UseValue(0))
                .ForMember(dest => dest.NorthWindId, opt => opt.MapFrom(src => src.SupplierID))
               .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.CompanyName))
               .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.ContactTitle))
               .ForMember(dest => dest.HomePage, opt => opt.MapFrom(src => src.HomePage == null ? "No homepage" : src.HomePage))
               .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.CompanyName.Contains(SettingsConst.DeletePostfix)));

            Mapper.CreateMap<Category, Genre>()
                .ForMember(dest => dest.Id, opt => opt.UseValue(0))
                .ForMember(dest => dest.NorthWindId, opt => opt.MapFrom(src => src.CategoryID))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.CategoryName))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.Description.Contains(SettingsConst.DeletePostfix)));

            Mapper.CreateMap<Genre, Category>()
                .ForMember(dest => dest.CategoryID, opt => opt.UseValue(0))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Name));


            Mapper.CreateMap<Publisher, PublisherEditModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.CompanyName))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.HomePage, opt => opt.MapFrom(src => src.HomePage == null ? "No homepage" : src.HomePage));

            Mapper.CreateMap<PublisherEditModel, Publisher>()
              .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
              .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.CompanyName))
              .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
              .ForMember(dest => dest.HomePage, opt => opt.MapFrom(src => src.HomePage == null ? "No homepage" : src.HomePage));

            Mapper.CreateMap<Model.Entities.Order, OrderViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.IsDeletable, opt => opt.MapFrom(src => src.IsDeleted))
                .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => src.OrderDate))
                .ForMember(dest => dest.OrderStatus, opt => opt.MapFrom(src => src.OrderStatus))
                .ForMember(dest => dest.ShippedDate, opt => opt.MapFrom(src => src.ShippedDate))
                .ForMember(dest => dest.OrderDetails, opt => opt.MapFrom(src => new List<OrderDetail>(src.OrderDetails)));

            Mapper.CreateMap<OrderViewModel, Model.Entities.Order>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.IsDeletable))
                .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => src.OrderDate))
                .ForMember(dest => dest.OrderStatus, opt => opt.MapFrom(src => src.OrderStatus))
                .ForMember(dest => dest.ShippedDate, opt => opt.MapFrom(src => src.ShippedDate))
                .ForMember(dest => dest.OrderDetails, opt => opt.MapFrom(src => new List<OrderDetail>()));



        }
    }
}
