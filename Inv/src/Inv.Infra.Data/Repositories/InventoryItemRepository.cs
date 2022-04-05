using Inv.Infra.Data.Context;

namespace Inv.Infra.Data.Repositories
{
    public  class InventoryItemRepository : Repository<Domain.Entities.InventoryItem>, Domain.Interfaces.Repositories.IInventoryItemRepository
    {
        public InventoryItemRepository(MongoDbContext context) : base(context)
        {
        }
    }
}
