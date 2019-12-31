
namespace ShelterMvc.Models
{
    public class ShelterDatabaseSettings : IShelterDatabaseSettings
    {
        public string ShelterCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IShelterDatabaseSettings
    {
        string ShelterCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}