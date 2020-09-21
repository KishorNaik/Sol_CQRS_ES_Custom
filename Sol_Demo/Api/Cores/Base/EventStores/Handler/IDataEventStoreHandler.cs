using System;

namespace Api.Business.EventStores.Handler
{
    public interface IDataEventStoreHandler<TNewData> where TNewData : class
    {
        event EventHandler<TNewData> DataEventStoreHandler;
    }

    public interface IDataEventStoreHandler<TOldData, TNewData> where TOldData : class where TNewData : class
    {
        event Action<Object, TOldData, TNewData> DataEventStoreHandler;
    }
}