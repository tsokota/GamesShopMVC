using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.IO.Pipes;
using System.Web.UI;
using AutoMapper;
using Model.Entities;
using Yevhenii_KoliesnikTask1.WebApi.ApiDTO;
namespace Yevhenii_KoliesnikTask1.Mappers
{
    public class MainMapper
    {

        public static void MainMappers()
		{

            #region API

            Mapper.CreateMap<Game, GameDTO>();

            Mapper.CreateMap<Game, GameDTOeasy>();

            Mapper.CreateMap<Publisher, PublisherDTO>();

            Mapper.CreateMap<Genre, GenreDTO>();

            Mapper.CreateMap<Comment, CommentDTO>()
                .ForMember(a => a.GameKey, opt => opt.MapFrom(src => src.Game.Key));
            
            Mapper.CreateMap<Platform, PlatformDTO>();

            Mapper.CreateMap<Model.Entities.Order, OrderDTO>();

            Mapper.CreateMap<OrderDetail, OrderDetailsDTO>();
            #endregion
            Mapper.CreateMap<List<Genre>, List<Genre>>();
		}

		public static object Map(object source, Type sourceType, Type destinationType)
		{
			return Mapper.Map(source, sourceType, destinationType);
		}
    }
}