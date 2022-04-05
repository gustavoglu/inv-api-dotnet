using MongoDB.Bson.Serialization.Attributes;

namespace Inv.Domain.Core.Entities
{
    public abstract class Entity
    {
        public Entity()
        {
            Id = Guid.NewGuid(); 
        }

        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public Guid Id { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateAt { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateAt { get; set; }
        public string DeleteBy { get; set; }
        public DateTime? DeleteAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
