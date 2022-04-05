using AutoMapper;
using Inv.Domain.Commands.Inventories;
using Inv.Domain.Commands.InventoryItems;
using Inv.Domain.Entities;

namespace Inv.Domain.Mappers
{
    public class CommandToEntityProfile : Profile
    {
        public CommandToEntityProfile()
        {
            CreateMap<InventoryInsertCommand, Inventory>();
            CreateMap<InventoryUpdateCommand, Inventory>();

            CreateMap<InventoryItemInsertCommand, InventoryItem>();
            CreateMap<InventoryItemUpdateCommand, InventoryItem>();
        }
    }
}
