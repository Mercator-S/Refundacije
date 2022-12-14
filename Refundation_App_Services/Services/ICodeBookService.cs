namespace Refundation_App_Services.Services
{
    public interface ICodeBookService
    {
        object DeleteEntity(object entity, string targetGroup);
        Task<List<object>> DeleteEntities(List<object> entities, string targetGroup);
        Task<List<object>> GetEntitiesAsync( string targetGroup);
    }
}
