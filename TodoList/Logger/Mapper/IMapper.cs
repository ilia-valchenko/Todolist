using System.Collections.Generic;

namespace Common.Mapper
{
    public interface IMapper
    {
        TTo Map<TFrom, TTo>(TFrom from) where TFrom : class where TTo : class;
        IEnumerable<TTo> Map<TFrom, TTo>(IEnumerable<TFrom> collectionFrom) where TFrom : class where TTo : class;
    }
}
