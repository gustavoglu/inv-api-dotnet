using Inv.Domain.Core.Bus;
using Inv.Domain.Core.Notifications;
using Inv.Services.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Inv.Services.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ControllerBaseApp : ControllerBase
    {
        protected IBus Bus;
        protected IDomainNotificationService Notifications;
        public ControllerBaseApp(IBus bus, IDomainNotificationService notifications)
        {
            Bus = bus;
            Notifications = notifications;
        }

        protected IActionResult ResponseDefault(object data = null)
        {
            GetStateErrorsAndConvertToNotifications();

            if (Notifications.HasNotification())
            {
                var errors = Notifications.GetNotifications()
                                .Select(notification => new KeyValuePair<string, string>(notification.Type,notification.Value)).ToList();

                return BadRequest(new ApiResponse(false,errors));
            }

            return Ok(new ApiResponse(data));
        }

        private void GetStateErrorsAndConvertToNotifications()
        {
            if (ModelState.IsValid) return;
            var errors = ModelState.SelectMany(ms => ms.Value.Errors).Select(error => new DomainNotification("ModelState", error.ErrorMessage)).ToList();
            Notifications.AddRange(errors);
        }
    }
}
