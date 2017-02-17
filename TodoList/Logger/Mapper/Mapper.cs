using System.Collections.Generic;
using AutoMapper;
using AutoMapper.Configuration;

namespace Common.Mapper
{
    public class Mapper : Common.Mapper.IMapper
    {
        private readonly AutoMapper.IMapper autoMapper;

        public Mapper(MapperConfigurationExpression config)
        {
            MapperConfiguration mapperConfig = new MapperConfiguration(config);
            autoMapper = mapperConfig.CreateMapper();
        }

        public IEnumerable<TTo> Map<TFrom, TTo>(IEnumerable<TFrom> collectionFrom)
            where TFrom : class
            where TTo : class
        {
            IEnumerable<TTo> result = autoMapper.Map<IEnumerable<TTo>>(collectionFrom);
            return result;
        }

        public TTo Map<TFrom, TTo>(TFrom from)
            where TFrom : class
            where TTo : class
        {
            TTo result = autoMapper.Map<TTo>(from);
            return result;
        }
    }
}
