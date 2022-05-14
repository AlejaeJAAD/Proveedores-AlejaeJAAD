namespace grud_backend.Helpers
{
    public class AppSettings : IAppSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string UsersCollectionName { get; set; }
        public string RolesCollectionName { get; set; }
        public string ProductsCollectionName { get; set; }
        public string AfiliacionCollectionName { get; set; }
        public string NotificacionesCollectionName { get; set; }
        public string Secret { get; set; }
    }
    public interface IAppSettings
    {
        public string Secret { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string UsersCollectionName { get; set; }
        public string RolesCollectionName { get; set; }
        public string ProductsCollectionName { get; set; }
        public string AfiliacionCollectionName { get; set; }
        public string NotificacionesCollectionName { get; set; }
    }
}