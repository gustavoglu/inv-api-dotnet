using Inv.Infra.Data.Context;

namespace Inv.Infra.Data.Repositories
{
    public class InventoryRepository : Repository<Domain.Entities.Inventory>, Domain.Interfaces.Repositories.IInventoryRepository
    {
        public InventoryRepository(MongoDbContext context) : base(context)
        {
        }
    }
}
