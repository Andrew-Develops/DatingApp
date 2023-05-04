using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Extensions;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Helpers
{
    public class LogUserActivity : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var resultContext = await next();

        if (resultContext.HttpContext == null || resultContext.HttpContext.User == null || !resultContext.HttpContext.User.Identity.IsAuthenticated) return;

        var userId = resultContext.HttpContext.User.GetUserId();

        if (resultContext.HttpContext.RequestServices == null) return;

        var uow = resultContext.HttpContext.RequestServices.GetRequiredService<IUnitOfWork>();

        if (uow == null) return;

        var user = await uow.UserRepository.GetUserByIdAsync(userId);

        if (user == null) return;

        user.LastActive = DateTime.UtcNow;
        await uow.Complete();
    }
}

}