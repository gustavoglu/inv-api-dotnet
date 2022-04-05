using Inv.Domain.Commands;
using Inv.Domain.Commands.InventoryItems;
using Inv.Domain.Core.Bus;
using Inv.Domain.Core.Notifications;
using Inv.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inv.Services.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class InventoryItemController : ControllerBaseApp
    {
        private readonly IInventoryItemRepository _repository;

        public InventoryItemController(IBus bus, IDomainNotificationService notifications, IInventoryItemRepository repository) : base(bus, notifications)
        {
            Bus = bus;
            Notifications = notifications;
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetAll(int page, int limit, string sortBy = null, bool sortDesc = false)
        {
            return ResponseDefault(_repository.GetAll(page, limit, sortBy, sortDesc));
        }


        [HttpGet("{id:Guid}")]
        public IActionResult GetById(Guid id)
        {
            return ResponseDefault(_repository.GetById(id));
        }

        [HttpPost]
        public IActionResult Insert([FromBody] InventoryItemInsertCommand command)
        {
            Bus.SendCommand(command);
            return ResponseDefault();
        }

        [HttpPut]
        public IActionResult Update([FromBody] InventoryItemUpdateCommand command)
        {
            Bus.SendCommand(command);
            return ResponseDefault();
        }

        [HttpDelete("{id:Guid}")]
        public IActionResult Update(Guid id)
        {
            Bus.SendCommand(new EntityDeleteCommand<InventoryItemCommand>(id));
            return ResponseDefault();
        }
    }
}
