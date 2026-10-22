using System;
using System.Security.Claims;

namespace backend.Extensions;

public static class ClaimsPrincipalExtenstion
{
    public static int? GetUserId(this ClaimsPrincipal user)
    {
        if(user.Identity?.IsAuthenticated != true)
            return null;

        var claim = user.FindFirstValue(ClaimTypes.NameIdentifier);

        return int.TryParse(claim, out var id) ? id : null;
    }
}
