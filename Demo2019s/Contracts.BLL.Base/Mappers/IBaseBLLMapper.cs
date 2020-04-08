namespace Contracts.BLL.Base.Mappers
{
    public interface IBaseBLLMapper
    {
        TOutObject Map<TOutObject, TInObject>(object inObject)
            where TOutObject : class, new()
            where TInObject : class, new ();
    }
}