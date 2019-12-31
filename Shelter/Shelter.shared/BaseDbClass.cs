using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Shelter.shared

{
  public class BaseDbClass
  {
    //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
  }
} 