using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class LocalOnlyFilter : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        var connection = filterContext.HttpContext.Connection;
        var remote     = connection.RemoteIpAddress;
        var local      = connection.LocalIpAddress;

        if (!IsLocalRequest(remote, local))
        {
            filterContext.Result = new ForbidResult();
            return; 
        }

        base.OnActionExecuting(filterContext);
    }

    private static bool IsLocalRequest(IPAddress? remote, IPAddress? local)
    {
        if (remote is null)
            return false;

        if (IPAddress.IsLoopback(remote))
            return true;

        var normalizedRemote = remote.IsIPv4MappedToIPv6
            ? remote.MapToIPv4()
            : remote;

        var normalizedLocal = local?.IsIPv4MappedToIPv6 == true
            ? local.MapToIPv4()
            : local;

        return normalizedLocal is not null && normalizedRemote.Equals(normalizedLocal);
    }
}