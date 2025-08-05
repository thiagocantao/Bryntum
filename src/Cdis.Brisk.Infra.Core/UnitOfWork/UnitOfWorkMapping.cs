using System;

namespace Cdis.Brisk.Infra.Core.UnitOfWork
{
    public class UnitOfWorkMapping<IUnitOfWork>
    {
        protected virtual TEntityMap GetUow<TEntityMap>(params object[] list) where TEntityMap : IUnitOfWork
        {
            var map = Activator.CreateInstance(typeof(TEntityMap), list);
            return (TEntityMap)map;
        }
    }
}
