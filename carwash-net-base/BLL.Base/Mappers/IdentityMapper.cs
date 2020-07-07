namespace ee.itcollege.carwash.kristjan.BLL.Base.Mappers
{
    public class IdentityMapper<TLeftObject, TRightObject> : global::ee.itcollege.carwash.kristjan.DAL.Base.Mappers.IdentityMapper<TLeftObject, TRightObject>
        where TRightObject : class?, new() 
        where TLeftObject : class?, new()
    {
    }
}