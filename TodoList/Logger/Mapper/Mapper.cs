using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Mapper;
using AutoMapper;

namespace Logger.Mapper
{
    public class Mapper : Infrastructure.Mapper.IMapper
    {
        private readonly AutoMapper.Mapper autoMapper;

        public Mapper()
        {
            //autoMapper = AutoMapper.Mapper       
        }

        public IEnumerable<TTo> Map<TFrom, TTo>(IEnumerable<TFrom> collectionFrom)
            where TFrom : class
            where TTo : class
        {
            throw new NotImplementedException();
        }

        public TTo Map<TFrom, TTo>(TFrom from)
            where TFrom : class
            where TTo : class
        {
            throw new NotImplementedException();
        }
    }
}
